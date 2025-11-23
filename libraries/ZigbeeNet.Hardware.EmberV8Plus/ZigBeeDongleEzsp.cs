using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Internal;
using ZigBeeNet.Hardware.EmberV8Plus.Internal.Ash;
using ZigBeeNet.Hardware.EmberV8Plus.Transaction;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;
using ZigBeeNet.Util;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.Hardware.Ember
{
    public class ZigBeeDongleEzsp : IZigBeeTransportTransmit, IEzspFrameHandler
    {
        static private readonly ILogger _logger = LogManager.GetLog<ZigBeeDongleEzsp>();

        private readonly int POLL_FRAME_ID = NetworkStateRequest.FrameId;
        private const int WAIT_FOR_ONLINE = 5000;

        /**
         * Response to the getBootloaderVersion if no bootloader is available
         */
        private const int BOOTLOADER_INVALID_VERSION = 0xFFFF;

        /**
         * The serial port used to connect to the dongle
         */
        private IZigBeePort _serialPort;

        /**
         * The protocol handler used to send and receive EZSP packets
         */
        private IEzspProtocolHandler _frameHandler;

        /**
         * The Ember bootload handler
         */
        //Not implemented yet
        //private EmberFirmwareUpdateHandler bootloadHandler;

        /**
         * The stack configuration we need for the NCP
         */
        private Dictionary<ZigbeeEzspConfigId, ushort> _stackConfiguration;

        /**
         * The stack policies we need for the NCP
         */
        private Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> _stackPolicies;

        /**
         * The reference to the receive interface
         */
        private IZigBeeTransportReceive _zigbeeTransportReceive;

        /**
         * The current link key as {@link ZigBeeKey}
         */
        private ZigBeeKey _linkKey = new ZigBeeKey();

        /**
         * The current network key as {@link ZigBeeKey}
         */
        private ZigBeeKey _networkKey = new ZigBeeKey();

        /**
         * The current network parameters as {@link EmberNetworkParameters}
         */
        private ZigbeeNetworkParameters _networkParameters = new ZigbeeNetworkParameters();

        /**
         * The IeeeAddress of the Ember NCP
         */
        public IeeeAddress IeeeAddress { get; set; }

        /**
         * The network address of the Ember NCP
         */
        public ushort NwkAddress { get; set; }

        /**
         * Defines the type of device we want to be - normally this should be COORDINATOR
         */
        private DeviceType _deviceType = DeviceType.COORDINATOR;

        /**
         * The low level protocol to use for this dongle
         */
        private EmberSerialProtocol _protocol;

        /**
         * The Ember version used in this system. Set during initialisation and saved in case the client is interested.
         */
        public string VersionString { get; set; }  = "Unknown";

        /**
         * Boolean that is true when the network is UP
         */
        private bool _networkStateUp = false;

        /**
         * Boolean to hold initialisation state. Set to true after {@link #startup()} completes.
         */
        private bool _initialised = false;

        /**
         * Flag to indicate if the framework should pass multicast and broadcast messages sent by the framework, and
         * returned from the NCP, back to the framework as a received frame.
         */
        private bool _passLoopbackMessages = true;

        /**
         * The default ProfileID to use
         */
        private ushort _defaultProfileId = (ushort)ZigBeeProfileType.ZIGBEE_HOME_AUTOMATION;

        /**
         * The default DeviceID to use
         */
        private ushort _defaultDeviceId = (ushort)ZigBeeDeviceType.HomeGateway;

        private System.Timers.Timer _pollingTimer = null;

        /**
         * The rate at which we will do a status poll if we've not sent any other messages within this period
         */
        private int _pollRate = 1000;

        /**
         * The time the last command was sent from the {@link ZigBeeNetworkManager}. This is used by the dongle polling task
         * to not poll if commands are otherwise being sent so as to reduce unnecessary communications with the dongle.
         */
        private DateTime _lastSendCommand;

        /**
         * If the dongle is being used with the manufacturing library, then this records the listener to be called when
         * packets are received.
         */
         //TODO
        //private EmberMfglibListener mfglibListener;

        /**
         * The {@link EmberNcpResetProvider} used to perform a hardware reset. If not set, no hardware reset will be
         * attempted.
         */
         //TODO
        //private EmberNcpResetProvider resetProvider;

        /**
         * List of input clusters supported - this will be added to the endpoint definition
         */
        private ushort[] _inputClusters = new ushort[] { 0 };

        /**
         * List of output clusters supported - this will be added to the endpoint definition
         */
        private ushort[] _outputClusters = new ushort[] { 0 };

        /**
         * We need to retain the transaction ID returned by the NCP when we're sending fragments so that we can use this in
         * the sendReply message and also pass the ACK to the application. This maps the NCP transaction IDs to the
         * framework IDs.
         */
        //Fragmentation not implemented yet
        /*
        Dictionary<int, int> fragmentationApsCounters = new Dictionary<int, int>();
        */

        /**
         * Create a {@link ZigBeeDongleEzsp} with the default ASH2 frame handler
         *
         * @param serialPort the {@link ZigBeePort} to use for the connection
         */
        public ZigBeeDongleEzsp(IZigBeePort serialPort) 
            : this(serialPort, EmberSerialProtocol.ASH2)
        {
        }

        /**
         * Create a {@link ZigBeeDongleEzsp} with the default ASH frame handler
         *
         * @param serialPort the {@link ZigBeePort} to use for the connection
         * @param protocol the {@link EmberSerialProtocol} to use
         */
        public ZigBeeDongleEzsp(IZigBeePort serialPort, EmberSerialProtocol protocol) {
            this._serialPort = serialPort;
            this._protocol = protocol;

            // Define the default configuration
            _stackConfiguration = new Dictionary<ZigbeeEzspConfigId, ushort>();
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_ROUTE_TABLE_SIZE, 16);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_SECURITY_LEVEL, 5);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_ADDRESS_TABLE_SIZE, 8);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE, 2);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_STACK_PROFILE, 2);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_INDIRECT_TRANSMISSION_TIMEOUT, 7680);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_MAX_HOPS, 30);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_TX_POWER_MODE, 0);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_SUPPORTED_NETWORKS, 1);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_KEY_TABLE_SIZE, 4);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_APPLICATION_ZDO_FLAGS, (int)ZigbeeZdoConfigurationFlags.SL_ZIGBEE_APP_RECEIVES_SUPPORTED_ZDO_REQUESTS);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_MAX_END_DEVICE_CHILDREN, 16);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_APS_UNICAST_MESSAGE_COUNT, 10);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_BROADCAST_TABLE_SIZE, 15);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_NEIGHBOR_TABLE_SIZE, 16);
            //Fragmentation not implemented yet
            //stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_FRAGMENT_WINDOW_SIZE, 1);
            //stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_FRAGMENT_DELAY_MS, 50);
            _stackConfiguration.Add(ZigbeeEzspConfigId.SL_ZIGBEE_EZSP_CONFIG_PACKET_BUFFER_COUNT, 255);

            // Define the default policies
            _stackPolicies = new Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId>();
            _stackPolicies.Add(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_TC_KEY_REQUEST_POLICY, ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_DENY_TC_KEY_REQUESTS);
            _stackPolicies.Add(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_TRUST_CENTER_POLICY, ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_ALLOW_PRECONFIGURED_KEY_JOINS);
            _stackPolicies.Add(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY, ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_MESSAGE_TAG_ONLY_IN_CALLBACK);
            _stackPolicies.Add(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_APP_KEY_REQUEST_POLICY, ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_DENY_APP_KEY_REQUESTS);
            _stackPolicies.Add(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_BINDING_MODIFICATION_POLICY, ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_CHECK_BINDING_MODIFICATIONS_ARE_VALID_ENDPOINT_CLUSTERS);

            _networkKey = new ZigBeeKey();
        }

        /**
         * Sets the hardware reset provider if the dongle supports a hardware reset. If this is not set, the dongle driver
         * will not attempt a hardware reset and will attempt to use other software methods to reset the dongle as may be
         * available by the low level protocol.
         *
         * @param resetProvider the {@link EmberNcpResetProvider} to be called to perform the reset
         */
         /*
        public void setEmberNcpResetProvider(EmberNcpResetProvider resetProvider) {
            this.resetProvider = resetProvider;
        }
        */

        /**
         * Update the Ember configuration that will be sent to the dongle during the initialisation.
         * <p>
         * Note that this must be called prior to {@link #initialize()} for the configuration to be effective.
         *
         * @param configId the {@link ZigbeeEzspConfigId} to be updated.
         * @param value the value to set (as {@link Integer}. Setting this to null will remove the configuration Id from the
         *            list of configuration to be sent during NCP initialisation.
         * @return the previously configured value, or null if no value was set for the {@link ZigbeeEzspConfigId}
         */
        public int? UpdateDefaultConfiguration(ZigbeeEzspConfigId configId, ushort? value) 
        {
            int? previousValue = _stackConfiguration.ContainsKey(configId) ? (int?)_stackConfiguration[configId] : null;
            if (value == null)
                _stackConfiguration.Remove(configId);
            else
                _stackConfiguration[configId] = value.Value;

            return previousValue;
        }

        /**
         * Update the Ember policies that will be sent to the dongle during the initialisation.
         * <p>
         * Note that this must be called prior to {@link #initialize()} for the configuration to be effective.
         *
         * @param policyId the {@link ZigbeeEzspPolicyId} to be updated
         * @param decisionId the (as {@link ZigbeeEzspDecisionId} to set. Setting this to null will remove the policy from
         *            the list of policies to be sent during NCP initialisation.
         * @return the previously configured {@link ZigbeeEzspDecisionId}, or null if no value was set for the
         *         {@link ZigbeeEzspPolicyId}
         */
        public ZigbeeEzspDecisionId? UpdateDefaultPolicy(ZigbeeEzspPolicyId policyId, ZigbeeEzspDecisionId? decisionId) 
        {
            ZigbeeEzspDecisionId? previousValue = _stackPolicies.ContainsKey(policyId) ? (ZigbeeEzspDecisionId?)_stackPolicies[policyId] : null;

            if (decisionId == null)
                _stackPolicies.Remove(policyId);
            else
                _stackPolicies[policyId] = decisionId.Value;

            return previousValue;
        }

        /**
         * Gets an {@link EmberMfglib} instance that can be used for low level testing of the Ember dongle.
         * <p>
         * This may only be used if the {@link ZigBeeDongleEmber} instance has not been initialized on a ZigBee network.
         *
         * @param mfglibListener a {@link EmberMfglibListener} to receive packets received. May be null.
         * @return the {@link EmberMfglib} instance, or null on error
         */
         /*
        public EmberMfglib getEmberMfglib(EmberMfglibListener mfglibListener) {
            if (frameHandler == null && !initialiseEzspProtocol()) {
                return null;
            }

            this.mfglibListener = mfglibListener;

            return new EmberMfglib(frameHandler);
        }
        */

        public void SetDefaultProfileId(ushort defaultProfileId) 
        {
            this._defaultProfileId = defaultProfileId;
        }

        public void SetDefaultDeviceId(int defaultDeviceId) 
        {
            this._defaultDeviceId = defaultDeviceId;
        }

        public ZigBeeStatus Initialize() 
        {
            _logger.LogDebug("EZSP Dongle: Initialize with protocol {Protocol}.", _protocol);
            _zigbeeTransportReceive.SetTransportState(ZigBeeTransportState.INITIALISING);

            if (_protocol != EmberSerialProtocol.NONE && !InitialiseEzspProtocol()) 
                return ZigBeeStatus.COMMUNICATION_ERROR;

            // Perform any stack configuration
            EmberStackConfiguration stackConfigurer = new EmberStackConfiguration(GetEmberNcp());

            Dictionary<ZigbeeEzspConfigId, int?> configuration = stackConfigurer.GetConfiguration(_stackConfiguration.Keys);
            foreach (var config in configuration) 
            {
                _logger.LogDebug("Configuration state {Key} = {Value}", config.Key, config.Value);
            }

            Dictionary<ZigbeeEzspPolicyId, ZigbeeEzspDecisionId> policies = stackConfigurer.GetPolicy(_stackPolicies.Keys);
            foreach (var policy in policies) 
            {
                _logger.LogDebug("Policy state {Key} = {Value}", policy.Key, policy.Value);
            }

            stackConfigurer.SetConfiguration(_stackConfiguration);
            configuration = stackConfigurer.GetConfiguration(_stackConfiguration.Keys);
            foreach (var config in configuration)
            {
                _logger.LogDebug("Configuration state {Key} = {Value}", config.Key, config.Value);
            }

            stackConfigurer.SetPolicy(_stackPolicies);
            policies = stackConfigurer.GetPolicy(_stackPolicies.Keys);
            foreach (var policy in policies)
            {
                _logger.LogDebug("Policy state {Key} = {Value}", policy.Key, policy.Value);
            }

            EmberNcp ncp = GetEmberNcp();

            // Get the current network parameters so that any configuration updates start from here
            _networkParameters = ncp.GetNetworkParameters().Parameters;
            _logger.LogDebug("Ember initial network parameters are {NetworkParameters}", _networkParameters);

            IeeeAddress = new IeeeAddress(ncp.GetEui64());
            _logger.LogDebug("Ember local IEEE Address is {IeeeAddress}", IeeeAddress);

            ncp.GetNetworkParameters();

            _logger.LogDebug("EZSP Dongle: initialize done");

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus Startup(bool reinitialize) 
        {
            _logger.LogDebug("EZSP Dongle: Startup - reinitialize={Reinitialize}", reinitialize);

            // If frameHandler is null then the serial port didn't initialise or startup has not been called
            if (_frameHandler == null) 
            {
                _logger.LogError("EZSP Dongle: Startup found low level handler is not initialised.");
                return ZigBeeStatus.INVALID_STATE;
            }

            EmberNcp ncp = GetEmberNcp();

            // Add the endpoint
            _logger.LogDebug("EZSP Adding Endpoint: ProfileID={ProfileID}, DeviceID={DeviceID}", _defaultProfileId.ToString("X4"), _defaultDeviceId.ToString("X4"));
            _logger.LogDebug("EZSP Adding Endpoint: Input Clusters   {InputClusters}", _inputClusters);
            _logger.LogDebug("EZSP Adding Endpoint: Output Clusters  {OutputClusters}", _outputClusters);
            ncp.AddEndpoint(1, _defaultProfileId, _defaultDeviceId, 0, (byte)_inputClusters.Count(), (byte)_outputClusters.Count(), _inputClusters, _outputClusters);

            // Now initialise the network
            Status initResponse = ncp.NetworkInit(new ZigbeeNetworkInitStruct() { bitmask = ZigbeeNetworkInitBitmask.SL_ZIGBEE_NETWORK_INIT_NO_OPTIONS});
            if (initResponse == Status.SL_STATUS_NOT_JOINED) 
            {
                _logger.LogDebug("EZSP dongle initialize done - response {Response}", initResponse);
            }

            // Print current security state to debug logs
            ncp.GetCurrentSecurityState();

            ScheduleNetworkStatePolling();

            // Check if the network is initialised
            ZigbeeNetworkStatus networkState = ncp.NetworkState();
            _logger.LogDebug("EZSP networkStateResponse {State}", networkState);

            // If we want to reinitialize the network, then go...
            EmberNetworkInitialisation netInitialiser = new EmberNetworkInitialisation(_frameHandler);
            if (reinitialize) 
            {
                _logger.LogDebug("Reinitialising Ember NCP network as {DeviceType}", _deviceType);
                if (_deviceType == DeviceType.COORDINATOR)
                    netInitialiser.FormNetwork(_networkParameters, _linkKey, _networkKey);
                else
                    netInitialiser.JoinNetwork(_networkParameters, _linkKey, ZigbeeLeaveNetworkOption.SL_ZIGBEE_LEAVE_NWK_WITH_NO_OPTION);

            } 
            else if (_deviceType == DeviceType.ROUTER) 
            {
                netInitialiser.RejoinNetwork();
            }
            ncp.GetNetworkParameters();

            // Wait for the network to come up
            networkState = WaitNetworkStartup(ncp);
            _logger.LogDebug("EZSP networkState after online wait {NetworkState}", networkState);

            // Get the security state - mainly for information
            var currentSecurityState = ncp.GetCurrentSecurityState();
            _logger.LogDebug("EZSP Current status {status} and Security State = {CurrentSecurityState}", currentSecurityState.Status, currentSecurityState.State);

            Status txPowerResponse = ncp.SetRadioPower(_networkParameters.radioTxPower);
            if (txPowerResponse != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Setting TX Power to {TxPower} resulted in {Response}", _networkParameters.radioTxPower, txPowerResponse);
            }

            ushort address = ncp.GetNodeId();
            if (address != 0xFFFE)
                NwkAddress = address;

            _logger.LogDebug("EZSP Dongle: Startup complete. NWK Address = {NwkAddress}, State = {NetworkState}", NwkAddress.ToString("X4"), networkState);

            // At this stage, we will now take note of the StackStatusHandler notifications
            bool joinedNetwork = (networkState == ZigbeeNetworkStatus.SL_ZIGBEE_JOINED_NETWORK || networkState == ZigbeeNetworkStatus.SL_ZIGBEE_JOINED_NETWORK_NO_PARENT);
            _initialised = true;
            HandleLinkStateChange(joinedNetwork);

            return joinedNetwork ? ZigBeeStatus.SUCCESS : ZigBeeStatus.BAD_RESPONSE;
        }

        /**
         * Waits for the network to start. This periodically polls the network state waiting for the network to come online.
         * If a terminal state is observed (eg SL_ZIGBEE_JOINED_NETWORK or SL_ZIGBEE_LEAVING_NETWORK) whereby the network cannot
         * start, then this method will return.
         * <p>
         * If the network start starts to join, but then shows SL_ZIGBEE_NO_NETWORK, it will return. Otherwise it will wait for
         * the timeout.
         *
         * @param ncp
         * @return
         */
        private ZigbeeNetworkStatus WaitNetworkStartup(EmberNcp ncp) 
        {
            ZigbeeNetworkStatus networkState;
            bool joinStarted = false;
            DateTime startTime = DateTime.Now;
            do 
            {
                networkState = ncp.NetworkState();
                switch (networkState) 
                {
                    case ZigbeeNetworkStatus.SL_ZIGBEE_JOINING_NETWORK:
                        joinStarted = true;
                        break;
                    case ZigbeeNetworkStatus.SL_ZIGBEE_NO_NETWORK:
                        if (joinStarted)
                            return networkState;
                        break;
                        
                    case ZigbeeNetworkStatus.SL_ZIGBEE_JOINED_NETWORK:
                    case ZigbeeNetworkStatus.SL_ZIGBEE_JOINED_NETWORK_NO_PARENT:
                    case ZigbeeNetworkStatus.SL_ZIGBEE_LEAVING_NETWORK:
                        return networkState;
                    default:
                        break;
                }

                Thread.Sleep(250);
            } 
            while ((DateTime.Now - startTime).TotalMilliseconds < WAIT_FOR_ONLINE);

            return networkState;
        }

        /**
         * This method schedules sending a status request frame on the interval specified by pollRate. If the frameHandler
         * does not receive a response after a certain amount of retries, the state will be set to OFFLINE.
         * The poll will not be sent if other commands have been sent to the dongle within the pollRate period so as to
         * eliminate any unnecessary traffic with the dongle.
         */
        private void ScheduleNetworkStatePolling() 
        {
            if (_pollingTimer != null) 
            {
                _pollingTimer.Stop();
            }

            if (_pollRate == 0) {
                return;
            }

            _pollingTimer = new System.Timers.Timer();
            _pollingTimer.AutoReset = true;
            _pollingTimer.Interval = _pollRate;
            _pollingTimer.Elapsed += new ElapsedEventHandler(OnPollTimerElapsedEvent);
            _pollingTimer.Start();
        }

        private void OnPollTimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Don't poll the state if the network is down
                // or we've sent a command to the dongle within the pollRate
                if (!_networkStateUp || (DateTime.Now - _lastSendCommand).TotalMilliseconds < _pollRate)
                {
                    return;
                }
                // Don't wait for the response. This is running in a single thread scheduler
                _frameHandler.QueueFrame(new NetworkStateRequest());
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "EZSP Dongle: error in poll timer elapsed event");
            }
        }

        /**
         * Set the polling rate at which the handler will poll the NCP to ensure it is still responding.
         * If the NCP fails to respond within a fixed time the driver will be set to OFFLINE.
         * <p>
         * Setting the rate to 0 will disable polling
         *
         * @param pollRate the polling rate in milliseconds. 0 will disable polling.
         */
        public void SetPollRate(int pollRate) 
        {
            this._pollRate = pollRate;
            ScheduleNetworkStatePolling();
        }

        public void Shutdown() 
        {
            _logger.LogDebug("EZSP Dongle: Shutdown");
            if (_frameHandler == null) 
            {
                _logger.LogDebug("EZSP Dongle: Shutdown frameHandler is null");
                return;
            }
            _frameHandler.SetClosing();

            /*
            if (mfglibListener != null) {
                mfglibListener = null;
            }
            */

            if (_pollingTimer != null) {
                _pollingTimer.Stop();
            }

            _frameHandler.Close();
            _serialPort.Close();
            _frameHandler = null;
        }

        /**
         * Configures the driver to pass or drop loopback messages received from the NCP. These are MUTICAST or BROADCAST
         * messages that were sent by the framework, and retransmitted by the NCP.
         * <p>
         * This defaults to passing the loopback frames back to the framework
         *
         * @param passLoopbackMessages true if the driver should pass loopback messages back as a received frame
         */
        public void PassLoopbackMessages(bool passLoopbackMessages) 
        {
            this._passLoopbackMessages = passLoopbackMessages;
        }

        /**
         * Returns an instance of the {@link EmberNcp}
         *
         * @return an instance of the {@link EmberNcp}
         */
        public EmberNcp GetEmberNcp() 
        {
            return new EmberNcp(_frameHandler);
        }

        /**
         * Returns an instance of the {@link EmberCbkeProvider}
         *
         * @return an instance of the {@link EmberCbkeProvider}
         */
         /*
        public EmberCbkeProvider getEmberCbkeProvider() {
            return new EmberCbkeProvider(this);
        }
        */

        public void SendCommand(/*int msgTag,*/ ZigBeeApsFrame apsFrame) 
        {
            if (_frameHandler == null)
                return;

            _lastSendCommand = DateTime.Now;

            ITransaction transaction;

            ZigbeeApsFrame zigbeeApsFrame = new ZigbeeApsFrame();
            zigbeeApsFrame.clusterId = apsFrame.Cluster;
            zigbeeApsFrame.profileId = apsFrame.Profile;
            zigbeeApsFrame.sourceEndpoint = apsFrame.SourceEndpoint;
            zigbeeApsFrame.destinationEndpoint = apsFrame.DestinationEndpoint;
            zigbeeApsFrame.sequence = apsFrame.ApsCounter;
            zigbeeApsFrame.options = ZigbeeApsOption.SL_ZIGBEE_APS_OPTION_RETRY | ZigbeeApsOption.SL_ZIGBEE_APS_OPTION_ENABLE_ROUTE_DISCOVERY | ZigbeeApsOption.SL_ZIGBEE_APS_OPTION_ENABLE_ADDRESS_DISCOVERY;

            if (apsFrame.SecurityEnabled) 
            {
                zigbeeApsFrame.options = zigbeeApsFrame.options | ZigbeeApsOption.SL_ZIGBEE_APS_OPTION_ENCRYPTION;
            }

            if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Device && !ZigBeeBroadcastDestinationHelper.IsBroadcast(apsFrame.DestinationAddress)) 
            {
                SendUnicastRequest emberUnicast = new SendUnicastRequest();
                emberUnicast.IndexOrDestination = apsFrame.DestinationAddress;
                //emberUnicast.SetMessageTag(msgTag);
                emberUnicast.SequenceNumber = apsFrame.ApsCounter;
                emberUnicast.Type = ZigbeeOutgoingMessageType.SL_ZIGBEE_OUTGOING_DIRECT;
                emberUnicast.ApsFrame = zigbeeApsFrame;
                emberUnicast.MessageContents = apsFrame.Payload;

                //Fragmentation not implemented yet
                /*
                if (apsFrame is ZigBeeApsFrameFragment) 
                {
                    ZigBeeApsFrameFragment fragment = (ZigBeeApsFrameFragment) apsFrame;
                    emberApsFrame.addOptions(EmberApsOption.SL_ZIGBEE_APS_OPTION_FRAGMENT);
                    emberApsFrame.setGroupId(fragment.getFragmentNumber() + (fragment.getFragmentTotal() << 8));
                    if (fragment.getFragmentNumber() != 0) {
                        emberApsFrame.setSequence(fragmentationApsCounters.get(msgTag));
                    }
                    if (fragment.getFragmentNumber() == fragment.getFragmentTotal() - 1) {
                        fragmentationApsCounters.remove(msgTag);
                    }
                }
                */

                transaction = new SingleResponseTransaction(emberUnicast, typeof(SendUnicastResponse));
            } 
            else if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Device && ZigBeeBroadcastDestinationHelper.IsBroadcast(apsFrame.DestinationAddress)) 
            {
                SendBroadcastRequest emberBroadcast = new SendBroadcastRequest();
                emberBroadcast.Destination = apsFrame.DestinationAddress;
                //emberMulticast.MessageTag = msgTag;
                emberBroadcast.SequenceNumber = apsFrame.ApsCounter;
                emberBroadcast.ApsFrame = zigbeeApsFrame;
                emberBroadcast.Radius = (byte)apsFrame.Radius;
                emberBroadcast.MessageContents = apsFrame.Payload;

                transaction = new SingleResponseTransaction(emberBroadcast, typeof(SendBroadcastResponse));
            } 
            else if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Group) 
            {
                zigbeeApsFrame.groupId = apsFrame.GroupAddress;

                SendMulticastRequest emberMulticast = new SendMulticastRequest();
                emberMulticast.ApsFrame = zigbeeApsFrame;
                emberMulticast.Hops = (byte)apsFrame.Radius;
                //emberMulticast.NonmemberRadius = apsFrame.NonMemberRadius;
                //emberMulticast.MessageTag = msgTag;
                emberMulticast.MessageContents = apsFrame.Payload;

                transaction = new SingleResponseTransaction(emberMulticast, typeof(SendMulticastResponse));
            } 
            else 
            {
                _logger.LogDebug("EZSP message not sent as unknown address mode: {ApsFrame}", apsFrame);
                return;
            }

            // The response from the SendXxxcast messages returns the network layer sequence number
            // We need to correlate this with the messageTag
            Task.Run(() =>
            {
                try
                {
                    _frameHandler.SendTransaction(transaction);

                    Status? status;
                    if (transaction.GetResponse() is SendUnicastResponse)
                    {
                        //Fragmentation not implemented yet        
                        //fragmentationApsCounters.put(msgTag, ((SendUnicastResponse) transaction.getResponse()).getSequence());
                        status = ((SendUnicastResponse)transaction.GetResponse()).Status;
                    }
                    else if (transaction.GetResponse() is SendBroadcastResponse)
                    {
                        status = ((SendBroadcastResponse)transaction.GetResponse()).Status;
                    }
                    else if (transaction.GetResponse() is SendMulticastResponse)
                    {
                        status = ((SendMulticastResponse)transaction.GetResponse()).Status;
                    }
                    else
                    {
                        _logger.LogDebug("Unable to get response from {Request} :: {Response}", transaction.GetRequest(), transaction.GetResponse());
                        return;
                    }

                    // If this is SL_STATUS_OK, then do nothing as the command is still not transmitted.
                    // If there was an error, then we let the system know we've failed already!
                    if (status == Status.SL_STATUS_OK)
                        return;

                    //Not implemented yet
                    //zigbeeTransportReceive.ReceiveCommandState(msgTag, ZigBeeTransportProgressState.TX_NAK);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "EZSP Dongle: error in SendCommand");
                }
            });
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigbeeTransportReceive) 
        {
            this._zigbeeTransportReceive = zigbeeTransportReceive;
        }

        public void SetNodeDescriptor(IeeeAddress ieeeAddress, NodeDescriptor nodeDescriptor) 
        {
            // Update the extendedTimeout flag in the address table.
            // Users should ensure the address table is large enough to hold all nodes on the network.
            _logger.LogDebug("{IeeeAddress}: NodeDescriptor passed to Ember NCP {NodeDescriptor}", ieeeAddress, nodeDescriptor);
            if ((nodeDescriptor.MacCapabilities&NodeDescriptor.MacCapabilitiesType.RECEIVER_ON_WHEN_IDLE)==0) 
            {
                EmberNcp ncp = GetEmberNcp();
                ncp.SetExtendedTimeout(ieeeAddress.GetAddress(), true);
            }
        }

        public void HandlePacket(EzspFrameV8Plus response) 
        {
            if (response.GetFrameId() != POLL_FRAME_ID) {
                _logger.LogDebug("RX EZSP: {Response}", response);
            }

            if (response is IncomingMessageHandlerResponse) 
            {
                if (!_initialised) {
                    _logger.LogDebug("Ignoring received frame as stack is still initialising");
                    return;
                }
                IncomingMessageHandlerResponse incomingMessage = (IncomingMessageHandlerResponse) response;
                ZigbeeApsFrame emberApsFrame = incomingMessage.ApsFrame;
                ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            //Fragmentation not implemented yet    
            /*
                if (ZigbeeApsFrame.getOptions().contains(EmberApsOption.SL_ZIGBEE_APS_OPTION_FRAGMENT)) {
                    ZigBeeApsFrameFragment fragment = new ZigBeeApsFrameFragment(emberApsFrame.getGroupId() & 0xFF);
                    if ((emberApsFrame.getGroupId() & 0xFF) == 0) {
                        fragment.setFragmentTotal((emberApsFrame.getGroupId() & 0xFF00) >> 8);
                    }
                    // We must respond to a fragment with a sendReply command
                    SendReplyRequest sendReply = new SendReplyRequest();
                    emberApsFrame.setGroupId(emberApsFrame.getGroupId() | 0xFF00);
                    sendReply.setApsFrame(emberApsFrame);
                    sendReply.setSender(incomingMessage.getSender());
                    sendReply.setMessageContents(new int[] {});
                    frameHandler.queueFrame(sendReply);
                    apsFrame = fragment;
                } else {
                    apsFrame = new ZigBeeApsFrame();
                }
                */

                switch (incomingMessage.Type) 
                {
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_BROADCAST_LOOPBACK:
                        if (!_passLoopbackMessages)
                            return;
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                        break;
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_BROADCAST:
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_UNICAST:
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_UNICAST_REPLY:
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                        break;
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_MULTICAST_LOOPBACK:
                        if (!_passLoopbackMessages)
                            return;
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                        break;
                    case ZigbeeIncomingMessageType.SL_ZIGBEE_INCOMING_MULTICAST:
                        apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                        break;
                    case ZigbeeIncomingMessageType.EMBER_INCOMING_MANY_TO_ONE_ROUTE_REQUEST:
                        return;
                    default:
                        _logger.LogWarning("Ignoring unknown EZSP incoming message type");
                        return;
                }

                apsFrame.ApsCounter = (byte)emberApsFrame.sequence;
                apsFrame.Cluster = (ushort)emberApsFrame.clusterId;
                apsFrame.Profile = (ushort)emberApsFrame.profileId;
                apsFrame.SecurityEnabled = emberApsFrame.options.HasFlag(ZigbeeApsOption.SL_ZIGBEE_APS_OPTION_ENCRYPTION);

                apsFrame.DestinationAddress = NwkAddress;
                apsFrame.DestinationEndpoint = (byte) emberApsFrame.destinationEndpoint;
                apsFrame.SourceAddress = incomingMessage.PacketInfo.sender_short_id;
                apsFrame.SourceEndpoint = (byte)emberApsFrame.sourceEndpoint;

                apsFrame.Payload = incomingMessage.Message;
                _zigbeeTransportReceive.ReceiveCommand(apsFrame);

                return;
            }

            // Message has been completed by the NCP
            //Not implemented yet
            /*
            if (response is MessageSentHandler) 
            {
                Task.Run(() =>
                {
                    MessageSentHandler sentHandler = (MessageSentHandler) response;
                    ZigBeeTransportProgressState sentHandlerState;
                    if (sentHandler.Status() == Status.SL_STATUS_OK) {
                        sentHandlerState = ZigBeeTransportProgressState.RX_ACK;
                    } else {
                        sentHandlerState = ZigBeeTransportProgressState.RX_NAK;
                    }
                    zigbeeTransportReceive.ReceiveCommandState(sentHandler.GetMessageTag(), sentHandlerState);
                });
                return;
            }
            */

            if (response is StackStatusHandlerResponse) 
            {
                switch (((StackStatusHandlerResponse) response).Status) 
                {
                    case Status.SL_STATUS_BUSY:
                        break;
                    case Status.SL_STATUS_ZIGBEE_PRECONFIGURED_KEY_REQUIRED:
                    case Status.SL_STATUS_NETWORK_DOWN:
                        HandleLinkStateChange(false);
                        break;
                    case Status.SL_STATUS_NETWORK_UP:
                        HandleLinkStateChange(true);
                        break;
                    default:
                        break;
                }
                return;
            }

            if (response is TrustCenterPostJoinHandlerResponse) 
            {
                TrustCenterPostJoinHandlerResponse joinHandler = (TrustCenterPostJoinHandlerResponse) response;

                ZigBeeNodeStatus status;
                switch (joinHandler.Status) 
                {
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_HIGH_SECURITY_UNSECURED_JOIN:
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_STANDARD_SECURITY_UNSECURED_JOIN:
                        status = ZigBeeNodeStatus.UNSECURED_JOIN;
                        break;
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_HIGH_SECURITY_UNSECURED_REJOIN:
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_STANDARD_SECURITY_UNSECURED_REJOIN:
                        status = ZigBeeNodeStatus.UNSECURED_REJOIN;
                        break;
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_HIGH_SECURITY_SECURED_REJOIN:
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_STANDARD_SECURITY_SECURED_REJOIN:
                        status = ZigBeeNodeStatus.SECURED_REJOIN;
                        break;
                    case ZigbeeDeviceUpdate.SL_ZIGBEE_DEVICE_LEFT:
                        status = ZigBeeNodeStatus.DEVICE_LEFT;
                        break;
                    default:
                        _logger.LogDebug("Unknown state in trust centre join handler {Status}", joinHandler.Status);
                        return;
                }

                _zigbeeTransportReceive.NodeStatusUpdate(status, (ushort)joinHandler.NewNodeId, new IeeeAddress(joinHandler.NewNodeEui64));
                return;
            }

            if (response is ChildJoinHandlerResponse) 
            {
                ChildJoinHandlerResponse joinHandler = (ChildJoinHandlerResponse) response;
                _zigbeeTransportReceive.NodeStatusUpdate(
                        joinHandler.Joining ? ZigBeeNodeStatus.UNSECURED_JOIN : ZigBeeNodeStatus.DEVICE_LEFT,
                        (ushort)joinHandler.ChildId, new IeeeAddress(joinHandler.ChildEui64));
                return;
            }
        
            /*
            if (response instanceof MfglibRxHandler) {
                if (mfglibListener != null) {
                    MfglibRxHandler mfglibHandler = (MfglibRxHandler) response;
                    mfglibListener.emberMfgLibPacketReceived(mfglibHandler.getLinkQuality(), mfglibHandler.getLinkQuality(),
                            mfglibHandler.getPacketContents());
                }
                return;
            }
            */
        }

        public void HandleLinkStateChange(bool linkState) 
        {
            _logger.LogDebug("Ember: Link State change to {LinkState}, initialised={Initialised}, networkStateUp={NetworkStateUp}", linkState, _initialised, _networkStateUp);

            // Only act on changes to OFFLINE once we have completed initialisation
            // changes to ONLINE have to work during init because they mark the end of the initialisation
            if (!_initialised || linkState == _networkStateUp) 
            {
                _logger.LogDebug("Ember: Link State change to {LinkState} ignored.", linkState);
                return;
            }
            _networkStateUp = linkState;

            Task.Run(() =>
            {
                if (linkState)
                {
                    _logger.LogDebug("Ember: Link State up running");

                    EmberNcp ncp = GetEmberNcp();
                    int addr = ncp.GetNodeId();
                    if (addr != 0xFFFE) {
                        NwkAddress = (ushort)addr;
                    }
                }
                // Handle link changes and notify framework
                _zigbeeTransportReceive.SetTransportState(linkState ? ZigBeeTransportState.ONLINE : ZigBeeTransportState.OFFLINE);
            });
        }

        public ZigBeeChannel ZigBeeChannel
        {
            get { return (ZigBeeChannel)(1 << _networkParameters.radioChannel); } 
        }

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel) 
        {
            if ((ZigBeeChannelMask.CHANNEL_MASK_2GHZ & (int)channel) == 0) 
            {
                _logger.LogDebug("Unable to set channel outside of 2.4GHz channels: {Channel}", channel);
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            _networkParameters.radioChannel = (byte)channel.GetChannelNum();
            return ZigBeeStatus.SUCCESS;
        }

        public ushort PanID
        {
            get { return (ushort) _networkParameters.panId; }
        }

        public ZigBeeStatus SetZigBeePanId(ushort panId) 
        {
            _networkParameters.panId = panId;
            return ZigBeeStatus.SUCCESS;
        }

        public ExtendedPanId ExtendedPanId 
        {
            get { return new ExtendedPanId(_networkParameters.extendedPanId); }
        }

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId extendedPanId) 
        {
            _networkParameters.extendedPanId = extendedPanId.PanId;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key) 
        {
            _networkKey = key;
            if (_networkStateUp) 
                return ZigBeeStatus.INVALID_STATE;

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeKey ZigBeeNetworkKey 
        {
            get
            {
                EmberNcp ncp = GetEmberNcp();
                ZigbeeKeyStruct key = ncp.GetKey(ZigbeeKeyType.SL_ZIGBEE_CURRENT_NETWORK_KEY);
                return EmberKeyToZigBeeKey(key);
            }
        }

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key) 
        {
            _linkKey = key;
            if (_networkStateUp)
                return ZigBeeStatus.INVALID_STATE;

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeKey TcLinkKey 
        {
            get
            {
                EmberNcp ncp = GetEmberNcp();
                ZigbeeKeyStruct key = ncp.GetKey(ZigbeeKeyType.SL_ZIGBEE_TRUST_CENTER_LINK_KEY);
                return EmberKeyToZigBeeKey(key);
            }
        }

        public void UpdateTransportConfig(TransportConfig configuration) 
        {
            foreach (TransportConfigOption option in configuration.GetOptions()) 
            {
                try 
                {
                    switch (option) 
                    {
                        case TransportConfigOption.CONCENTRATOR_CONFIG:
                            configuration.SetResult(option, SetConcentrator((ConcentratorConfig) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.INSTALL_KEY:
                            EmberNcp ncp = GetEmberNcp();
                            ZigBeeKey nodeKey = (ZigBeeKey) configuration.GetValue(option);
                            if (!nodeKey.HasAddress()) 
                            {
                                _logger.LogDebug("Attempt to set INSTALL_KEY without setting address");
                                configuration.SetResult(option, ZigBeeStatus.FAILURE);
                                break;
                            }
                            Status result = ncp.AddTransientLinkKey(nodeKey.address, nodeKey);

                            configuration.SetResult(option, result == Status.SL_STATUS_OK ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE);
                            break;

                        case TransportConfigOption.RADIO_TX_POWER:
                            configuration.SetResult(option, SetEmberTxPower((sbyte) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.DEVICE_TYPE:
                            _deviceType = (DeviceType) configuration.GetValue(option);
                            configuration.SetResult(option, ZigBeeStatus.SUCCESS);
                            break;

                        case TransportConfigOption.TRUST_CENTRE_LINK_KEY:
                            SetTcLinkKey((ZigBeeKey) configuration.GetValue(option));
                            configuration.SetResult(option, ZigBeeStatus.SUCCESS);
                            break;

                        case TransportConfigOption.TRUST_CENTRE_JOIN_MODE:
                            configuration.SetResult(option, SetTcJoinMode((TrustCentreJoinMode) configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.SUPPORTED_INPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedInputClusters((ICollection<ushort>)configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.SUPPORTED_OUTPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedOutputClusters((ICollection<ushort>)configuration.GetValue(option)));
                            break;

                        default:
                            configuration.SetResult(option, ZigBeeStatus.UNSUPPORTED);
                            _logger.LogDebug("Unsupported configuration option \"{Option}\" in EZSP dongle", option);
                            break;
                    }
                } 
                catch (Exception ex) 
                {
                    _logger.LogDebug(ex, "EZSP Dongle: error in UpdateTransportConfig");
                    configuration.SetResult(option, ZigBeeStatus.INVALID_ARGUMENTS);
                }
            }
        }

        private ushort[] CopyClusters(ICollection<ushort> clusterList) 
        {
            ushort[] clusters = new ushort[clusterList.Count];
            int cnt = 0;
            foreach (ushort value in clusterList) 
            {
                clusters[cnt++] = value;
            }
            return clusters;
        }

        private ZigBeeStatus SetSupportedInputClusters(ICollection<ushort> supportedClusters) 
        {
            if (_initialised)
                return ZigBeeStatus.INVALID_STATE;

            _inputClusters = CopyClusters(supportedClusters);
            return ZigBeeStatus.SUCCESS;
        }

        private ZigBeeStatus SetSupportedOutputClusters(ICollection<ushort> supportedClusters) 
        {
            if (_initialised)
                return ZigBeeStatus.INVALID_STATE;
 
            _outputClusters = CopyClusters(supportedClusters);
            return ZigBeeStatus.SUCCESS;
        }

        private ZigBeeStatus SetTcJoinMode(TrustCentreJoinMode joinMode) 
        {
            ZigbeeEzspDecisionId emberJoinMode;
            switch (joinMode) 
            {
                case TrustCentreJoinMode.TC_JOIN_INSECURE:
                    emberJoinMode = ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_ALLOW_JOINS;
                    break;
                case TrustCentreJoinMode.TC_JOIN_SECURE:
                    emberJoinMode = ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_ALLOW_PRECONFIGURED_KEY_JOINS;
                    break;
                case TrustCentreJoinMode.TC_JOIN_DENY:
                    emberJoinMode = ZigbeeEzspDecisionId.SL_ZIGBEE_EZSP_DISALLOW_ALL_JOINS_AND_REJOINS;
                    break;
                default:
                    return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            return (GetEmberNcp().SetPolicy(ZigbeeEzspPolicyId.SL_ZIGBEE_EZSP_TRUST_CENTER_POLICY, emberJoinMode) == Status.SL_STATUS_OK) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        private bool InitialiseEzspProtocol() 
        {
            if (_frameHandler != null) 
            {
                _logger.LogError("EZSP Dongle: Attempt to initialise Ember dongle when already initialised");
                return false;
            }
            if (!_serialPort.Open()) 
            {
                _logger.LogError("EZSP Dongle: Unable to open serial port");
                return false;
            }

            switch (_protocol) {
                case EmberSerialProtocol.ASH2:
                    _frameHandler = new AshFrameHandler(this);
                    break;
                    //Not implemented yet
                    /*
                case EmberSerialProtocol.SPI:
                    frameHandler = new SpiFrameHandler(this);
                    break;
                    */
                case EmberSerialProtocol.NONE:
                    return true;
                default:
                    _logger.LogError("EZSP Dongle: Unknown serial protocol {Protocol}", _protocol);
                    return false;
            }

            // Connect to the ASH handler and NCP
            _frameHandler.Start(_serialPort);

            // If possible, perform a hardware reset of the NCP
            /*
            if (resetProvider != null) {
                resetProvider.emberNcpReset(serialPort);
            }
            */

            _frameHandler.Connect();

            EmberNcp ncp = GetEmberNcp();

            // We MUST send the version command first.
            // Any failure to respond here indicates a failure of the ASH or EZSP layers to initialise
            VersionResponse version = ncp.Version(4);
            if (version == null) 
            {
                _logger.LogDebug("EZSP Dongle: Version returned null. ASH/EZSP not initialised.");
                return false;
            }

            if (version.ProtocolVersion != EzspFrameV8Plus.GetEzspVersion()) 
            {
                // The device supports a different version that we current have set
                if (!EzspFrameV8Plus.SetEzspVersion(version.ProtocolVersion))
                {
                    _logger.LogError("EZSP Dongle: NCP requires unsupported version of EZSP (required = V{RequiredVersion}, supported = V{SupportedVersion})",
                            version.ProtocolVersion, EzspFrameV8Plus.GetEzspVersion());
                    return false;
                }

                version = ncp.Version(EzspFrameV8Plus.GetEzspVersion());
                _logger.LogDebug(version.ToString());
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("EZSP Version=");
            builder.Append(version.ProtocolVersion);
            builder.Append(", Stack Type=");
            builder.Append(version.StackType);
            builder.Append(", Stack Version=");
            for (int cnt = 3; cnt >= 0; cnt--) 
            {
                builder.Append((version.StackVersion >> (cnt * 4)) & 0x0F);
                if (cnt != 0) 
                {
                    builder.Append('.');
                }
            }

            var standaloneBootloaderVersionPlatMicroPhy = ncp.GetStandaloneBootloaderVersionPlatMicroPhy();
            builder.Append(", Bootloader Version=");
            if (standaloneBootloaderVersionPlatMicroPhy.BootloaderVersion == BOOTLOADER_INVALID_VERSION) 
            {
                builder.Append("NONE");
            } 
            else 
            {
                builder.Append((standaloneBootloaderVersionPlatMicroPhy.BootloaderVersion >> 12) & 0x0F);
                builder.Append('.');
                builder.Append((standaloneBootloaderVersionPlatMicroPhy.BootloaderVersion >> 8) & 0x0F);

                builder.Append(" build ");
                builder.Append(standaloneBootloaderVersionPlatMicroPhy.BootloaderVersion & 0xFF);
            }
            builder.Append(", Platform=");
            builder.Append(standaloneBootloaderVersionPlatMicroPhy.NodePlat);
            builder.Append(", Micro=");
            builder.Append(standaloneBootloaderVersionPlatMicroPhy.NodeMicro);
            builder.Append(", Phy=");
            builder.Append(standaloneBootloaderVersionPlatMicroPhy.NodePhy);

            VersionString = builder.ToString();

            return true;
        }

        // Callback from the bootload handler when the transfer is completed/aborted/failed
        //Not implemented yet
        /*
        public void bootloadComplete() {
            bootloadHandler = null;
        }
        */

        private ZigBeeStatus SetConcentrator(ConcentratorConfig concentratorConfig) 
        {
            SetConcentratorRequest concentratorRequest = new SetConcentratorRequest();
            concentratorRequest.MinTime = (ushort)concentratorConfig.RefreshMinimum;
            concentratorRequest.MaxTime = (ushort)concentratorConfig.RefreshMaximum;
            concentratorRequest.MaxHops = (byte)concentratorConfig.MaxHops;
            concentratorRequest.RouteErrorThreshold = (byte)concentratorConfig.MaxFailures;
            concentratorRequest.DeliveryFailureThreshold = (byte)concentratorConfig.MaxFailures;
            switch (concentratorConfig.Type) 
            {
                case ConcentratorType.DISABLED:
                    concentratorRequest.On = false;
                    concentratorRequest.ConcentratorType = (ushort)ConcentratorType.DISABLED;
                    break;
                case ConcentratorType.HIGH_RAM:
                    concentratorRequest.ConcentratorType = (ushort)ConcentratorType.HIGH_RAM;
                    concentratorRequest.On = true;
                    break;
                case ConcentratorType.LOW_RAM:
                    concentratorRequest.ConcentratorType = (ushort)ConcentratorType.LOW_RAM;
                    concentratorRequest.On = true;
                    break;
                default:
                    break;
            }

            ITransaction concentratorTransaction = _frameHandler.SendTransaction(new SingleResponseTransaction(concentratorRequest, typeof(SetConcentratorResponse)));
            SetConcentratorResponse concentratorResponse = (SetConcentratorResponse) concentratorTransaction.GetResponse();
            _logger.LogDebug(concentratorResponse.ToString());

            if (concentratorResponse.Status == Status.SL_STATUS_OK)
                return ZigBeeStatus.SUCCESS;
            
            return ZigBeeStatus.FAILURE;
        }

        /**
         * Set the Ember Radio transmitter power
         *
         * @param txPower the power in dBm
         * @return {@link ZigBeeStatus}
         */
        private ZigBeeStatus SetEmberTxPower(sbyte txPower) 
        {
            _networkParameters.radioTxPower = txPower;

            EmberNcp ncp = GetEmberNcp();
            return (ncp.SetRadioPower(txPower) == Status.SL_STATUS_OK) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.BAD_RESPONSE;
        }

        /**
         * Get a map of statistics counters from the dongle
         *
         * @return map of counters
         */
        public Dictionary<string, long> GetCounters() 
        {
            if (_frameHandler != null)
                return _frameHandler.GetCounters();
        
            return new Dictionary<string, long>();
        }

        /**
         * Converts from an {@link EmberKeyStruct} to {@link ZigBeeKey}
         *
         * @param emberKey the {@link EmberKeyStruct} read from the NCP
         * @return the {@link ZigBeeKey} used by the framework. May be null if the key is invalid.
         */
        private ZigBeeKey EmberKeyToZigBeeKey(ZigbeeKeyStruct zigbeeKey) 
        {
            ZigBeeKey key = new ZigBeeKey(zigbeeKey.key.contents);

            if (zigbeeKey.bitmask.HasFlag(ZigbeeKeyStructBitmask.SL_ZIGBEE_KEY_HAS_PARTNER_EUI64))
                key.address = new IeeeAddress(zigbeeKey.partnerEUI64);
            
            if (zigbeeKey.bitmask.HasFlag(ZigbeeKeyStructBitmask.SL_ZIGBEE_KEY_HAS_SEQUENCE_NUMBER))
                key.SequenceNumber = (byte)zigbeeKey.sequenceNumber;
            
            if (zigbeeKey.bitmask.HasFlag(ZigbeeKeyStructBitmask.SL_ZIGBEE_KEY_HAS_OUTGOING_FRAME_COUNTER))
                key.OutgoingFrameCounter = (byte) zigbeeKey.outgoingFrameCounter;

            if (zigbeeKey.bitmask.HasFlag(ZigbeeKeyStructBitmask.SL_ZIGBEE_KEY_HAS_INCOMING_FRAME_COUNTER))
                key.IncomingFrameCounter = (byte) zigbeeKey.incomingFrameCounter;

            return key;
        }

        /**
         * Gets the {@link ProtocolHandler}
         *
         * @return the {@link ProtocolHandler}
         */
        protected IEzspProtocolHandler GetProtocolHandler() 
        {
            return _frameHandler;
        }
    }
}

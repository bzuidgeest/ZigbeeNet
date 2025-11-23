using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Transaction;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;
using ZigBeeNet.Util;
using Microsoft.Extensions.Logging;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;
using System.Buffers.Binary;

namespace ZigBeeNet.Hardware.EmberV8Plus.Internal
{
    /// <summary>
    /// This class provides utility functions to establish an Ember ZigBee network
    /// </summary>
    public class EmberNetworkInitialisation 
    {
        static private readonly ILogger _logger = LogManager.GetLog<EmberNetworkInitialisation>();
        /**
         * The frame handler used to send the EZSP frames to the NCP
         */
        private IEzspProtocolHandler _protocolHandler;

        /**
         * Scan duration used for scans
         */
        private int _scanDuration = 1;

        /**
         * @param protocolHandler the {@link EzspProtocolHandler} used to communicate with the NCP
         */
        public EmberNetworkInitialisation(IEzspProtocolHandler protocolHandler) 
        {
            this._protocolHandler = protocolHandler;
        }

        /**
         * Sets the scan duration used when performing scans.
         *
         * @param scanDuration the scan duration. Sets the exponent of the number of scan periods, where a scan period is
         *            960 symbols. The scan will occur for ((2^duration) + 1) scan periods.
         */
        public void SetScanDuration(int scanDuration) 
        {
            this._scanDuration = scanDuration;
        }

        /**
         * This utility function uses emberStartScan, emberStopScan, emberScanCompleteHandler, emberEnergyScanResultHandler,
         * and emberNetworkFoundHandler to discover other networks or determine the background noise level. It then uses
         * emberFormNetwork to create a new network with a unique PAN-ID on a channel with low background noise.
         * <p>
         * Setting the PAN-ID or Extended PAN-ID to 0 will set these values to a random value.
         * <p>
         * If channel is set to 0, the quietest channel will be used.
         *
         * @param networkParameters the required {@link EmberNetworkParameters}
         * @param linkKey the {@link ZigBeeKey} with the link key. This can not be set to all 00 or all FF.
         * @param networkKey the {@link ZigBeeKey} with the network key. This can not be set to all 00 or all FF.
         */
        public void FormNetwork(ZigbeeNetworkParameters networkParameters, ZigBeeKey linkKey, ZigBeeKey networkKey) 
        {
            if (networkParameters.extendedPanId == null) 
            {
                networkParameters.extendedPanId = new ExtendedPanId().PanId;
            }

            _logger.LogDebug("Initialising Zigbee network with configuration {NetworkParameters}", networkParameters);

            EmberNcp ncp = new EmberNcp(_protocolHandler);

            // Leave the current network so we can initialise a new network
            if (CheckNetworkJoined()) 
            {
                ncp.LeaveNetwork(ZigbeeLeaveNetworkOption.SL_ZIGBEE_LEAVE_NWK_WITH_NO_OPTION);
            }

            ncp.ClearKeyTable();

            // Perform an energy scan to find a clear channel
            //fix
            //int? quietestChannel = DoEnergyScan(ncp, _scanDuration);
            //_logger.LogDebug("Energy scan reports quietest channel is {QuietestChannel}", quietestChannel);

            // Check if any current networks were found and avoid those channels, PAN ID and especially Extended PAN ID
            //fix
            //ncp.EnergyScanRequest(0, ZigBeeChannelMask.CHANNEL_MASK_2GHZ, _scanDuration, 1);

            // Read the current network parameters
            GetNetworkParameters();

            // Create a random PAN ID and Extended PAN ID
            if (networkParameters.panId == 0 || networkParameters.extendedPanId.Equals(new ExtendedPanId())) 
            {
                Random random = new Random();
                int panId = random.Next(65535);
                networkParameters.panId = (ushort)panId;
                _logger.LogDebug("Created random PAN ID: {PanId}", panId);

                byte[] extendedPanIdBytes = new byte[8];
                random.NextBytes(extendedPanIdBytes);
                ExtendedPanId extendedPanId = new ExtendedPanId(extendedPanIdBytes);
                networkParameters.extendedPanId = extendedPanId.PanId;
                _logger.LogDebug("Created random Extended PAN ID: {ExtendedPanId}", extendedPanId.ToString());
            }

            //fix
            //if (networkParameters.radioChannel == 0 && quietestChannel.HasValue) 
            //{
            //    networkParameters.radioChannel = (byte)quietestChannel.Value;
            //}

            // If the channel set is empty, use the single channel defined above
            if (networkParameters.channels == 0) 
            {
                networkParameters.channels = (uint)(1 << networkParameters.radioChannel);
            }

            // Initialise security
            SetSecurityState(linkKey, networkKey);

            // And now form the network
            DoFormNetwork(networkParameters);
        }

        /**
         * Utility function to join an existing network as a Router
         *
         * @param networkParameters the required {@link EmberNetworkParameters}
         * @param linkKey the {@link ZigBeeKey} with the initial link key. This cannot be set to all 00 or all FF.
         */
        public void JoinNetwork(ZigbeeNetworkParameters networkParameters, ZigBeeKey linkKey, ZigbeeLeaveNetworkOption zigbeeLeaveNetworkOption) 
        {
            _logger.LogDebug("Joining Ember network with configuration {Parameters}", networkParameters);

            // Leave the current network so we can initialise a new network
            EmberNcp ncp = new EmberNcp(_protocolHandler);
            if (CheckNetworkJoined())
                ncp.LeaveNetwork(zigbeeLeaveNetworkOption);

            ncp.ClearKeyTable();

            // Initialise security - no network key as we'll get that from the coordinator
            SetSecurityState(linkKey, null);

            DoJoinNetwork(networkParameters);
        }

        /**
         * Searches for the current network, assuming that we know the network key.
         * This will search all current channels.
         */
        public void RejoinNetwork() 
        {
            DoRejoinNetwork(true, new ZigBeeChannelMask(0));
        }

        private bool CheckNetworkJoined() 
        {
            // Check if the network is initialised
            NetworkStateRequest networkStateRequest = new NetworkStateRequest();
            ITransaction networkStateTransaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(networkStateRequest, typeof(NetworkStateResponse)));
            NetworkStateResponse networkStateResponse = (NetworkStateResponse) networkStateTransaction.GetResponse();
            _logger.LogDebug(networkStateResponse.ToString());
            _logger.LogDebug("EZSP networkStateResponse {Status}", networkStateResponse.Status);

            return networkStateResponse.Status == ZigbeeNetworkStatus.SL_ZIGBEE_JOINED_NETWORK;
        }

        /**
         * Performs an energy scan and returns the quietest channel
         *
         * @param ncp {@link EmberNcp}
         * @param scanDuration duration of the scan on each channel
         * @return the quietest channel, or null on error
         */
        //private int? DoEnergyScan(EmberNcp ncp, byte scanDuration) 
        //{
        //    List<EnergyScanResultHandlerResponse> channels = ncp.EnergyScanRequest(0, ZigBeeChannelMask.CHANNEL_MASK_2GHZ, scanDuration, 1);

        //    if (channels == null) {
        //        _logger.LogDebug("Error during energy scan: {Status}", ncp.GetLastStatus());
        //        return null;
        //    }

        //    int lowestRSSI = 999;
        //    int lowestChannel = 11;
        //    foreach (EnergyScanResultHandlerResponse channel in channels) 
        //    {
        //        if (channel.MaxRssiValue < lowestRSSI) {
        //            lowestRSSI = channel.MaxRssiValue;
        //            lowestChannel = channel.Channel;
        //        }
        //    }

        //    return lowestChannel;
        //}

        /**
         * Get the current network parameters
         *
         * @return the {@link EmberNetworkParameters} or null on error
         */
        private ZigbeeNetworkParameters? GetNetworkParameters() 
        {
            GetNetworkParametersRequest networkParms = new GetNetworkParametersRequest();
            SingleResponseTransaction transaction = new SingleResponseTransaction(networkParms, typeof(GetNetworkParametersResponse));
            _protocolHandler.SendTransaction(transaction);
            GetNetworkParametersResponse getNetworkParametersResponse = (GetNetworkParametersResponse) transaction.GetResponse();
            _logger.LogDebug(getNetworkParametersResponse.ToString());
            if (getNetworkParametersResponse.Status != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Error during retrieval of network parameters: {Response}", getNetworkParametersResponse);
                return null; 
            }
            return getNetworkParametersResponse.Parameters;
        }

        /**
         * Sets the initial security state
         *
         * @param linkKey the initial {@link ZigBeeKey}
         * @param networkKey the initial {@link ZigBeeKey}
         * @return true if the security state was set successfully
         */
        private bool SetSecurityState(ZigBeeKey linkKey, ZigBeeKey networkKey) 
        {
            SetInitialSecurityStateRequest securityState = new SetInitialSecurityStateRequest();
            ZigbeeInitialSecurityState state = new ZigbeeInitialSecurityState();
            state.bitmask |= ZigbeeInitialSecurityBitmask.SL_ZIGBEE_TRUST_CENTER_GLOBAL_LINK_KEY;

            ZigbeeKeyData networkKeyData = new ZigbeeKeyData();
            if (networkKey != null) 
            {
                networkKeyData.contents = networkKey.Key;
                state.bitmask |= ZigbeeInitialSecurityBitmask.SL_ZIGBEE_HAVE_NETWORK_KEY;
                if (networkKey.SequenceNumber.HasValue) 
                {
                    state.networkKeySequenceNumber = networkKey.SequenceNumber.Value;
                }
            }
            state.networkKey = networkKeyData;

            ZigbeeKeyData linkKeyData = new ZigbeeKeyData();
            if (linkKey != null) 
            {
                linkKeyData.contents = linkKey.Key;
                state.bitmask = ZigbeeInitialSecurityBitmask.SL_ZIGBEE_HAVE_PRECONFIGURED_KEY;
                state.bitmask = ZigbeeInitialSecurityBitmask.SL_ZIGBEE_REQUIRE_ENCRYPTED_KEY;
            }
            state.preconfiguredKey = linkKeyData;


            state.preconfiguredTrustCenterEui64 = new IeeeAddress().AsByteArray();

            securityState.State = state;
            SingleResponseTransaction transaction = new SingleResponseTransaction(securityState, typeof(SetInitialSecurityStateResponse));
            _protocolHandler.SendTransaction(transaction);
            SetInitialSecurityStateResponse securityStateResponse = (SetInitialSecurityStateResponse) transaction.GetResponse();
            _logger.LogDebug(securityStateResponse.ToString());
            if (securityStateResponse.Success != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Error during retrieval of network parameters: {Response}", securityStateResponse);
                return false;
            }

            EmberNcp ncp = new EmberNcp(_protocolHandler);
            if (networkKey != null && networkKey.OutgoingFrameCounter.HasValue) 
            {
                Span<byte> payload = new Span<byte>(new byte[4]);
                BinaryPrimitives.WriteUInt32LittleEndian(payload, networkKey.OutgoingFrameCounter.Value);
                if (ncp.SetValue(ZigbeeEzspValueId.SL_ZIGBEE_EZSP_VALUE_NWK_FRAME_COUNTER, (byte)payload.Length, payload.ToArray()) != Status.SL_STATUS_OK)
                    return false;
            }
            if (linkKey != null && linkKey.OutgoingFrameCounter.HasValue) 
            {
                Span<byte> payload = new Span<byte>(new byte[4]);
                BinaryPrimitives.WriteUInt32LittleEndian(payload, networkKey.OutgoingFrameCounter.Value);
                if (ncp.SetValue(ZigbeeEzspValueId.SL_ZIGBEE_EZSP_VALUE_APS_FRAME_COUNTER, (byte)payload.Length, payload.ToArray()) != Status.SL_STATUS_OK)
                    return false;
            }

            return true;
        }

        /**
         * Forms the ZigBee network as a coordinator
         *
         * @param networkParameters the {@link EmberNetworkParameters}
         * @return true if the network was formed successfully
         */
        private bool DoFormNetwork(ZigbeeNetworkParameters networkParameters) 
        {
            networkParameters.joinMethod = ZigbeeJoinMethod.SL_ZIGBEE_USE_MAC_ASSOCIATION;

            FormNetworkRequest formNetwork = new FormNetworkRequest();
            formNetwork.Parameters = networkParameters;
            SingleResponseTransaction transaction = new SingleResponseTransaction(formNetwork, typeof(FormNetworkResponse));
            _protocolHandler.SendTransaction(transaction);
            FormNetworkResponse formNetworkResponse = (FormNetworkResponse) transaction.GetResponse();
            _logger.LogDebug(formNetworkResponse.ToString());
            if (formNetworkResponse.Status != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Error forming network: {Response}", formNetworkResponse);
                return false;
            }

            return true;
        }

        /**
         * Joins an existing ZigBee network as a router
         *
         * @param networkParameters the {@link EmberNetworkParameters}
         * @return true if the network was joined successfully
         */
        private bool DoJoinNetwork(ZigbeeNetworkParameters networkParameters) 
        {
            networkParameters.joinMethod = ZigbeeJoinMethod.SL_ZIGBEE_USE_MAC_ASSOCIATION;

            JoinNetworkRequest joinNetwork = new JoinNetworkRequest();
            joinNetwork.NodeType = ZigbeeNodeType.SL_ZIGBEE_ROUTER;
            joinNetwork.Parameters =networkParameters;
            SingleResponseTransaction transaction = new SingleResponseTransaction(joinNetwork, typeof(JoinNetworkResponse));
            _protocolHandler.SendTransaction(transaction);

            JoinNetworkResponse joinNetworkResponse = (JoinNetworkResponse) transaction.GetResponse();
            _logger.LogDebug(joinNetworkResponse.ToString());
            if (joinNetworkResponse.Status != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Error joining network: {Response}", joinNetworkResponse);
                return false;
            }
            return true;
        }

        /**
         * Rejoins an existing ZigBee network as a router.
         *
         * @param haveCurrentNetworkKey true if we already know the network key
         * @param channelMask the channel mask to scan.
         * @return true if the network was joined successfully
         */
        private bool DoRejoinNetwork(bool haveCurrentNetworkKey, ZigBeeChannelMask channelMask) 
        {
            FindAndRejoinNetworkRequest rejoinNetwork = new FindAndRejoinNetworkRequest();
            rejoinNetwork.HaveCurrentNetworkKey = haveCurrentNetworkKey;
            rejoinNetwork.ChannelMask = (uint)channelMask.ChannelMask;
            SingleResponseTransaction transaction = new SingleResponseTransaction(rejoinNetwork, typeof(FindAndRejoinNetworkResponse));
            _protocolHandler.SendTransaction(transaction);

            FindAndRejoinNetworkResponse rejoinNetworkResponse = (FindAndRejoinNetworkResponse) transaction.GetResponse();
            _logger.LogDebug(rejoinNetworkResponse.ToString());
            if (rejoinNetworkResponse.Status != Status.SL_STATUS_OK) 
            {
                _logger.LogDebug("Error rejoining network: {Response}", rejoinNetworkResponse);
                return false;
            }

            return true;
        }

    }
}

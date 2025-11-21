#if VERSION_2025_6_2
using System;
using System.Collections.Generic;
using ZigBeeNet.Hardware.EmberV8Plus.Transaction;
using ZigBeeNet.Hardware.EmberV8Plus.Internal;
using Microsoft.Extensions.Logging;
using ZigBeeNet.Util;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp
{
    public partial class EmberNcp
    {
        static private readonly ILogger _logger = LogManager.GetLog<EmberNcp>();
        private IEzspProtocolHandler _protocolHandler;
        private ZigbeeEzspStatus _lastStatus;
        public EmberNcp(IEzspProtocolHandler protocolHandler)
        {
            this._protocolHandler = protocolHandler;
        }

        /// <summary>
        /// Returns the status from the last request.
        /// </summary>
        /// <returns>The last status value</returns>
        public ZigbeeEzspStatus GetLastStatus()
        {
            return _lastStatus;
        }

        /// <summary>
        /// The command allows the Host to specify the desired EZSP version and must be sent before any other command. The response provides information about the firmware running on the NCP.
        /// </summary>
        /// <param name="DesiredProtocolVersion">The EZSP version the Host wishes to use. To successfully set the version and allow other commands, this must be same as EZSP_PROTOCOL_VERSION.</param>
        /// <returns>A tuple containing:
        /// - ProtocolVersion: The EZSP version the NCP is using.
        /// - StackType: The type of stack running on the NCP (2).
        /// - StackVersion: The version number of the stack.
        /// </returns>
        public (byte ProtocolVersion, byte StackType, ushort StackVersion) Version(byte desiredProtocolVersion)
        {
            VersionRequest request = new VersionRequest();
            request.DesiredProtocolVersion = desiredProtocolVersion;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(VersionResponse)));
            VersionResponse response = (VersionResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.ProtocolVersion, response.StackType, response.StackVersion);
        }

        /// <summary>
        /// Reads a configuration value from the NCP.
        /// </summary>
        /// <param name="ConfigId">Identifies which configuration value to read.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize <i>configId</i>.
        /// - Value: The configuration value.
        /// </returns>
        public (Status Status, ushort Value) GetConfigurationValue(ZigbeeEzspConfigId configId)
        {
            GetConfigurationValueRequest request = new GetConfigurationValueRequest();
            request.ConfigId = configId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetConfigurationValueResponse)));
            GetConfigurationValueResponse response = (GetConfigurationValueResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Writes a configuration value to the NCP. Configuration values can be modified by the Host after the NCP has reset. Once the status of the stack changes to SL_STATUS_NETWORK_UP, configuration values can no longer be modified and this command will respond with SL_ZIGBEE_EZSP_ERROR_INVALID_CALL.
        /// </summary>
        /// <param name="ConfigId">Identifies which configuration value to change.</param>
        /// <param name="Value">The new configuration value.</param>
        /// <returns>SL_STATUS_OK if the configuration value was changed, SL_STATUS_ZIGBEE_EZSP_ERROR if there was an error. Retrievable EZSP errors can be SL_ZIGBEE_EZSP_ERROR_OUT_OF_MEMORY if the new value exceeded the available memory, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the new value was out of bounds, SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;configId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if configuration values can no longer be modified.</returns>
        public Status SetConfigurationValue(ZigbeeEzspConfigId configId, ushort value)
        {
            SetConfigurationValueRequest request = new SetConfigurationValueRequest();
            request.ConfigId = configId;
            request.Value = value;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetConfigurationValueResponse)));
            SetConfigurationValueResponse response = (SetConfigurationValueResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Read attribute data on NCP endpoints.
        /// </summary>
        /// <param name="Endpoint">Endpoint</param>
        /// <param name="Cluster">Cluster.</param>
        /// <param name="AttributeId">Attribute ID.</param>
        /// <param name="Mask">Mask.</param>
        /// <param name="ManufacturerCode">Manufacturer code.</param>
        /// <returns>A tuple containing:
        /// - AfStatus: An sl_zigbee_af_status_t value indicating success or the reason for failure, handled by the EZSP layer as a uint8_t. 255 indicates an EZSP-specific error.
        /// - DataType: Attribute data type.
        /// - ReadLength: Length of attribute data.
        /// - DataPtr: Attribute data.
        /// </returns>
        public (ZigbeeAfStatus AfStatus, byte DataType, byte ReadLength, byte[] DataPtr) ReadAttribute(byte endpoint, ushort cluster, ushort attributeId, byte mask, ushort manufacturerCode)
        {
            ReadAttributeRequest request = new ReadAttributeRequest();
            request.Endpoint = endpoint;
            request.Cluster = cluster;
            request.AttributeId = attributeId;
            request.Mask = mask;
            request.ManufacturerCode = manufacturerCode;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ReadAttributeResponse)));
            ReadAttributeResponse response = (ReadAttributeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.AfStatus, response.DataType, response.ReadLength, response.DataPtr);
        }

        /// <summary>
        /// Write attribute data on NCP endpoints.
        /// </summary>
        /// <param name="Endpoint">Endpoint</param>
        /// <param name="Cluster">Cluster.</param>
        /// <param name="AttributeId">Attribute ID.</param>
        /// <param name="Mask">Mask.</param>
        /// <param name="ManufacturerCode">Manufacturer code.</param>
        /// <param name="OverrideReadOnlyAndDataType">Override read only and data type.</param>
        /// <param name="JustTest">Override read only and data type.</param>
        /// <param name="DataType">Attribute data type.</param>
        /// <param name="DataLength">Attribute data length.</param>
        /// <param name="Data">Attribute data.</param>
        /// <returns>An sl_zigbee_af_status_t value indicating success or the reason for failure.</returns>
        public ZigbeeAfStatus WriteAttribute(byte endpoint, ushort cluster, ushort attributeId, byte mask, ushort manufacturerCode, bool overrideReadOnlyAndDataType, bool justTest, byte dataType, byte dataLength, byte[] data)
        {
            WriteAttributeRequest request = new WriteAttributeRequest();
            request.Endpoint = endpoint;
            request.Cluster = cluster;
            request.AttributeId = attributeId;
            request.Mask = mask;
            request.ManufacturerCode = manufacturerCode;
            request.OverrideReadOnlyAndDataType = overrideReadOnlyAndDataType;
            request.JustTest = justTest;
            request.DataType = dataType;
            request.DataLength = dataLength;
            request.Data = data;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(WriteAttributeResponse)));
            WriteAttributeResponse response = (WriteAttributeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.AfStatus;
        }

        /// <summary>
        /// Configures endpoint information on the NCP. The NCP does not remember these settings after a reset. Endpoints can be added by the Host after the NCP has reset. Once the status of the stack changes to SL_STATUS_NETWORK_UP, endpoints can no longer be added and this command will respond with SL_ZIGBEE_EZSP_ERROR_INVALID_CALL.
        /// </summary>
        /// <param name="Endpoint">The application endpoint to be added.</param>
        /// <param name="ProfileId">The endpoint&apos;s application profile.</param>
        /// <param name="DeviceId">The endpoint&apos;s device ID within the application profile.</param>
        /// <param name="DeviceVersion">The endpoint&apos;s device version.</param>
        /// <param name="InputClusterCount">The number of cluster IDs in &lt;i&gt;inputClusterList&lt;/i&gt;.</param>
        /// <param name="OutputClusterCount">The number of cluster IDs in &lt;i&gt;outputClusterList&lt;/i&gt;.</param>
        /// <param name="InputClusterList">Input cluster IDs the endpoint will accept.</param>
        /// <param name="OutputClusterList">Output cluster IDs the endpoint may send.</param>
        /// <returns>SL_STATUS_OK if the endpoint was added, SL_STATUS_ZIGBEE_EZSP_ERROR if there was an error. Errors could be SL_ZIGBEE_EZSP_ERROR_OUT_OF_MEMORY if there is not enough memory available to add the endpoint, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the endpoint already exists, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if endpoints can no longer be added.</returns>
        public Status AddEndpoint(byte endpoint, ushort profileId, ushort deviceId, byte deviceVersion, byte inputClusterCount, byte outputClusterCount, ushort[] inputClusterList, ushort[] outputClusterList)
        {
            AddEndpointRequest request = new AddEndpointRequest();
            request.Endpoint = endpoint;
            request.ProfileId = profileId;
            request.DeviceId = deviceId;
            request.DeviceVersion = deviceVersion;
            request.InputClusterCount = inputClusterCount;
            request.OutputClusterCount = outputClusterCount;
            request.InputClusterList = inputClusterList;
            request.OutputClusterList = outputClusterList;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(AddEndpointResponse)));
            AddEndpointResponse response = (AddEndpointResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the Host to change the policies used by the NCP to make fast decisions.
        /// </summary>
        /// <param name="PolicyId">Identifies which policy to modify.</param>
        /// <param name="DecisionId">The new decision for the specified policy.</param>
        /// <returns>SL_STATUS_OK if the policy was changed, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize &lt;i&gt;policyId&lt;/i&gt;.</returns>
        public Status SetPolicy(ZigbeeEzspPolicyId policyId, ZigbeeEzspDecisionId decisionId)
        {
            SetPolicyRequest request = new SetPolicyRequest();
            request.PolicyId = policyId;
            request.DecisionId = decisionId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetPolicyResponse)));
            SetPolicyResponse response = (SetPolicyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the Host to read the policies used by the NCP to make fast decisions.
        /// </summary>
        /// <param name="PolicyId">Identifies which policy to read.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the policy was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize <i>policyId</i>.
        /// - DecisionId: The current decision for the specified policy.
        /// </returns>
        public (Status Status, ZigbeeEzspDecisionId DecisionId) GetPolicy(ZigbeeEzspPolicyId policyId)
        {
            GetPolicyRequest request = new GetPolicyRequest();
            request.PolicyId = policyId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetPolicyResponse)));
            GetPolicyResponse response = (GetPolicyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.DecisionId);
        }

        /// <summary>
        /// Triggers a pan id update message.
        /// </summary>
        /// <param name="NewPan">The new Pan Id</param>
        /// <returns>true if the request was successfully handed to the stack, false otherwise</returns>
        public bool SendPanIdUpdate(ushort newPan)
        {
            SendPanIdUpdateRequest request = new SendPanIdUpdateRequest();
            request.NewPan = newPan;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendPanIdUpdateResponse)));
            SendPanIdUpdateResponse response = (SendPanIdUpdateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Reads a value from the NCP.
        /// </summary>
        /// <param name="ValueId">Identifies which value to read.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize <i>valueId</i>, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the length of the returned <i>value</i> exceeds the size of local storage allocated to receive it.
        /// - ValueLength: Both a command and response parameter. On command, the maximum size in bytes of local storage allocated to receive the returned <i>value</i>. On response, the actual length in bytes of the returned <i>value</i>.
        /// - Value: The value.
        /// </returns>
        public (Status Status, byte ValueLength, byte[] Value) GetValue(ZigbeeEzspValueId valueId)
        {
            GetValueRequest request = new GetValueRequest();
            request.ValueId = valueId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetValueResponse)));
            GetValueResponse response = (GetValueResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ValueLength, response.Value);
        }

        /// <summary>
        /// Reads a value from the NCP but passes an extra argument specific to the value being retrieved.
        /// </summary>
        /// <param name="ValueId">Identifies which extended value ID to read.</param>
        /// <param name="Characteristics">Identifies which characteristics of the extended value ID to read. These are specific to the value being read.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize <i>valueId</i>, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the length of the returned <i>value</i> exceeds the size of local storage allocated to receive it.
        /// - ValueLength: Both a command and response parameter. On command, the maximum size in bytes of local storage allocated to receive the returned <i>value</i>. On response, the actual length in bytes of the returned <i>value</i>.
        /// - Value: The value.
        /// </returns>
        public (Status Status, byte ValueLength, byte[] Value) GetExtendedValue(ZigbeeEzspExtendedValueId valueId, uint characteristics)
        {
            GetExtendedValueRequest request = new GetExtendedValueRequest();
            request.ValueId = valueId;
            request.Characteristics = characteristics;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetExtendedValueResponse)));
            GetExtendedValueResponse response = (GetExtendedValueResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ValueLength, response.Value);
        }

        /// <summary>
        /// Writes a value to the NCP.
        /// </summary>
        /// <param name="ValueId">Identifies which value to change.</param>
        /// <param name="ValueLength">The length of the &lt;i&gt;value&lt;/i&gt; parameter in bytes.</param>
        /// <param name="Value">The new value.</param>
        /// <returns>SL_STATUS_OK if the value was changed, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the new value was out of bounds, SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;valueId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if the value could not be modified.</returns>
        public Status SetValue(ZigbeeEzspValueId valueId, byte valueLength, byte[] value)
        {
            SetValueRequest request = new SetValueRequest();
            request.ValueId = valueId;
            request.ValueLength = valueLength;
            request.Value = value;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetValueResponse)));
            SetValueResponse response = (SetValueResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the Host to control the broadcast behaviour of a routing device used by the NCP.
        /// </summary>
        /// <param name="Config">Passive ack config enum.</param>
        /// <param name="MinAcksNeeded">The minimum number of acknowledgments (re-broadcasts) to wait for until deeming the broadcast transmission complete.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetPassiveAckConfig(byte config, byte minAcksNeeded)
        {
            SetPassiveAckConfigRequest request = new SetPassiveAckConfigRequest();
            request.Config = config;
            request.MinAcksNeeded = minAcksNeeded;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetPassiveAckConfigResponse)));
            SetPassiveAckConfigResponse response = (SetPassiveAckConfigResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set the PAN ID to be accepted by the device in a NLME Network Update command.  If this is set to a different value than its default 0xFFFF, NLME network update messages will be ignored if they do not match this PAN ID.
        /// </summary>
        /// <param name="PanId">PAN ID to be accepted in a network update.</param>
        /// <returns>The SetPendingNetworkUpdatePanIdResponse object from the NCP</returns>
        public SetPendingNetworkUpdatePanIdResponse SetPendingNetworkUpdatePanId(ushort panId)
        {
            SetPendingNetworkUpdatePanIdRequest request = new SetPendingNetworkUpdatePanIdRequest();
            request.PanId = panId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetPendingNetworkUpdatePanIdResponse)));
            SetPendingNetworkUpdatePanIdResponse response = (SetPendingNetworkUpdatePanIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Retrieve the endpoint number located at the specified index.
        /// </summary>
        /// <param name="Index">Index to retrieve the endpoint number for.</param>
        /// <returns>Endpoint number at the index.</returns>
        public byte GetEndpoint(byte index)
        {
            GetEndpointRequest request = new GetEndpointRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetEndpointResponse)));
            GetEndpointResponse response = (GetEndpointResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Endpoint;
        }

        /// <summary>
        /// Get the number of configured endpoints.
        /// </summary>
        /// <returns>Number of configured endpoints.</returns>
        public byte GetEndpointCount()
        {
            GetEndpointCountRequest request = new GetEndpointCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetEndpointCountResponse)));
            GetEndpointCountResponse response = (GetEndpointCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Count;
        }

        /// <summary>
        /// Retrieve the endpoint description for the given endpoint number.
        /// </summary>
        /// <param name="Endpoint">Endpoint number to get the description of.</param>
        /// <returns>Description of this endpoint.</returns>
        public ZigbeeEndpointDescription GetEndpointDescription(byte endpoint)
        {
            GetEndpointDescriptionRequest request = new GetEndpointDescriptionRequest();
            request.Endpoint = endpoint;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetEndpointDescriptionResponse)));
            GetEndpointDescriptionResponse response = (GetEndpointDescriptionResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Result;
        }

        /// <summary>
        /// Retrieve one of the cluster IDs associated with the given endpoint.
        /// </summary>
        /// <param name="Endpoint">Endpoint number to get a cluster ID for.</param>
        /// <param name="ListId">Which list to get the cluster ID from.  (0 for input, 1 for output).</param>
        /// <param name="ListIndex">Index from requested list to look at the cluster ID of.</param>
        /// <returns>ID of the requested cluster.</returns>
        public ushort GetEndpointCluster(byte endpoint, byte listId, byte listIndex)
        {
            GetEndpointClusterRequest request = new GetEndpointClusterRequest();
            request.Endpoint = endpoint;
            request.ListId = listId;
            request.ListIndex = listIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetEndpointClusterResponse)));
            GetEndpointClusterResponse response = (GetEndpointClusterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.EndpointCluster;
        }

        /// <summary>
        /// A command which does nothing. The Host can use this to set the sleep mode or to check the status of the NCP.
        /// </summary>
        /// <returns>The NopResponse object from the NCP</returns>
        public NopResponse Nop()
        {
            NopRequest request = new NopRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NopResponse)));
            NopResponse response = (NopResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Variable length data from the Host is echoed back by the NCP. This command has no other effects and is designed for testing the link between the Host and NCP.
        /// </summary>
        /// <param name="DataLength">The length of the &lt;i&gt;data&lt;/i&gt; parameter in bytes.</param>
        /// <param name="Data">The data to be echoed back.</param>
        /// <returns>A tuple containing:
        /// - EchoLength: The length of the <i>echo</i> parameter in bytes.
        /// - Echo: The echo of the data.
        /// </returns>
        public (byte EchoLength, byte[] Echo) Echo(byte dataLength, byte[] data)
        {
            EchoRequest request = new EchoRequest();
            request.DataLength = dataLength;
            request.Data = data;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(EchoResponse)));
            EchoResponse response = (EchoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.EchoLength, response.Echo);
        }

        /// <summary>
        /// Indicates that the NCP received an invalid command.
        /// </summary>
        /// <returns>The reason why the command was invalid.</returns>
        public ZigbeeEzspStatus InvalidCommand()
        {
            InvalidCommandRequest request = new InvalidCommandRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(InvalidCommandResponse)));
            InvalidCommandResponse response = (InvalidCommandResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Reason;
        }

        /// <summary>
        /// Allows the NCP to respond with a pending callback.
        /// </summary>
        /// <returns>The CallbackResponse object from the NCP</returns>
        public CallbackResponse Callback()
        {
            CallbackRequest request = new CallbackRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CallbackResponse)));
            CallbackResponse response = (CallbackResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Indicates that there are currently no pending callbacks.
        /// </summary>
        /// <returns>The NoCallbacksResponse object from the NCP</returns>
        public NoCallbacksResponse NoCallbacks()
        {
            NoCallbacksRequest request = new NoCallbacksRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NoCallbacksResponse)));
            NoCallbacksResponse response = (NoCallbacksResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Sets a token (8 bytes of non-volatile storage) in the Simulated EEPROM of the NCP.
        /// </summary>
        /// <param name="TokenId">Which token to set</param>
        /// <param name="TokenData">The data to write to the token.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetToken(byte tokenId, byte[] tokenData)
        {
            SetTokenRequest request = new SetTokenRequest();
            request.TokenId = tokenId;
            request.TokenData = tokenData;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetTokenResponse)));
            SetTokenResponse response = (SetTokenResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Retrieves a token (8 bytes of non-volatile storage) from the Simulated EEPROM of the NCP.
        /// </summary>
        /// <param name="TokenId">Which token to read</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - TokenData: The contents of the token.
        /// </returns>
        public (Status Status, byte[] TokenData) GetToken(byte tokenId)
        {
            GetTokenRequest request = new GetTokenRequest();
            request.TokenId = tokenId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTokenResponse)));
            GetTokenResponse response = (GetTokenResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.TokenData);
        }

        /// <summary>
        /// Retrieves a manufacturing token from the Flash Information Area of the NCP (except for SL_ZIGBEE_EZSP_STACK_CAL_DATA which is managed by the stack).
        /// </summary>
        /// <param name="TokenId">Which manufacturing token to read.</param>
        /// <returns>A tuple containing:
        /// - TokenDataLength: The length of the <i>tokenData</i> parameter in bytes.
        /// - TokenData: The manufacturing token data.
        /// </returns>
        public (byte TokenDataLength, byte[] TokenData) GetMfgToken(ZigbeeEzspMfgTokenId tokenId)
        {
            GetMfgTokenRequest request = new GetMfgTokenRequest();
            request.TokenId = tokenId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetMfgTokenResponse)));
            GetMfgTokenResponse response = (GetMfgTokenResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.TokenDataLength, response.TokenData);
        }

        /// <summary>
        /// Sets a manufacturing token in the Customer Information Block (CIB) area of the NCP if that token currently unset (fully erased). Cannot be used with SL_ZIGBEE_EZSP_STACK_CAL_DATA, SL_ZIGBEE_EZSP_STACK_CAL_FILTER, SL_ZIGBEE_EZSP_MFG_ASH_CONFIG, or SL_ZIGBEE_EZSP_MFG_CBKE_DATA token.
        /// </summary>
        /// <param name="TokenId">Which manufacturing token to set.</param>
        /// <param name="TokenDataLength">The length of the &lt;i&gt;tokenData&lt;/i&gt; parameter in bytes.</param>
        /// <param name="TokenData">The manufacturing token data.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetMfgToken(ZigbeeEzspMfgTokenId tokenId, byte tokenDataLength, byte[] tokenData)
        {
            SetMfgTokenRequest request = new SetMfgTokenRequest();
            request.TokenId = tokenId;
            request.TokenDataLength = tokenDataLength;
            request.TokenData = tokenData;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetMfgTokenResponse)));
            SetMfgTokenResponse response = (SetMfgTokenResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked to inform the application that a stack token has changed.
        /// </summary>
        /// <returns>The address of the stack token that has changed.</returns>
        public ushort StackTokenChangedHandler()
        {
            StackTokenChangedHandlerRequest request = new StackTokenChangedHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StackTokenChangedHandlerResponse)));
            StackTokenChangedHandlerResponse response = (StackTokenChangedHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.TokenAddress;
        }

        /// <summary>
        /// Returns a pseudorandom number.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: Always returns SL_STATUS_OK.
        /// - Value: A pseudorandom number.
        /// </returns>
        public (Status Status, ushort Value) GetRandomNumber()
        {
            GetRandomNumberRequest request = new GetRandomNumberRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetRandomNumberResponse)));
            GetRandomNumberResponse response = (GetRandomNumberResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Sets a timer on the NCP. There are 2 independent timers available for use by the Host. A timer can be cancelled by setting &lt;i&gt;time&lt;/i&gt; to 0 or &lt;i&gt;units&lt;/i&gt; to SL_ZIGBEE_EVENT_INACTIVE.
        /// </summary>
        /// <param name="TimerId">Which timer to set (0 or 1).</param>
        /// <param name="Time">The delay before the &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated. Note that the timer clock is free running and is not synchronized with this command. This means that the actual delay will be between &lt;i&gt;time&lt;/i&gt; and (&lt;i&gt;time&lt;/i&gt; - 1). The maximum delay is 32767.</param>
        /// <param name="Units">The units for &lt;i&gt;time&lt;/i&gt;.</param>
        /// <param name="Repeat">If true, a &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated repeatedly. If false, only a single &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetTimer(byte timerId, ushort time, ZigbeeEventUnits units, bool repeat)
        {
            SetTimerRequest request = new SetTimerRequest();
            request.TimerId = timerId;
            request.Time = time;
            request.Units = units;
            request.Repeat = repeat;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetTimerResponse)));
            SetTimerResponse response = (SetTimerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets information about a timer. The Host can use this command to find out how much longer it will be before a previously set timer will generate a callback.
        /// </summary>
        /// <param name="TimerId">Which timer to get information about (0 or 1).</param>
        /// <returns>A tuple containing:
        /// - Time: The delay before the <i>timerHandler</i> callback will be generated.
        /// - Units: The units for <i>time</i>.
        /// - Repeat: True if a <i>timerHandler</i> callback will be generated repeatedly. False if only a single <i>timerHandler</i> callback will be generated.
        /// </returns>
        public (ushort Time, ZigbeeEventUnits Units, bool Repeat) GetTimer(byte timerId)
        {
            GetTimerRequest request = new GetTimerRequest();
            request.TimerId = timerId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTimerResponse)));
            GetTimerResponse response = (GetTimerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Time, response.Units, response.Repeat);
        }

        /// <summary>
        /// A callback from the timer.
        /// </summary>
        /// <returns>Which timer generated the callback (0 or 1).</returns>
        public byte TimerHandler()
        {
            TimerHandlerRequest request = new TimerHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(TimerHandlerResponse)));
            TimerHandlerResponse response = (TimerHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.TimerId;
        }

        /// <summary>
        /// Sends a debug message from the Host to the Network Analyzer utility via the NCP.
        /// </summary>
        /// <param name="BinaryMessage">true if the message should be interpreted as binary data, false if the message should be interpreted as ASCII text.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The binary message.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status DebugWrite(bool binaryMessage, byte messageLength, byte[] messageContents)
        {
            DebugWriteRequest request = new DebugWriteRequest();
            request.BinaryMessage = binaryMessage;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DebugWriteResponse)));
            DebugWriteResponse response = (DebugWriteResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Retrieves and clears Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
        /// </summary>
        /// <returns>A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.</returns>
        public ushort[] ReadAndClearCounters()
        {
            ReadAndClearCountersRequest request = new ReadAndClearCountersRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ReadAndClearCountersResponse)));
            ReadAndClearCountersResponse response = (ReadAndClearCountersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Values;
        }

        /// <summary>
        /// Retrieves Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
        /// </summary>
        /// <returns>A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.</returns>
        public ushort[] ReadCounters()
        {
            ReadCountersRequest request = new ReadCountersRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ReadCountersResponse)));
            ReadCountersResponse response = (ReadCountersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Values;
        }

        /// <summary>
        /// This call is fired when a counter exceeds its threshold
        /// </summary>
        /// <returns>Type of Counter</returns>
        public ZigbeeCounterType CounterRolloverHandler()
        {
            CounterRolloverHandlerRequest request = new CounterRolloverHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CounterRolloverHandlerResponse)));
            CounterRolloverHandlerResponse response = (CounterRolloverHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Type;
        }

        /// <summary>
        /// This call is fired when mux detects an invalid rx case, which would be different rx channels for different protocol contexts, when fast cahnnel switching is not enabled
        /// </summary>
        /// <returns>A tuple containing:
        /// - NewRxChannel: 
        /// - OldRxChannel: 
        /// </returns>
        public (byte NewRxChannel, byte OldRxChannel) MuxInvalidRxHandler()
        {
            MuxInvalidRxHandlerRequest request = new MuxInvalidRxHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MuxInvalidRxHandlerResponse)));
            MuxInvalidRxHandlerResponse response = (MuxInvalidRxHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.NewRxChannel, response.OldRxChannel);
        }

        /// <summary>
        /// Used to test that UART flow control is working correctly.
        /// </summary>
        /// <param name="Delay">Data will not be read from the host for this many milliseconds.</param>
        /// <returns>The DelayTestResponse object from the NCP</returns>
        public DelayTestResponse DelayTest(ushort delay)
        {
            DelayTestRequest request = new DelayTestRequest();
            request.Delay = delay;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DelayTestResponse)));
            DelayTestResponse response = (DelayTestResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// This retrieves the status of the passed library ID to determine if it is compiled into the stack.
        /// </summary>
        /// <param name="LibraryId">The ID of the library being queried.</param>
        /// <returns>The status of the library being queried.</returns>
        public byte GetLibraryStatus(byte libraryId)
        {
            GetLibraryStatusRequest request = new GetLibraryStatusRequest();
            request.LibraryId = libraryId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetLibraryStatusResponse)));
            GetLibraryStatusResponse response = (GetLibraryStatusResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the HOST to know whether the NCP is running the XNCP library. If so, the response contains also the manufacturer ID and the version number of the XNCP application that is running on the NCP.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the NCP is running the XNCP library. SL_STATUS_INVALID_STATE otherwise.
        /// - ManufacturerId: The manufactured ID the user has defined in the XNCP application.
        /// - VersionNumber: The version number of the XNCP application.
        /// </returns>
        public (Status Status, ushort ManufacturerId, ushort VersionNumber) GetXncpInfo()
        {
            GetXncpInfoRequest request = new GetXncpInfoRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetXncpInfoResponse)));
            GetXncpInfoResponse response = (GetXncpInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ManufacturerId, response.VersionNumber);
        }

        /// <summary>
        /// Provides the customer a custom EZSP frame. On the NCP, these frames are only handled if the XNCP library is included. On the NCP side these frames are handled in the sl_zigbee_xncp_incoming_custom_ezsp_message_cb() callback function.
        /// </summary>
        /// <param name="PayloadLength">The length of the custom frame payload (maximum 119 bytes).</param>
        /// <param name="Payload">The payload of the custom frame.</param>
        /// <returns>A tuple containing:
        /// - Status: The status returned by the custom command.
        /// - ReplyLength: The length of the response.
        /// - Reply: The response.
        /// </returns>
        public (Status Status, byte ReplyLength, byte[] Reply) CustomFrame(byte payloadLength, byte[] payload)
        {
            CustomFrameRequest request = new CustomFrameRequest();
            request.PayloadLength = payloadLength;
            request.Payload = payload;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CustomFrameResponse)));
            CustomFrameResponse response = (CustomFrameResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ReplyLength, response.Reply);
        }

        /// <summary>
        /// A callback indicating a custom EZSP message has been received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - PayloadLength: The length of the custom frame payload.
        /// - Payload: The payload of the custom frame.
        /// </returns>
        public (byte PayloadLength, byte[] Payload) CustomFrameHandler()
        {
            CustomFrameHandlerRequest request = new CustomFrameHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CustomFrameHandlerResponse)));
            CustomFrameHandlerResponse response = (CustomFrameHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.PayloadLength, response.Payload);
        }

        /// <summary>
        /// Returns the EUI64 ID of the local node.
        /// </summary>
        /// <returns>The 64-bit ID.</returns>
        public byte[] GetEui64()
        {
            GetEui64Request request = new GetEui64Request();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetEui64Response)));
            GetEui64Response response = (GetEui64Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Eui64;
        }

        /// <summary>
        /// Returns the 16-bit node ID of the local node.
        /// </summary>
        /// <returns>The 16-bit ID.</returns>
        public ushort GetNodeId()
        {
            GetNodeIdRequest request = new GetNodeIdRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetNodeIdResponse)));
            GetNodeIdResponse response = (GetNodeIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.NodeId;
        }

        /// <summary>
        /// Returns number of phy interfaces present.
        /// </summary>
        /// <returns>Value indicate how many phy interfaces present.</returns>
        public byte GetPhyInterfaceCount()
        {
            GetPhyInterfaceCountRequest request = new GetPhyInterfaceCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetPhyInterfaceCountResponse)));
            GetPhyInterfaceCountResponse response = (GetPhyInterfaceCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.InterfaceCount;
        }

        /// <summary>
        /// Returns the entropy source used for true random number generation.
        /// </summary>
        /// <returns>Value indicates the used entropy source.</returns>
        public ZigbeeEntropySource GetTrueRandomEntropySource()
        {
            GetTrueRandomEntropySourceRequest request = new GetTrueRandomEntropySourceRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTrueRandomEntropySourceResponse)));
            GetTrueRandomEntropySourceResponse response = (GetTrueRandomEntropySourceResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.EntropySource;
        }

        /// <summary>
        /// Extend a joiner&apos;s timeout to wait for the network key on the joiner default key timeout is 3 sec, and only values greater equal to 3 sec are accepted.
        /// </summary>
        /// <param name="NetworkKeyTimeoutS">Network key timeout</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetupDelayedJoin(byte networkKeyTimeoutS)
        {
            SetupDelayedJoinRequest request = new SetupDelayedJoinRequest();
            request.NetworkKeyTimeoutS = networkKeyTimeoutS;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetupDelayedJoinResponse)));
            SetupDelayedJoinResponse response = (SetupDelayedJoinResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Get the current scheduler priorities for radio operations
        /// </summary>
        /// <returns>The current priorities.</returns>
        public _802154RadioPriorities RadioGetSchedulerPriorities()
        {
            RadioGetSchedulerPrioritiesRequest request = new RadioGetSchedulerPrioritiesRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RadioGetSchedulerPrioritiesResponse)));
            RadioGetSchedulerPrioritiesResponse response = (RadioGetSchedulerPrioritiesResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Priorities;
        }

        /// <summary>
        /// Set the current scheduler priorities for radio operations
        /// </summary>
        /// <param name="Priorities">The current priorities.</param>
        /// <returns>The RadioSetSchedulerPrioritiesResponse object from the NCP</returns>
        public RadioSetSchedulerPrioritiesResponse RadioSetSchedulerPriorities(_802154RadioPriorities priorities)
        {
            RadioSetSchedulerPrioritiesRequest request = new RadioSetSchedulerPrioritiesRequest();
            request.Priorities = priorities;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RadioSetSchedulerPrioritiesResponse)));
            RadioSetSchedulerPrioritiesResponse response = (RadioSetSchedulerPrioritiesResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Get the current multiprotocol sliptime
        /// </summary>
        /// <returns>Value of the current slip time.</returns>
        public uint[] RadioGetSchedulerSliptime()
        {
            RadioGetSchedulerSliptimeRequest request = new RadioGetSchedulerSliptimeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RadioGetSchedulerSliptimeResponse)));
            RadioGetSchedulerSliptimeResponse response = (RadioGetSchedulerSliptimeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.SlipTime;
        }

        /// <summary>
        /// Set the current multiprotocol sliptime
        /// </summary>
        /// <param name="SlipTime">Value of the current slip time.</param>
        /// <returns>The RadioSetSchedulerSliptimeResponse object from the NCP</returns>
        public RadioSetSchedulerSliptimeResponse RadioSetSchedulerSliptime(uint slipTime)
        {
            RadioSetSchedulerSliptimeRequest request = new RadioSetSchedulerSliptimeRequest();
            request.SlipTime = slipTime;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RadioSetSchedulerSliptimeResponse)));
            RadioSetSchedulerSliptimeResponse response = (RadioSetSchedulerSliptimeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Check if a particular counter is one that could report from either a 2.4GHz or sub-GHz interface.
        /// </summary>
        /// <param name="Counter">The counter to be checked.</param>
        /// <returns>Whether this counter requires a PHY index when operating on a dual-PHY system.</returns>
        public bool CounterRequiresPhyIndex(ZigbeeCounterType counter)
        {
            CounterRequiresPhyIndexRequest request = new CounterRequiresPhyIndexRequest();
            request.Counter = counter;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CounterRequiresPhyIndexResponse)));
            CounterRequiresPhyIndexResponse response = (CounterRequiresPhyIndexResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Requires;
        }

        /// <summary>
        /// Check if a particular counter can report on the destination node ID they have been triggered from.
        /// </summary>
        /// <param name="Counter">The counter to be checked.</param>
        /// <returns>Whether this counter requires the destination node ID.</returns>
        public bool CounterRequiresDestinationNodeId(ZigbeeCounterType counter)
        {
            CounterRequiresDestinationNodeIdRequest request = new CounterRequiresDestinationNodeIdRequest();
            request.Counter = counter;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CounterRequiresDestinationNodeIdResponse)));
            CounterRequiresDestinationNodeIdResponse response = (CounterRequiresDestinationNodeIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Requires;
        }

        /// <summary>
        /// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
        /// </summary>
        /// <param name="Code">The manufacturer code for the local node.</param>
        /// <returns></returns>
        public Status SetManufacturerCode(ushort code)
        {
            SetManufacturerCodeRequest request = new SetManufacturerCodeRequest();
            request.Code = code;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetManufacturerCodeResponse)));
            SetManufacturerCodeResponse response = (SetManufacturerCodeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
        /// </summary>
        /// <returns>The manufacturer code for the local node.</returns>
        public ushort GetManufacturerCode()
        {
            GetManufacturerCodeRequest request = new GetManufacturerCodeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetManufacturerCodeResponse)));
            GetManufacturerCodeResponse response = (GetManufacturerCodeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Code;
        }

        /// <summary>
        /// Sets the power descriptor to the specified value. The power descriptor is a dynamic value. Therefore, you should call this function whenever the value changes.
        /// </summary>
        /// <param name="Descriptor">The new power descriptor for the local node.</param>
        /// <returns></returns>
        public Status SetPowerDescriptor(ushort descriptor)
        {
            SetPowerDescriptorRequest request = new SetPowerDescriptorRequest();
            request.Descriptor = descriptor;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetPowerDescriptorResponse)));
            SetPowerDescriptorResponse response = (SetPowerDescriptorResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Resume network operation after a reboot. The node retains its original type. This should be called on startup whether or not the node was previously part of a network. SL_STATUS_NOT_JOINED is returned if the node is not part of a network. This command accepts options to control the network initialization.
        /// </summary>
        /// <param name="NetworkInitStruct">An sl_zigbee_network_init_struct_t containing the options for initialization.</param>
        /// <returns>An sl_status_t value that indicates one of the following: successful initialization, SL_STATUS_NOT_JOINED if the node is not part of a network, or the reason for failure.</returns>
        public Status NetworkInit(ZigbeeNetworkInitStruct networkInitStruct)
        {
            NetworkInitRequest request = new NetworkInitRequest();
            request.NetworkInitStruct = networkInitStruct;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NetworkInitResponse)));
            NetworkInitResponse response = (NetworkInitResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns a value indicating whether the node is joining, joined to, or leaving a network.
        /// </summary>
        /// <returns>An sl_zigbee_network_status_t value indicating the current join status.</returns>
        public ZigbeeNetworkStatus NetworkState()
        {
            NetworkStateRequest request = new NetworkStateRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NetworkStateResponse)));
            NetworkStateResponse response = (NetworkStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked when the status of the stack changes. If the status parameter equals SL_STATUS_NETWORK_UP, then the &lt;i&gt;getNetworkParameters&lt;/i&gt; command can be called to obtain the new network parameters. If any of the parameters are being stored in nonvolatile memory by the Host, the stored values should be updated.
        /// </summary>
        /// <returns>Stack status</returns>
        public Status StackStatusHandler()
        {
            StackStatusHandlerRequest request = new StackStatusHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StackStatusHandlerResponse)));
            StackStatusHandlerResponse response = (StackStatusHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function will start a scan.
        /// </summary>
        /// <param name="ScanType">Indicates the type of scan to be performed. Possible values are: SL_ZIGBEE_EZSP_ENERGY_SCAN and SL_ZIGBEE_EZSP_ACTIVE_SCAN. For each type, the respective callback for reporting results is: energyScanResultHandler and networkFoundHandler. The energy scan and active scan report errors and completion via the scanCompleteHandler.</param>
        /// <param name="ChannelMask">Bits set as 1 indicate that this particular channel should be scanned. Bits set to 0 indicate that this particular channel should not be scanned. For example, a channelMask value of 0x00000001 would indicate that only channel 0 should be scanned. Valid channels range from 11 to 26 inclusive. This translates to a channel mask value of 0x07FFF800. As a convenience, a value of 0 is reinterpreted as the mask for the current channel.</param>
        /// <param name="Duration">Sets the exponent of the number of scan periods, where a scan period is 960 symbols. The scan will occur for ((2^duration) + 1) scan periods.</param>
        /// <returns>SL_STATUS_OK signals that the scan successfully started. Possible error responses and their meanings: SL_STATUS_MAC_SCANNING, we are already scanning; SL_STATUS_BAD_SCAN_DURATION, we have set a duration value that is not 0..14 inclusive; SL_STATUS_MAC_INCORRECT_SCAN_TYPE, we have requested an undefined scanning type; SL_STATUS_INVALID_CHANNEL_MASK, our channel mask did not specify any valid channels.</returns>
        public Status StartScan(ZigbeeEzspNetworkScanType scanType, uint channelMask, byte duration)
        {
            StartScanRequest request = new StartScanRequest();
            request.ScanType = scanType;
            request.ChannelMask = channelMask;
            request.Duration = duration;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StartScanResponse)));
            StartScanResponse response = (StartScanResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Reports the result of an energy scan for a single channel. The scan is not complete until the &lt;i&gt;scanCompleteHandler&lt;/i&gt; callback is called.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Channel: The 802.15.4 channel number that was scanned.
        /// - MaxRssiValue: The maximum RSSI value found on the channel.
        /// </returns>
        public (byte Channel, sbyte MaxRssiValue) EnergyScanResultHandler()
        {
            EnergyScanResultHandlerRequest request = new EnergyScanResultHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(EnergyScanResultHandlerResponse)));
            EnergyScanResultHandlerResponse response = (EnergyScanResultHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Channel, response.MaxRssiValue);
        }

        /// <summary>
        /// Reports that a network was found as a result of a prior call to startScan. Gives the network parameters useful for deciding which network to join.
        /// </summary>
        /// <returns>A tuple containing:
        /// - NetworkFound: The parameters associated with the network found.
        /// - LastHopLqi: Link quality of incoming packet from network.
        /// - LastHopRssi: Power (in dBm) of incoming packet.
        /// </returns>
        public (ZigbeeZigbeeNetwork NetworkFound, byte LastHopLqi, sbyte LastHopRssi) NetworkFoundHandler()
        {
            NetworkFoundHandlerRequest request = new NetworkFoundHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NetworkFoundHandlerResponse)));
            NetworkFoundHandlerResponse response = (NetworkFoundHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.NetworkFound, response.LastHopLqi, response.LastHopRssi);
        }

        /// <summary>
        /// Returns the status of the current scan of type SL_ZIGBEE_EZSP_ENERGY_SCAN or SL_ZIGBEE_EZSP_ACTIVE_SCAN. SL_STATUS_OK signals that the scan has completed. Other error conditions signify a failure to scan on the channel specified.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Channel: The channel on which the current error occurred. Undefined for the case of SL_STATUS_OK.
        /// - Status: The error condition that occurred on the current channel. Value will be SL_STATUS_OK when the scan has completed.
        /// </returns>
        public (byte Channel, Status Status) ScanCompleteHandler()
        {
            ScanCompleteHandlerRequest request = new ScanCompleteHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ScanCompleteHandlerResponse)));
            ScanCompleteHandlerResponse response = (ScanCompleteHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Channel, response.Status);
        }

        /// <summary>
        /// This function returns an unused panID and channel pair found via the find unused panId scan procedure.
        /// </summary>
        /// <returns>A tuple containing:
        /// - PanId: The unused panID which has been found.
        /// - Channel: The channel that the unused panID was found on.
        /// </returns>
        public (ushort PanId, byte Channel) UnusedPanIdFoundHandler()
        {
            UnusedPanIdFoundHandlerRequest request = new UnusedPanIdFoundHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(UnusedPanIdFoundHandlerResponse)));
            UnusedPanIdFoundHandlerResponse response = (UnusedPanIdFoundHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.PanId, response.Channel);
        }

        /// <summary>
        /// This function starts a series of scans which will return an available panId.
        /// </summary>
        /// <param name="ChannelMask">The channels that will be scanned for available panIds.</param>
        /// <param name="Duration">The duration of the procedure.</param>
        /// <returns>The error condition that occurred during the scan. Value will be SL_STATUS_OK if there are no errors.</returns>
        public Status FindUnusedPanId(uint channelMask, byte duration)
        {
            FindUnusedPanIdRequest request = new FindUnusedPanIdRequest();
            request.ChannelMask = channelMask;
            request.Duration = duration;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(FindUnusedPanIdResponse)));
            FindUnusedPanIdResponse response = (FindUnusedPanIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Terminates a scan in progress.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status StopScan()
        {
            StopScanRequest request = new StopScanRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StopScanResponse)));
            StopScanResponse response = (StopScanResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Forms a new network by becoming the coordinator.
        /// </summary>
        /// <param name="Parameters">Specification of the new network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status FormNetwork(ZigbeeNetworkParameters parameters)
        {
            FormNetworkRequest request = new FormNetworkRequest();
            request.Parameters = parameters;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(FormNetworkResponse)));
            FormNetworkResponse response = (FormNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Causes the stack to associate with the network using the specified network parameters. It can take several seconds for the stack to associate with the local network. Do not send messages until the &lt;i&gt;stackStatusHandler&lt;/i&gt; callback informs you that the stack is up.
        /// </summary>
        /// <param name="NodeType">Specification of the role that this node will have in the network. This role must not be SL_ZIGBEE_COORDINATOR. To be a coordinator, use the &lt;i&gt;formNetwork&lt;/i&gt; command.</param>
        /// <param name="Parameters">Specification of the network with which the node should associate.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status JoinNetwork(ZigbeeNodeType nodeType, ZigbeeNetworkParameters parameters)
        {
            JoinNetworkRequest request = new JoinNetworkRequest();
            request.NodeType = nodeType;
            request.Parameters = parameters;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(JoinNetworkResponse)));
            JoinNetworkResponse response = (JoinNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Causes the stack to associate with the network using the specified network parameters in the beacon parameter. It can take several seconds for the stack to associate with the local network. Do not send messages until the &lt;i&gt;stackStatusHandler&lt;/i&gt; callback informs you that the stack is up. Unlike ::sli_zigbee_stack_join_network(), this function does not issue an active scan before joining. Instead, it will cause the local node to issue a MAC Association Request directly to the specified target node. It is assumed that the beacon parameter is an artifact after issuing an active scan. (For more information, see &lt;i&gt;sli_zigbee_stack_get_stored_beacon&lt;/i&gt;.)
        /// </summary>
        /// <param name="LocalNodeType">Specifies the role that this node will have in the network. This role must not be SL_ZIGBEE_COORDINATOR. To be a coordinator, use the &lt;i&gt;formNetwork&lt;/i&gt; command.</param>
        /// <param name="Beacon">Specifies the network with which the node should associate.</param>
        /// <param name="RadioTxPower">The radio transmit power to use, specified in dBm.</param>
        /// <param name="ClearBeaconsAfterNetworkUp">If true, clear beacons in cache upon join success. If join fail, do nothing.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status JoinNetworkDirectly(ZigbeeNodeType localNodeType, ZigbeeBeaconData beacon, sbyte radioTxPower, bool clearBeaconsAfterNetworkUp)
        {
            JoinNetworkDirectlyRequest request = new JoinNetworkDirectlyRequest();
            request.LocalNodeType = localNodeType;
            request.Beacon = beacon;
            request.RadioTxPower = radioTxPower;
            request.ClearBeaconsAfterNetworkUp = clearBeaconsAfterNetworkUp;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(JoinNetworkDirectlyResponse)));
            JoinNetworkDirectlyResponse response = (JoinNetworkDirectlyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Causes the stack to leave the current network. This generates a &lt;i&gt;stackStatusHandler&lt;/i&gt; callback to indicate that the network is down. The radio will not be used until after sending a &lt;i&gt;formNetwork&lt;/i&gt; or &lt;i&gt;joinNetwork&lt;/i&gt; command.
        /// </summary>
        /// <param name="Options">This parameter gives options when leave network</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status LeaveNetwork(ZigbeeLeaveNetworkOption options)
        {
            LeaveNetworkRequest request = new LeaveNetworkRequest();
            request.Options = options;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(LeaveNetworkResponse)));
            LeaveNetworkResponse response = (LeaveNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// The application may call this function when contact with the network has been lost. The most common usage case is when an end device can no longer communicate with its parent and wishes to find a new one. Another case is when a device has missed a Network Key update and no longer has the current Network Key. &lt;p&gt; The stack will call &lt;i&gt;sl_zigbee_ezsp_stack_status_handler&lt;/i&gt; to indicate that the network is down, then try to re-establish contact with the network by performing an active scan, choosing a network with matching extended pan id, and sending a ZigBee network rejoin request. A second call to the &lt;i&gt;sl_zigbee_ezsp_stack_status_handler&lt;/i&gt; callback indicates either the success or the failure of the attempt. The process takes approximately 150 milliseconds per channel to complete. &lt;p&gt;
        /// </summary>
        /// <param name="HaveCurrentNetworkKey">This parameter tells the stack whether to try to use the current network key. If it has the current network key it will perform a secure rejoin (encrypted). If this fails the device should try an unsecure rejoin. If the Trust Center allows the rejoin then the current Network Key will be sent encrypted using the device&apos;s Link Key.</param>
        /// <param name="ChannelMask">A mask indicating the channels to be scanned. See &lt;i&gt;sli_zigbee_stack_start_scan&lt;/i&gt; for format details. A value of 0 is reinterpreted as the mask for the current channel.</param>
        /// <param name="Reason">A sl_zigbee_rejoin_reason_t variable which could be passed in if there is actually a reason for rejoin, or could be left at 0xFF</param>
        /// <param name="NodeType">The rejoin could be triggered with a different nodeType. This value could be set to 0 or SL_ZIGBEE_DEVICE_TYPE_UNCHANGED if not needed.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status FindAndRejoinNetwork(bool haveCurrentNetworkKey, uint channelMask, byte reason, byte nodeType)
        {
            FindAndRejoinNetworkRequest request = new FindAndRejoinNetworkRequest();
            request.HaveCurrentNetworkKey = haveCurrentNetworkKey;
            request.ChannelMask = channelMask;
            request.Reason = reason;
            request.NodeType = nodeType;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(FindAndRejoinNetworkResponse)));
            FindAndRejoinNetworkResponse response = (FindAndRejoinNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Tells the stack to allow other nodes to join the network with this node as their parent. Joining is initially disabled by default.
        /// </summary>
        /// <param name="Duration">A value of 0x00 disables joining. A value of 0xFF enables joining. Any other value enables joining for that number of seconds.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status PermitJoining(byte duration)
        {
            PermitJoiningRequest request = new PermitJoiningRequest();
            request.Duration = duration;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(PermitJoiningResponse)));
            PermitJoiningResponse response = (PermitJoiningResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates that a child has joined or left.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Index: The index of the child of interest.
        /// - Joining: True if the child is joining. False the child is leaving.
        /// - ChildId: The node ID of the child.
        /// - ChildEui64: The EUI64 of the child.
        /// - ChildType: The node type of the child.
        /// </returns>
        public (byte Index, bool Joining, ushort ChildId, byte[] ChildEui64, ZigbeeNodeType ChildType) ChildJoinHandler()
        {
            ChildJoinHandlerRequest request = new ChildJoinHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ChildJoinHandlerResponse)));
            ChildJoinHandlerResponse response = (ChildJoinHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Index, response.Joining, response.ChildId, response.ChildEui64, response.ChildType);
        }

        /// <summary>
        /// Sends a ZDO energy scan request. This request may only be sent by the current network manager and must be unicast, not broadcast. See ezsp-utils.h for related macros sli_zigbee_stack_set_network_manager_request() and sl_zigbee_change_channel_request().
        /// </summary>
        /// <param name="Target">The network address of the node to perform the scan.</param>
        /// <param name="ScanChannels">A mask of the channels to be scanned</param>
        /// <param name="ScanDuration">How long to scan on each channel. Allowed values are 0..5, with the scan times as specified by 802.15.4 (0 = 31ms, 1 = 46ms, 2 = 77ms, 3 = 138ms, 4 = 261ms, 5 = 507ms).</param>
        /// <param name="ScanCount">The number of scans to be performed on each channel (1..8).</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status EnergyScanRequest(ushort target, uint scanChannels, byte scanDuration, ushort scanCount)
        {
            EnergyScanRequestRequest request = new EnergyScanRequestRequest();
            request.Target = target;
            request.ScanChannels = scanChannels;
            request.ScanDuration = scanDuration;
            request.ScanCount = scanCount;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(EnergyScanRequestResponse)));
            EnergyScanRequestResponse response = (EnergyScanRequestResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns the current network parameters.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - NodeType: An sl_zigbee_node_type_t value indicating the current node type.
        /// - Parameters: The current network parameters.
        /// </returns>
        public (Status Status, ZigbeeNodeType NodeType, ZigbeeNetworkParameters Parameters) GetNetworkParameters()
        {
            GetNetworkParametersRequest request = new GetNetworkParametersRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetNetworkParametersResponse)));
            GetNetworkParametersResponse response = (GetNetworkParametersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.NodeType, response.Parameters);
        }

        /// <summary>
        /// Returns the current radio parameters based on phy index.
        /// </summary>
        /// <param name="PhyIndex">Desired index of phy interface for radio parameters.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Parameters: The current radio parameters based on provided phy index.
        /// </returns>
        public (Status Status, ZigbeeMultiPhyRadioParameters Parameters) GetRadioParameters(byte phyIndex)
        {
            GetRadioParametersRequest request = new GetRadioParametersRequest();
            request.PhyIndex = phyIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetRadioParametersResponse)));
            GetRadioParametersResponse response = (GetRadioParametersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Parameters);
        }

        /// <summary>
        /// Returns information about the children of the local node and the parent of the local node.
        /// </summary>
        /// <returns>A tuple containing:
        /// - ChildCount: The number of children the node currently has.
        /// - ParentEui64: The parent's EUI64. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).
        /// - ParentNodeId: The parent's node ID. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).
        /// </returns>
        public (byte ChildCount, byte[] ParentEui64, ushort ParentNodeId) GetParentChildParameters()
        {
            GetParentChildParametersRequest request = new GetParentChildParametersRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetParentChildParametersResponse)));
            GetParentChildParametersResponse response = (GetParentChildParametersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.ChildCount, response.ParentEui64, response.ParentNodeId);
        }

        /// <summary>
        /// Return the number of router children that the node currently has.
        /// </summary>
        /// <returns>The number of router children.</returns>
        public byte RouterChildCount()
        {
            RouterChildCountRequest request = new RouterChildCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RouterChildCountResponse)));
            RouterChildCountResponse response = (RouterChildCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.RouterChildCount;
        }

        /// <summary>
        /// Return the maximum number of children for this node. The return value is undefined for nodes that are not joined to a network.
        /// </summary>
        /// <returns>The maximum number of children.</returns>
        public byte MaxChildCount()
        {
            MaxChildCountRequest request = new MaxChildCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MaxChildCountResponse)));
            MaxChildCountResponse response = (MaxChildCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.MaxChildCount;
        }

        /// <summary>
        /// Return the maximum number of router children for this node. The return value is undefined for nodes that are not joined to a network.
        /// </summary>
        /// <returns>The maximum number of router children.</returns>
        public byte MaxRouterChildCount()
        {
            MaxRouterChildCountRequest request = new MaxRouterChildCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MaxRouterChildCountResponse)));
            MaxRouterChildCountResponse response = (MaxRouterChildCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.MaxRouterChildCount;
        }

        public uint GetParentIncomingNwkFrameCounter()
        {
            GetParentIncomingNwkFrameCounterRequest request = new GetParentIncomingNwkFrameCounterRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetParentIncomingNwkFrameCounterResponse)));
            GetParentIncomingNwkFrameCounterResponse response = (GetParentIncomingNwkFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ParentIncomingNwkFrameCounter;
        }

        public Status SetParentIncomingNwkFrameCounter(uint value)
        {
            SetParentIncomingNwkFrameCounterRequest request = new SetParentIncomingNwkFrameCounterRequest();
            request.Value = value;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetParentIncomingNwkFrameCounterResponse)));
            SetParentIncomingNwkFrameCounterResponse response = (SetParentIncomingNwkFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Return a bitmask indicating the stack&apos;s current tasks. The mask ::SL_ZIGBEE_HIGH_PRIORITY_TASKS defines which tasks are high priority.  Devices should not sleep if any high priority tasks are active. Active tasks that are not high priority are waiting for messages to arrive from other devices.  If there are active tasks, but no high priority ones, the device may sleep but should periodically wake up and call ::emberPollForData() in order to receive messages.  Parents will hold messages for ::SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT milliseconds before discarding them.
        /// </summary>
        /// <returns>A bitmask of the stack&apos;s active tasks.</returns>
        public ushort CurrentStackTasks()
        {
            CurrentStackTasksRequest request = new CurrentStackTasksRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CurrentStackTasksResponse)));
            CurrentStackTasksResponse response = (CurrentStackTasksResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ActiveTasks;
        }

        /// <summary>
        /// Indicate whether the stack is currently in a state where there are no high-priority tasks, allowing the device to sleep.
        /// There may be tasks expecting incoming messages, in which case the device should periodically wake up and call ::emberPollForData() in order to receive messages. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE
        /// </summary>
        /// <returns>True if the application may sleep but the stack may be expecting incoming messages.</returns>
        public bool OkToNap()
        {
            OkToNapRequest request = new OkToNapRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(OkToNapResponse)));
            OkToNapResponse response = (OkToNapResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Value;
        }

        /// <summary>
        /// Indicate whether the parent token has been set by association.
        /// </summary>
        /// <returns>True if the parent token has been set.</returns>
        public bool ParentTokenSet()
        {
            ParentTokenSetRequest request = new ParentTokenSetRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ParentTokenSetResponse)));
            ParentTokenSetResponse response = (ParentTokenSetResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Indicate whether the stack currently has any tasks pending. If no tasks are pending, ::emberTick() does not need to be called until the next time a stack API function is called. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE.
        /// </summary>
        /// <returns>True if the application may sleep for as long as it wishes.</returns>
        public bool OkToHibernate()
        {
            OkToHibernateRequest request = new OkToHibernateRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(OkToHibernateResponse)));
            OkToHibernateResponse response = (OkToHibernateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Indicate whether the stack is currently in a state that does not require the application to periodically poll.
        /// </summary>
        /// <returns>True if the device may poll less frequently.</returns>
        public bool OkToLongPoll()
        {
            OkToLongPollRequest request = new OkToLongPollRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(OkToLongPollResponse)));
            OkToLongPollResponse response = (OkToLongPollResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Calling this function will render all other stack functions except sli_zigbee_stack_stack_power_up() non-functional until the radio is powered back on.
        /// </summary>
        /// <returns>The StackPowerDownResponse object from the NCP</returns>
        public StackPowerDownResponse StackPowerDown()
        {
            StackPowerDownRequest request = new StackPowerDownRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StackPowerDownResponse)));
            StackPowerDownResponse response = (StackPowerDownResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Initialize the radio.  Typically called coming out of deep sleep. For non-sleepy devices, also turns the radio on and leaves it in RX mode.
        /// </summary>
        /// <returns>The StackPowerUpResponse object from the NCP</returns>
        public StackPowerUpResponse StackPowerUp()
        {
            StackPowerUpRequest request = new StackPowerUpRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(StackPowerUpResponse)));
            StackPowerUpResponse response = (StackPowerUpResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Returns information about a child of the local node.
        /// </summary>
        /// <param name="Index">The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if there is a child at <i>index</i>. SL_STATUS_NOT_JOINED if there is no child at <i>index</i>.
        /// - ChildData: The data of the child.
        /// </returns>
        public (Status Status, ZigbeeChildData ChildData) GetChildData(byte index)
        {
            GetChildDataRequest request = new GetChildDataRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetChildDataResponse)));
            GetChildDataResponse response = (GetChildDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ChildData);
        }

        /// <summary>
        /// Sets child data to the child table token.
        /// </summary>
        /// <param name="Index">The index of the child of interest in the child table. Possible indexes range from zero to (SL_ZIGBEE_CHILD_TABLE_SIZE - 1).</param>
        /// <param name="ChildData">The data of the child.</param>
        /// <returns>SL_STATUS_OK if the child data is set successfully at &lt;i&gt;index&lt;/i&gt;. SL_STATUS_INVALID_INDEX if provided &lt;i&gt;index&lt;/i&gt; is out of range.</returns>
        public Status SetChildData(byte index, ZigbeeChildData childData)
        {
            SetChildDataRequest request = new SetChildDataRequest();
            request.Index = index;
            request.ChildData = childData;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetChildDataResponse)));
            SetChildDataResponse response = (SetChildDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Convert a child index to a node ID
        /// </summary>
        /// <param name="ChildIndex">The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.</param>
        /// <returns>The node ID of the child or SL_ZIGBEE_NULL_NODE_ID if there isn&apos;t a child at the childIndex specified</returns>
        public ushort ChildId(byte childIndex)
        {
            ChildIdRequest request = new ChildIdRequest();
            request.ChildIndex = childIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ChildIdResponse)));
            ChildIdResponse response = (ChildIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ChildId;
        }

        /// <summary>
        /// Return radio power value of the child from the given childIndex
        /// </summary>
        /// <param name="ChildIndex">The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.</param>
        /// <returns>The power of the child or maximum radio power, which is the power value provided by the user while forming/joining a network if there isn&apos;t a child at the childIndex specified</returns>
        public sbyte ChildPower(byte childIndex)
        {
            ChildPowerRequest request = new ChildPowerRequest();
            request.ChildIndex = childIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ChildPowerResponse)));
            ChildPowerResponse response = (ChildPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ChildPower;
        }

        /// <summary>
        /// Set the radio power value for a given child index.
        /// </summary>
        /// <param name="ChildIndex">The index.</param>
        /// <param name="NewPower">The new power value.</param>
        /// <returns>The SetChildPowerResponse object from the NCP</returns>
        public SetChildPowerResponse SetChildPower(byte childIndex, sbyte newPower)
        {
            SetChildPowerRequest request = new SetChildPowerRequest();
            request.ChildIndex = childIndex;
            request.NewPower = newPower;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetChildPowerResponse)));
            SetChildPowerResponse response = (SetChildPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Convert a node ID to a child index
        /// </summary>
        /// <param name="ChildId">The node ID of the child</param>
        /// <returns>The child index or 0xFF if the node ID doesn&apos;t belong to a child</returns>
        public byte ChildIndex(ushort childId)
        {
            ChildIndexRequest request = new ChildIndexRequest();
            request.ChildId = childId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ChildIndexResponse)));
            ChildIndexResponse response = (ChildIndexResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ChildIndex;
        }

        /// <summary>
        /// Returns the source route table total size.
        /// </summary>
        /// <returns>Total size of source route table.</returns>
        public byte GetSourceRouteTableTotalSize()
        {
            GetSourceRouteTableTotalSizeRequest request = new GetSourceRouteTableTotalSizeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetSourceRouteTableTotalSizeResponse)));
            GetSourceRouteTableTotalSizeResponse response = (GetSourceRouteTableTotalSizeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.SourceRouteTableTotalSize;
        }

        /// <summary>
        /// Returns the number of filled entries in source route table.
        /// </summary>
        /// <returns>The number of filled entries in source route table.</returns>
        public byte GetSourceRouteTableFilledSize()
        {
            GetSourceRouteTableFilledSizeRequest request = new GetSourceRouteTableFilledSizeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetSourceRouteTableFilledSizeResponse)));
            GetSourceRouteTableFilledSizeResponse response = (GetSourceRouteTableFilledSizeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.SourceRouteTableFilledSize;
        }

        /// <summary>
        /// Returns information about a source route table entry
        /// </summary>
        /// <param name="Index">
        /// The index of the entry of interest in the
        /// source route table. Possible indexes range from zero to
        /// SOURCE_ROUTE_TABLE_FILLED_SIZE.
        /// </param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if there is source route entry at
        /// <i>index</i>. SL_STATUS_NOT_FOUND if there is no
        /// source route at <i>index</i>.
        /// - Destination: The node ID of the destination in that entry.
        /// - CloserIndex: The closer node index for this source route table entry
        /// </returns>
        public (Status Status, ushort Destination, byte CloserIndex) GetSourceRouteTableEntry(byte index)
        {
            GetSourceRouteTableEntryRequest request = new GetSourceRouteTableEntryRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetSourceRouteTableEntryResponse)));
            GetSourceRouteTableEntryResponse response = (GetSourceRouteTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Destination, response.CloserIndex);
        }

        /// <summary>
        /// Returns the neighbor table entry at the given index. The number of active neighbors can be obtained using the neighborCount command.
        /// </summary>
        /// <param name="Index">The index of the neighbor of interest. Neighbors are stored in ascending order by node id, with all unused entries at the end of the table.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_FAIL if the index is greater or equal to the number of active neighbors, or if the device is an end device. Returns SL_STATUS_OK otherwise.
        /// - Value: The contents of the neighbor table entry.
        /// </returns>
        public (Status Status, ZigbeeNeighborTableEntry Value) GetNeighbor(byte index)
        {
            GetNeighborRequest request = new GetNeighborRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetNeighborResponse)));
            GetNeighborResponse response = (GetNeighborResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Return sl_status_t depending on whether the frame counter of the node is found in the neighbor or child table. This function gets the last received frame counter as found in the Network Auxiliary header for the specified neighbor or child
        /// </summary>
        /// <param name="Eui64">eui64 of the node</param>
        /// <returns>A tuple containing:
        /// - Status: Return SL_STATUS_NOT_FOUND if the node is not found in the neighbor or child table. Returns SL_STATUS_OK otherwise
        /// - ReturnFrameCounter: Return the frame counter of the node from the neighbor or child table
        /// </returns>
        public (Status Status, uint ReturnFrameCounter) GetNeighborFrameCounter(byte[] eui64)
        {
            GetNeighborFrameCounterRequest request = new GetNeighborFrameCounterRequest();
            request.Eui64 = eui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetNeighborFrameCounterResponse)));
            GetNeighborFrameCounterResponse response = (GetNeighborFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ReturnFrameCounter);
        }

        /// <summary>
        /// Sets the frame counter for the neighbour or child.
        /// </summary>
        /// <param name="Eui64">eui64 of the node</param>
        /// <param name="FrameCounter">Return the frame counter of the node from the neighbor or child table</param>
        /// <returns>Return SL_STATUS_NOT_FOUND if the node is not found in the neighbor or child table. Returns SL_STATUS_OK otherwise</returns>
        public Status SetNeighborFrameCounter(byte[] eui64, uint frameCounter)
        {
            SetNeighborFrameCounterRequest request = new SetNeighborFrameCounterRequest();
            request.Eui64 = eui64;
            request.FrameCounter = frameCounter;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetNeighborFrameCounterResponse)));
            SetNeighborFrameCounterResponse response = (SetNeighborFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the routing shortcut threshold to directly use a neighbor instead of performing routing.
        /// </summary>
        /// <param name="CostThresh">The routing shortcut threshold to configure.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetRoutingShortcutThreshold(byte costThresh)
        {
            SetRoutingShortcutThresholdRequest request = new SetRoutingShortcutThresholdRequest();
            request.CostThresh = costThresh;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetRoutingShortcutThresholdResponse)));
            SetRoutingShortcutThresholdResponse response = (SetRoutingShortcutThresholdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the routing shortcut threshold used to differentiate between directly using a neighbor vs. performing routing.
        /// </summary>
        /// <returns>The routing shortcut threshold</returns>
        public byte GetRoutingShortcutThreshold()
        {
            GetRoutingShortcutThresholdRequest request = new GetRoutingShortcutThresholdRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetRoutingShortcutThresholdResponse)));
            GetRoutingShortcutThresholdResponse response = (GetRoutingShortcutThresholdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.RoutingShortcutThresh;
        }

        /// <summary>
        /// Returns the number of active entries in the neighbor table.
        /// </summary>
        /// <returns>The number of active entries in the neighbor table.</returns>
        public byte NeighborCount()
        {
            NeighborCountRequest request = new NeighborCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(NeighborCountResponse)));
            NeighborCountResponse response = (NeighborCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Value;
        }

        /// <summary>
        /// Returns the route table entry at the given index. The route table size can be obtained using the getConfigurationValue command.
        /// </summary>
        /// <param name="Index">The index of the route table entry of interest.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_FAIL if the index is out of range or the device is an end device, and SL_STATUS_OK otherwise.
        /// - Value: The contents of the route table entry.
        /// </returns>
        public (Status Status, ZigbeeRouteTableEntry Value) GetRouteTableEntry(byte index)
        {
            GetRouteTableEntryRequest request = new GetRouteTableEntryRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetRouteTableEntryResponse)));
            GetRouteTableEntryResponse response = (GetRouteTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Sets the radio output power at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
        /// </summary>
        /// <param name="Power">Desired radio output power, in dBm.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status SetRadioPower(sbyte power)
        {
            SetRadioPowerRequest request = new SetRadioPowerRequest();
            request.Power = power;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetRadioPowerResponse)));
            SetRadioPowerResponse response = (SetRadioPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the channel to use for sending and receiving messages. For a list of available radio channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same channel.
        /// </summary>
        /// <param name="Channel">Desired radio channel.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status SetRadioChannel(byte channel)
        {
            SetRadioChannelRequest request = new SetRadioChannelRequest();
            request.Channel = channel;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetRadioChannelResponse)));
            SetRadioChannelResponse response = (SetRadioChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the channel in use for sending and receiving messages.
        /// </summary>
        /// <returns>Current radio channel.</returns>
        public byte GetRadioChannel()
        {
            GetRadioChannelRequest request = new GetRadioChannelRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetRadioChannelResponse)));
            GetRadioChannelResponse response = (GetRadioChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Channel;
        }

        /// <summary>
        /// Set the configured 802.15.4 CCA mode in the radio.
        /// </summary>
        /// <param name="CcaMode">A RAIL_IEEE802154_CcaMode_t value.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status SetRadioIeee802154CcaMode(byte ccaMode)
        {
            SetRadioIeee802154CcaModeRequest request = new SetRadioIeee802154CcaModeRequest();
            request.CcaMode = ccaMode;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetRadioIeee802154CcaModeResponse)));
            SetRadioIeee802154CcaModeResponse response = (SetRadioIeee802154CcaModeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Enable/disable concentrator support.
        /// </summary>
        /// <param name="On">If this bool is true the concentrator support is enabled. Otherwise is disabled. If this bool is false all the other arguments are ignored.</param>
        /// <param name="ConcentratorType">Must be either SL_ZIGBEE_HIGH_RAM_CONCENTRATOR or SL_ZIGBEE_LOW_RAM_CONCENTRATOR. The former is used when the caller has enough memory to store source routes for the whole network. In that case, remote nodes stop sending route records once the concentrator has successfully received one. The latter is used when the concentrator has insufficient RAM to store all outbound source routes. In that case, route records are sent to the concentrator prior to every inbound APS unicast.</param>
        /// <param name="MinTime">The minimum amount of time that must pass between MTORR broadcasts.</param>
        /// <param name="MaxTime">The maximum amount of time that can pass between MTORR broadcasts.</param>
        /// <param name="RouteErrorThreshold">The number of route errors that will trigger a re-broadcast of the MTORR.</param>
        /// <param name="DeliveryFailureThreshold">The number of APS delivery failures that will trigger a re-broadcast of the MTORR.</param>
        /// <param name="MaxHops">The maximum number of hops that the MTORR broadcast will be allowed to have. A value of 0 will be converted to the SL_ZIGBEE_MAX_HOPS value set by the stack.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetConcentrator(bool on, ushort concentratorType, ushort minTime, ushort maxTime, byte routeErrorThreshold, byte deliveryFailureThreshold, byte maxHops)
        {
            SetConcentratorRequest request = new SetConcentratorRequest();
            request.On = on;
            request.ConcentratorType = concentratorType;
            request.MinTime = minTime;
            request.MaxTime = maxTime;
            request.RouteErrorThreshold = routeErrorThreshold;
            request.DeliveryFailureThreshold = deliveryFailureThreshold;
            request.MaxHops = maxHops;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetConcentratorResponse)));
            SetConcentratorResponse response = (SetConcentratorResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts periodic many-to-one route discovery. Periodic discovery is started by default on bootup, but this function may be used if discovery has been stopped by a call to ::emberConcentratorStopDiscovery().
        /// </summary>
        /// <returns>The ConcentratorStartDiscoveryResponse object from the NCP</returns>
        public ConcentratorStartDiscoveryResponse ConcentratorStartDiscovery()
        {
            ConcentratorStartDiscoveryRequest request = new ConcentratorStartDiscoveryRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ConcentratorStartDiscoveryResponse)));
            ConcentratorStartDiscoveryResponse response = (ConcentratorStartDiscoveryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Stops periodic many-to-one route discovery.
        /// </summary>
        /// <returns>The ConcentratorStopDiscoveryResponse object from the NCP</returns>
        public ConcentratorStopDiscoveryResponse ConcentratorStopDiscovery()
        {
            ConcentratorStopDiscoveryRequest request = new ConcentratorStopDiscoveryRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ConcentratorStopDiscoveryResponse)));
            ConcentratorStopDiscoveryResponse response = (ConcentratorStopDiscoveryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Notes when a route error has occurred.
        /// </summary>
        /// <param name="Status">
        /// </param>
        /// <param name="NodeId">
        /// </param>
        /// <returns>The ConcentratorNoteRouteErrorResponse object from the NCP</returns>
        public ConcentratorNoteRouteErrorResponse ConcentratorNoteRouteError(Status status, ushort nodeId)
        {
            ConcentratorNoteRouteErrorRequest request = new ConcentratorNoteRouteErrorRequest();
            request.Status = status;
            request.NodeId = nodeId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ConcentratorNoteRouteErrorResponse)));
            ConcentratorNoteRouteErrorResponse response = (ConcentratorNoteRouteErrorResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Sets the error code that is sent back from a router with a broken route. 
        /// </summary>
        /// <param name="ErrorCode">Desired error code.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status SetBrokenRouteErrorCode(byte errorCode)
        {
            SetBrokenRouteErrorCodeRequest request = new SetBrokenRouteErrorCodeRequest();
            request.ErrorCode = errorCode;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetBrokenRouteErrorCodeResponse)));
            SetBrokenRouteErrorCodeResponse response = (SetBrokenRouteErrorCodeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This causes to initialize the desired radio interface other than native and form a new network by becoming the coordinator with same panId as native radio network.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <param name="Page">Desired radio channel page.</param>
        /// <param name="Channel">Desired radio channel.</param>
        /// <param name="Power">Desired radio output power, in dBm.</param>
        /// <param name="Bitmask">Network configuration bitmask.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MultiPhyStart(byte phyIndex, byte page, byte channel, sbyte power, ZigbeeMultiPhyNwkConfig bitmask)
        {
            MultiPhyStartRequest request = new MultiPhyStartRequest();
            request.PhyIndex = phyIndex;
            request.Page = page;
            request.Channel = channel;
            request.Power = power;
            request.Bitmask = bitmask;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MultiPhyStartResponse)));
            MultiPhyStartResponse response = (MultiPhyStartResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This causes to bring down the radio interface other than native.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MultiPhyStop(byte phyIndex)
        {
            MultiPhyStopRequest request = new MultiPhyStopRequest();
            request.PhyIndex = phyIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MultiPhyStopResponse)));
            MultiPhyStopResponse response = (MultiPhyStopResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the radio output power for desired phy interface at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this api on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <param name="Power">Desired radio output power, in dBm.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status MultiPhySetRadioPower(byte phyIndex, sbyte power)
        {
            MultiPhySetRadioPowerRequest request = new MultiPhySetRadioPowerRequest();
            request.PhyIndex = phyIndex;
            request.Power = power;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MultiPhySetRadioPowerResponse)));
            MultiPhySetRadioPowerResponse response = (MultiPhySetRadioPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send Link Power Delta Request from a child to its parent
        /// </summary>
        /// <returns>An sl_status_t value indicating the success or failure of sending the request.</returns>
        public Status SendLinkPowerDeltaRequest()
        {
            SendLinkPowerDeltaRequestRequest request = new SendLinkPowerDeltaRequestRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendLinkPowerDeltaRequestResponse)));
            SendLinkPowerDeltaRequestResponse response = (SendLinkPowerDeltaRequestResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the channel for desired phy interface to use for sending and receiving messages. For a list of available radio pages and channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same page and channel.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <param name="Page">Desired radio channel page.</param>
        /// <param name="Channel">Desired radio channel.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public Status MultiPhySetRadioChannel(byte phyIndex, byte page, byte channel)
        {
            MultiPhySetRadioChannelRequest request = new MultiPhySetRadioChannelRequest();
            request.PhyIndex = phyIndex;
            request.Page = page;
            request.Channel = channel;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MultiPhySetRadioChannelResponse)));
            MultiPhySetRadioChannelResponse response = (MultiPhySetRadioChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Obtains the current duty cycle state.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating the success or failure of the command.
        /// - ReturnedState: The current duty cycle state in effect.
        /// </returns>
        public (Status Status, ZigbeeDutyCycleState ReturnedState) GetDutyCycleState()
        {
            GetDutyCycleStateRequest request = new GetDutyCycleStateRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetDutyCycleStateResponse)));
            GetDutyCycleStateResponse response = (GetDutyCycleStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ReturnedState);
        }

        /// <summary>
        /// Set the current duty cycle limits configuration. The Default limits set by stack if this call is not made.
        /// </summary>
        /// <param name="Limits">The duty cycle limits configuration to utilize.</param>
        /// <returns>SL_STATUS_OK  if the duty cycle limit configurations set successfully, SL_STATUS_INVALID_PARAMETER if set illegal value such as setting only one of the limits to default or violates constraints Susp &gt; Crit &gt; Limi, SL_STATUS_INVALID_STATE if device is operating on 2.4Ghz</returns>
        public Status SetDutyCycleLimitsInStack(ZigbeeDutyCycleLimits limits)
        {
            SetDutyCycleLimitsInStackRequest request = new SetDutyCycleLimitsInStackRequest();
            request.Limits = limits;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetDutyCycleLimitsInStackResponse)));
            SetDutyCycleLimitsInStackResponse response = (SetDutyCycleLimitsInStackResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Obtains the current duty cycle limits that were previously set by a call to sli_zigbee_stack_set_duty_cycle_limits_in_stack(), or the defaults set by the stack if no set call was made.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating the success or failure of the command.
        /// - ReturnedLimits: Return current duty cycle limits if returnedLimits is not NULL
        /// </returns>
        public (Status Status, ZigbeeDutyCycleLimits ReturnedLimits) GetDutyCycleLimits()
        {
            GetDutyCycleLimitsRequest request = new GetDutyCycleLimitsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetDutyCycleLimitsResponse)));
            GetDutyCycleLimitsResponse response = (GetDutyCycleLimitsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ReturnedLimits);
        }

        /// <summary>
        /// Returns the duty cycle of the stack&apos;s connected children that are being monitored, up to maxDevices. It indicates the amount of overall duty cycle they have consumed (up to the suspend limit). The first entry is always the local stack&apos;s nodeId, and thus the total aggregate duty cycle for the device. The passed pointer arrayOfDeviceDutyCycles MUST have space for maxDevices.
        /// </summary>
        /// <param name="MaxDevices">Number of devices to retrieve consumed duty cycle.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK  if the duty cycles were read successfully, SL_STATUS_INVALID_PARAMETER maxDevices is greater than SL_ZIGBEE_MAX_END_DEVICE_CHILDREN + 1.
        /// - ArrayOfDeviceDutyCycles: Consumed duty cycles up to maxDevices. When the number of children that are being monitored is less than maxDevices, the sl_802154_short_addr_t element in the sl_zigbee_per_device_duty_cycle_t will be 0xFFFF.
        /// </returns>
        public (Status Status, byte[] ArrayOfDeviceDutyCycles) GetCurrentDutyCycle(byte maxDevices)
        {
            GetCurrentDutyCycleRequest request = new GetCurrentDutyCycleRequest();
            request.MaxDevices = maxDevices;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetCurrentDutyCycleResponse)));
            GetCurrentDutyCycleResponse response = (GetCurrentDutyCycleResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ArrayOfDeviceDutyCycles);
        }

        /// <summary>
        /// Callback fires when the duty cycle state has changed
        /// </summary>
        /// <returns>A tuple containing:
        /// - ChannelPage: The channel page whose duty cycle state has changed.
        /// - Channel: The channel number whose duty cycle state has changed.
        /// - State: The current duty cycle state.
        /// - TotalDevices: The total number of connected end devices that are being monitored for duty cycle.
        /// - ArrayOfDeviceDutyCycles: Consumed duty cycles of end devices that are being monitored. The first entry always be the local stack's nodeId, and thus the total aggregate duty cycle for the device.
        /// </returns>
        public (byte ChannelPage, byte Channel, ZigbeeDutyCycleState State, byte TotalDevices, ZigbeePerDeviceDutyCycle ArrayOfDeviceDutyCycles) DutyCycleHandler()
        {
            DutyCycleHandlerRequest request = new DutyCycleHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DutyCycleHandlerResponse)));
            DutyCycleHandlerResponse response = (DutyCycleHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.ChannelPage, response.Channel, response.State, response.TotalDevices, response.ArrayOfDeviceDutyCycles);
        }

        /// <summary>
        /// Configure the number of beacons to store when issuing active scans for networks.
        /// </summary>
        /// <param name="NumBeacons">The number of beacons to cache when scanning.</param>
        /// <returns>SL_STATUS_INVALID_PARAMETER if numBeacons is greater than SL_ZIGBEE_MAX_BEACONS_TO_STORE, otherwise SL_STATUS_OK</returns>
        public Status SetNumBeaconsToStore(byte numBeacons)
        {
            SetNumBeaconsToStoreRequest request = new SetNumBeaconsToStoreRequest();
            request.NumBeacons = numBeacons;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetNumBeaconsToStoreResponse)));
            SetNumBeaconsToStoreResponse response = (SetNumBeaconsToStoreResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Fetches the specified beacon in the cache. Beacons are stored in cache after issuing an active scan.
        /// </summary>
        /// <param name="BeaconNumber">The beacon index to fetch. Valid values range from 0 to &lt;i&gt;sli_zigbee_stack_get_num_stored_beacons&lt;/i&gt;-1.</param>
        /// <returns>A tuple containing:
        /// - Status: An appropriate sl_status_t status code.
        /// - Beacon: The beacon to populate upon success.
        /// </returns>
        public (Status Status, ZigbeeBeaconData Beacon) GetStoredBeacon(byte beaconNumber)
        {
            GetStoredBeaconRequest request = new GetStoredBeaconRequest();
            request.BeaconNumber = beaconNumber;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetStoredBeaconResponse)));
            GetStoredBeaconResponse response = (GetStoredBeaconResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Beacon);
        }

        /// <summary>
        /// Returns the number of cached beacons that have been collected from a scan.
        /// </summary>
        /// <returns>The number of cached beacons that have been collected from a scan.</returns>
        public byte GetNumStoredBeacons()
        {
            GetNumStoredBeaconsRequest request = new GetNumStoredBeaconsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetNumStoredBeaconsResponse)));
            GetNumStoredBeaconsResponse response = (GetNumStoredBeaconsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.NumBeacons;
        }

        /// <summary>
        /// Clears all cached beacons that have been collected from a scan.
        /// </summary>
        /// <returns></returns>
        public Status ClearStoredBeacons()
        {
            ClearStoredBeaconsRequest request = new ClearStoredBeaconsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearStoredBeaconsResponse)));
            ClearStoredBeaconsResponse response = (ClearStoredBeaconsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call sets the radio channel in the stack and propagates the information to the hardware.
        /// </summary>
        /// <param name="RadioChannel">The radio channel to be set.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetLogicalAndRadioChannel(byte radioChannel)
        {
            SetLogicalAndRadioChannelRequest request = new SetLogicalAndRadioChannelRequest();
            request.RadioChannel = radioChannel;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetLogicalAndRadioChannelResponse)));
            SetLogicalAndRadioChannelResponse response = (SetLogicalAndRadioChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Form a new sleepy-to-sleepy network.  If the network is using security, the device must call sli_zigbee_stack_set_initial_security_state() first.
        /// </summary>
        /// <param name="Parameters">Specification of the new network.</param>
        /// <param name="Initiator">Whether this device is initiating or joining the network.</param>
        /// <returns>An sl_status_t value indicating success or a reason for failure.</returns>
        public Status SleepyToSleepyNetworkStart(ZigbeeNetworkParameters parameters, bool initiator)
        {
            SleepyToSleepyNetworkStartRequest request = new SleepyToSleepyNetworkStartRequest();
            request.Parameters = parameters;
            request.Initiator = initiator;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SleepyToSleepyNetworkStartResponse)));
            SleepyToSleepyNetworkStartResponse response = (SleepyToSleepyNetworkStartResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send a Zigbee NWK Leave command to the destination.
        /// </summary>
        /// <param name="Destination">Node ID of the device being told to leave.</param>
        /// <param name="Flags">Bitmask indicating additional considerations for the leave request.</param>
        /// <returns>Status indicating success or a reason for failure. Call is invalid if destination is on network or is the local node.</returns>
        public Status SendZigbeeLeave(ushort destination, ZigbeeLeaveRequestFlags flags)
        {
            SendZigbeeLeaveRequest request = new SendZigbeeLeaveRequest();
            request.Destination = destination;
            request.Flags = flags;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendZigbeeLeaveResponse)));
            SendZigbeeLeaveResponse response = (SendZigbeeLeaveResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicate the state of permit joining in MAC.
        /// </summary>
        /// <returns>Whether the current network permits joining.</returns>
        public bool GetPermitJoining()
        {
            GetPermitJoiningRequest request = new GetPermitJoiningRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetPermitJoiningResponse)));
            GetPermitJoiningResponse response = (GetPermitJoiningResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.JoiningPermitted;
        }

        /// <summary>
        /// Get the 8-byte extended PAN ID of this node.
        /// </summary>
        /// <returns>Extended PAN ID of this node.  Valid only if it is currently on a network.</returns>
        public byte[] GetExtendedPanId()
        {
            GetExtendedPanIdRequest request = new GetExtendedPanIdRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetExtendedPanIdResponse)));
            GetExtendedPanIdResponse response = (GetExtendedPanIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ExtendedPanId;
        }

        /// <summary>
        /// Get the current network.
        /// </summary>
        /// <returns>Return the current network index.</returns>
        public byte GetCurrentNetwork()
        {
            GetCurrentNetworkRequest request = new GetCurrentNetworkRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetCurrentNetworkResponse)));
            GetCurrentNetworkResponse response = (GetCurrentNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Index;
        }

        /// <summary>
        /// Set initial outgoing link cost for neighbor.
        /// </summary>
        /// <param name="Cost">The new default cost. Valid values are 0, 1, 3, 5, and 7.</param>
        /// <returns>Whether or not initial cost was successfully set.</returns>
        public Status SetInitialNeighborOutgoingCost(byte cost)
        {
            SetInitialNeighborOutgoingCostRequest request = new SetInitialNeighborOutgoingCostRequest();
            request.Cost = cost;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetInitialNeighborOutgoingCostResponse)));
            SetInitialNeighborOutgoingCostResponse response = (SetInitialNeighborOutgoingCostResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Get initial outgoing link cost for neighbor.
        /// </summary>
        /// <returns>The default cost associated with new neighbor&apos;s outgoing links.</returns>
        public byte GetInitialNeighborOutgoingCost()
        {
            GetInitialNeighborOutgoingCostRequest request = new GetInitialNeighborOutgoingCostRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetInitialNeighborOutgoingCostResponse)));
            GetInitialNeighborOutgoingCostResponse response = (GetInitialNeighborOutgoingCostResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Cost;
        }

        /// <summary>
        /// Indicate whether a rejoining neighbor should have its incoming frame counter reset.
        /// </summary>
        /// <param name="Reset">Whether or not a neighbor&apos;s incoming FC should be reset upon rejoining (true or false).</param>
        /// <returns>The ResetRejoiningNeighborsFrameCounterResponse object from the NCP</returns>
        public ResetRejoiningNeighborsFrameCounterResponse ResetRejoiningNeighborsFrameCounter(bool reset)
        {
            ResetRejoiningNeighborsFrameCounterRequest request = new ResetRejoiningNeighborsFrameCounterRequest();
            request.Reset = reset;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ResetRejoiningNeighborsFrameCounterResponse)));
            ResetRejoiningNeighborsFrameCounterResponse response = (ResetRejoiningNeighborsFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Check whether a rejoining neighbor will have its incoming frame counter reset based on the currently set policy.
        /// </summary>
        /// <returns>Whether or not a rejoining neighbor&apos;s incoming FC gets reset (true or false).</returns>
        public bool IsResetRejoiningNeighborsFrameCounterEnabled()
        {
            IsResetRejoiningNeighborsFrameCounterEnabledRequest request = new IsResetRejoiningNeighborsFrameCounterEnabledRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IsResetRejoiningNeighborsFrameCounterEnabledResponse)));
            IsResetRejoiningNeighborsFrameCounterEnabledResponse response = (IsResetRejoiningNeighborsFrameCounterEnabledResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.GetsReset;
        }

        /// <summary>
        /// Deletes all binding table entries.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ClearBindingTable()
        {
            ClearBindingTableRequest request = new ClearBindingTableRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearBindingTableResponse)));
            ClearBindingTableResponse response = (ClearBindingTableResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets an entry in the binding table.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <param name="Value">The contents of the binding entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetBinding(byte index, ZigbeeBindingTableEntry value)
        {
            SetBindingRequest request = new SetBindingRequest();
            request.Index = index;
            request.Value = value;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetBindingResponse)));
            SetBindingResponse response = (SetBindingResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets an entry from the binding table.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Value: The contents of the binding entry.
        /// </returns>
        public (Status Status, ZigbeeBindingTableEntry Value) GetBinding(byte index)
        {
            GetBindingRequest request = new GetBindingRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetBindingResponse)));
            GetBindingResponse response = (GetBindingResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Deletes a binding table entry.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status DeleteBinding(byte index)
        {
            DeleteBindingRequest request = new DeleteBindingRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DeleteBindingResponse)));
            DeleteBindingResponse response = (DeleteBindingResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether any messages are currently being sent using this binding table entry. Note that this command does not indicate whether a binding is clear. To determine whether a binding is clear, check whether the type field of the sl_zigbee_binding_table_entry_t has the value SL_ZIGBEE_UNUSED_BINDING.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>True if the binding table entry is active, false otherwise.</returns>
        public bool BindingIsActive(byte index)
        {
            BindingIsActiveRequest request = new BindingIsActiveRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(BindingIsActiveResponse)));
            BindingIsActiveResponse response = (BindingIsActiveResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Active;
        }

        /// <summary>
        /// Returns the node ID for the binding&apos;s destination, if the ID is known. If a message is sent using the binding and the destination&apos;s ID is not known, the stack will discover the ID by broadcasting a ZDO address request. The application can avoid the need for this discovery by using &lt;i&gt;setBindingRemoteNodeId&lt;/i&gt; when it knows the correct ID via some other means. The destination&apos;s node ID is forgotten when the binding is changed, when the local node reboots or, much more rarely, when the destination node changes its ID in response to an ID conflict.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>The short ID of the destination node or SL_ZIGBEE_NULL_NODE_ID if no destination is known.</returns>
        public ushort GetBindingRemoteNodeId(byte index)
        {
            GetBindingRemoteNodeIdRequest request = new GetBindingRemoteNodeIdRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetBindingRemoteNodeIdResponse)));
            GetBindingRemoteNodeIdResponse response = (GetBindingRemoteNodeIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.NodeId;
        }

        /// <summary>
        /// Set the node ID for the binding&apos;s destination. See &lt;i&gt;getBindingRemoteNodeId&lt;/i&gt; for a description.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <param name="NodeId">The short ID of the destination node.</param>
        /// <returns>The SetBindingRemoteNodeIdResponse object from the NCP</returns>
        public SetBindingRemoteNodeIdResponse SetBindingRemoteNodeId(byte index, ushort nodeId)
        {
            SetBindingRemoteNodeIdRequest request = new SetBindingRemoteNodeIdRequest();
            request.Index = index;
            request.NodeId = nodeId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetBindingRemoteNodeIdResponse)));
            SetBindingRemoteNodeIdResponse response = (SetBindingRemoteNodeIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// The NCP used the external binding modification policy to decide how to handle a remote set binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Entry: The requested binding.
        /// - Index: The index at which the binding was added.
        /// - PolicyDecision: SL_STATUS_OK if the binding was added to the table and any other status if not.
        /// </returns>
        public (ZigbeeBindingTableEntry Entry, byte Index, Status PolicyDecision) RemoteSetBindingHandler()
        {
            RemoteSetBindingHandlerRequest request = new RemoteSetBindingHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RemoteSetBindingHandlerResponse)));
            RemoteSetBindingHandlerResponse response = (RemoteSetBindingHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Entry, response.Index, response.PolicyDecision);
        }

        /// <summary>
        /// The NCP used the external binding modification policy to decide how to handle a remote delete binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Index: The index of the binding whose deletion was requested.
        /// - PolicyDecision: SL_STATUS_OK if the binding was removed from the table and any other status if not.
        /// </returns>
        public (byte Index, Status PolicyDecision) RemoteDeleteBindingHandler()
        {
            RemoteDeleteBindingHandlerRequest request = new RemoteDeleteBindingHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RemoteDeleteBindingHandlerResponse)));
            RemoteDeleteBindingHandlerResponse response = (RemoteDeleteBindingHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Index, response.PolicyDecision);
        }

        /// <summary>
        /// Returns the maximum size of the payload. The size depends on the security level in use.
        /// </summary>
        /// <returns>The maximum APS payload length.</returns>
        public byte MaximumPayloadLength()
        {
            MaximumPayloadLengthRequest request = new MaximumPayloadLengthRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MaximumPayloadLengthResponse)));
            MaximumPayloadLengthResponse response = (MaximumPayloadLengthResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ApsLength;
        }

        /// <summary>
        /// Sends a unicast message as per the ZigBee specification. The message will arrive at its destination only if there is a known route to the destination node. Setting the ENABLE_ROUTE_DISCOVERY option will cause a route to be discovered if none is known. Setting the FORCE_ROUTE_DISCOVERY option will force route discovery. Routes to end-device children of the local node are always known. Setting the APS_RETRY option will cause the message to be retransmitted until either a matching acknowledgement is received or three transmissions have been made. &lt;b&gt;Note:&lt;/b&gt; Using the FORCE_ROUTE_DISCOVERY option will cause the first transmission to be consumed by a route request as part of discovery, so the application payload of this packet will not reach its destination on the first attempt. If you want the packet to reach its destination, the APS_RETRY option must be set so that another attempt is made to transmit the message with its application payload after the route has been constructed. &lt;b&gt;Note:&lt;/b&gt; When sending fragmented messages, the stack will only assign a new APS sequence number for the first fragment of the message (i.e., SL_ZIGBEE_APS_OPTION_FRAGMENT is set and the low-order byte of the groupId field in the APS frame is zero). For all subsequent fragments of the same message, the application must set the sequence number field in the APS frame to the sequence number assigned by the stack to the first fragment.
        /// </summary>
        /// <param name="Type">Specifies the outgoing message type. Must be one of SL_ZIGBEE_OUTGOING_DIRECT, SL_ZIGBEE_OUTGOING_VIA_ADDRESS_TABLE, or SL_ZIGBEE_OUTGOING_VIA_BINDING.</param>
        /// <param name="IndexOrDestination">Depending on the type of addressing used, this is either the sl_802154_short_addr_t of the destination, an index into the address table, or an index into the binding table.</param>
        /// <param name="ApsFrame">The APS frame which is to be added to the message.</param>
        /// <param name="MessageTag">A value chosen by the Host. This value is used in the &lt;i&gt;sl_zigbee_ezsp_message_sent_handler&lt;/i&gt; response to refer to this message.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">Content of the message.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Sequence: The sequence number that will be used when this message is transmitted.
        /// </returns>
        public (Status Status, byte Sequence) SendUnicast(ZigbeeOutgoingMessageType type, ushort indexOrDestination, ZigbeeApsFrame apsFrame, ushort messageTag, byte messageLength, byte[] messageContents)
        {
            SendUnicastRequest request = new SendUnicastRequest();
            request.Type = type;
            request.IndexOrDestination = indexOrDestination;
            request.ApsFrame = apsFrame;
            request.MessageTag = messageTag;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendUnicastResponse)));
            SendUnicastResponse response = (SendUnicastResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Sequence);
        }

        /// <summary>
        /// Sends a broadcast message as per the ZigBee specification.
        /// </summary>
        /// <param name="Alias">The aliased source from which we send the broadcast. This must be SL_ZIGBEE_NULL_NODE_ID if we do not need an aliased source</param>
        /// <param name="Destination">The destination to which to send the broadcast. This must be one of the three ZigBee broadcast addresses.</param>
        /// <param name="NwkSequence">The alias nwk sequence number. this won&apos;t be used if there is no aliased source.</param>
        /// <param name="ApsFrame">The APS frame for the message.</param>
        /// <param name="Radius">The message will be delivered to all nodes within &lt;i&gt;radius&lt;/i&gt; hops of the sender. A radius of zero is converted to SL_ZIGBEE_MAX_HOPS.</param>
        /// <param name="MessageTag">A value chosen by the Host. This value is used in the &lt;i&gt;sl_zigbee_ezsp_message_sent_handler&lt;/i&gt; response to refer to this message.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The broadcast message.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - ApsSequence: The APS sequence number that will be used when this message is transmitted.
        /// </returns>
        public (Status Status, byte ApsSequence) SendBroadcast(ushort alias, ushort destination, byte nwkSequence, ZigbeeApsFrame apsFrame, byte radius, ushort messageTag, byte messageLength, byte[] messageContents)
        {
            SendBroadcastRequest request = new SendBroadcastRequest();
            request.Alias = alias;
            request.Destination = destination;
            request.NwkSequence = nwkSequence;
            request.ApsFrame = apsFrame;
            request.Radius = radius;
            request.MessageTag = messageTag;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendBroadcastResponse)));
            SendBroadcastResponse response = (SendBroadcastResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ApsSequence);
        }

        /// <summary>
        /// Sends proxied broadcast message for another node in conjunction with sl_zigbee_proxy_broadcast where a long source is also specified in the NWK frame control.
        /// </summary>
        /// <param name="EuiSource">The long source from which to send the broadcast</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ProxyNextBroadcastFromLong(byte[] euiSource)
        {
            ProxyNextBroadcastFromLongRequest request = new ProxyNextBroadcastFromLongRequest();
            request.EuiSource = euiSource;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ProxyNextBroadcastFromLongResponse)));
            ProxyNextBroadcastFromLongResponse response = (ProxyNextBroadcastFromLongResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sends a multicast message to all endpoints that share a specific multicast ID and are within a specified number of hops of the sender.
        /// </summary>
        /// <param name="ApsFrame">The APS frame for the message. The multicast will be sent to the groupId in this frame.</param>
        /// <param name="Hops">The message will be delivered to all nodes within this number of hops of the sender. A value of zero is converted to SL_ZIGBEE_MAX_HOPS.</param>
        /// <param name="BroadcastAddr">The number of hops that the message will be forwarded by devices that are not members of the group. A value of 7 or greater is treated as infinite.</param>
        /// <param name="Alias">The alias source address</param>
        /// <param name="NwkSequence">the alias sequence number</param>
        /// <param name="MessageTag">A value chosen by the Host. This value is used in the &lt;i&gt;sl_zigbee_ezsp_message_sent_handler&lt;/i&gt; response to refer to this message.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The multicast message.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value. For any result other than SL_STATUS_OK, the message will not be sent. SL_STATUS_OK - The message has been submitted for transmission. SL_STATUS_INVALID_INDEX - The bindingTableIndex refers to a non-multicast binding. SL_STATUS_NETWORK_DOWN - The node is not part of a network. SL_STATUS_MESSAGE_TOO_LONG - The message is too large to fit in a MAC layer frame. SL_STATUS_ALLOCATION_FAILED - The free packet buffer pool is empty. SL_STATUS_BUSY - Insufficient resources available in Network or MAC layers to send message.
        /// - Sequence: The sequence number that will be used when this message is transmitted.
        /// </returns>
        public (Status Status, byte Sequence) SendMulticast(ZigbeeApsFrame apsFrame, byte hops, ushort broadcastAddr, ushort alias, byte nwkSequence, ushort messageTag, byte messageLength, byte[] messageContents)
        {
            SendMulticastRequest request = new SendMulticastRequest();
            request.ApsFrame = apsFrame;
            request.Hops = hops;
            request.BroadcastAddr = broadcastAddr;
            request.Alias = alias;
            request.NwkSequence = nwkSequence;
            request.MessageTag = messageTag;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendMulticastResponse)));
            SendMulticastResponse response = (SendMulticastResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Sequence);
        }

        /// <summary>
        /// Sends a reply to a received unicast message. The &lt;i&gt;incomingMessageHandler&lt;/i&gt; callback for the unicast being replied to supplies the values for all the parameters except the reply itself.
        /// </summary>
        /// <param name="Sender">Value supplied by incoming unicast.</param>
        /// <param name="ApsFrame">Value supplied by incoming unicast.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The reply message.</param>
        /// <returns>An sl_status_t value. SL_STATUS_INVALID_STATE - The SL_ZIGBEE_EZSP_UNICAST_REPLIES_POLICY is set to SL_ZIGBEE_EZSP_HOST_WILL_NOT_SUPPLY_REPLY. This means the NCP will automatically send an empty reply. The Host must change the policy to SL_ZIGBEE_EZSP_HOST_WILL_SUPPLY_REPLY before it can supply the reply. There is one exception to this rule: In the case of responses to message fragments, the host must call sendReply when a message fragment is received. In this case, the policy set on the NCP does not matter. The NCP expects a sendReply call from the Host for message fragments regardless of the current policy settings. SL_STATUS_ALLOCATION_FAILED - Not enough memory was available to send the reply. SL_STATUS_BUSY - Either no route or insufficient resources available. SL_STATUS_OK - The reply was successfully queued for transmission.</returns>
        public Status SendReply(ushort sender, ZigbeeApsFrame apsFrame, byte messageLength, byte[] messageContents)
        {
            SendReplyRequest request = new SendReplyRequest();
            request.Sender = sender;
            request.ApsFrame = apsFrame;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendReplyResponse)));
            SendReplyResponse response = (SendReplyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback indicating the stack has completed sending a message.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value of SL_STATUS_OK if an ACK was received from the destination or SL_STATUS_ZIGBEE_DELIVERY_FAILED if no ACK was received.
        /// - Type: The type of message sent.
        /// - IndexOrDestination: The destination to which the message was sent, for direct unicasts, or the address table or binding index for other unicasts. The value is unspecified for multicasts and broadcasts.
        /// - ApsFrame: The APS frame for the message.
        /// - MessageTag: The value supplied by the Host in the <i>sl_zigbee_ezsp_send_unicast</i>, <i>sl_zigbee_ezsp_send_broadcast</i> or <i>sl_zigbee_ezsp_send_multicast</i> command.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The unicast message supplied by the Host. The message contents are only included here if the decision for the messageContentsInCallback policy is messageTagAndContentsInCallback.
        /// </returns>
        public (Status Status, ZigbeeOutgoingMessageType Type, ushort IndexOrDestination, ZigbeeApsFrame ApsFrame, ushort MessageTag, byte MessageLength, byte[] MessageContents) MessageSentHandler()
        {
            MessageSentHandlerRequest request = new MessageSentHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MessageSentHandlerResponse)));
            MessageSentHandlerResponse response = (MessageSentHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Type, response.IndexOrDestination, response.ApsFrame, response.MessageTag, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// Sends a route request packet that creates routes from every node in the network back to this node. This function should be called by an application that wishes to communicate with many nodes, for example, a gateway, central monitor, or controller. A device using this function was referred to as an &apos;aggregator&apos; in EmberZNet 2.x and earlier, and is referred to as a &apos;concentrator&apos; in the ZigBee specification and EmberZNet 3. &lt;p&gt; This function enables large scale networks, because the other devices do not have to individually perform bandwidth-intensive route discoveries. Instead, when a remote node sends an APS unicast to a concentrator, its network layer automatically delivers a special route record packet first, which lists the network ids of all the intermediate relays. The concentrator can then use source routing to send outbound APS unicasts. (A source routed message is one in which the entire route is listed in the network layer header.) This allows the concentrator to communicate with thousands of devices without requiring large route tables on neighboring nodes. &lt;p&gt; This function is only available in ZigBee Pro (stack profile 2), and cannot be called on end devices. Any router can be a concentrator (not just the coordinator), and there can be multiple concentrators on a network. &lt;p&gt; Note that a concentrator does not automatically obtain routes to all network nodes after calling this function. Remote applications must first initiate an inbound APS unicast. &lt;p&gt; Many-to-one routes are not repaired automatically. Instead, the concentrator application must call this function to rediscover the routes as necessary, for example, upon failure of a retried APS message. The reason for this is that there is no scalable one-size-fits-all route repair strategy. A common and recommended strategy is for the concentrator application to refresh the routes by calling this function periodically.
        /// </summary>
        /// <param name="ConcentratorType">Must be either SL_ZIGBEE_HIGH_RAM_CONCENTRATOR or SL_ZIGBEE_LOW_RAM_CONCENTRATOR. The former is used when the caller has enough memory to store source routes for the whole network. In that case, remote nodes stop sending route records once the concentrator has successfully received one. The latter is used when the concentrator has insufficient RAM to store all outbound source routes. In that case, route records are sent to the concentrator prior to every inbound APS unicast.</param>
        /// <param name="Radius">The maximum number of hops the route request will be relayed. A radius of zero is converted to SL_ZIGBEE_MAX_HOPS</param>
        /// <returns>SL_STATUS_OK if the route request was successfully submitted to the transmit queue, and SL_STATUS_FAIL otherwise.</returns>
        public Status SendManyToOneRouteRequest(ushort concentratorType, byte radius)
        {
            SendManyToOneRouteRequestRequest request = new SendManyToOneRouteRequestRequest();
            request.ConcentratorType = concentratorType;
            request.Radius = radius;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendManyToOneRouteRequestResponse)));
            SendManyToOneRouteRequestResponse response = (SendManyToOneRouteRequestResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Periodically request any pending data from our parent. Setting &lt;i&gt;interval&lt;/i&gt; to 0 or &lt;i&gt;units&lt;/i&gt; to SL_ZIGBEE_EVENT_INACTIVE will generate a single poll.
        /// </summary>
        /// <param name="Interval">The time between polls. Note that the timer clock is free running and is not synchronized with this command. This means that the time will be between &lt;i&gt;interval&lt;/i&gt; and (&lt;i&gt;interval&lt;/i&gt; - 1). The maximum interval is 32767.</param>
        /// <param name="Units">The units for &lt;i&gt;interval&lt;/i&gt;.</param>
        /// <param name="FailureLimit">The number of poll failures that will be tolerated before a &lt;i&gt;pollCompleteHandler&lt;/i&gt; callback is generated. A value of zero will result in a callback for every poll. Any status value apart from SL_STATUS_OK and SL_STATUS_MAC_NO_DATA is counted as a failure.</param>
        /// <returns>The result of sending the first poll.</returns>
        public Status PollForData(ushort interval, ZigbeeEventUnits units, byte failureLimit)
        {
            PollForDataRequest request = new PollForDataRequest();
            request.Interval = interval;
            request.Units = units;
            request.FailureLimit = failureLimit;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(PollForDataResponse)));
            PollForDataResponse response = (PollForDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates the result of a data poll to the parent of the local node.
        /// </summary>
        /// <returns>An sl_status_t value: SL_STATUS_OK - Data was received in response to the poll. SL_STATUS_MAC_NO_DATA - No data was pending. SL_STATUS_ZIGBEE_DELIVERY_FAILED - The poll message could not be sent. SL_STATUS_MAC_NO_ACK_RECEIVED - The poll message was sent but not acknowledged by the parent.</returns>
        public Status PollCompleteHandler()
        {
            PollCompleteHandlerRequest request = new PollCompleteHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(PollCompleteHandlerResponse)));
            PollCompleteHandlerResponse response = (PollCompleteHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set a flag to indicate that a message is pending for a child. The next time that the child polls, it will be informed that it has a pending message. The message is sent from emberPollHandler, which is called when the child requests data.
        /// </summary>
        /// <param name="ChildId">The ID of the child that just polled for data.</param>
        /// <returns>SL_STATUS_OK - The next time that the child polls, it will be informed that it has pending data. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.</returns>
        public Status SetMessageFlag(ushort childId)
        {
            SetMessageFlagRequest request = new SetMessageFlagRequest();
            request.ChildId = childId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetMessageFlagResponse)));
            SetMessageFlagResponse response = (SetMessageFlagResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clear a flag to indicate that there are no more messages for a child. The next time the child polls, it will be informed that it does not have any pending messages.
        /// </summary>
        /// <param name="ChildId">The ID of the child that no longer has pending messages.</param>
        /// <returns>SL_STATUS_OK - The next time that the child polls, it will be informed that it does not have any pending messages. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.</returns>
        public Status ClearMessageFlag(ushort childId)
        {
            ClearMessageFlagRequest request = new ClearMessageFlagRequest();
            request.ChildId = childId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearMessageFlagResponse)));
            ClearMessageFlagResponse response = (ClearMessageFlagResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates that the local node received a data poll from a child.
        /// </summary>
        /// <returns>A tuple containing:
        /// - ChildId: The node ID of the child that is requesting data.
        /// - TransmitExpected: True if transmit is expected, false otherwise.
        /// </returns>
        public (ushort ChildId, bool TransmitExpected) PollHandler()
        {
            PollHandlerRequest request = new PollHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(PollHandlerResponse)));
            PollHandlerResponse response = (PollHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.ChildId, response.TransmitExpected);
        }

        /// <summary>
        /// Add a child to the child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely. If used appropriately, the application can maintain more than the maximum of children provided by the stack.
        /// </summary>
        /// <param name="ShortId">The preferred short ID of the node.</param>
        /// <param name="LongId">The long ID of the node.</param>
        /// <param name="NodeType">The nodetype e.g., SL_ZIGBEE_ROUTER defining, if this would be added to the child table or neighbor table.</param>
        /// <returns>SL_STATUS_OK - This node has been successfully added. SL_STATUS_FAIL - The child was not added to the child/neighbor table.</returns>
        public Status AddChild(ushort shortId, byte[] longId, ZigbeeNodeType nodeType)
        {
            AddChildRequest request = new AddChildRequest();
            request.ShortId = shortId;
            request.LongId = longId;
            request.NodeType = nodeType;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(AddChildResponse)));
            AddChildResponse response = (AddChildResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Remove a node from child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely.
        /// </summary>
        /// <param name="ChildEui64">The long ID of the node.</param>
        /// <returns>SL_STATUS_OK - This node has been successfully removed. SL_STATUS_FAIL - The node was not found in either of the child or neighbor tables.</returns>
        public Status RemoveChild(byte[] childEui64)
        {
            RemoveChildRequest request = new RemoveChildRequest();
            request.ChildEui64 = childEui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RemoveChildResponse)));
            RemoveChildResponse response = (RemoveChildResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Remove a neighbor from neighbor table only on SoC, allowing direct manipulation of neighbor table by the application. This can affect the network functionality, and needs to be used wisely.
        /// </summary>
        /// <param name="ShortId">The short ID of the neighbor.</param>
        /// <param name="LongId">The long ID of the neighbor.</param>
        /// <returns>The RemoveNeighborResponse object from the NCP</returns>
        public RemoveNeighborResponse RemoveNeighbor(ushort shortId, byte[] longId)
        {
            RemoveNeighborRequest request = new RemoveNeighborRequest();
            request.ShortId = shortId;
            request.LongId = longId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RemoveNeighborResponse)));
            RemoveNeighborResponse response = (RemoveNeighborResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// A callback indicating a message has been received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Type: The type of the incoming message. One of the following: SL_ZIGBEE_INCOMING_UNICAST, SL_ZIGBEE_INCOMING_UNICAST_REPLY, SL_ZIGBEE_INCOMING_MULTICAST, SL_ZIGBEE_INCOMING_MULTICAST_LOOPBACK, SL_ZIGBEE_INCOMING_BROADCAST, SL_ZIGBEE_INCOMING_BROADCAST_LOOPBACK
        /// - ApsFrame: The APS frame from the incoming message.
        /// - PacketInfo: Miscellanous message information.
        /// - MessageLength: The length of the <i>message</i> parameter in bytes.
        /// - Message: The incoming message.
        /// </returns>
        public (ZigbeeIncomingMessageType Type, ZigbeeApsFrame ApsFrame, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] Message) IncomingMessageHandler()
        {
            IncomingMessageHandlerRequest request = new IncomingMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingMessageHandlerResponse)));
            IncomingMessageHandlerResponse response = (IncomingMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Type, response.ApsFrame, response.PacketInfo, response.MessageLength, response.Message);
        }

        /// <summary>
        /// Sets source route discovery(MTORR) mode to on, off, reschedule
        /// </summary>
        /// <param name="Mode">Source route discovery mode: off:0, on:1, reschedule:2</param>
        /// <returns>Remaining time(ms) until next MTORR broadcast if the mode is on, MAX_INT32U_VALUE if the mode is off</returns>
        public uint SetSourceRouteDiscoveryMode(byte mode)
        {
            SetSourceRouteDiscoveryModeRequest request = new SetSourceRouteDiscoveryModeRequest();
            request.Mode = mode;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetSourceRouteDiscoveryModeResponse)));
            SetSourceRouteDiscoveryModeResponse response = (SetSourceRouteDiscoveryModeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.RemainingTime;
        }

        /// <summary>
        /// A callback indicating that a many-to-one route to the concentrator with the given short and long id is available for use.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Source: The short id of the concentrator.
        /// - LongId: The EUI64 of the concentrator.
        /// - Cost: The path cost to the concentrator. The cost may decrease as additional route request packets for this discovery arrive, but the callback is made only once.
        /// </returns>
        public (ushort Source, byte[] LongId, byte Cost) IncomingManyToOneRouteRequestHandler()
        {
            IncomingManyToOneRouteRequestHandlerRequest request = new IncomingManyToOneRouteRequestHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingManyToOneRouteRequestHandlerResponse)));
            IncomingManyToOneRouteRequestHandlerResponse response = (IncomingManyToOneRouteRequestHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Source, response.LongId, response.Cost);
        }

        /// <summary>
        /// A callback invoked when a route error message is received. The error indicates that a problem routing to or from the target node was encountered.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_ZIGBEE_SOURCE_ROUTE_FAILURE or SL_STATUS_ZIGBEE_MANY_TO_ONE_ROUTE_FAILURE.
        /// - Target: The short id of the remote node.
        /// </returns>
        public (Status Status, ushort Target) IncomingRouteErrorHandler()
        {
            IncomingRouteErrorHandlerRequest request = new IncomingRouteErrorHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingRouteErrorHandlerResponse)));
            IncomingRouteErrorHandlerResponse response = (IncomingRouteErrorHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Target);
        }

        /// <summary>
        /// A callback invoked when a network status/route error message is received. The error indicates that there was a problem sending/receiving messages from the target node
        /// </summary>
        /// <returns>A tuple containing:
        /// - ErrorCode: One byte over-the-air error code from network status message
        /// - Target: The short ID of the remote node
        /// </returns>
        public (byte ErrorCode, ushort Target) IncomingNetworkStatusHandler()
        {
            IncomingNetworkStatusHandlerRequest request = new IncomingNetworkStatusHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingNetworkStatusHandlerResponse)));
            IncomingNetworkStatusHandlerResponse response = (IncomingNetworkStatusHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.ErrorCode, response.Target);
        }

        /// <summary>
        /// Reports the arrival of a route record command frame.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Source: The source of the route record.
        /// - SourceEui: The EUI64 of the source.
        /// - LastHopLqi: The link quality from the node that last relayed the route record.
        /// - LastHopRssi: The energy level (in units of dBm) observed during the reception.
        /// - RelayCount: The number of relays in <i>relayList</i>.
        /// - RelayList: The route record. Each relay in the list is an uint16_t node ID. The list is passed as uint8_t * to avoid alignment problems.
        /// </returns>
        public (ushort Source, byte[] SourceEui, byte LastHopLqi, sbyte LastHopRssi, byte RelayCount, byte[] RelayList) IncomingRouteRecordHandler()
        {
            IncomingRouteRecordHandlerRequest request = new IncomingRouteRecordHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingRouteRecordHandlerResponse)));
            IncomingRouteRecordHandlerResponse response = (IncomingRouteRecordHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Source, response.SourceEui, response.LastHopLqi, response.LastHopRssi, response.RelayCount, response.RelayList);
        }

        /// <summary>
        /// Supply a source route for the next outgoing message.
        /// </summary>
        /// <param name="Destination">The destination of the source route.</param>
        /// <param name="RelayCount">The number of relays in &lt;i&gt;relayList&lt;/i&gt;.</param>
        /// <param name="RelayList">The source route.</param>
        /// <returns>SL_STATUS_OK if the source route was successfully stored, and SL_STATUS_ALLOCATION_FAILED otherwise.</returns>
        public Status SetSourceRoute(ushort destination, byte relayCount, ushort[] relayList)
        {
            SetSourceRouteRequest request = new SetSourceRouteRequest();
            request.Destination = destination;
            request.RelayCount = relayCount;
            request.RelayList = relayList;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetSourceRouteResponse)));
            SetSourceRouteResponse response = (SetSourceRouteResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send the network key to a destination.
        /// </summary>
        /// <param name="TargetShort">The destination node of the key.</param>
        /// <param name="TargetLong">The long address of the destination node.</param>
        /// <param name="ParentShortId">The parent node of the destination node.</param>
        /// <returns>SL_STATUS_OK if send was successful</returns>
        public Status UnicastCurrentNetworkKey(ushort targetShort, byte[] targetLong, ushort parentShortId)
        {
            UnicastCurrentNetworkKeyRequest request = new UnicastCurrentNetworkKeyRequest();
            request.TargetShort = targetShort;
            request.TargetLong = targetLong;
            request.ParentShortId = parentShortId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(UnicastCurrentNetworkKeyResponse)));
            UnicastCurrentNetworkKeyResponse response = (UnicastCurrentNetworkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether any messages are currently being sent using this address table entry. Note that this function does not indicate whether the address table entry is unused. To determine whether an address table entry is unused, check the remote node ID. The remote node ID will have the value SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID when the address table entry is not in use.
        /// </summary>
        /// <param name="AddressTableIndex">The index of an address table entry.</param>
        /// <returns>True if the address table entry is active, false otherwise.</returns>
        public bool AddressTableEntryIsActive(byte addressTableIndex)
        {
            AddressTableEntryIsActiveRequest request = new AddressTableEntryIsActiveRequest();
            request.AddressTableIndex = addressTableIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(AddressTableEntryIsActiveResponse)));
            AddressTableEntryIsActiveResponse response = (AddressTableEntryIsActiveResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Active;
        }

        /// <summary>
        /// Sets the EUI64 and short ID of an address table entry. Usually the application will not need to set the short ID in the address table. Once the remote EUI64 is set the stack is capable of figuring out the short ID on its own. However, in cases where the application does set the short ID, the application must set the remote EUI64 prior to setting the short ID. This function will also check other address table entries, the child table and the neighbor table to see if the node ID for the given EUI64 is already known. If known then this function will set node ID. If not known it will set the node ID to SL_ZIGBEE_UNKNOWN_NODE_ID.
        /// </summary>
        /// <param name="AddressTableIndex">The index of an address table entry.</param>
        /// <param name="Eui64">The EUI64 to use for the address table entry.</param>
        /// <param name="Id">The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index or SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID which indicates that the entry stored in the address table at the given index is not in use.</param>
        /// <returns>SL_STATUS_OK if the information was successfully set, and SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE otherwise.</returns>
        public Status SetAddressTableInfo(byte addressTableIndex, byte[] eui64, ushort id)
        {
            SetAddressTableInfoRequest request = new SetAddressTableInfoRequest();
            request.AddressTableIndex = addressTableIndex;
            request.Eui64 = eui64;
            request.Id = id;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetAddressTableInfoResponse)));
            SetAddressTableInfoResponse response = (SetAddressTableInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the EUI64 and short ID of an address table entry.
        /// </summary>
        /// <param name="AddressTableIndex">The index of an address table entry.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - NodeId: One of the following: The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index. SL_ZIGBEE_UNKNOWN_NODE_ID - Indicates that the EUI64 stored in the address table at the given index is valid but the short ID is currently unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID - Indicates that the EUI64 stored in the address table at the given location is valid and network address discovery is underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID - Indicates that the entry stored in the address table at the given index is not in use.
        /// - Eui64: The EUI64 of the address table entry is copied to this location.
        /// </returns>
        public (Status Status, ushort NodeId, byte[] Eui64) GetAddressTableInfo(byte addressTableIndex)
        {
            GetAddressTableInfoRequest request = new GetAddressTableInfoRequest();
            request.AddressTableIndex = addressTableIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetAddressTableInfoResponse)));
            GetAddressTableInfoResponse response = (GetAddressTableInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.NodeId, response.Eui64);
        }

        /// <summary>
        /// Tells the stack whether or not the normal interval between retransmissions of a retried unicast message should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. The interval needs to be increased when sending to a sleepy node so that the message is not retransmitted until the destination has had time to wake up and poll its parent. The stack will automatically extend the timeout: - For our own sleepy children. - When an address response is received from a parent on behalf of its child. - When an indirect transaction expiry route error is received. - When an end device announcement is received from a sleepy node.
        /// </summary>
        /// <param name="RemoteEui64">The address of the node for which the timeout is to be set.</param>
        /// <param name="ExtendedTimeout">true if the retry interval should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval should be used.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure  </returns>
        public Status SetExtendedTimeout(byte[] remoteEui64, bool extendedTimeout)
        {
            SetExtendedTimeoutRequest request = new SetExtendedTimeoutRequest();
            request.RemoteEui64 = remoteEui64;
            request.ExtendedTimeout = extendedTimeout;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetExtendedTimeoutResponse)));
            SetExtendedTimeoutResponse response = (SetExtendedTimeoutResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether or not the stack will extend the normal interval between retransmissions of a retried unicast message by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT.
        /// </summary>
        /// <param name="RemoteEui64">The address of the node for which the timeout is to be returned.</param>
        /// <returns>SL_STATUS_OK if the retry interval will be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT and SL_STATUS_FAIL if the normal retry interval will be used.</returns>
        public Status GetExtendedTimeout(byte[] remoteEui64)
        {
            GetExtendedTimeoutRequest request = new GetExtendedTimeoutRequest();
            request.RemoteEui64 = remoteEui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetExtendedTimeoutResponse)));
            GetExtendedTimeoutResponse response = (GetExtendedTimeoutResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Replaces the EUI64, short ID and extended timeout setting of an address table entry. The previous EUI64, short ID and extended timeout setting are returned.
        /// </summary>
        /// <param name="AddressTableIndex">The index of the address table entry that will be modified.</param>
        /// <param name="NewEui64">The EUI64 to be written to the address table entry.</param>
        /// <param name="NewId">One of the following: The short ID corresponding to the new EUI64. SL_ZIGBEE_UNKNOWN_NODE_ID if the new EUI64 is valid but the short ID is unknown and should be discovered by the stack. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID if the address table entry is now unused.</param>
        /// <param name="NewExtendedTimeout">true if the retry interval should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval should be used.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the EUI64, short ID and extended timeout setting were successfully modified, and SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE otherwise.
        /// - OldEui64: The EUI64 of the address table entry before it was modified.
        /// - OldId: One of the following: The short ID corresponding to the EUI64 before it was modified. SL_ZIGBEE_UNKNOWN_NODE_ID if the short ID was unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID if discovery of the short ID was underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID if the address table entry was unused.
        /// - OldExtendedTimeout: true if the retry interval was being increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval was being used.
        /// </returns>
        public (Status Status, byte[] OldEui64, ushort OldId, bool OldExtendedTimeout) ReplaceAddressTableEntry(byte addressTableIndex, byte[] newEui64, ushort newId, bool newExtendedTimeout)
        {
            ReplaceAddressTableEntryRequest request = new ReplaceAddressTableEntryRequest();
            request.AddressTableIndex = addressTableIndex;
            request.NewEui64 = newEui64;
            request.NewId = newId;
            request.NewExtendedTimeout = newExtendedTimeout;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ReplaceAddressTableEntryResponse)));
            ReplaceAddressTableEntryResponse response = (ReplaceAddressTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.OldEui64, response.OldId, response.OldExtendedTimeout);
        }

        /// <summary>
        /// Returns the node ID that corresponds to the specified EUI64. The node ID is found by searching through all stack tables for the specified EUI64.
        /// </summary>
        /// <param name="Eui64">The EUI64 of the node to look up.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the short ID was found, SL_STATUS_FAIL if the short ID is not known.
        /// - NodeId: The short ID of the node or SL_ZIGBEE_NULL_NODE_ID if the short ID is not known.
        /// </returns>
        public (Status Status, ushort NodeId) LookupNodeIdByEui64(byte[] eui64)
        {
            LookupNodeIdByEui64Request request = new LookupNodeIdByEui64Request();
            request.Eui64 = eui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(LookupNodeIdByEui64Response)));
            LookupNodeIdByEui64Response response = (LookupNodeIdByEui64Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.NodeId);
        }

        /// <summary>
        /// Returns the EUI64 that corresponds to the specified node ID. The EUI64 is found by searching through all stack tables for the specified node ID.
        /// </summary>
        /// <param name="NodeId">The short ID of the node to look up.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the EUI64 was found, SL_STATUS_FAIL if the EUI64 is not known.
        /// - Eui64: The EUI64 of the node.
        /// </returns>
        public (Status Status, byte[] Eui64) LookupEui64ByNodeId(ushort nodeId)
        {
            LookupEui64ByNodeIdRequest request = new LookupEui64ByNodeIdRequest();
            request.NodeId = nodeId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(LookupEui64ByNodeIdResponse)));
            LookupEui64ByNodeIdResponse response = (LookupEui64ByNodeIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Eui64);
        }

        /// <summary>
        /// Gets an entry from the multicast table.
        /// </summary>
        /// <param name="Index">The index of a multicast table entry.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Value: The contents of the multicast entry.
        /// </returns>
        public (Status Status, ZigbeeMulticastTableEntry Value) GetMulticastTableEntry(byte index)
        {
            GetMulticastTableEntryRequest request = new GetMulticastTableEntryRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetMulticastTableEntryResponse)));
            GetMulticastTableEntryResponse response = (GetMulticastTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Sets an entry in the multicast table.
        /// </summary>
        /// <param name="Index">The index of a multicast table entry</param>
        /// <param name="Value">The contents of the multicast entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetMulticastTableEntry(byte index, ZigbeeMulticastTableEntry value)
        {
            SetMulticastTableEntryRequest request = new SetMulticastTableEntryRequest();
            request.Index = index;
            request.Value = value;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetMulticastTableEntryResponse)));
            SetMulticastTableEntryResponse response = (SetMulticastTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when an id conflict is discovered, that is, two different nodes in the network were found to be using the same short id. The stack automatically removes the conflicting short id from its internal tables (address, binding, route, neighbor, and child tables). The application should discontinue any other use of the id.
        /// </summary>
        /// <returns>The short id for which a conflict was detected</returns>
        public ushort IdConflictHandler()
        {
            IdConflictHandlerRequest request = new IdConflictHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IdConflictHandlerResponse)));
            IdConflictHandlerResponse response = (IdConflictHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Id;
        }

        /// <summary>
        /// Write the current node Id, PAN ID, or Node type to the tokens
        /// </summary>
        /// <param name="Erase">Erase the node type or not</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status WriteNodeData(bool erase)
        {
            WriteNodeDataRequest request = new WriteNodeDataRequest();
            request.Erase = erase;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(WriteNodeDataResponse)));
            WriteNodeDataResponse response = (WriteNodeDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Transmits the given message without modification. The MAC header is assumed to be configured in the message at the time this function is called.
        /// </summary>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The raw message.</param>
        /// <param name="Priority">transmit priority.</param>
        /// <param name="UseCca">Should we enable CCA or not.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SendRawMessage(byte messageLength, byte[] messageContents, byte priority, bool useCca)
        {
            SendRawMessageRequest request = new SendRawMessageRequest();
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            request.Priority = priority;
            request.UseCca = useCca;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendRawMessageResponse)));
            SendRawMessageResponse response = (SendRawMessageResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when a MAC passthrough message is received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - MessageType: The type of MAC passthrough message received.
        /// - PacketInfo: Information about the incoming packet.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The raw message that was received.
        /// </returns>
        public (ZigbeeMacPassthroughType MessageType, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents) MacPassthroughMessageHandler()
        {
            MacPassthroughMessageHandlerRequest request = new MacPassthroughMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MacPassthroughMessageHandlerResponse)));
            MacPassthroughMessageHandlerResponse response = (MacPassthroughMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.MessageType, response.PacketInfo, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when a raw MAC message that has matched one of the application&apos;s configured MAC filters.
        /// </summary>
        /// <returns>A tuple containing:
        /// - FilterValueMatch: The value of the filter that was matched.
        /// - LegacyPassthroughType: The type of MAC passthrough message received.
        /// - PacketInfo: Information about the incoming packet.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The raw message that was received.
        /// </returns>
        public (ushort FilterValueMatch, ZigbeeMacPassthroughType LegacyPassthroughType, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents) MacFilterMatchMessageHandler()
        {
            MacFilterMatchMessageHandlerRequest request = new MacFilterMatchMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MacFilterMatchMessageHandlerResponse)));
            MacFilterMatchMessageHandlerResponse response = (MacFilterMatchMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.FilterValueMatch, response.LegacyPassthroughType, response.PacketInfo, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a raw message.
        /// </summary>
        /// <returns>A tuple containing:
        /// - MessageLength: Length of the message that was transmitted.
        /// - MessageContents: The message that was transmitted.
        /// - Status: SL_STATUS_OK if the transmission was successful, or SL_STATUS_ZIGBEE_DELIVERY_FAILED if not
        /// </returns>
        public (byte MessageLength, byte[] MessageContents, Status Status) RawTransmitCompleteHandler()
        {
            RawTransmitCompleteHandlerRequest request = new RawTransmitCompleteHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RawTransmitCompleteHandlerResponse)));
            RawTransmitCompleteHandlerResponse response = (RawTransmitCompleteHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.MessageLength, response.MessageContents, response.Status);
        }

        /// <summary>
        /// This function is useful to sleepy end devices. This function will set the retry interval (in milliseconds) for mac data poll. This interval is the time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.
        /// </summary>
        /// <param name="WaitBeforeRetryIntervalMs">Time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.</param>
        /// <returns>The SetMacPollFailureWaitTimeResponse object from the NCP</returns>
        public SetMacPollFailureWaitTimeResponse SetMacPollFailureWaitTime(uint waitBeforeRetryIntervalMs)
        {
            SetMacPollFailureWaitTimeRequest request = new SetMacPollFailureWaitTimeRequest();
            request.WaitBeforeRetryIntervalMs = waitBeforeRetryIntervalMs;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetMacPollFailureWaitTimeResponse)));
            SetMacPollFailureWaitTimeResponse response = (SetMacPollFailureWaitTimeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Returns the maximum number of no-ack retries that will be attempted
        /// </summary>
        /// <returns>Max MAC retries</returns>
        public byte GetMaxMacRetries()
        {
            GetMaxMacRetriesRequest request = new GetMaxMacRetriesRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetMaxMacRetriesResponse)));
            GetMaxMacRetriesResponse response = (GetMaxMacRetriesResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Retries;
        }

        /// <summary>
        /// Sets the priority masks and related variables for choosing the best beacon.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The attempt to set the pramaters returns SL_STATUS_OK
        /// - Param: Gets the beacon prioritization related variable
        /// </returns>
        public (Status Status, ZigbeeBeaconClassificationParams Param) SetBeaconClassificationParams()
        {
            SetBeaconClassificationParamsRequest request = new SetBeaconClassificationParamsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetBeaconClassificationParamsResponse)));
            SetBeaconClassificationParamsResponse response = (SetBeaconClassificationParamsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Param);
        }

        /// <summary>
        /// Gets the priority masks and related variables for choosing the best beacon.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The attempt to get the pramaters returns SL_STATUS_OK
        /// - Param: Gets the beacon prioritization related variable
        /// </returns>
        public (Status Status, ZigbeeBeaconClassificationParams Param) GetBeaconClassificationParams()
        {
            GetBeaconClassificationParamsRequest request = new GetBeaconClassificationParamsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetBeaconClassificationParamsResponse)));
            GetBeaconClassificationParamsResponse response = (GetBeaconClassificationParamsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Param);
        }

        /// <summary>
        /// Indicate whether there are pending messages in the APS retry queue.
        /// </summary>
        /// <returns>True if there is a pending message for this network in the APS retry queue, false if not.</returns>
        public bool PendingAckedMessages()
        {
            PendingAckedMessagesRequest request = new PendingAckedMessagesRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(PendingAckedMessagesResponse)));
            PendingAckedMessagesResponse response = (PendingAckedMessagesResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.PendingMessages;
        }

        /// <summary>
        /// Reschedule sending link status message, with first one being sent immediately.
        /// </summary>
        /// <returns></returns>
        public Status RescheduleLinkStatusMsg()
        {
            RescheduleLinkStatusMsgRequest request = new RescheduleLinkStatusMsgRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RescheduleLinkStatusMsgResponse)));
            RescheduleLinkStatusMsgResponse response = (RescheduleLinkStatusMsgResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set the network update ID to the desired value. Must be called before joining or forming the network.
        /// </summary>
        /// <param name="NwkUpdateId">Desired value of the network update ID.</param>
        /// <param name="SetWhenOnNetwork">Set to true in case change should also apply when on network.</param>
        /// <returns>Status of set operation for the network update ID.</returns>
        public Status SetNwkUpdateId(byte nwkUpdateId, bool setWhenOnNetwork)
        {
            SetNwkUpdateIdRequest request = new SetNwkUpdateIdRequest();
            request.NwkUpdateId = nwkUpdateId;
            request.SetWhenOnNetwork = setWhenOnNetwork;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetNwkUpdateIdResponse)));
            SetNwkUpdateIdResponse response = (SetNwkUpdateIdResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the security state that will be used by the device when it forms or joins the network. This call &lt;b&gt;should not&lt;/b&gt; be used when restoring saved network state via networkInit as this will result in a loss of security data and will cause communication problems when the device re-enters the network.
        /// </summary>
        /// <param name="State">The security configuration to be set.</param>
        /// <returns>The success or failure code of the operation.</returns>
        public Status SetInitialSecurityState(ZigbeeInitialSecurityState state)
        {
            SetInitialSecurityStateRequest request = new SetInitialSecurityStateRequest();
            request.State = state;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetInitialSecurityStateResponse)));
            SetInitialSecurityStateResponse response = (SetInitialSecurityStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Success;
        }

        /// <summary>
        /// Gets the current security state that is being used by a device that is joined in the network.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The success or failure code of the operation.
        /// - State: The security configuration in use by the stack.
        /// </returns>
        public (Status Status, ZigbeeCurrentSecurityState State) GetCurrentSecurityState()
        {
            GetCurrentSecurityStateRequest request = new GetCurrentSecurityStateRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetCurrentSecurityStateResponse)));
            GetCurrentSecurityStateResponse response = (GetCurrentSecurityStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.State);
        }

        /// <summary>
        /// Exports a key from security manager based on passed context.
        /// </summary>
        /// <param name="Context">Metadata to identify the requested key.</param>
        /// <returns>A tuple containing:
        /// - Status: The success or failure code of the operation.
        /// - Key: Data to store the exported key in.
        /// </returns>
        public (Status Status, ZigbeeSecManKey Key) SecManExportKey(ZigbeeSecManContext context)
        {
            SecManExportKeyRequest request = new SecManExportKeyRequest();
            request.Context = context;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManExportKeyResponse)));
            SecManExportKeyResponse response = (SecManExportKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Key);
        }

        /// <summary>
        /// Imports a key into security manager based on passed context.
        /// </summary>
        /// <param name="Context">Metadata to identify where the imported key should be stored.</param>
        /// <param name="Key">The key to be imported.</param>
        /// <returns>The success or failure code of the operation.</returns>
        public Status SecManImportKey(ZigbeeSecManContext context, ZigbeeSecManKey key)
        {
            SecManImportKeyRequest request = new SecManImportKeyRequest();
            request.Context = context;
            request.Key = key;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManImportKeyResponse)));
            SecManImportKeyResponse response = (SecManImportKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to inform the application that the Network Key has been updated and the node has been switched over to use the new key. The actual key being used is not passed up, but the sequence number is.
        /// </summary>
        /// <returns>The sequence number of the new network key.</returns>
        public byte SwitchNetworkKeyHandler()
        {
            SwitchNetworkKeyHandlerRequest request = new SwitchNetworkKeyHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SwitchNetworkKeyHandlerResponse)));
            SwitchNetworkKeyHandlerResponse response = (SwitchNetworkKeyHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.SequenceNumber;
        }

        /// <summary>
        /// This function searches through the Key Table and tries to find the entry that matches the passed search criteria.
        /// </summary>
        /// <param name="Address">The address to search for. Alternatively, all zeros may be passed in to search for the first empty entry.</param>
        /// <param name="LinkKey">This indicates whether to search for an entry that contains a link key or a master key. true means to search for an entry with a Link Key.</param>
        /// <returns>This indicates the index of the entry that matches the search criteria. A value of 0xFF is returned if not matching entry is found.</returns>
        public byte FindKeyTableEntry(byte[] address, bool linkKey)
        {
            FindKeyTableEntryRequest request = new FindKeyTableEntryRequest();
            request.Address = address;
            request.LinkKey = linkKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(FindKeyTableEntryResponse)));
            FindKeyTableEntryResponse response = (FindKeyTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Index;
        }

        /// <summary>
        /// This function sends an APS TransportKey command containing the current trust center link key. The node to which the command is sent is specified via the short and long address arguments.
        /// </summary>
        /// <param name="DestinationNodeId">The short address of the node to which this command will be sent</param>
        /// <param name="DestinationEui64">The long address of the node to which this command will be sent</param>
        /// <returns>An sl_status_t value indicating success of failure of the operation</returns>
        public Status SendTrustCenterLinkKey(ushort destinationNodeId, byte[] destinationEui64)
        {
            SendTrustCenterLinkKeyRequest request = new SendTrustCenterLinkKeyRequest();
            request.DestinationNodeId = destinationNodeId;
            request.DestinationEui64 = destinationEui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendTrustCenterLinkKeyResponse)));
            SendTrustCenterLinkKeyResponse response = (SendTrustCenterLinkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function erases the data in the key table entry at the specified index. If the index is invalid, false is returned.
        /// </summary>
        /// <param name="Index">This indicates the index of entry to erase.</param>
        /// <returns>The success or failure of the operation.</returns>
        public Status EraseKeyTableEntry(byte index)
        {
            EraseKeyTableEntryRequest request = new EraseKeyTableEntryRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(EraseKeyTableEntryResponse)));
            EraseKeyTableEntryResponse response = (EraseKeyTableEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function clears the key table of the current network.
        /// </summary>
        /// <returns>The success or failure of the operation.</returns>
        public Status ClearKeyTable()
        {
            ClearKeyTableRequest request = new ClearKeyTableRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearKeyTableResponse)));
            ClearKeyTableResponse response = (ClearKeyTableResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function to request a Link Key from the Trust Center with another device on the Network (which could be the Trust Center). A Link Key with the Trust Center is possible but the requesting device cannot be the Trust Center. Link Keys are optional in ZigBee Standard Security and thus the stack cannot know whether the other device supports them. If SL_ZIGBEE_REQUEST_KEY_TIMEOUT is non-zero on the Trust Center and the partner device is not the Trust Center, both devices must request keys with their partner device within the time period. The Trust Center only supports one outstanding key request at a time and therefore will ignore other requests. If the timeout is zero then the Trust Center will immediately respond and not wait for the second request. The Trust Center will always immediately respond to requests for a Link Key with it. Sleepy devices should poll at a higher rate until a response is received or the request times out. The success or failure of the request is returned via sl_zigbee_ezsp_zigbee_key_establishment_handler(...)
        /// </summary>
        /// <param name="Partner">This is the IEEE address of the partner device that will share the link key.</param>
        /// <returns>The success or failure of sending the request. This is not the final result of the attempt. sl_zigbee_ezsp_zigbee_key_establishment_handler(...) will return that.</returns>
        public Status RequestLinkKey(byte[] partner)
        {
            RequestLinkKeyRequest request = new RequestLinkKeyRequest();
            request.Partner = partner;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RequestLinkKeyResponse)));
            RequestLinkKeyResponse response = (RequestLinkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Requests a new link key from the Trust Center. This function starts by sending a Node Descriptor request to the Trust Center to verify its R21+ stack version compliance. A Request Key message will then be sent, followed by a Verify Key Confirm message.
        /// </summary>
        /// <param name="MaxAttempts">The maximum number of attempts a node should make when sending the Node Descriptor, Request Key, and Verify Key Confirm messages. The number of attempts resets for each message type sent (e.g., if maxAttempts is 3, up to 3 Node Descriptors are sent, up to 3 Request Keys, and up to 3 Verify Key Confirm messages are sent).</param>
        /// <returns>The success or failure of sending the request. If the Node Descriptor is successfully transmitted, sl_zigbee_ezsp_zigbee_key_establishment_handler(...) will be called at a later time with a final status result.</returns>
        public Status UpdateTcLinkKey(byte maxAttempts)
        {
            UpdateTcLinkKeyRequest request = new UpdateTcLinkKeyRequest();
            request.MaxAttempts = maxAttempts;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(UpdateTcLinkKeyResponse)));
            UpdateTcLinkKeyResponse response = (UpdateTcLinkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This is a callback that indicates the success or failure of an attempt to establish a key with a partner device.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Partner: This is the IEEE address of the partner that the device successfully established a key with. This value is all zeros on a failure.
        /// - Status: This is the status indicating what was established or why the key establishment failed.
        /// </returns>
        public (byte[] Partner, ZigbeeKeyStatus Status) ZigbeeKeyEstablishmentHandler()
        {
            ZigbeeKeyEstablishmentHandlerRequest request = new ZigbeeKeyEstablishmentHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZigbeeKeyEstablishmentHandlerResponse)));
            ZigbeeKeyEstablishmentHandlerResponse response = (ZigbeeKeyEstablishmentHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Partner, response.Status);
        }

        /// <summary>
        /// Clear all of the transient link keys from RAM.
        /// </summary>
        /// <returns>The ClearTransientLinkKeysResponse object from the NCP</returns>
        public ClearTransientLinkKeysResponse ClearTransientLinkKeys()
        {
            ClearTransientLinkKeysRequest request = new ClearTransientLinkKeysRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearTransientLinkKeysResponse)));
            ClearTransientLinkKeysResponse response = (ClearTransientLinkKeysResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Retrieve information about the current and alternate network key, excluding their contents.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: Success or failure of retrieving network key info.
        /// - NetworkKeyInfo: Information about current and alternate network keys.
        /// </returns>
        public (Status Status, ZigbeeSecManNetworkKeyInfo NetworkKeyInfo) SecManGetNetworkKeyInfo()
        {
            SecManGetNetworkKeyInfoRequest request = new SecManGetNetworkKeyInfoRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManGetNetworkKeyInfoResponse)));
            SecManGetNetworkKeyInfoResponse response = (SecManGetNetworkKeyInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.NetworkKeyInfo);
        }

        /// <summary>
        /// Retrieve metadata about an APS link key.  Does not retrieve contents.
        /// </summary>
        /// <param name="Context">Context used to input information about key.</param>
        /// <returns>A tuple containing:
        /// - Status: Status of metadata retrieval operation.
        /// - KeyData: Metadata about the referenced key.
        /// </returns>
        public (Status Status, ZigbeeSecManApsKeyMetadata KeyData) SecManGetApsKeyInfo(ZigbeeSecManContext context)
        {
            SecManGetApsKeyInfoRequest request = new SecManGetApsKeyInfoRequest();
            request.Context = context;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManGetApsKeyInfoResponse)));
            SecManGetApsKeyInfoResponse response = (SecManGetApsKeyInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.KeyData);
        }

        /// <summary>
        /// Import an application link key into the key table.
        /// </summary>
        /// <param name="Index">Index where this key is to be imported to.</param>
        /// <param name="Address">EUI64 this key is associated with.</param>
        /// <param name="PlaintextKey">The key data to be imported.</param>
        /// <returns>Status of key import operation.</returns>
        public Status SecManImportLinkKey(byte index, byte[] address, ZigbeeSecManKey plaintextKey)
        {
            SecManImportLinkKeyRequest request = new SecManImportLinkKeyRequest();
            request.Index = index;
            request.Address = address;
            request.PlaintextKey = plaintextKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManImportLinkKeyResponse)));
            SecManImportLinkKeyResponse response = (SecManImportLinkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Export the link key at given index from the key table.
        /// </summary>
        /// <param name="Index">Index of key to export.</param>
        /// <returns>A tuple containing:
        /// - Status: Status of key export operation.
        /// - Context: Context referencing the exported key.  Contains information like the EUI64 address it is associated with.
        /// - PlaintextKey: The exported key.
        /// - KeyData: Metadata about the key.
        /// </returns>
        public (Status Status, ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData) SecManExportLinkKeyByIndex(byte index)
        {
            SecManExportLinkKeyByIndexRequest request = new SecManExportLinkKeyByIndexRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManExportLinkKeyByIndexResponse)));
            SecManExportLinkKeyByIndexResponse response = (SecManExportLinkKeyByIndexResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Context, response.PlaintextKey, response.KeyData);
        }

        /// <summary>
        /// Export the link key associated with the given EUI from the key table.
        /// </summary>
        /// <param name="Eui">EUI64 associated with the key to export.</param>
        /// <returns>A tuple containing:
        /// - Status: Status of key export operation.
        /// - Context: Context referring to the exported key, containing the table index that this key is located in.
        /// - PlaintextKey: The exported key.
        /// - KeyData: Metadata about the key.
        /// </returns>
        public (Status Status, ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData) SecManExportLinkKeyByEui(byte[] eui)
        {
            SecManExportLinkKeyByEuiRequest request = new SecManExportLinkKeyByEuiRequest();
            request.Eui = eui;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManExportLinkKeyByEuiResponse)));
            SecManExportLinkKeyByEuiResponse response = (SecManExportLinkKeyByEuiResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Context, response.PlaintextKey, response.KeyData);
        }

        /// <summary>
        /// Check whether a key context can be used to load a valid key.
        /// </summary>
        /// <param name="Context">Context struct to check the validity of.</param>
        /// <returns>Validity of the checked context.</returns>
        public Status SecManCheckKeyContext(ZigbeeSecManContext context)
        {
            SecManCheckKeyContextRequest request = new SecManCheckKeyContextRequest();
            request.Context = context;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManCheckKeyContextResponse)));
            SecManCheckKeyContextResponse response = (SecManCheckKeyContextResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Import a transient link key.
        /// </summary>
        /// <param name="Eui64">EUI64 associated with this transient key.</param>
        /// <param name="PlaintextKey">The key to import.</param>
        /// <returns>Status of key import operation.</returns>
        public Status SecManImportTransientKey(byte[] eui64, ZigbeeSecManKey plaintextKey)
        {
            SecManImportTransientKeyRequest request = new SecManImportTransientKeyRequest();
            request.Eui64 = eui64;
            request.PlaintextKey = plaintextKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManImportTransientKeyResponse)));
            SecManImportTransientKeyResponse response = (SecManImportTransientKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Export a transient link key from a given table index.
        /// </summary>
        /// <param name="Index">Index to export from.</param>
        /// <returns>A tuple containing:
        /// - Status: Status of key export operation.
        /// - Context: Context struct for export operation.
        /// - PlaintextKey: The exported key.
        /// - KeyData: Metadata about the key.
        /// </returns>
        public (Status Status, ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData) SecManExportTransientKeyByIndex(byte index)
        {
            SecManExportTransientKeyByIndexRequest request = new SecManExportTransientKeyByIndexRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManExportTransientKeyByIndexResponse)));
            SecManExportTransientKeyByIndexResponse response = (SecManExportTransientKeyByIndexResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Context, response.PlaintextKey, response.KeyData);
        }

        /// <summary>
        /// Export a transient link key associated with a given EUI64
        /// </summary>
        /// <param name="Eui">Index to export from.</param>
        /// <returns>A tuple containing:
        /// - Status: Status of key export operation.
        /// - Context: Context struct for export operation.
        /// - PlaintextKey: The exported key.
        /// - KeyData: Metadata about the key.
        /// </returns>
        public (Status Status, ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData) SecManExportTransientKeyByEui(byte[] eui)
        {
            SecManExportTransientKeyByEuiRequest request = new SecManExportTransientKeyByEuiRequest();
            request.Eui = eui;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SecManExportTransientKeyByEuiResponse)));
            SecManExportTransientKeyByEuiResponse response = (SecManExportTransientKeyByEuiResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Context, response.PlaintextKey, response.KeyData);
        }

        /// <summary>
        /// Set the incoming TC link key frame counter to desired value.
        /// </summary>
        /// <param name="FrameCounter">Value to set the frame counter to.</param>
        /// <returns>The SetIncomingTcLinkKeyFrameCounterResponse object from the NCP</returns>
        public SetIncomingTcLinkKeyFrameCounterResponse SetIncomingTcLinkKeyFrameCounter(uint frameCounter)
        {
            SetIncomingTcLinkKeyFrameCounterRequest request = new SetIncomingTcLinkKeyFrameCounterRequest();
            request.FrameCounter = frameCounter;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetIncomingTcLinkKeyFrameCounterResponse)));
            SetIncomingTcLinkKeyFrameCounterResponse response = (SetIncomingTcLinkKeyFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Encrypt/decrypt a message in-place using APS.
        /// </summary>
        /// <param name="Encrypt">Encrypt (true) or decrypt (false) the message.</param>
        /// <param name="LengthCombinedArg">Length of the array containing message, needs to be long enough to include the auxiliary header and MIC.</param>
        /// <param name="Message">The message to be en/de-crypted.</param>
        /// <param name="ApsHeaderEndIndex">Index just past the APS frame.</param>
        /// <param name="RemoteEui64">IEEE address of the device this message is associated with.</param>
        /// <returns>Status of the encryption/decryption call.</returns>
        public Status ApsCryptMessage(bool encrypt, byte lengthCombinedArg, byte[] message, byte apsHeaderEndIndex, byte[] remoteEui64)
        {
            ApsCryptMessageRequest request = new ApsCryptMessageRequest();
            request.Encrypt = encrypt;
            request.LengthCombinedArg = lengthCombinedArg;
            request.Message = message;
            request.ApsHeaderEndIndex = apsHeaderEndIndex;
            request.RemoteEui64 = remoteEui64;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ApsCryptMessageResponse)));
            ApsCryptMessageResponse response = (ApsCryptMessageResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// The NCP uses the trust center behavior policy to decide whether to allow a new node to join the network (part of the trust center pre-join handler). The Host cannot change the current decision in this post-join callback, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
        /// </summary>
        /// <returns>A tuple containing:
        /// - NewNodeId: The Node Id of the node whose status changed
        /// - NewNodeEui64: The EUI64 of the node whose status changed.
        /// - Status: The status of the node: Secure Join/Rejoin, Unsecure Join/Rejoin, Device left.
        /// - PolicyDecision: An sl_zigbee_join_decision_t reflecting the decision made.
        /// - ParentOfNewNodeId: The parent of the node whose status has changed.
        /// </returns>
        public (ushort NewNodeId, byte[] NewNodeEui64, ZigbeeDeviceUpdate Status, ZigbeeJoinDecision PolicyDecision, ushort ParentOfNewNodeId) TrustCenterPostJoinHandler()
        {
            TrustCenterPostJoinHandlerRequest request = new TrustCenterPostJoinHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(TrustCenterPostJoinHandlerResponse)));
            TrustCenterPostJoinHandlerResponse response = (TrustCenterPostJoinHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.NewNodeId, response.NewNodeEui64, response.Status, response.PolicyDecision, response.ParentOfNewNodeId);
        }

        /// <summary>
        /// This function broadcasts a new encryption key, but does not tell the nodes in the network to start using it. To tell nodes to switch to the new key, use sl_zigbee_send_network_key_switch(). This is only valid for the Trust Center/Coordinator. It is up to the application to determine how quickly to send the Switch Key after sending the alternate encryption key.
        /// </summary>
        /// <param name="Key">An optional pointer to a 16-byte encryption key (SL_ZIGBEE_ENCRYPTION_KEY_SIZE). An all zero key may be passed in, which will cause the stack to randomly generate a new key.</param>
        /// <returns>sl_status_t value that indicates the success or failure of the command.</returns>
        public Status BroadcastNextNetworkKey(ZigbeeKeyData key)
        {
            BroadcastNextNetworkKeyRequest request = new BroadcastNextNetworkKeyRequest();
            request.Key = key;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(BroadcastNextNetworkKeyResponse)));
            BroadcastNextNetworkKeyResponse response = (BroadcastNextNetworkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function broadcasts a switch key message to tell all nodes to change to the sequence number of the previously sent Alternate Encryption Key.
        /// </summary>
        /// <returns>sl_status_t value that indicates the success or failure of the command.</returns>
        public Status BroadcastNetworkKeySwitch()
        {
            BroadcastNetworkKeySwitchRequest request = new BroadcastNetworkKeySwitchRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(BroadcastNetworkKeySwitchResponse)));
            BroadcastNetworkKeySwitchResponse response = (BroadcastNetworkKeySwitchResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This routine processes the passed chunk of data and updates the hash context based on it. If the &apos;finalize&apos; parameter is not set, then the length of the data passed in must be a multiple of 16. If the &apos;finalize&apos; parameter is set then the length can be any value up 1-16, and the final hash value will be calculated.
        /// </summary>
        /// <param name="Context">The hash context to update.</param>
        /// <param name="Finalize">This indicates whether the final hash value should be calculated</param>
        /// <param name="Length">The length of the data to hash.</param>
        /// <param name="Data">The data to hash.</param>
        /// <returns>A tuple containing:
        /// - Status: The result of the operation
        /// - ReturnContext: The updated hash context.
        /// </returns>
        public (Status Status, ZigbeeAesMmoHashContext ReturnContext) AesMmoHash(ZigbeeAesMmoHashContext context, bool finalize, byte length, byte[] data)
        {
            AesMmoHashRequest request = new AesMmoHashRequest();
            request.Context = context;
            request.Finalize = finalize;
            request.Length = length;
            request.Data = data;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(AesMmoHashResponse)));
            AesMmoHashResponse response = (AesMmoHashResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.ReturnContext);
        }

        /// <summary>
        /// This command sends an APS remove device using APS encryption to the destination indicating either to remove itself from the network, or one of its children.
        /// </summary>
        /// <param name="DestShort">The node ID of the device that will receive the message</param>
        /// <param name="DestLong">The long address (EUI64) of the device that will receive the message.</param>
        /// <param name="TargetLong">The long address (EUI64) of the device to be removed.</param>
        /// <returns>An sl_status_t value indicating success, or the reason for failure</returns>
        public Status RemoveDevice(ushort destShort, byte[] destLong, byte[] targetLong)
        {
            RemoveDeviceRequest request = new RemoveDeviceRequest();
            request.DestShort = destShort;
            request.DestLong = destLong;
            request.TargetLong = targetLong;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(RemoveDeviceResponse)));
            RemoveDeviceResponse response = (RemoveDeviceResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This command will send a unicast transport key message with a new NWK key to the specified device. APS encryption using the device&apos;s existing link key will be used.
        /// </summary>
        /// <param name="DestShort">The node ID of the device that will receive the message</param>
        /// <param name="DestLong">The long address (EUI64) of the device that will receive the message.</param>
        /// <param name="Key">The NWK key to send to the new device.</param>
        /// <returns>An sl_status_t value indicating success, or the reason for failure</returns>
        public Status UnicastNwkKeyUpdate(ushort destShort, byte[] destLong, ZigbeeKeyData key)
        {
            UnicastNwkKeyUpdateRequest request = new UnicastNwkKeyUpdateRequest();
            request.DestShort = destShort;
            request.DestLong = destLong;
            request.Key = key;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(UnicastNwkKeyUpdateResponse)));
            UnicastNwkKeyUpdateResponse response = (UnicastNwkKeyUpdateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call starts the generation of the ECC Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_handler().
        /// </summary>
        /// <returns></returns>
        public Status GenerateCbkeKeys()
        {
            GenerateCbkeKeysRequest request = new GenerateCbkeKeysRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GenerateCbkeKeysResponse)));
            GenerateCbkeKeysResponse response = (GenerateCbkeKeysResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback by the Crypto Engine indicating that a new ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The result of the CBKE operation.
        /// - EphemeralPublicKey: The generated ephemeral public key.
        /// </returns>
        public (Status Status, ZigbeePublicKeyData EphemeralPublicKey) GenerateCbkeKeysHandler()
        {
            GenerateCbkeKeysHandlerRequest request = new GenerateCbkeKeysHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GenerateCbkeKeysHandlerResponse)));
            GenerateCbkeKeysHandlerResponse response = (GenerateCbkeKeysHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.EphemeralPublicKey);
        }

        /// <summary>
        /// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE using the passed parameters and the stored public/private key pair previously generated with ezspGenerateKeysRetrieveCert(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
        /// </summary>
        /// <param name="AmInitiator">The role of this device in the Key Establishment protocol.</param>
        /// <param name="PartnerCertificate">The key establishment partner&apos;s implicit certificate.</param>
        /// <param name="PartnerEphemeralPublicKey">The key establishment partner&apos;s ephemeral public key</param>
        /// <returns></returns>
        public Status CalculateSmacs(bool amInitiator, ZigbeeCertificateData partnerCertificate, ZigbeePublicKeyData partnerEphemeralPublicKey)
        {
            CalculateSmacsRequest request = new CalculateSmacsRequest();
            request.AmInitiator = amInitiator;
            request.PartnerCertificate = partnerCertificate;
            request.PartnerEphemeralPublicKey = partnerEphemeralPublicKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CalculateSmacsResponse)));
            CalculateSmacsResponse response = (CalculateSmacsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to indicate that the NCP has finished calculating the Secure Message Authentication Codes (SMAC) for both the initiator and responder. The associated link key is kept in temporary storage until the host tells the NCP to store or discard the key via sli_zigbee_stack_clear_temporary_data_maybe_store_link_key().
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The Result of the CBKE operation.
        /// - InitiatorSmac: The calculated value of the initiator's SMAC
        /// - ResponderSmac: The calculated value of the responder's SMAC
        /// </returns>
        public (Status Status, ZigbeeSmacData InitiatorSmac, ZigbeeSmacData ResponderSmac) CalculateSmacsHandler()
        {
            CalculateSmacsHandlerRequest request = new CalculateSmacsHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CalculateSmacsHandlerResponse)));
            CalculateSmacsHandlerResponse response = (CalculateSmacsHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.InitiatorSmac, response.ResponderSmac);
        }

        /// <summary>
        /// This call starts the generation of the ECC 283k1 curve Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_283k1_handler().
        /// </summary>
        /// <returns></returns>
        public Status GenerateCbkeKeys283k1()
        {
            GenerateCbkeKeys283k1Request request = new GenerateCbkeKeys283k1Request();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GenerateCbkeKeys283k1Response)));
            GenerateCbkeKeys283k1Response response = (GenerateCbkeKeys283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback by the Crypto Engine indicating that a new 283k1 ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The result of the CBKE operation.
        /// - EphemeralPublicKey: The generated ephemeral public key.
        /// </returns>
        public (Status Status, ZigbeePublicKey283k1Data EphemeralPublicKey) GenerateCbkeKeys283k1Handler()
        {
            GenerateCbkeKeys283k1HandlerRequest request = new GenerateCbkeKeys283k1HandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GenerateCbkeKeys283k1HandlerResponse)));
            GenerateCbkeKeys283k1HandlerResponse response = (GenerateCbkeKeys283k1HandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.EphemeralPublicKey);
        }

        /// <summary>
        /// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE for the 283k1 ECC curve using the passed parameters and the stored public/private key pair previously generated with sl_zigbee_ezsp_generate_keys_retrieve_cert_283k1(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
        /// </summary>
        /// <param name="AmInitiator">The role of this device in the Key Establishment protocol.</param>
        /// <param name="PartnerCertificate">The key establishment partner&apos;s implicit certificate.</param>
        /// <param name="PartnerEphemeralPublicKey">The key establishment partner&apos;s ephemeral public key</param>
        /// <returns></returns>
        public Status CalculateSmacs283k1(bool amInitiator, ZigbeeCertificate283k1Data partnerCertificate, ZigbeePublicKey283k1Data partnerEphemeralPublicKey)
        {
            CalculateSmacs283k1Request request = new CalculateSmacs283k1Request();
            request.AmInitiator = amInitiator;
            request.PartnerCertificate = partnerCertificate;
            request.PartnerEphemeralPublicKey = partnerEphemeralPublicKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CalculateSmacs283k1Response)));
            CalculateSmacs283k1Response response = (CalculateSmacs283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to indicate that the NCP has finished calculating the Secure Message Authentication Codes (SMAC) for both the initiator and responder for the CBKE 283k1 Library. The associated link key is kept in temporary storage until the host tells the NCP to store or discard the key via sli_zigbee_stack_clear_temporary_data_maybe_store_link_key().
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The Result of the CBKE operation.
        /// - InitiatorSmac: The calculated value of the initiator's SMAC
        /// - ResponderSmac: The calculated value of the responder's SMAC
        /// </returns>
        public (Status Status, ZigbeeSmacData InitiatorSmac, ZigbeeSmacData ResponderSmac) CalculateSmacs283k1Handler()
        {
            CalculateSmacs283k1HandlerRequest request = new CalculateSmacs283k1HandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(CalculateSmacs283k1HandlerResponse)));
            CalculateSmacs283k1HandlerResponse response = (CalculateSmacs283k1HandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.InitiatorSmac, response.ResponderSmac);
        }

        /// <summary>
        /// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
        /// </summary>
        /// <param name="StoreLinkKey">A bool indicating whether to store (true) or discard (false) the unverified link key derived when sl_zigbee_ezsp_calculate_smacs() was previously called.</param>
        /// <returns></returns>
        public Status ClearTemporaryDataMaybeStoreLinkKey(bool storeLinkKey)
        {
            ClearTemporaryDataMaybeStoreLinkKeyRequest request = new ClearTemporaryDataMaybeStoreLinkKeyRequest();
            request.StoreLinkKey = storeLinkKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearTemporaryDataMaybeStoreLinkKeyResponse)));
            ClearTemporaryDataMaybeStoreLinkKeyResponse response = (ClearTemporaryDataMaybeStoreLinkKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
        /// </summary>
        /// <param name="StoreLinkKey">A bool indicating whether to store (true) or discard (false) the unverified link key derived when sl_zigbee_ezsp_calculate_smacs() was previously called.</param>
        /// <returns></returns>
        public Status ClearTemporaryDataMaybeStoreLinkKey283k1(bool storeLinkKey)
        {
            ClearTemporaryDataMaybeStoreLinkKey283k1Request request = new ClearTemporaryDataMaybeStoreLinkKey283k1Request();
            request.StoreLinkKey = storeLinkKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ClearTemporaryDataMaybeStoreLinkKey283k1Response)));
            ClearTemporaryDataMaybeStoreLinkKey283k1Response response = (ClearTemporaryDataMaybeStoreLinkKey283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Retrieves the certificate installed on the NCP.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: 
        /// - LocalCert: The locally installed certificate.
        /// </returns>
        public (Status Status, ZigbeeCertificateData LocalCert) GetCertificate()
        {
            GetCertificateRequest request = new GetCertificateRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetCertificateResponse)));
            GetCertificateResponse response = (GetCertificateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.LocalCert);
        }

        /// <summary>
        /// Retrieves the 283k certificate installed on the NCP.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: 
        /// - LocalCert: The locally installed certificate.
        /// </returns>
        public (Status Status, ZigbeeCertificate283k1Data LocalCert) GetCertificate283k1()
        {
            GetCertificate283k1Request request = new GetCertificate283k1Request();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetCertificate283k1Response)));
            GetCertificate283k1Response response = (GetCertificate283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.LocalCert);
        }

        /// <summary>
        /// LEGACY FUNCTION: This functionality has been replaced by a single bit in the sl_zigbee_aps_frame_t, SL_ZIGBEE_APS_OPTION_DSA_SIGN. Devices wishing to send signed messages should use that as it requires fewer function calls and message buffering. The dsaSignHandler response is still called when SL_ZIGBEE_APS_OPTION_DSA_SIGN is used. However, this function is still supported. This function begins the process of signing the passed message contained within the messageContents array. If no other ECC operation is going on, it will immediately return with SL_STATUS_IN_PROGRESS to indicate the start of ECC operation. It will delay a period of time to let APS retries take place, but then it will shut down the radio and consume the CPU processing until the signing is complete. This may take up to 1 second. The signed message will be returned in the dsaSignHandler response. Note that the last byte of the messageContents passed to this function has special significance. As the typical use case for DSA signing is to sign the ZCL payload of a DRLC Report Event Status message in SE 1.0, there is often both a signed portion (ZCL payload) and an unsigned portion (ZCL header). The last byte in the content of messageToSign is therefore used as a special indicator to signify how many bytes of leading data in the array should be excluded from consideration during the signing process. If the signature needs to cover the entire array (all bytes except last one), the caller should ensure that the last byte of messageContents is 0x00. When the signature operation is complete, this final byte will be replaced by the signature type indicator (0x01 for ECDSA signatures), and the actual signature will be appended to the original contents after this byte.
        /// </summary>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The message contents for which to create a signature. Per above notes, this may include a leading portion of data not included in the signature, in which case the last byte of this array should be set to the index of the first byte to be considered for signing. Otherwise, the last byte of messageContents should be 0x00 to indicate that a signature should occur across the entire contents.</param>
        /// <returns>SL_STATUS_IN_PROGRESS if the stack has queued up the operation for execution. SL_STATUS_INVALID_STATE if the operation can&apos;t be performed in this context, possibly because another ECC operation is pending.</returns>
        public Status DsaSign(byte messageLength, byte[] messageContents)
        {
            DsaSignRequest request = new DsaSignRequest();
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DsaSignResponse)));
            DsaSignResponse response = (DsaSignResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// The handler that returns the results of the signing operation. On success, the signature will be appended to the original message (including the signature type indicator that replaced the startIndex field for the signing) and both are returned via this callback.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The result of the DSA signing operation.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The message and attached which includes the original message and the appended signature.
        /// </returns>
        public (Status Status, byte MessageLength, byte[] MessageContents) DsaSignHandler()
        {
            DsaSignHandlerRequest request = new DsaSignHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DsaSignHandlerResponse)));
            DsaSignHandlerResponse response = (DsaSignHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
        /// </summary>
        /// <param name="Digest">The AES-MMO message digest of the signed data. If dsaSign command was used to generate the signature for this data, the final byte (replaced by signature type of 0x01) in the messageContents array passed to dsaSign is included in the hash context used for the digest calculation.</param>
        /// <param name="SignerCertificate">The certificate of the signer. Note that the signer&apos;s certificate and the verifier&apos;s certificate must both be issued by the same Certificate Authority, so they should share the same CA Public Key.</param>
        /// <param name="ReceivedSig">The signature of the signed data.</param>
        /// <returns></returns>
        public Status DsaVerify(ZigbeeMessageDigest digest, ZigbeeCertificateData signerCertificate, ZigbeeSignatureData receivedSig)
        {
            DsaVerifyRequest request = new DsaVerifyRequest();
            request.Digest = digest;
            request.SignerCertificate = signerCertificate;
            request.ReceivedSig = receivedSig;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DsaVerifyResponse)));
            DsaVerifyResponse response = (DsaVerifyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This callback is executed by the stack when the DSA verification has completed and has a result. If the result is SL_STATUS_OK, the signature is valid. If the result is SL_STATUS_ZIGBEE_SIGNATURE_VERIFY_FAILURE then the signature is invalid. If the result is anything else then the signature verify operation failed and the validity is unknown.
        /// </summary>
        /// <returns>The result of the DSA verification operation.</returns>
        public Status DsaVerifyHandler()
        {
            DsaVerifyHandlerRequest request = new DsaVerifyHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DsaVerifyHandlerResponse)));
            DsaVerifyHandlerResponse response = (DsaVerifyHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
        /// </summary>
        /// <param name="Digest">The AES-MMO message digest of the signed data. If dsaSign command was used to generate the signature for this data, the final byte (replaced by signature type of 0x01) in the messageContents array passed to dsaSign is included in the hash context used for the digest calculation.</param>
        /// <param name="SignerCertificate">The certificate of the signer. Note that the signer&apos;s certificate and the verifier&apos;s certificate must both be issued by the same Certificate Authority, so they should share the same CA Public Key.</param>
        /// <param name="ReceivedSig">The signature of the signed data.</param>
        /// <returns></returns>
        public Status DsaVerify283k1(ZigbeeMessageDigest digest, ZigbeeCertificate283k1Data signerCertificate, ZigbeeSignature283k1Data receivedSig)
        {
            DsaVerify283k1Request request = new DsaVerify283k1Request();
            request.Digest = digest;
            request.SignerCertificate = signerCertificate;
            request.ReceivedSig = receivedSig;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DsaVerify283k1Response)));
            DsaVerify283k1Response response = (DsaVerify283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the device&apos;s CA public key, local certificate, and static private key on the NCP associated with this node.
        /// </summary>
        /// <param name="CaPublic">The Certificate Authority&apos;s public key.</param>
        /// <param name="MyCert">The node&apos;s new certificate signed by the CA.</param>
        /// <param name="MyKey">The node&apos;s new static private key.</param>
        /// <returns></returns>
        public Status SetPreinstalledCbkeData(ZigbeePublicKeyData caPublic, ZigbeeCertificateData myCert, ZigbeePrivateKeyData myKey)
        {
            SetPreinstalledCbkeDataRequest request = new SetPreinstalledCbkeDataRequest();
            request.CaPublic = caPublic;
            request.MyCert = myCert;
            request.MyKey = myKey;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetPreinstalledCbkeDataResponse)));
            SetPreinstalledCbkeDataResponse response = (SetPreinstalledCbkeDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the device&apos;s 283k1 curve CA public key, local certificate, and static private key on the NCP associated with this node.
        /// </summary>
        /// <returns></returns>
        public Status SavePreinstalledCbkeData283k1()
        {
            SavePreinstalledCbkeData283k1Request request = new SavePreinstalledCbkeData283k1Request();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SavePreinstalledCbkeData283k1Response)));
            SavePreinstalledCbkeData283k1Response response = (SavePreinstalledCbkeData283k1Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Activate use of mfglib test routines and enables the radio receiver to report packets it receives to the mfgLibRxHandler() callback. These packets will not be passed up with a CRC failure. All other mfglib functions will return an error until the mfglibInternalStart() has been called
        /// </summary>
        /// <param name="RxCallback">true to generate a mfglibRxHandler callback when a packet is received.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalStart(bool rxCallback)
        {
            MfglibInternalStartRequest request = new MfglibInternalStartRequest();
            request.RxCallback = rxCallback;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalStartResponse)));
            MfglibInternalStartResponse response = (MfglibInternalStartResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Deactivate use of mfglib test routines; restores the hardware to the state it was in prior to mfglibInternalStart() and stops receiving packets started by mfglibInternalStart() at the same time.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalEnd()
        {
            MfglibInternalEndRequest request = new MfglibInternalEndRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalEndResponse)));
            MfglibInternalEndResponse response = (MfglibInternalEndResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts transmitting an unmodulated tone on the currently set channel and power level. Upon successful return, the tone will be transmitting. To stop transmitting tone, application must call mfglibInternalStopTone(), allowing it the flexibility to determine its own criteria for tone duration (time, event, etc.)
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalStartTone()
        {
            MfglibInternalStartToneRequest request = new MfglibInternalStartToneRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalStartToneResponse)));
            MfglibInternalStartToneResponse response = (MfglibInternalStartToneResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Stops transmitting tone started by mfglibInternalStartTone().
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalStopTone()
        {
            MfglibInternalStopToneRequest request = new MfglibInternalStopToneRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalStopToneResponse)));
            MfglibInternalStopToneResponse response = (MfglibInternalStopToneResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts transmitting a random stream of characters. This is so that the radio modulation can be measured.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalStartStream()
        {
            MfglibInternalStartStreamRequest request = new MfglibInternalStartStreamRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalStartStreamResponse)));
            MfglibInternalStartStreamResponse response = (MfglibInternalStartStreamResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Stops transmitting a random stream of characters started by mfglibInternalStartStream().
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalStopStream()
        {
            MfglibInternalStopStreamRequest request = new MfglibInternalStopStreamRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalStopStreamResponse)));
            MfglibInternalStopStreamResponse response = (MfglibInternalStopStreamResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sends a single packet consisting of the following bytes: packetLength, packetContents[0], ... , packetContents[packetLength - 3], CRC[0], CRC[1]. The total number of bytes sent is packetLength + 1. The radio replaces the last two bytes of packetContents[] with the 16-bit CRC for the packet.
        /// </summary>
        /// <param name="PacketLength">The length of the packetContents parameter in bytes. Must be greater than 3 and less than 123.</param>
        /// <param name="PacketContents">The packet to send. The last two bytes will be replaced with the 16-bit CRC.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalSendPacket(byte packetLength, byte[] packetContents)
        {
            MfglibInternalSendPacketRequest request = new MfglibInternalSendPacketRequest();
            request.PacketLength = packetLength;
            request.PacketContents = packetContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalSendPacketResponse)));
            MfglibInternalSendPacketResponse response = (MfglibInternalSendPacketResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the radio channel. Calibration occurs if this is the first time the channel has been used.
        /// </summary>
        /// <param name="Channel">The channel to switch to. Valid values are 11 to 26.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalSetChannel(byte channel)
        {
            MfglibInternalSetChannelRequest request = new MfglibInternalSetChannelRequest();
            request.Channel = channel;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalSetChannelResponse)));
            MfglibInternalSetChannelResponse response = (MfglibInternalSetChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns the current radio channel, as previously set via mfglibInternalSetChannel().
        /// </summary>
        /// <returns>The current channel.</returns>
        public byte MfglibInternalGetChannel()
        {
            MfglibInternalGetChannelRequest request = new MfglibInternalGetChannelRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalGetChannelResponse)));
            MfglibInternalGetChannelResponse response = (MfglibInternalGetChannelResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Channel;
        }

        /// <summary>
        /// First select the transmit power mode, and then include a method for selecting the radio transmit power. The valid power settings depend upon the specific radio in use. Ember radios have discrete power settings, and then requested power is rounded to a valid power setting; the actual power output is available to the caller via mfglibInternalGetPower().
        /// </summary>
        /// <param name="TxPowerMode">Power mode. Refer to txPowerModes in stack/include/sl_zigbee_types.h for possible values.</param>
        /// <param name="Power">Power in units of dBm. Refer to radio data sheet for valid range.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status MfglibInternalSetPower(ushort txPowerMode, sbyte power)
        {
            MfglibInternalSetPowerRequest request = new MfglibInternalSetPowerRequest();
            request.TxPowerMode = txPowerMode;
            request.Power = power;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalSetPowerResponse)));
            MfglibInternalSetPowerResponse response = (MfglibInternalSetPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns the current radio power setting, as previously set via mfglibInternalSetPower().
        /// </summary>
        /// <returns>Power in units of dBm. Refer to radio data sheet for valid range.</returns>
        public sbyte MfglibInternalGetPower()
        {
            MfglibInternalGetPowerRequest request = new MfglibInternalGetPowerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibInternalGetPowerResponse)));
            MfglibInternalGetPowerResponse response = (MfglibInternalGetPowerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Power;
        }

        /// <summary>
        /// A callback indicating a packet with a valid CRC has been received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - LinkQuality: The link quality observed during the reception
        /// - Rssi: The energy level (in units of dBm) observed during the reception.
        /// - PacketLength: The length of the packetContents parameter in bytes. Will be greater than 3 and less than 123.
        /// - PacketContents: The received packet (last 2 bytes are not FCS / CRC and may be discarded)
        /// </returns>
        public (byte LinkQuality, sbyte Rssi, byte PacketLength, byte[] PacketContents) MfglibRxHandler()
        {
            MfglibRxHandlerRequest request = new MfglibRxHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfglibRxHandlerResponse)));
            MfglibRxHandlerResponse response = (MfglibRxHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.LinkQuality, response.Rssi, response.PacketLength, response.PacketContents);
        }

        /// <summary>
        /// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
        /// </summary>
        /// <param name="Enabled">If true, launch the standalone bootloader. If false, do nothing.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status LaunchStandaloneBootloader(bool enabled)
        {
            LaunchStandaloneBootloaderRequest request = new LaunchStandaloneBootloaderRequest();
            request.Enabled = enabled;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(LaunchStandaloneBootloaderResponse)));
            LaunchStandaloneBootloaderResponse response = (LaunchStandaloneBootloaderResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Transmits the given bootload message to a neighboring node using a specific 802.15.4 header that allows the EmberZNet stack as well as the bootloader to recognize the message, but will not interfere with other ZigBee stacks.
        /// </summary>
        /// <param name="Broadcast">If true, the destination address and pan id are both set to the broadcast address.</param>
        /// <param name="DestEui64">The EUI64 of the target node. Ignored if the broadcast field is set to true.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The multicast message.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SendBootloadMessage(bool broadcast, byte[] destEui64, byte messageLength, byte[] messageContents)
        {
            SendBootloadMessageRequest request = new SendBootloadMessageRequest();
            request.Broadcast = broadcast;
            request.DestEui64 = destEui64;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SendBootloadMessageResponse)));
            SendBootloadMessageResponse response = (SendBootloadMessageResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Detects if the standalone bootloader is installed, and if so returns the installed version. If not return 0xffff. A returned version of 0x1234 would indicate version 1.2 build 34. Also return the node&apos;s version of PLAT, MICRO and PHY.
        /// </summary>
        /// <returns>A tuple containing:
        /// - BootloaderVersion: BOOTLOADER_INVALID_VERSION if the standalone bootloader is not present, or the version of the installed standalone bootloader.
        /// - NodePlat: The value of PLAT on the node
        /// - NodeMicro: The value of MICRO on the node
        /// - NodePhy: The value of PHY on the node
        /// </returns>
        public (ushort BootloaderVersion, byte NodePlat, byte NodeMicro, byte NodePhy) GetStandaloneBootloaderVersionPlatMicroPhy()
        {
            GetStandaloneBootloaderVersionPlatMicroPhyRequest request = new GetStandaloneBootloaderVersionPlatMicroPhyRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetStandaloneBootloaderVersionPlatMicroPhyResponse)));
            GetStandaloneBootloaderVersionPlatMicroPhyResponse response = (GetStandaloneBootloaderVersionPlatMicroPhyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.BootloaderVersion, response.NodePlat, response.NodeMicro, response.NodePhy);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when a bootload message is received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - LongId: The EUI64 of the sending node.
        /// - PacketInfo: Information about the incoming packet.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The bootload message that was sent.
        /// </returns>
        public (byte[] LongId, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents) IncomingBootloadMessageHandler()
        {
            IncomingBootloadMessageHandlerRequest request = new IncomingBootloadMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingBootloadMessageHandlerResponse)));
            IncomingBootloadMessageHandlerResponse response = (IncomingBootloadMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.LongId, response.PacketInfo, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a bootload message.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value of SL_STATUS_OK if an ACK was received from the destination or SL_STATUS_ZIGBEE_DELIVERY_FAILED if no ACK was received.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The message that was sent.
        /// </returns>
        public (Status Status, byte MessageLength, byte[] MessageContents) BootloadTransmitCompleteHandler()
        {
            BootloadTransmitCompleteHandlerRequest request = new BootloadTransmitCompleteHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(BootloadTransmitCompleteHandlerResponse)));
            BootloadTransmitCompleteHandlerResponse response = (BootloadTransmitCompleteHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// Perform AES encryption on plaintext using key.
        /// </summary>
        /// <param name="Plaintext">16 bytes of plaintext.</param>
        /// <param name="Key">The 16-byte encryption key to use.</param>
        /// <returns>16 bytes of ciphertext.</returns>
        public byte[] AesEncrypt(byte[] plaintext, byte[] key)
        {
            AesEncryptRequest request = new AesEncryptRequest();
            request.Plaintext = plaintext;
            request.Key = key;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(AesEncryptResponse)));
            AesEncryptResponse response = (AesEncryptResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Ciphertext;
        }

        /// <summary>
        /// A callback to be implemented on the Golden Node to process acknowledgements. If you supply a custom version of this handler, you must define SL_ZIGBEE_APPLICATION_HAS_INCOMING_MFG_TEST_MESSAGE_HANDLER in your application&apos;s CONFIGURATION_HEADER
        /// </summary>
        /// <returns>A tuple containing:
        /// - MessageType: The type of the incoming message. Currently, the only possibility is MFG_TEST_TYPE_ACK.
        /// - DataLength: The length of the incoming message.
        /// - Data: A pointer to the data received in the current message.
        /// </returns>
        public (byte MessageType, byte DataLength, byte[] Data) IncomingMfgTestMessageHandler()
        {
            IncomingMfgTestMessageHandlerRequest request = new IncomingMfgTestMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IncomingMfgTestMessageHandlerResponse)));
            IncomingMfgTestMessageHandlerResponse response = (IncomingMfgTestMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.MessageType, response.DataLength, response.Data);
        }

        /// <summary>
        /// A function used on the Golden Node to switch between normal network operation (for testing) and manufacturing configuration. Like emberSleep(), it may not be possible to execute this command due to pending network activity. For the transition from normal network operation to manufacturing configuration, it is customary to loop, calling this function alternately with emberTick() until the mode change succeeds.
        /// </summary>
        /// <param name="BeginConfiguration">Determines the new mode of operation. true causes the node to enter manufacturing configuration. false causes the node to return to normal network operation.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSetPacketMode(bool beginConfiguration)
        {
            MfgTestSetPacketModeRequest request = new MfgTestSetPacketModeRequest();
            request.BeginConfiguration = beginConfiguration;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSetPacketModeResponse)));
            MfgTestSetPacketModeResponse response = (MfgTestSetPacketModeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to send the DUT a reboot command. The usual practice is to execute this command at the end of manufacturing configuration, to place the DUT into normal network operation for testing. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the reboot command within 20 milliseconds and then reboots.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSendRebootCommand()
        {
            MfgTestSendRebootCommandRequest request = new MfgTestSendRebootCommandRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSendRebootCommandResponse)));
            MfgTestSendRebootCommandResponse response = (MfgTestSendRebootCommandResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 8-byte EUI ID. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new EUI ID within 150 milliseconds.
        /// </summary>
        /// <param name="NewId">The 8-byte EUID for the DUT.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSendEui64(byte[] newId)
        {
            MfgTestSendEui64Request request = new MfgTestSendEui64Request();
            request.NewId = newId;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSendEui64Response)));
            MfgTestSendEui64Response response = (MfgTestSendEui64Response)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 16-byte configuration string. This function executes only during manufacturing configuration mode and will return an error otherwise. If successful, the DUT will acknowledge the new string within 150 milliseconds.
        /// </summary>
        /// <param name="NewString">The 16-byte manufacturing string.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSendManufacturingString(byte[] newString)
        {
            MfgTestSendManufacturingStringRequest request = new MfgTestSendManufacturingStringRequest();
            request.NewString = newString;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSendManufacturingStringResponse)));
            MfgTestSendManufacturingStringResponse response = (MfgTestSendManufacturingStringResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s radio parameters. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new parameters within 25 milliseconds.
        /// </summary>
        /// <param name="SupportedBands">Sets the radio band for the DUT. See ember-common.h for possible values.</param>
        /// <param name="CrystalOffset">Sets the CC1020 crystal offset. This parameter has no effect on the EM2420, and it may safely be set to 0 for this RFIC.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSendRadioParameters(byte supportedBands, sbyte crystalOffset)
        {
            MfgTestSendRadioParametersRequest request = new MfgTestSendRadioParametersRequest();
            request.SupportedBands = supportedBands;
            request.CrystalOffset = crystalOffset;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSendRadioParametersResponse)));
            MfgTestSendRadioParametersResponse response = (MfgTestSendRadioParametersResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used in each of the manufacturing configuration API calls. Most implementations will not need to call this function directly. See mfg-test.c for more detail. This function executes only during manufacturing configuration mode and returns an error otherwise.
        /// </summary>
        /// <param name="Command">A pointer to the outgoing command string.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public Status MfgTestSendCommand(byte[] command)
        {
            MfgTestSendCommandRequest request = new MfgTestSendCommandRequest();
            request.Command = command;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(MfgTestSendCommandResponse)));
            MfgTestSendCommandResponse response = (MfgTestSendCommandResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A consolidation of ZLL network operations with similar signatures; specifically, forming and joining networks or touch-linking.
        /// </summary>
        /// <param name="NetworkInfo">Information about the network.</param>
        /// <param name="Op">Operation indicator.</param>
        /// <param name="RadioTxPower">Radio transmission power.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ZllNetworkOps(ZigbeeZllNetwork networkInfo, ZigbeeEzspZllNetworkOperation op, sbyte radioTxPower)
        {
            ZllNetworkOpsRequest request = new ZllNetworkOpsRequest();
            request.NetworkInfo = networkInfo;
            request.Op = op;
            request.RadioTxPower = radioTxPower;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllNetworkOpsResponse)));
            ZllNetworkOpsResponse response = (ZllNetworkOpsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will cause the device to setup the security information used in its network. It must be called prior to forming, starting, or joining a network.
        /// </summary>
        /// <param name="NetworkKey">ZLL Network key.</param>
        /// <param name="SecurityState">Initial security state of the network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ZllSetInitialSecurityState(ZigbeeKeyData networkKey, ZigbeeZllInitialSecurityState securityState)
        {
            ZllSetInitialSecurityStateRequest request = new ZllSetInitialSecurityStateRequest();
            request.NetworkKey = networkKey;
            request.SecurityState = securityState;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetInitialSecurityStateResponse)));
            ZllSetInitialSecurityStateResponse response = (ZllSetInitialSecurityStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will update ZLL security token information. Unlike sli_zigbee_stack_zll_set_initial_security_state, this can be called while a network is already established.
        /// </summary>
        /// <param name="SecurityState">Security state of the network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ZllSetSecurityStateWithoutKey(ZigbeeZllInitialSecurityState securityState)
        {
            ZllSetSecurityStateWithoutKeyRequest request = new ZllSetSecurityStateWithoutKeyRequest();
            request.SecurityState = securityState;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetSecurityStateWithoutKeyResponse)));
            ZllSetSecurityStateWithoutKeyResponse response = (ZllSetSecurityStateWithoutKeyResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will initiate a ZLL network scan on all the specified channels.
        /// </summary>
        /// <param name="ChannelMask">The range of channels to scan.</param>
        /// <param name="RadioPowerForScan">The radio output power used for the scan requests.</param>
        /// <param name="NodeType">The node type of the local device.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ZllStartScan(uint channelMask, sbyte radioPowerForScan, ZigbeeNodeType nodeType)
        {
            ZllStartScanRequest request = new ZllStartScanRequest();
            request.ChannelMask = channelMask;
            request.RadioPowerForScan = radioPowerForScan;
            request.NodeType = nodeType;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllStartScanResponse)));
            ZllStartScanResponse response = (ZllStartScanResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will change the mode of the radio so that the receiver is on for a specified amount of time when the device is idle.
        /// </summary>
        /// <param name="DurationMs">The duration in milliseconds to leave the radio on.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status ZllSetRxOnWhenIdle(uint durationMs)
        {
            ZllSetRxOnWhenIdleRequest request = new ZllSetRxOnWhenIdleRequest();
            request.DurationMs = durationMs;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetRxOnWhenIdleResponse)));
            ZllSetRxOnWhenIdleResponse response = (ZllSetRxOnWhenIdleResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call is fired when a ZLL network scan finds a ZLL network.
        /// </summary>
        /// <returns>A tuple containing:
        /// - NetworkInfo: Information about the network.
        /// - IsDeviceInfoNull: Used to interpret deviceInfo field.
        /// - DeviceInfo: Device specific information.
        /// - PacketInfo: Information about the incoming packet received from this network.
        /// </returns>
        public (ZigbeeZllNetwork NetworkInfo, bool IsDeviceInfoNull, ZigbeeZllDeviceInfoRecord DeviceInfo, ZigbeeRxPacketInfo PacketInfo) ZllNetworkFoundHandler()
        {
            ZllNetworkFoundHandlerRequest request = new ZllNetworkFoundHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllNetworkFoundHandlerResponse)));
            ZllNetworkFoundHandlerResponse response = (ZllNetworkFoundHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.NetworkInfo, response.IsDeviceInfoNull, response.DeviceInfo, response.PacketInfo);
        }

        /// <summary>
        /// This call is fired when a ZLL network scan is complete.
        /// </summary>
        /// <returns>Status of the operation.</returns>
        public Status ZllScanCompleteHandler()
        {
            ZllScanCompleteHandlerRequest request = new ZllScanCompleteHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllScanCompleteHandlerResponse)));
            ZllScanCompleteHandlerResponse response = (ZllScanCompleteHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call is fired when network and group addresses are assigned to a remote mode in a network start or network join request.
        /// </summary>
        /// <returns>A tuple containing:
        /// - AddressInfo: Address assignment information.
        /// - PacketInfo: Information about the incoming packet.
        /// </returns>
        public (ZigbeeZllAddressAssignment AddressInfo, ZigbeeRxPacketInfo PacketInfo) ZllAddressAssignmentHandler()
        {
            ZllAddressAssignmentHandlerRequest request = new ZllAddressAssignmentHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllAddressAssignmentHandlerResponse)));
            ZllAddressAssignmentHandlerResponse response = (ZllAddressAssignmentHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.AddressInfo, response.PacketInfo);
        }

        /// <summary>
        /// This call is fired when the device is a target of a touch link.
        /// </summary>
        /// <returns>Information about the network.</returns>
        public ZigbeeZllNetwork ZllTouchLinkTargetHandler()
        {
            ZllTouchLinkTargetHandlerRequest request = new ZllTouchLinkTargetHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllTouchLinkTargetHandlerResponse)));
            ZllTouchLinkTargetHandlerResponse response = (ZllTouchLinkTargetHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.NetworkInfo;
        }

        /// <summary>
        /// Get the ZLL tokens.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Data: Data token return value.
        /// - Security: Security token return value.
        /// </returns>
        public (ZigbeeTokTypeStackZllData Data, ZigbeeTokTypeStackZllSecurity Security) ZllGetTokens()
        {
            ZllGetTokensRequest request = new ZllGetTokensRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllGetTokensResponse)));
            ZllGetTokensResponse response = (ZllGetTokensResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Data, response.Security);
        }

        /// <summary>
        /// Set the ZLL data token.
        /// </summary>
        /// <param name="Data">Data token to be set.</param>
        /// <returns>The ZllSetDataTokenResponse object from the NCP</returns>
        public ZllSetDataTokenResponse ZllSetDataToken(ZigbeeTokTypeStackZllData data)
        {
            ZllSetDataTokenRequest request = new ZllSetDataTokenRequest();
            request.Data = data;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetDataTokenResponse)));
            ZllSetDataTokenResponse response = (ZllSetDataTokenResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Set the ZLL data token bitmask to reflect the ZLL network state.
        /// </summary>
        /// <returns>The ZllSetNonZllNetworkResponse object from the NCP</returns>
        public ZllSetNonZllNetworkResponse ZllSetNonZllNetwork()
        {
            ZllSetNonZllNetworkRequest request = new ZllSetNonZllNetworkRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetNonZllNetworkResponse)));
            ZllSetNonZllNetworkResponse response = (ZllSetNonZllNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Is this a ZLL network?
        /// </summary>
        /// <returns>ZLL network?</returns>
        public bool IsZllNetwork()
        {
            IsZllNetworkRequest request = new IsZllNetworkRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(IsZllNetworkResponse)));
            IsZllNetworkResponse response = (IsZllNetworkResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.IsZllNetwork;
        }

        /// <summary>
        /// This call sets the radio&apos;s default idle power mode.
        /// </summary>
        /// <param name="Mode">The power mode to be set.</param>
        /// <returns>The ZllSetRadioIdleModeResponse object from the NCP</returns>
        public ZllSetRadioIdleModeResponse ZllSetRadioIdleMode(ZigbeeRadioPowerMode mode)
        {
            ZllSetRadioIdleModeRequest request = new ZllSetRadioIdleModeRequest();
            request.Mode = mode;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllSetRadioIdleModeResponse)));
            ZllSetRadioIdleModeResponse response = (ZllSetRadioIdleModeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// This call gets the radio&apos;s default idle power mode.
        /// </summary>
        /// <returns>The current power mode.</returns>
        public byte ZllGetRadioIdleMode()
        {
            ZllGetRadioIdleModeRequest request = new ZllGetRadioIdleModeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllGetRadioIdleModeResponse)));
            ZllGetRadioIdleModeResponse response = (ZllGetRadioIdleModeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.RadioIdleMode;
        }

        /// <summary>
        /// This call sets the default node type for a factory new ZLL device.
        /// </summary>
        /// <param name="NodeType">The node type to be set.</param>
        /// <returns>The SetZllNodeTypeResponse object from the NCP</returns>
        public SetZllNodeTypeResponse SetZllNodeType(ZigbeeNodeType nodeType)
        {
            SetZllNodeTypeRequest request = new SetZllNodeTypeRequest();
            request.NodeType = nodeType;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetZllNodeTypeResponse)));
            SetZllNodeTypeResponse response = (SetZllNodeTypeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// This call sets additional capability bits in the ZLL state.
        /// </summary>
        /// <param name="State">A mask with the bits to be set or cleared.</param>
        /// <returns>The SetZllAdditionalStateResponse object from the NCP</returns>
        public SetZllAdditionalStateResponse SetZllAdditionalState(ushort state)
        {
            SetZllAdditionalStateRequest request = new SetZllAdditionalStateRequest();
            request.State = state;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetZllAdditionalStateResponse)));
            SetZllAdditionalStateResponse response = (SetZllAdditionalStateResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Is there a ZLL (Touchlink) operation in progress?
        /// </summary>
        /// <returns>ZLL operation in progress?</returns>
        public bool ZllOperationInProgress()
        {
            ZllOperationInProgressRequest request = new ZllOperationInProgressRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllOperationInProgressResponse)));
            ZllOperationInProgressResponse response = (ZllOperationInProgressResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ZllOperationInProgress;
        }

        /// <summary>
        /// Is the ZLL radio on when idle mode is active?
        /// </summary>
        /// <returns>ZLL radio on when idle mode is active?</returns>
        public bool ZllRxOnWhenIdleGetActive()
        {
            ZllRxOnWhenIdleGetActiveRequest request = new ZllRxOnWhenIdleGetActiveRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllRxOnWhenIdleGetActiveResponse)));
            ZllRxOnWhenIdleGetActiveResponse response = (ZllRxOnWhenIdleGetActiveResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ZllRxOnWhenIdleGetActive;
        }

        /// <summary>
        /// Informs the ZLL API that application scanning is complete
        /// </summary>
        /// <returns>The ZllScanningCompleteResponse object from the NCP</returns>
        public ZllScanningCompleteResponse ZllScanningComplete()
        {
            ZllScanningCompleteRequest request = new ZllScanningCompleteRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllScanningCompleteResponse)));
            ZllScanningCompleteResponse response = (ZllScanningCompleteResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Get the primary ZLL (touchlink) channel mask.
        /// </summary>
        /// <returns>The primary ZLL channel mask</returns>
        public uint GetZllPrimaryChannelMask()
        {
            GetZllPrimaryChannelMaskRequest request = new GetZllPrimaryChannelMaskRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetZllPrimaryChannelMaskResponse)));
            GetZllPrimaryChannelMaskResponse response = (GetZllPrimaryChannelMaskResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ZllPrimaryChannelMask;
        }

        /// <summary>
        /// Get the secondary ZLL (touchlink) channel mask.
        /// </summary>
        /// <returns>The secondary ZLL channel mask</returns>
        public uint GetZllSecondaryChannelMask()
        {
            GetZllSecondaryChannelMaskRequest request = new GetZllSecondaryChannelMaskRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetZllSecondaryChannelMaskResponse)));
            GetZllSecondaryChannelMaskResponse response = (GetZllSecondaryChannelMaskResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.ZllSecondaryChannelMask;
        }

        /// <summary>
        /// Set the primary ZLL (touchlink) channel mask
        /// </summary>
        /// <param name="ZllPrimaryChannelMask">The primary ZLL channel mask</param>
        /// <returns>The SetZllPrimaryChannelMaskResponse object from the NCP</returns>
        public SetZllPrimaryChannelMaskResponse SetZllPrimaryChannelMask(uint zllPrimaryChannelMask)
        {
            SetZllPrimaryChannelMaskRequest request = new SetZllPrimaryChannelMaskRequest();
            request.ZllPrimaryChannelMask = zllPrimaryChannelMask;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetZllPrimaryChannelMaskResponse)));
            SetZllPrimaryChannelMaskResponse response = (SetZllPrimaryChannelMaskResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Set the secondary ZLL (touchlink) channel mask.
        /// </summary>
        /// <param name="ZllSecondaryChannelMask">The secondary ZLL channel mask</param>
        /// <returns>The SetZllSecondaryChannelMaskResponse object from the NCP</returns>
        public SetZllSecondaryChannelMaskResponse SetZllSecondaryChannelMask(uint zllSecondaryChannelMask)
        {
            SetZllSecondaryChannelMaskRequest request = new SetZllSecondaryChannelMaskRequest();
            request.ZllSecondaryChannelMask = zllSecondaryChannelMask;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetZllSecondaryChannelMaskResponse)));
            SetZllSecondaryChannelMaskResponse response = (SetZllSecondaryChannelMaskResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Clear ZLL stack tokens.
        /// </summary>
        /// <returns>The ZllClearTokensResponse object from the NCP</returns>
        public ZllClearTokensResponse ZllClearTokens()
        {
            ZllClearTokensRequest request = new ZllClearTokensRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ZllClearTokensResponse)));
            ZllClearTokensResponse response = (ZllClearTokensResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Update the GP Proxy table based on a GP pairing.
        /// </summary>
        /// <param name="Options">The options field of the GP Pairing command.</param>
        /// <param name="Addr">The target GPD.</param>
        /// <param name="CommMode">The communication mode of the GP Sink.</param>
        /// <param name="SinkNetworkAddress">The network address of the GP Sink.</param>
        /// <param name="SinkGroupId">The group ID of the GP Sink.</param>
        /// <param name="AssignedAlias">The alias assigned to the GPD.</param>
        /// <param name="SinkIeeeAddress">The IEEE address of the GP Sink.</param>
        /// <param name="GpdKey">The key to use for the target GPD.</param>
        /// <param name="GpdSecurityFrameCounter">The GPD security frame counter.</param>
        /// <param name="ForwardingRadius">The forwarding radius.</param>
        /// <returns>Whether a GP Pairing has been created or not.</returns>
        public bool GpProxyTableProcessGpPairing(uint options, ZigbeeGpAddress addr, byte commMode, ushort sinkNetworkAddress, ushort sinkGroupId, ushort assignedAlias, byte[] sinkIeeeAddress, ZigbeeKeyData gpdKey, uint gpdSecurityFrameCounter, byte forwardingRadius)
        {
            GpProxyTableProcessGpPairingRequest request = new GpProxyTableProcessGpPairingRequest();
            request.Options = options;
            request.Addr = addr;
            request.CommMode = commMode;
            request.SinkNetworkAddress = sinkNetworkAddress;
            request.SinkGroupId = sinkGroupId;
            request.AssignedAlias = assignedAlias;
            request.SinkIeeeAddress = sinkIeeeAddress;
            request.GpdKey = gpdKey;
            request.GpdSecurityFrameCounter = gpdSecurityFrameCounter;
            request.ForwardingRadius = forwardingRadius;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpProxyTableProcessGpPairingResponse)));
            GpProxyTableProcessGpPairingResponse response = (GpProxyTableProcessGpPairingResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.GpPairingAdded;
        }

        /// <summary>
        /// Adds/removes an entry from the GP Tx Queue.
        /// </summary>
        /// <param name="Action">The action to perform on the GP TX queue (true to add, false to remove).</param>
        /// <param name="UseCca">Whether to use ClearChannelAssessment when transmitting the GPDF.</param>
        /// <param name="Addr">The Address of the destination GPD.</param>
        /// <param name="GpdCommandId">The GPD command ID to send.</param>
        /// <param name="GpdAsduLength">The length of the GP command payload.</param>
        /// <param name="GpdAsdu">The GP command payload.</param>
        /// <param name="GpepHandle">The handle to refer to the GPDF.</param>
        /// <param name="GpTxQueueEntryLifetimeMs">How long to keep the GPDF in the TX Queue.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status DGpSend(bool action, bool useCca, ZigbeeGpAddress addr, byte gpdCommandId, byte gpdAsduLength, byte[] gpdAsdu, byte gpepHandle, ushort gpTxQueueEntryLifetimeMs)
        {
            DGpSendRequest request = new DGpSendRequest();
            request.Action = action;
            request.UseCca = useCca;
            request.Addr = addr;
            request.GpdCommandId = gpdCommandId;
            request.GpdAsduLength = gpdAsduLength;
            request.GpdAsdu = gpdAsdu;
            request.GpepHandle = gpepHandle;
            request.GpTxQueueEntryLifetimeMs = gpTxQueueEntryLifetimeMs;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DGpSendResponse)));
            DGpSendResponse response = (DGpSendResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to the GP endpoint to indicate the result of the GPDF transmission.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - GpepHandle: The handle of the GPDF.
        /// </returns>
        public (Status Status, byte GpepHandle) DGpSentHandler()
        {
            DGpSentHandlerRequest request = new DGpSentHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(DGpSentHandlerResponse)));
            DGpSentHandlerResponse response = (DGpSentHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.GpepHandle);
        }

        /// <summary>
        /// A callback invoked by the ZigBee GP stack when a GPDF is received.
        /// </summary>
        /// <returns>GP parameters list represented as a macro for GP endpoint incoming message handler and callbacks prototypes.</returns>
        public ZigbeeGpParams GpepIncomingMessageHandler()
        {
            GpepIncomingMessageHandlerRequest request = new GpepIncomingMessageHandlerRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpepIncomingMessageHandlerResponse)));
            GpepIncomingMessageHandlerResponse response = (GpepIncomingMessageHandlerResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Param;
        }

        /// <summary>
        /// Retrieves the proxy table entry stored at the passed index.
        /// </summary>
        /// <param name="ProxyIndex">The index of the requested proxy table entry.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Entry: An sl_zigbee_gp_proxy_table_entry_t struct containing a copy of the requested proxy entry.
        /// </returns>
        public (Status Status, ZigbeeGpProxyTableEntry Entry) GpProxyTableGetEntry(byte proxyIndex)
        {
            GpProxyTableGetEntryRequest request = new GpProxyTableGetEntryRequest();
            request.ProxyIndex = proxyIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpProxyTableGetEntryResponse)));
            GpProxyTableGetEntryResponse response = (GpProxyTableGetEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Entry);
        }

        /// <summary>
        /// Finds the index of the passed address in the gp table.
        /// </summary>
        /// <param name="Addr">The address to search for</param>
        /// <returns>The index, or 0xFF for not found</returns>
        public byte GpProxyTableLookup(ZigbeeGpAddress addr)
        {
            GpProxyTableLookupRequest request = new GpProxyTableLookupRequest();
            request.Addr = addr;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpProxyTableLookupResponse)));
            GpProxyTableLookupResponse response = (GpProxyTableLookupResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Index;
        }

        /// <summary>
        /// Removes the proxy table entry stored at the passed index.
        /// </summary>
        /// <param name="ProxyIndex">The index of the requested proxy table entry.</param>
        /// <returns>The GpProxyTableRemoveEntryResponse object from the NCP</returns>
        public GpProxyTableRemoveEntryResponse GpProxyTableRemoveEntry(byte proxyIndex)
        {
            GpProxyTableRemoveEntryRequest request = new GpProxyTableRemoveEntryRequest();
            request.ProxyIndex = proxyIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpProxyTableRemoveEntryResponse)));
            GpProxyTableRemoveEntryResponse response = (GpProxyTableRemoveEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Clear the entire proxy table
        /// </summary>
        /// <returns>The GpClearProxyTableResponse object from the NCP</returns>
        public GpClearProxyTableResponse GpClearProxyTable()
        {
            GpClearProxyTableRequest request = new GpClearProxyTableRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpClearProxyTableResponse)));
            GpClearProxyTableResponse response = (GpClearProxyTableResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Retrieves the sink table entry stored at the passed index.
        /// </summary>
        /// <param name="SinkIndex">The index of the requested sink table entry.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Entry: An sl_zigbee_gp_sink_table_entry_t struct containing a copy of the requested sink entry.
        /// </returns>
        public (Status Status, ZigbeeGpSinkTableEntry Entry) GpSinkTableGetEntry(byte sinkIndex)
        {
            GpSinkTableGetEntryRequest request = new GpSinkTableGetEntryRequest();
            request.SinkIndex = sinkIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableGetEntryResponse)));
            GpSinkTableGetEntryResponse response = (GpSinkTableGetEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.Entry);
        }

        /// <summary>
        /// Finds the index of the passed address in the gp table.
        /// </summary>
        /// <param name="Addr">The address to search for.</param>
        /// <returns>The index, or 0xFF for not found</returns>
        public byte GpSinkTableLookup(ZigbeeGpAddress addr)
        {
            GpSinkTableLookupRequest request = new GpSinkTableLookupRequest();
            request.Addr = addr;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableLookupResponse)));
            GpSinkTableLookupResponse response = (GpSinkTableLookupResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Index;
        }

        /// <summary>
        /// Retrieves the sink table entry stored at the passed index.
        /// </summary>
        /// <param name="SinkIndex">The index of the requested sink table entry.</param>
        /// <param name="Entry">An sl_zigbee_gp_sink_table_entry_t struct containing a copy of the sink entry to be updated.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status GpSinkTableSetEntry(byte sinkIndex, ZigbeeGpSinkTableEntry entry)
        {
            GpSinkTableSetEntryRequest request = new GpSinkTableSetEntryRequest();
            request.SinkIndex = sinkIndex;
            request.Entry = entry;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableSetEntryResponse)));
            GpSinkTableSetEntryResponse response = (GpSinkTableSetEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Removes the sink table entry stored at the passed index.
        /// </summary>
        /// <param name="SinkIndex">The index of the requested sink table entry.</param>
        /// <returns>The GpSinkTableRemoveEntryResponse object from the NCP</returns>
        public GpSinkTableRemoveEntryResponse GpSinkTableRemoveEntry(byte sinkIndex)
        {
            GpSinkTableRemoveEntryRequest request = new GpSinkTableRemoveEntryRequest();
            request.SinkIndex = sinkIndex;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableRemoveEntryResponse)));
            GpSinkTableRemoveEntryResponse response = (GpSinkTableRemoveEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Finds or allocates a sink entry
        /// </summary>
        /// <param name="Addr">An sl_zigbee_gp_address_t struct containing a copy of the gpd address to be found.</param>
        /// <returns>An index of found or allocated sink or 0xFF if failed.</returns>
        public byte GpSinkTableFindOrAllocateEntry(ZigbeeGpAddress addr)
        {
            GpSinkTableFindOrAllocateEntryRequest request = new GpSinkTableFindOrAllocateEntryRequest();
            request.Addr = addr;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableFindOrAllocateEntryResponse)));
            GpSinkTableFindOrAllocateEntryResponse response = (GpSinkTableFindOrAllocateEntryResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Index;
        }

        /// <summary>
        /// Clear the entire sink table
        /// </summary>
        /// <returns>The GpSinkTableClearAllResponse object from the NCP</returns>
        public GpSinkTableClearAllResponse GpSinkTableClearAll()
        {
            GpSinkTableClearAllRequest request = new GpSinkTableClearAllRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableClearAllResponse)));
            GpSinkTableClearAllResponse response = (GpSinkTableClearAllResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Iniitializes Sink Table
        /// </summary>
        /// <returns>The GpSinkTableInitResponse object from the NCP</returns>
        public GpSinkTableInitResponse GpSinkTableInit()
        {
            GpSinkTableInitRequest request = new GpSinkTableInitRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableInitResponse)));
            GpSinkTableInitResponse response = (GpSinkTableInitResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Sets security framecounter in the sink table
        /// </summary>
        /// <param name="Index">Index to the Sink table</param>
        /// <param name="Sfc">Security Frame Counter</param>
        /// <returns>The GpSinkTableSetSecurityFrameCounterResponse object from the NCP</returns>
        public GpSinkTableSetSecurityFrameCounterResponse GpSinkTableSetSecurityFrameCounter(byte index, uint sfc)
        {
            GpSinkTableSetSecurityFrameCounterRequest request = new GpSinkTableSetSecurityFrameCounterRequest();
            request.Index = index;
            request.Sfc = sfc;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableSetSecurityFrameCounterResponse)));
            GpSinkTableSetSecurityFrameCounterResponse response = (GpSinkTableSetSecurityFrameCounterResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Puts the GPS in commissioning mode.
        /// </summary>
        /// <param name="Options">commissioning options</param>
        /// <param name="GpmAddrForSecurity">gpm address for security.</param>
        /// <param name="GpmAddrForPairing">gpm address for pairing.</param>
        /// <param name="SinkEndpoint">sink endpoint.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status GpSinkCommission(byte options, ushort gpmAddrForSecurity, ushort gpmAddrForPairing, byte sinkEndpoint)
        {
            GpSinkCommissionRequest request = new GpSinkCommissionRequest();
            request.Options = options;
            request.GpmAddrForSecurity = gpmAddrForSecurity;
            request.GpmAddrForPairing = gpmAddrForPairing;
            request.SinkEndpoint = sinkEndpoint;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkCommissionResponse)));
            GpSinkCommissionResponse response = (GpSinkCommissionResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clears all entries within the translation table.
        /// </summary>
        /// <returns>The GpTranslationTableClearResponse object from the NCP</returns>
        public GpTranslationTableClearResponse GpTranslationTableClear()
        {
            GpTranslationTableClearRequest request = new GpTranslationTableClearRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpTranslationTableClearResponse)));
            GpTranslationTableClearResponse response = (GpTranslationTableClearResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Return number of active entries in sink table.
        /// </summary>
        /// <returns>Number of active entries in sink table.</returns>
        public byte GpSinkTableGetNumberOfActiveEntries()
        {
            GpSinkTableGetNumberOfActiveEntriesRequest request = new GpSinkTableGetNumberOfActiveEntriesRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSinkTableGetNumberOfActiveEntriesResponse)));
            GpSinkTableGetNumberOfActiveEntriesResponse response = (GpSinkTableGetNumberOfActiveEntriesResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.NumberOfEntries;
        }

        /// <summary>
        /// Gets the total number of tokens.
        /// </summary>
        /// <returns>Total number of tokens.</returns>
        public uint GetTokenCount()
        {
            GetTokenCountRequest request = new GetTokenCountRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTokenCountResponse)));
            GetTokenCountResponse response = (GetTokenCountResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Count;
        }

        /// <summary>
        /// Gets the token information for a single token at provided index
        /// </summary>
        /// <param name="Index">Index of the token in the token table for which information is needed.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - TokenInfo: Token information.
        /// </returns>
        public (Status Status, ZigbeeTokenInfo TokenInfo) GetTokenInfo(byte index)
        {
            GetTokenInfoRequest request = new GetTokenInfoRequest();
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTokenInfoResponse)));
            GetTokenInfoResponse response = (GetTokenInfoResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.TokenInfo);
        }

        /// <summary>
        /// Gets the token data for a single token with provided key
        /// </summary>
        /// <param name="Token">Key of the token in the token table for which data is needed.</param>
        /// <param name="Index">Index in case of the indexed token.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - TokenData: Token Data
        /// </returns>
        public (Status Status, ZigbeeTokenData TokenData) GetTokenData(uint token, uint index)
        {
            GetTokenDataRequest request = new GetTokenDataRequest();
            request.Token = token;
            request.Index = index;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GetTokenDataResponse)));
            GetTokenDataResponse response = (GetTokenDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return (response.Status, response.TokenData);
        }

        /// <summary>
        /// Sets the token data for a single token with provided key
        /// </summary>
        /// <param name="Token">Key of the token in the token table for which data is to be set.</param>
        /// <param name="Index">Index in case of the indexed token.</param>
        /// <param name="TokenData">Token Data</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status SetTokenData(uint token, uint index, ZigbeeTokenData tokenData)
        {
            SetTokenDataRequest request = new SetTokenDataRequest();
            request.Token = token;
            request.Index = index;
            request.TokenData = tokenData;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(SetTokenDataResponse)));
            SetTokenDataResponse response = (SetTokenDataResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Reset the node by calling halReboot.
        /// </summary>
        /// <returns>The ResetNodeResponse object from the NCP</returns>
        public ResetNodeResponse ResetNode()
        {
            ResetNodeRequest request = new ResetNodeRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(ResetNodeResponse)));
            ResetNodeResponse response = (ResetNodeResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }

        /// <summary>
        /// Run GP security test vectors.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public Status GpSecurityTestVectors()
        {
            GpSecurityTestVectorsRequest request = new GpSecurityTestVectorsRequest();
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(GpSecurityTestVectorsResponse)));
            GpSecurityTestVectorsResponse response = (GpSecurityTestVectorsResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response.Status;
        }

        /// <summary>
        /// Factory reset all configured zigbee tokens
        /// </summary>
        /// <param name="ExcludeOutgoingFC">Exclude network and APS outgoing frame counter tokens.</param>
        /// <param name="ExcludeBootCounter">Exclude stack boot counter token.</param>
        /// <returns>The TokenFactoryResetResponse object from the NCP</returns>
        public TokenFactoryResetResponse TokenFactoryReset(bool excludeOutgoingFC, bool excludeBootCounter)
        {
            TokenFactoryResetRequest request = new TokenFactoryResetRequest();
            request.ExcludeOutgoingFC = excludeOutgoingFC;
            request.ExcludeBootCounter = excludeBootCounter;
            ITransaction transaction = _protocolHandler.SendTransaction(new SingleResponseTransaction(request, typeof(TokenFactoryResetResponse)));
            TokenFactoryResetResponse response = (TokenFactoryResetResponse)transaction.GetResponse();
            _logger.LogDebug(response.ToString());
            return response;
        }
    }
}
#endif
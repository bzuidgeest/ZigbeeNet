#if VERSION_2025_6_2
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    /// <summary>
    /// Result type for Version method.
    /// </summary>
    /// <param name="ProtocolVersion">The EZSP version the NCP is using.</param>
    /// <param name="StackType">The type of stack running on the NCP (2).</param>
    /// <param name="StackVersion">The version number of the stack.</param>
    public readonly record struct Version(byte ProtocolVersion, byte StackType, ushort StackVersion);
    /// <summary>
    /// Result type for ReadAttribute method.
    /// </summary>
    /// <param name="AfStatus">An sl_zigbee_af_status_t value indicating success or the reason for failure, handled by the EZSP layer as a uint8_t. 255 indicates an EZSP-specific error.</param>
    /// <param name="DataType">Attribute data type.</param>
    /// <param name="ReadLength">Length of attribute data.</param>
    /// <param name="DataPtr">Attribute data.</param>
    public readonly record struct ReadAttribute(ZigbeeAfStatus AfStatus, byte DataType, byte ReadLength, byte[] DataPtr);
    /// <summary>
    /// Result type for GetValue method.
    /// </summary>
    /// <param name="ValueLength">Both a command and response parameter. On command, the maximum size in bytes of local storage allocated to receive the returned <i>value</i>. On response, the actual length in bytes of the returned <i>value</i>.</param>
    /// <param name="Value">The value.</param>
    public readonly record struct GetValue(byte ValueLength, byte[] Value);
    /// <summary>
    /// Result type for GetExtendedValue method.
    /// </summary>
    /// <param name="ValueLength">Both a command and response parameter. On command, the maximum size in bytes of local storage allocated to receive the returned <i>value</i>. On response, the actual length in bytes of the returned <i>value</i>.</param>
    /// <param name="Value">The value.</param>
    public readonly record struct GetExtendedValue(byte ValueLength, byte[] Value);
    /// <summary>
    /// Result type for Echo method.
    /// </summary>
    /// <param name="EchoLength">The length of the <i>echo</i> parameter in bytes.</param>
    /// <param name="Echo">The echo of the data.</param>
    public readonly record struct Echo(byte EchoLength, byte[] Echo);
    /// <summary>
    /// Result type for GetMfgToken method.
    /// </summary>
    /// <param name="TokenDataLength">The length of the <i>tokenData</i> parameter in bytes.</param>
    /// <param name="TokenData">The manufacturing token data.</param>
    public readonly record struct GetMfgToken(byte TokenDataLength, byte[] TokenData);
    /// <summary>
    /// Result type for GetTimer method.
    /// </summary>
    /// <param name="Time">The delay before the <i>timerHandler</i> callback will be generated.</param>
    /// <param name="Units">The units for <i>time</i>.</param>
    /// <param name="Repeat">True if a <i>timerHandler</i> callback will be generated repeatedly. False if only a single <i>timerHandler</i> callback will be generated.</param>
    public readonly record struct GetTimer(ushort Time, ZigbeeEventUnits Units, bool Repeat);
    /// <summary>
    /// Result type for MuxInvalidRxHandler method.
    /// </summary>
    /// <param name="NewRxChannel"></param>
    /// <param name="OldRxChannel"></param>
    public readonly record struct MuxInvalidRxHandler(byte NewRxChannel, byte OldRxChannel);
    /// <summary>
    /// Result type for GetXncpInfo method.
    /// </summary>
    /// <param name="ManufacturerId">The manufactured ID the user has defined in the XNCP application.</param>
    /// <param name="VersionNumber">The version number of the XNCP application.</param>
    public readonly record struct GetXncpInfo(ushort ManufacturerId, ushort VersionNumber);
    /// <summary>
    /// Result type for CustomFrame method.
    /// </summary>
    /// <param name="ReplyLength">The length of the response.</param>
    /// <param name="Reply">The response.</param>
    public readonly record struct CustomFrame(byte ReplyLength, byte[] Reply);
    /// <summary>
    /// Result type for CustomFrameHandler method.
    /// </summary>
    /// <param name="PayloadLength">The length of the custom frame payload.</param>
    /// <param name="Payload">The payload of the custom frame.</param>
    public readonly record struct CustomFrameHandler(byte PayloadLength, byte[] Payload);
    /// <summary>
    /// Result type for EnergyScanResultHandler method.
    /// </summary>
    /// <param name="Channel">The 802.15.4 channel number that was scanned.</param>
    /// <param name="MaxRssiValue">The maximum RSSI value found on the channel.</param>
    public readonly record struct EnergyScanResultHandler(byte Channel, sbyte MaxRssiValue);
    /// <summary>
    /// Result type for NetworkFoundHandler method.
    /// </summary>
    /// <param name="NetworkFound">The parameters associated with the network found.</param>
    /// <param name="LastHopLqi">Link quality of incoming packet from network.</param>
    /// <param name="LastHopRssi">Power (in dBm) of incoming packet.</param>
    public readonly record struct NetworkFoundHandler(ZigbeeZigbeeNetwork NetworkFound, byte LastHopLqi, sbyte LastHopRssi);
    /// <summary>
    /// Result type for UnusedPanIdFoundHandler method.
    /// </summary>
    /// <param name="PanId">The unused panID which has been found.</param>
    /// <param name="Channel">The channel that the unused panID was found on.</param>
    public readonly record struct UnusedPanIdFoundHandler(ushort PanId, byte Channel);
    /// <summary>
    /// Result type for ChildJoinHandler method.
    /// </summary>
    /// <param name="Index">The index of the child of interest.</param>
    /// <param name="Joining">True if the child is joining. False the child is leaving.</param>
    /// <param name="ChildId">The node ID of the child.</param>
    /// <param name="ChildEui64">The EUI64 of the child.</param>
    /// <param name="ChildType">The node type of the child.</param>
    public readonly record struct ChildJoinHandler(byte Index, bool Joining, ushort ChildId, byte[] ChildEui64, ZigbeeNodeType ChildType);
    /// <summary>
    /// Result type for GetNetworkParameters method.
    /// </summary>
    /// <param name="NodeType">An sl_zigbee_node_type_t value indicating the current node type.</param>
    /// <param name="Parameters">The current network parameters.</param>
    public readonly record struct GetNetworkParameters(ZigbeeNodeType NodeType, ZigbeeNetworkParameters Parameters);
    /// <summary>
    /// Result type for GetParentChildParameters method.
    /// </summary>
    /// <param name="ChildCount">The number of children the node currently has.</param>
    /// <param name="ParentEui64">The parent's EUI64. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).</param>
    /// <param name="ParentNodeId">The parent's node ID. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).</param>
    public readonly record struct GetParentChildParameters(byte ChildCount, byte[] ParentEui64, ushort ParentNodeId);
    /// <summary>
    /// Result type for GetSourceRouteTableEntry method.
    /// </summary>
    /// <param name="Destination">The node ID of the destination in that entry.</param>
    /// <param name="CloserIndex">The closer node index for this source route table entry</param>
    public readonly record struct GetSourceRouteTableEntry(ushort Destination, byte CloserIndex);
    /// <summary>
    /// Result type for DutyCycleHandler method.
    /// </summary>
    /// <param name="ChannelPage">The channel page whose duty cycle state has changed.</param>
    /// <param name="Channel">The channel number whose duty cycle state has changed.</param>
    /// <param name="State">The current duty cycle state.</param>
    /// <param name="TotalDevices">The total number of connected end devices that are being monitored for duty cycle.</param>
    /// <param name="ArrayOfDeviceDutyCycles">Consumed duty cycles of end devices that are being monitored. The first entry always be the local stack's nodeId, and thus the total aggregate duty cycle for the device.</param>
    public readonly record struct DutyCycleHandler(byte ChannelPage, byte Channel, ZigbeeDutyCycleState State, byte TotalDevices, ZigbeePerDeviceDutyCycle ArrayOfDeviceDutyCycles);
    /// <summary>
    /// Result type for RemoteSetBindingHandler method.
    /// </summary>
    /// <param name="Entry">The requested binding.</param>
    /// <param name="Index">The index at which the binding was added.</param>
    public readonly record struct RemoteSetBindingHandler(ZigbeeBindingTableEntry Entry, byte Index);
    /// <summary>
    /// Result type for MessageSentHandler method.
    /// </summary>
    /// <param name="Type">The type of message sent.</param>
    /// <param name="IndexOrDestination">The destination to which the message was sent, for direct unicasts, or the address table or binding index for other unicasts. The value is unspecified for multicasts and broadcasts.</param>
    /// <param name="ApsFrame">The APS frame for the message.</param>
    /// <param name="MessageTag">The value supplied by the Host in the <i>sl_zigbee_ezsp_send_unicast</i>, <i>sl_zigbee_ezsp_send_broadcast</i> or <i>sl_zigbee_ezsp_send_multicast</i> command.</param>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The unicast message supplied by the Host. The message contents are only included here if the decision for the messageContentsInCallback policy is messageTagAndContentsInCallback.</param>
    public readonly record struct MessageSentHandler(ZigbeeOutgoingMessageType Type, ushort IndexOrDestination, ZigbeeApsFrame ApsFrame, ushort MessageTag, byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for PollHandler method.
    /// </summary>
    /// <param name="ChildId">The node ID of the child that is requesting data.</param>
    /// <param name="TransmitExpected">True if transmit is expected, false otherwise.</param>
    public readonly record struct PollHandler(ushort ChildId, bool TransmitExpected);
    /// <summary>
    /// Result type for IncomingMessageHandler method.
    /// </summary>
    /// <param name="Type">The type of the incoming message. One of the following: SL_ZIGBEE_INCOMING_UNICAST, SL_ZIGBEE_INCOMING_UNICAST_REPLY, SL_ZIGBEE_INCOMING_MULTICAST, SL_ZIGBEE_INCOMING_MULTICAST_LOOPBACK, SL_ZIGBEE_INCOMING_BROADCAST, SL_ZIGBEE_INCOMING_BROADCAST_LOOPBACK</param>
    /// <param name="ApsFrame">The APS frame from the incoming message.</param>
    /// <param name="PacketInfo">Miscellanous message information.</param>
    /// <param name="MessageLength">The length of the <i>message</i> parameter in bytes.</param>
    /// <param name="Message">The incoming message.</param>
    public readonly record struct IncomingMessageHandler(ZigbeeIncomingMessageType Type, ZigbeeApsFrame ApsFrame, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] Message);
    /// <summary>
    /// Result type for IncomingManyToOneRouteRequestHandler method.
    /// </summary>
    /// <param name="Source">The short id of the concentrator.</param>
    /// <param name="LongId">The EUI64 of the concentrator.</param>
    /// <param name="Cost">The path cost to the concentrator. The cost may decrease as additional route request packets for this discovery arrive, but the callback is made only once.</param>
    public readonly record struct IncomingManyToOneRouteRequestHandler(ushort Source, byte[] LongId, byte Cost);
    /// <summary>
    /// Result type for IncomingNetworkStatusHandler method.
    /// </summary>
    /// <param name="ErrorCode">One byte over-the-air error code from network status message</param>
    /// <param name="Target">The short ID of the remote node</param>
    public readonly record struct IncomingNetworkStatusHandler(byte ErrorCode, ushort Target);
    /// <summary>
    /// Result type for IncomingRouteRecordHandler method.
    /// </summary>
    /// <param name="Source">The source of the route record.</param>
    /// <param name="SourceEui">The EUI64 of the source.</param>
    /// <param name="LastHopLqi">The link quality from the node that last relayed the route record.</param>
    /// <param name="LastHopRssi">The energy level (in units of dBm) observed during the reception.</param>
    /// <param name="RelayCount">The number of relays in <i>relayList</i>.</param>
    /// <param name="RelayList">The route record. Each relay in the list is an uint16_t node ID. The list is passed as uint8_t * to avoid alignment problems.</param>
    public readonly record struct IncomingRouteRecordHandler(ushort Source, byte[] SourceEui, byte LastHopLqi, sbyte LastHopRssi, byte RelayCount, byte[] RelayList);
    /// <summary>
    /// Result type for GetAddressTableInfo method.
    /// </summary>
    /// <param name="NodeId">One of the following: The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index. SL_ZIGBEE_UNKNOWN_NODE_ID - Indicates that the EUI64 stored in the address table at the given index is valid but the short ID is currently unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID - Indicates that the EUI64 stored in the address table at the given location is valid and network address discovery is underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID - Indicates that the entry stored in the address table at the given index is not in use.</param>
    /// <param name="Eui64">The EUI64 of the address table entry is copied to this location.</param>
    public readonly record struct GetAddressTableInfo(ushort NodeId, byte[] Eui64);
    /// <summary>
    /// Result type for ReplaceAddressTableEntry method.
    /// </summary>
    /// <param name="OldEui64">The EUI64 of the address table entry before it was modified.</param>
    /// <param name="OldId">One of the following: The short ID corresponding to the EUI64 before it was modified. SL_ZIGBEE_UNKNOWN_NODE_ID if the short ID was unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID if discovery of the short ID was underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID if the address table entry was unused.</param>
    /// <param name="OldExtendedTimeout">true if the retry interval was being increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval was being used.</param>
    public readonly record struct ReplaceAddressTableEntry(byte[] OldEui64, ushort OldId, bool OldExtendedTimeout);
    /// <summary>
    /// Result type for MacPassthroughMessageHandler method.
    /// </summary>
    /// <param name="MessageType">The type of MAC passthrough message received.</param>
    /// <param name="PacketInfo">Information about the incoming packet.</param>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The raw message that was received.</param>
    public readonly record struct MacPassthroughMessageHandler(ZigbeeMacPassthroughType MessageType, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for MacFilterMatchMessageHandler method.
    /// </summary>
    /// <param name="FilterValueMatch">The value of the filter that was matched.</param>
    /// <param name="LegacyPassthroughType">The type of MAC passthrough message received.</param>
    /// <param name="PacketInfo">Information about the incoming packet.</param>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The raw message that was received.</param>
    public readonly record struct MacFilterMatchMessageHandler(ushort FilterValueMatch, ZigbeeMacPassthroughType LegacyPassthroughType, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for RawTransmitCompleteHandler method.
    /// </summary>
    /// <param name="MessageLength">Length of the message that was transmitted.</param>
    /// <param name="MessageContents">The message that was transmitted.</param>
    public readonly record struct RawTransmitCompleteHandler(byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for ZigbeeKeyEstablishmentHandler method.
    /// </summary>
    /// <param name="Partner">This is the IEEE address of the partner that the device successfully established a key with. This value is all zeros on a failure.</param>
    /// <param name="Status">This is the status indicating what was established or why the key establishment failed.</param>
    public readonly record struct ZigbeeKeyEstablishmentHandler(byte[] Partner, ZigbeeKeyStatus Status);
    /// <summary>
    /// Result type for SecManExportLinkKeyByIndex method.
    /// </summary>
    /// <param name="Context">Context referencing the exported key.  Contains information like the EUI64 address it is associated with.</param>
    /// <param name="PlaintextKey">The exported key.</param>
    /// <param name="KeyData">Metadata about the key.</param>
    public readonly record struct SecManExportLinkKeyByIndex(ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData);
    /// <summary>
    /// Result type for SecManExportLinkKeyByEui method.
    /// </summary>
    /// <param name="Context">Context referring to the exported key, containing the table index that this key is located in.</param>
    /// <param name="PlaintextKey">The exported key.</param>
    /// <param name="KeyData">Metadata about the key.</param>
    public readonly record struct SecManExportLinkKeyByEui(ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData);
    /// <summary>
    /// Result type for SecManExportTransientKeyByIndex method.
    /// </summary>
    /// <param name="Context">Context struct for export operation.</param>
    /// <param name="PlaintextKey">The exported key.</param>
    /// <param name="KeyData">Metadata about the key.</param>
    public readonly record struct SecManExportTransientKeyByIndex(ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData);
    /// <summary>
    /// Result type for SecManExportTransientKeyByEui method.
    /// </summary>
    /// <param name="Context">Context struct for export operation.</param>
    /// <param name="PlaintextKey">The exported key.</param>
    /// <param name="KeyData">Metadata about the key.</param>
    public readonly record struct SecManExportTransientKeyByEui(ZigbeeSecManContext Context, ZigbeeSecManKey PlaintextKey, ZigbeeSecManApsKeyMetadata KeyData);
    /// <summary>
    /// Result type for TrustCenterPostJoinHandler method.
    /// </summary>
    /// <param name="NewNodeId">The Node Id of the node whose status changed</param>
    /// <param name="NewNodeEui64">The EUI64 of the node whose status changed.</param>
    /// <param name="Status">The status of the node: Secure Join/Rejoin, Unsecure Join/Rejoin, Device left.</param>
    /// <param name="PolicyDecision">An sl_zigbee_join_decision_t reflecting the decision made.</param>
    /// <param name="ParentOfNewNodeId">The parent of the node whose status has changed.</param>
    public readonly record struct TrustCenterPostJoinHandler(ushort NewNodeId, byte[] NewNodeEui64, ZigbeeDeviceUpdate Status, ZigbeeJoinDecision PolicyDecision, ushort ParentOfNewNodeId);
    /// <summary>
    /// Result type for CalculateSmacsHandler method.
    /// </summary>
    /// <param name="InitiatorSmac">The calculated value of the initiator's SMAC</param>
    /// <param name="ResponderSmac">The calculated value of the responder's SMAC</param>
    public readonly record struct CalculateSmacsHandler(ZigbeeSmacData InitiatorSmac, ZigbeeSmacData ResponderSmac);
    /// <summary>
    /// Result type for CalculateSmacs283k1Handler method.
    /// </summary>
    /// <param name="InitiatorSmac">The calculated value of the initiator's SMAC</param>
    /// <param name="ResponderSmac">The calculated value of the responder's SMAC</param>
    public readonly record struct CalculateSmacs283k1Handler(ZigbeeSmacData InitiatorSmac, ZigbeeSmacData ResponderSmac);
    /// <summary>
    /// Result type for DsaSignHandler method.
    /// </summary>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The message and attached which includes the original message and the appended signature.</param>
    public readonly record struct DsaSignHandler(byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for MfglibRxHandler method.
    /// </summary>
    /// <param name="LinkQuality">The link quality observed during the reception</param>
    /// <param name="Rssi">The energy level (in units of dBm) observed during the reception.</param>
    /// <param name="PacketLength">The length of the packetContents parameter in bytes. Will be greater than 3 and less than 123.</param>
    /// <param name="PacketContents">The received packet (last 2 bytes are not FCS / CRC and may be discarded)</param>
    public readonly record struct MfglibRxHandler(byte LinkQuality, sbyte Rssi, byte PacketLength, byte[] PacketContents);
    /// <summary>
    /// Result type for GetStandaloneBootloaderVersionPlatMicroPhy method.
    /// </summary>
    /// <param name="BootloaderVersion">BOOTLOADER_INVALID_VERSION if the standalone bootloader is not present, or the version of the installed standalone bootloader.</param>
    /// <param name="NodePlat">The value of PLAT on the node</param>
    /// <param name="NodeMicro">The value of MICRO on the node</param>
    /// <param name="NodePhy">The value of PHY on the node</param>
    public readonly record struct GetStandaloneBootloaderVersionPlatMicroPhy(ushort BootloaderVersion, byte NodePlat, byte NodeMicro, byte NodePhy);
    /// <summary>
    /// Result type for IncomingBootloadMessageHandler method.
    /// </summary>
    /// <param name="LongId">The EUI64 of the sending node.</param>
    /// <param name="PacketInfo">Information about the incoming packet.</param>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The bootload message that was sent.</param>
    public readonly record struct IncomingBootloadMessageHandler(byte[] LongId, ZigbeeRxPacketInfo PacketInfo, byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for BootloadTransmitCompleteHandler method.
    /// </summary>
    /// <param name="MessageLength">The length of the <i>messageContents</i> parameter in bytes.</param>
    /// <param name="MessageContents">The message that was sent.</param>
    public readonly record struct BootloadTransmitCompleteHandler(byte MessageLength, byte[] MessageContents);
    /// <summary>
    /// Result type for IncomingMfgTestMessageHandler method.
    /// </summary>
    /// <param name="MessageType">The type of the incoming message. Currently, the only possibility is MFG_TEST_TYPE_ACK.</param>
    /// <param name="DataLength">The length of the incoming message.</param>
    /// <param name="Data">A pointer to the data received in the current message.</param>
    public readonly record struct IncomingMfgTestMessageHandler(byte MessageType, byte DataLength, byte[] Data);
    /// <summary>
    /// Result type for ZllNetworkFoundHandler method.
    /// </summary>
    /// <param name="NetworkInfo">Information about the network.</param>
    /// <param name="IsDeviceInfoNull">Used to interpret deviceInfo field.</param>
    /// <param name="DeviceInfo">Device specific information.</param>
    /// <param name="PacketInfo">Information about the incoming packet received from this network.</param>
    public readonly record struct ZllNetworkFoundHandler(ZigbeeZllNetwork NetworkInfo, bool IsDeviceInfoNull, ZigbeeZllDeviceInfoRecord DeviceInfo, ZigbeeRxPacketInfo PacketInfo);
    /// <summary>
    /// Result type for ZllAddressAssignmentHandler method.
    /// </summary>
    /// <param name="AddressInfo">Address assignment information.</param>
    /// <param name="PacketInfo">Information about the incoming packet.</param>
    public readonly record struct ZllAddressAssignmentHandler(ZigbeeZllAddressAssignment AddressInfo, ZigbeeRxPacketInfo PacketInfo);
    /// <summary>
    /// Result type for ZllGetTokens method.
    /// </summary>
    /// <param name="Data">Data token return value.</param>
    /// <param name="Security">Security token return value.</param>
    public readonly record struct ZllGetTokens(ZigbeeTokTypeStackZllData Data, ZigbeeTokTypeStackZllSecurity Security);
    public partial class EmberNcp
    {
        static private readonly ILogger _logger = LogManager.GetLog<EmberNcp>();
        /// <summary>
        /// The command allows the Host to specify the desired EZSP version and must be sent before any other command. The response provides information about the firmware running on the NCP.
        /// </summary>
        /// <param name="DesiredProtocolVersion">The EZSP version the Host wishes to use. To successfully set the version and allow other commands, this must be same as EZSP_PROTOCOL_VERSION.</param>
        /// <returns>A tuple containing:
        /// - ProtocolVersion: The EZSP version the NCP is using.
        /// - StackType: The type of stack running on the NCP (2).
        /// - StackVersion: The version number of the stack.
        /// </returns>
        public async Task<Version> Version(byte desiredProtocolVersion, CancellationToken cancellationToken = default)
        {
            VersionRequest request = new VersionRequest();
            request.DesiredProtocolVersion = desiredProtocolVersion;
            VersionResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as VersionResponse;
            _logger.LogDebug(response?.ToString());
            return new Version(response.ProtocolVersion, response.StackType, response.StackVersion);
        }

        /// <summary>
        /// Reads a configuration value from the NCP.
        /// </summary>
        /// <param name="ConfigId">Identifies which configuration value to read.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize <i>configId</i>.
        /// - Value: The configuration value.
        /// </returns>
        public async Task<(Status Status, ushort Value)> GetConfigurationValue(ZigbeeEzspConfigId configId, CancellationToken cancellationToken = default)
        {
            GetConfigurationValueRequest request = new GetConfigurationValueRequest();
            request.ConfigId = configId;
            GetConfigurationValueResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetConfigurationValueResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Writes a configuration value to the NCP. Configuration values can be modified by the Host after the NCP has reset. Once the status of the stack changes to SL_STATUS_NETWORK_UP, configuration values can no longer be modified and this command will respond with SL_ZIGBEE_EZSP_ERROR_INVALID_CALL.
        /// </summary>
        /// <param name="ConfigId">Identifies which configuration value to change.</param>
        /// <param name="Value">The new configuration value.</param>
        /// <returns>SL_STATUS_OK if the configuration value was changed, SL_STATUS_ZIGBEE_EZSP_ERROR if there was an error. Retrievable EZSP errors can be SL_ZIGBEE_EZSP_ERROR_OUT_OF_MEMORY if the new value exceeded the available memory, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the new value was out of bounds, SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;configId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if configuration values can no longer be modified.</returns>
        public async Task<Status> SetConfigurationValue(ZigbeeEzspConfigId configId, ushort value, CancellationToken cancellationToken = default)
        {
            SetConfigurationValueRequest request = new SetConfigurationValueRequest();
            request.ConfigId = configId;
            request.Value = value;
            SetConfigurationValueResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetConfigurationValueResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<ReadAttribute> ReadAttribute(byte endpoint, ushort cluster, ushort attributeId, byte mask, ushort manufacturerCode, CancellationToken cancellationToken = default)
        {
            ReadAttributeRequest request = new ReadAttributeRequest();
            request.Endpoint = endpoint;
            request.Cluster = cluster;
            request.AttributeId = attributeId;
            request.Mask = mask;
            request.ManufacturerCode = manufacturerCode;
            ReadAttributeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ReadAttributeResponse;
            _logger.LogDebug(response?.ToString());
            return new ReadAttribute(response.AfStatus, response.DataType, response.ReadLength, response.DataPtr);
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
        public async Task<ZigbeeAfStatus> WriteAttribute(byte endpoint, ushort cluster, ushort attributeId, byte mask, ushort manufacturerCode, bool overrideReadOnlyAndDataType, bool justTest, byte dataType, byte dataLength, byte[] data, CancellationToken cancellationToken = default)
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
            WriteAttributeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as WriteAttributeResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> AddEndpoint(byte endpoint, ushort profileId, ushort deviceId, byte deviceVersion, byte inputClusterCount, byte outputClusterCount, ushort[] inputClusterList, ushort[] outputClusterList, CancellationToken cancellationToken = default)
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
            AddEndpointResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as AddEndpointResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the Host to change the policies used by the NCP to make fast decisions.
        /// </summary>
        /// <param name="PolicyId">Identifies which policy to modify.</param>
        /// <param name="DecisionId">The new decision for the specified policy.</param>
        /// <returns>SL_STATUS_OK if the policy was changed, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize &lt;i&gt;policyId&lt;/i&gt;.</returns>
        public async Task<Status> SetPolicy(ZigbeeEzspPolicyId policyId, ZigbeeEzspDecisionId decisionId, CancellationToken cancellationToken = default)
        {
            SetPolicyRequest request = new SetPolicyRequest();
            request.PolicyId = policyId;
            request.DecisionId = decisionId;
            SetPolicyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetPolicyResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeEzspDecisionId DecisionId)> GetPolicy(ZigbeeEzspPolicyId policyId, CancellationToken cancellationToken = default)
        {
            GetPolicyRequest request = new GetPolicyRequest();
            request.PolicyId = policyId;
            GetPolicyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetPolicyResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.DecisionId);
        }

        /// <summary>
        /// Triggers a pan id update message.
        /// </summary>
        /// <param name="NewPan">The new Pan Id</param>
        /// <returns>true if the request was successfully handed to the stack, false otherwise</returns>
        public async Task<bool> SendPanIdUpdate(ushort newPan, CancellationToken cancellationToken = default)
        {
            SendPanIdUpdateRequest request = new SendPanIdUpdateRequest();
            request.NewPan = newPan;
            SendPanIdUpdateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendPanIdUpdateResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, GetValue Result)> GetValue(ZigbeeEzspValueId valueId, CancellationToken cancellationToken = default)
        {
            GetValueRequest request = new GetValueRequest();
            request.ValueId = valueId;
            GetValueResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetValueResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetValue(response.ValueLength, response.Value));
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
        public async Task<(Status Status, GetExtendedValue Result)> GetExtendedValue(ZigbeeEzspExtendedValueId valueId, uint characteristics, CancellationToken cancellationToken = default)
        {
            GetExtendedValueRequest request = new GetExtendedValueRequest();
            request.ValueId = valueId;
            request.Characteristics = characteristics;
            GetExtendedValueResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetExtendedValueResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetExtendedValue(response.ValueLength, response.Value));
        }

        /// <summary>
        /// Writes a value to the NCP.
        /// </summary>
        /// <param name="ValueId">Identifies which value to change.</param>
        /// <param name="ValueLength">The length of the &lt;i&gt;value&lt;/i&gt; parameter in bytes.</param>
        /// <param name="Value">The new value.</param>
        /// <returns>SL_STATUS_OK if the value was changed, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the new value was out of bounds, SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;valueId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if the value could not be modified.</returns>
        public async Task<Status> SetValue(ZigbeeEzspValueId valueId, byte valueLength, byte[] value, CancellationToken cancellationToken = default)
        {
            SetValueRequest request = new SetValueRequest();
            request.ValueId = valueId;
            request.ValueLength = valueLength;
            request.Value = value;
            SetValueResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetValueResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Allows the Host to control the broadcast behaviour of a routing device used by the NCP.
        /// </summary>
        /// <param name="Config">Passive ack config enum.</param>
        /// <param name="MinAcksNeeded">The minimum number of acknowledgments (re-broadcasts) to wait for until deeming the broadcast transmission complete.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetPassiveAckConfig(byte config, byte minAcksNeeded, CancellationToken cancellationToken = default)
        {
            SetPassiveAckConfigRequest request = new SetPassiveAckConfigRequest();
            request.Config = config;
            request.MinAcksNeeded = minAcksNeeded;
            SetPassiveAckConfigResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetPassiveAckConfigResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set the PAN ID to be accepted by the device in a NLME Network Update command.  If this is set to a different value than its default 0xFFFF, NLME network update messages will be ignored if they do not match this PAN ID.
        /// </summary>
        /// <param name="PanId">PAN ID to be accepted in a network update.</param>
        /// <returns>The SetPendingNetworkUpdatePanIdResponse object from the NCP</returns>
        public async Task<SetPendingNetworkUpdatePanIdResponse> SetPendingNetworkUpdatePanId(ushort panId, CancellationToken cancellationToken = default)
        {
            SetPendingNetworkUpdatePanIdRequest request = new SetPendingNetworkUpdatePanIdRequest();
            request.PanId = panId;
            SetPendingNetworkUpdatePanIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetPendingNetworkUpdatePanIdResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Retrieve the endpoint number located at the specified index.
        /// </summary>
        /// <param name="Index">Index to retrieve the endpoint number for.</param>
        /// <returns>Endpoint number at the index.</returns>
        public async Task<byte> GetEndpoint(byte index, CancellationToken cancellationToken = default)
        {
            GetEndpointRequest request = new GetEndpointRequest();
            request.Index = index;
            GetEndpointResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetEndpointResponse;
            _logger.LogDebug(response?.ToString());
            return response.Endpoint;
        }

        /// <summary>
        /// Get the number of configured endpoints.
        /// </summary>
        /// <returns>Number of configured endpoints.</returns>
        public async Task<byte> GetEndpointCount(CancellationToken cancellationToken = default)
        {
            GetEndpointCountRequest request = new GetEndpointCountRequest();
            GetEndpointCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetEndpointCountResponse;
            _logger.LogDebug(response?.ToString());
            return response.Count;
        }

        /// <summary>
        /// Retrieve the endpoint description for the given endpoint number.
        /// </summary>
        /// <param name="Endpoint">Endpoint number to get the description of.</param>
        /// <returns>Description of this endpoint.</returns>
        public async Task<ZigbeeEndpointDescription> GetEndpointDescription(byte endpoint, CancellationToken cancellationToken = default)
        {
            GetEndpointDescriptionRequest request = new GetEndpointDescriptionRequest();
            request.Endpoint = endpoint;
            GetEndpointDescriptionResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetEndpointDescriptionResponse;
            _logger.LogDebug(response?.ToString());
            return response.Result;
        }

        /// <summary>
        /// Retrieve one of the cluster IDs associated with the given endpoint.
        /// </summary>
        /// <param name="Endpoint">Endpoint number to get a cluster ID for.</param>
        /// <param name="ListId">Which list to get the cluster ID from.  (0 for input, 1 for output).</param>
        /// <param name="ListIndex">Index from requested list to look at the cluster ID of.</param>
        /// <returns>ID of the requested cluster.</returns>
        public async Task<ushort> GetEndpointCluster(byte endpoint, byte listId, byte listIndex, CancellationToken cancellationToken = default)
        {
            GetEndpointClusterRequest request = new GetEndpointClusterRequest();
            request.Endpoint = endpoint;
            request.ListId = listId;
            request.ListIndex = listIndex;
            GetEndpointClusterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetEndpointClusterResponse;
            _logger.LogDebug(response?.ToString());
            return response.EndpointCluster;
        }

        /// <summary>
        /// A command which does nothing. The Host can use this to set the sleep mode or to check the status of the NCP.
        /// </summary>
        /// <returns>The NopResponse object from the NCP</returns>
        public async Task<NopResponse> Nop(CancellationToken cancellationToken = default)
        {
            NopRequest request = new NopRequest();
            NopResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NopResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Echo> Echo(byte dataLength, byte[] data, CancellationToken cancellationToken = default)
        {
            EchoRequest request = new EchoRequest();
            request.DataLength = dataLength;
            request.Data = data;
            EchoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as EchoResponse;
            _logger.LogDebug(response?.ToString());
            return new Echo(response.EchoLength, response.Echo);
        }

        /// <summary>
        /// Indicates that the NCP received an invalid command.
        /// </summary>
        /// <returns>The reason why the command was invalid.</returns>
        public async Task<ZigbeeEzspStatus> InvalidCommand(CancellationToken cancellationToken = default)
        {
            InvalidCommandRequest request = new InvalidCommandRequest();
            InvalidCommandResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as InvalidCommandResponse;
            _logger.LogDebug(response?.ToString());
            return response.Reason;
        }

        /// <summary>
        /// Allows the NCP to respond with a pending callback.
        /// </summary>
        /// <returns>The CallbackResponse object from the NCP</returns>
        public async Task<CallbackResponse> Callback(CancellationToken cancellationToken = default)
        {
            CallbackRequest request = new CallbackRequest();
            CallbackResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CallbackResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Indicates that there are currently no pending callbacks.
        /// </summary>
        /// <returns>The NoCallbacksResponse object from the NCP</returns>
        public async Task<NoCallbacksResponse> NoCallbacks(CancellationToken cancellationToken = default)
        {
            NoCallbacksRequest request = new NoCallbacksRequest();
            NoCallbacksResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NoCallbacksResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Sets a token (8 bytes of non-volatile storage) in the Simulated EEPROM of the NCP.
        /// </summary>
        /// <param name="TokenId">Which token to set</param>
        /// <param name="TokenData">The data to write to the token.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetToken(byte tokenId, byte[] tokenData, CancellationToken cancellationToken = default)
        {
            SetTokenRequest request = new SetTokenRequest();
            request.TokenId = tokenId;
            request.TokenData = tokenData;
            SetTokenResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetTokenResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte[] TokenData)> GetToken(byte tokenId, CancellationToken cancellationToken = default)
        {
            GetTokenRequest request = new GetTokenRequest();
            request.TokenId = tokenId;
            GetTokenResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTokenResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<GetMfgToken> GetMfgToken(ZigbeeEzspMfgTokenId tokenId, CancellationToken cancellationToken = default)
        {
            GetMfgTokenRequest request = new GetMfgTokenRequest();
            request.TokenId = tokenId;
            GetMfgTokenResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetMfgTokenResponse;
            _logger.LogDebug(response?.ToString());
            return new GetMfgToken(response.TokenDataLength, response.TokenData);
        }

        /// <summary>
        /// Sets a manufacturing token in the Customer Information Block (CIB) area of the NCP if that token currently unset (fully erased). Cannot be used with SL_ZIGBEE_EZSP_STACK_CAL_DATA, SL_ZIGBEE_EZSP_STACK_CAL_FILTER, SL_ZIGBEE_EZSP_MFG_ASH_CONFIG, or SL_ZIGBEE_EZSP_MFG_CBKE_DATA token.
        /// </summary>
        /// <param name="TokenId">Which manufacturing token to set.</param>
        /// <param name="TokenDataLength">The length of the &lt;i&gt;tokenData&lt;/i&gt; parameter in bytes.</param>
        /// <param name="TokenData">The manufacturing token data.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetMfgToken(ZigbeeEzspMfgTokenId tokenId, byte tokenDataLength, byte[] tokenData, CancellationToken cancellationToken = default)
        {
            SetMfgTokenRequest request = new SetMfgTokenRequest();
            request.TokenId = tokenId;
            request.TokenDataLength = tokenDataLength;
            request.TokenData = tokenData;
            SetMfgTokenResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetMfgTokenResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked to inform the application that a stack token has changed.
        /// </summary>
        /// <returns>The address of the stack token that has changed.</returns>
        public async Task<ushort> StackTokenChangedHandler(CancellationToken cancellationToken = default)
        {
            StackTokenChangedHandlerRequest request = new StackTokenChangedHandlerRequest();
            StackTokenChangedHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StackTokenChangedHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.TokenAddress;
        }

        /// <summary>
        /// Returns a pseudorandom number.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: Always returns SL_STATUS_OK.
        /// - Value: A pseudorandom number.
        /// </returns>
        public async Task<(Status Status, ushort Value)> GetRandomNumber(CancellationToken cancellationToken = default)
        {
            GetRandomNumberRequest request = new GetRandomNumberRequest();
            GetRandomNumberResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetRandomNumberResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> SetTimer(byte timerId, ushort time, ZigbeeEventUnits units, bool repeat, CancellationToken cancellationToken = default)
        {
            SetTimerRequest request = new SetTimerRequest();
            request.TimerId = timerId;
            request.Time = time;
            request.Units = units;
            request.Repeat = repeat;
            SetTimerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetTimerResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<GetTimer> GetTimer(byte timerId, CancellationToken cancellationToken = default)
        {
            GetTimerRequest request = new GetTimerRequest();
            request.TimerId = timerId;
            GetTimerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTimerResponse;
            _logger.LogDebug(response?.ToString());
            return new GetTimer(response.Time, response.Units, response.Repeat);
        }

        /// <summary>
        /// A callback from the timer.
        /// </summary>
        /// <returns>Which timer generated the callback (0 or 1).</returns>
        public async Task<byte> TimerHandler(CancellationToken cancellationToken = default)
        {
            TimerHandlerRequest request = new TimerHandlerRequest();
            TimerHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as TimerHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.TimerId;
        }

        /// <summary>
        /// Sends a debug message from the Host to the Network Analyzer utility via the NCP.
        /// </summary>
        /// <param name="BinaryMessage">true if the message should be interpreted as binary data, false if the message should be interpreted as ASCII text.</param>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The binary message.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> DebugWrite(bool binaryMessage, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
        {
            DebugWriteRequest request = new DebugWriteRequest();
            request.BinaryMessage = binaryMessage;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            DebugWriteResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DebugWriteResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Retrieves and clears Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
        /// </summary>
        /// <returns>A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.</returns>
        public async Task<ushort[]> ReadAndClearCounters(CancellationToken cancellationToken = default)
        {
            ReadAndClearCountersRequest request = new ReadAndClearCountersRequest();
            ReadAndClearCountersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ReadAndClearCountersResponse;
            _logger.LogDebug(response?.ToString());
            return response.Values;
        }

        /// <summary>
        /// Retrieves Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
        /// </summary>
        /// <returns>A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.</returns>
        public async Task<ushort[]> ReadCounters(CancellationToken cancellationToken = default)
        {
            ReadCountersRequest request = new ReadCountersRequest();
            ReadCountersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ReadCountersResponse;
            _logger.LogDebug(response?.ToString());
            return response.Values;
        }

        /// <summary>
        /// This call is fired when a counter exceeds its threshold
        /// </summary>
        /// <returns>Type of Counter</returns>
        public async Task<ZigbeeCounterType> CounterRolloverHandler(CancellationToken cancellationToken = default)
        {
            CounterRolloverHandlerRequest request = new CounterRolloverHandlerRequest();
            CounterRolloverHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CounterRolloverHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Type;
        }

        /// <summary>
        /// This call is fired when mux detects an invalid rx case, which would be different rx channels for different protocol contexts, when fast cahnnel switching is not enabled
        /// </summary>
        /// <returns>A tuple containing:
        /// - NewRxChannel: 
        /// - OldRxChannel: 
        /// </returns>
        public async Task<MuxInvalidRxHandler> MuxInvalidRxHandler(CancellationToken cancellationToken = default)
        {
            MuxInvalidRxHandlerRequest request = new MuxInvalidRxHandlerRequest();
            MuxInvalidRxHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MuxInvalidRxHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new MuxInvalidRxHandler(response.NewRxChannel, response.OldRxChannel);
        }

        /// <summary>
        /// Used to test that UART flow control is working correctly.
        /// </summary>
        /// <param name="Delay">Data will not be read from the host for this many milliseconds.</param>
        /// <returns>The DelayTestResponse object from the NCP</returns>
        public async Task<DelayTestResponse> DelayTest(ushort delay, CancellationToken cancellationToken = default)
        {
            DelayTestRequest request = new DelayTestRequest();
            request.Delay = delay;
            DelayTestResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DelayTestResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// This retrieves the status of the passed library ID to determine if it is compiled into the stack.
        /// </summary>
        /// <param name="LibraryId">The ID of the library being queried.</param>
        /// <returns>The status of the library being queried.</returns>
        public async Task<byte> GetLibraryStatus(byte libraryId, CancellationToken cancellationToken = default)
        {
            GetLibraryStatusRequest request = new GetLibraryStatusRequest();
            request.LibraryId = libraryId;
            GetLibraryStatusResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetLibraryStatusResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, GetXncpInfo Result)> GetXncpInfo(CancellationToken cancellationToken = default)
        {
            GetXncpInfoRequest request = new GetXncpInfoRequest();
            GetXncpInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetXncpInfoResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetXncpInfo(response.ManufacturerId, response.VersionNumber));
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
        public async Task<(Status Status, CustomFrame Result)> CustomFrame(byte payloadLength, byte[] payload, CancellationToken cancellationToken = default)
        {
            CustomFrameRequest request = new CustomFrameRequest();
            request.PayloadLength = payloadLength;
            request.Payload = payload;
            CustomFrameResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CustomFrameResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new CustomFrame(response.ReplyLength, response.Reply));
        }

        /// <summary>
        /// A callback indicating a custom EZSP message has been received.
        /// </summary>
        /// <returns>A tuple containing:
        /// - PayloadLength: The length of the custom frame payload.
        /// - Payload: The payload of the custom frame.
        /// </returns>
        public async Task<CustomFrameHandler> CustomFrameHandler(CancellationToken cancellationToken = default)
        {
            CustomFrameHandlerRequest request = new CustomFrameHandlerRequest();
            CustomFrameHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CustomFrameHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new CustomFrameHandler(response.PayloadLength, response.Payload);
        }

        /// <summary>
        /// Returns the EUI64 ID of the local node.
        /// </summary>
        /// <returns>The 64-bit ID.</returns>
        public async Task<byte[]> GetEui64(CancellationToken cancellationToken = default)
        {
            GetEui64Request request = new GetEui64Request();
            GetEui64Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetEui64Response;
            _logger.LogDebug(response?.ToString());
            return response.Eui64;
        }

        /// <summary>
        /// Returns the 16-bit node ID of the local node.
        /// </summary>
        /// <returns>The 16-bit ID.</returns>
        public async Task<ushort> GetNodeId(CancellationToken cancellationToken = default)
        {
            GetNodeIdRequest request = new GetNodeIdRequest();
            GetNodeIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetNodeIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.NodeId;
        }

        /// <summary>
        /// Returns number of phy interfaces present.
        /// </summary>
        /// <returns>Value indicate how many phy interfaces present.</returns>
        public async Task<byte> GetPhyInterfaceCount(CancellationToken cancellationToken = default)
        {
            GetPhyInterfaceCountRequest request = new GetPhyInterfaceCountRequest();
            GetPhyInterfaceCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetPhyInterfaceCountResponse;
            _logger.LogDebug(response?.ToString());
            return response.InterfaceCount;
        }

        /// <summary>
        /// Returns the entropy source used for true random number generation.
        /// </summary>
        /// <returns>Value indicates the used entropy source.</returns>
        public async Task<ZigbeeEntropySource> GetTrueRandomEntropySource(CancellationToken cancellationToken = default)
        {
            GetTrueRandomEntropySourceRequest request = new GetTrueRandomEntropySourceRequest();
            GetTrueRandomEntropySourceResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTrueRandomEntropySourceResponse;
            _logger.LogDebug(response?.ToString());
            return response.EntropySource;
        }

        /// <summary>
        /// Extend a joiner&apos;s timeout to wait for the network key on the joiner default key timeout is 3 sec, and only values greater equal to 3 sec are accepted.
        /// </summary>
        /// <param name="NetworkKeyTimeoutS">Network key timeout</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetupDelayedJoin(byte networkKeyTimeoutS, CancellationToken cancellationToken = default)
        {
            SetupDelayedJoinRequest request = new SetupDelayedJoinRequest();
            request.NetworkKeyTimeoutS = networkKeyTimeoutS;
            SetupDelayedJoinResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetupDelayedJoinResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Get the current scheduler priorities for radio operations
        /// </summary>
        /// <returns>The current priorities.</returns>
        public async Task<_802154RadioPriorities> RadioGetSchedulerPriorities(CancellationToken cancellationToken = default)
        {
            RadioGetSchedulerPrioritiesRequest request = new RadioGetSchedulerPrioritiesRequest();
            RadioGetSchedulerPrioritiesResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RadioGetSchedulerPrioritiesResponse;
            _logger.LogDebug(response?.ToString());
            return response.Priorities;
        }

        /// <summary>
        /// Set the current scheduler priorities for radio operations
        /// </summary>
        /// <param name="Priorities">The current priorities.</param>
        /// <returns>The RadioSetSchedulerPrioritiesResponse object from the NCP</returns>
        public async Task<RadioSetSchedulerPrioritiesResponse> RadioSetSchedulerPriorities(_802154RadioPriorities priorities, CancellationToken cancellationToken = default)
        {
            RadioSetSchedulerPrioritiesRequest request = new RadioSetSchedulerPrioritiesRequest();
            request.Priorities = priorities;
            RadioSetSchedulerPrioritiesResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RadioSetSchedulerPrioritiesResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Get the current multiprotocol sliptime
        /// </summary>
        /// <returns>Value of the current slip time.</returns>
        public async Task<uint[]> RadioGetSchedulerSliptime(CancellationToken cancellationToken = default)
        {
            RadioGetSchedulerSliptimeRequest request = new RadioGetSchedulerSliptimeRequest();
            RadioGetSchedulerSliptimeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RadioGetSchedulerSliptimeResponse;
            _logger.LogDebug(response?.ToString());
            return response.SlipTime;
        }

        /// <summary>
        /// Set the current multiprotocol sliptime
        /// </summary>
        /// <param name="SlipTime">Value of the current slip time.</param>
        /// <returns>The RadioSetSchedulerSliptimeResponse object from the NCP</returns>
        public async Task<RadioSetSchedulerSliptimeResponse> RadioSetSchedulerSliptime(uint slipTime, CancellationToken cancellationToken = default)
        {
            RadioSetSchedulerSliptimeRequest request = new RadioSetSchedulerSliptimeRequest();
            request.SlipTime = slipTime;
            RadioSetSchedulerSliptimeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RadioSetSchedulerSliptimeResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Check if a particular counter is one that could report from either a 2.4GHz or sub-GHz interface.
        /// </summary>
        /// <param name="Counter">The counter to be checked.</param>
        /// <returns>Whether this counter requires a PHY index when operating on a dual-PHY system.</returns>
        public async Task<bool> CounterRequiresPhyIndex(ZigbeeCounterType counter, CancellationToken cancellationToken = default)
        {
            CounterRequiresPhyIndexRequest request = new CounterRequiresPhyIndexRequest();
            request.Counter = counter;
            CounterRequiresPhyIndexResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CounterRequiresPhyIndexResponse;
            _logger.LogDebug(response?.ToString());
            return response.Requires;
        }

        /// <summary>
        /// Check if a particular counter can report on the destination node ID they have been triggered from.
        /// </summary>
        /// <param name="Counter">The counter to be checked.</param>
        /// <returns>Whether this counter requires the destination node ID.</returns>
        public async Task<bool> CounterRequiresDestinationNodeId(ZigbeeCounterType counter, CancellationToken cancellationToken = default)
        {
            CounterRequiresDestinationNodeIdRequest request = new CounterRequiresDestinationNodeIdRequest();
            request.Counter = counter;
            CounterRequiresDestinationNodeIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CounterRequiresDestinationNodeIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.Requires;
        }

        /// <summary>
        /// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
        /// </summary>
        /// <param name="Code">The manufacturer code for the local node.</param>
        /// <returns></returns>
        public async Task<Status> SetManufacturerCode(ushort code, CancellationToken cancellationToken = default)
        {
            SetManufacturerCodeRequest request = new SetManufacturerCodeRequest();
            request.Code = code;
            SetManufacturerCodeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetManufacturerCodeResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
        /// </summary>
        /// <returns>The manufacturer code for the local node.</returns>
        public async Task<ushort> GetManufacturerCode(CancellationToken cancellationToken = default)
        {
            GetManufacturerCodeRequest request = new GetManufacturerCodeRequest();
            GetManufacturerCodeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetManufacturerCodeResponse;
            _logger.LogDebug(response?.ToString());
            return response.Code;
        }

        /// <summary>
        /// Sets the power descriptor to the specified value. The power descriptor is a dynamic value. Therefore, you should call this function whenever the value changes.
        /// </summary>
        /// <param name="Descriptor">The new power descriptor for the local node.</param>
        /// <returns></returns>
        public async Task<Status> SetPowerDescriptor(ushort descriptor, CancellationToken cancellationToken = default)
        {
            SetPowerDescriptorRequest request = new SetPowerDescriptorRequest();
            request.Descriptor = descriptor;
            SetPowerDescriptorResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetPowerDescriptorResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Resume network operation after a reboot. The node retains its original type. This should be called on startup whether or not the node was previously part of a network. SL_STATUS_NOT_JOINED is returned if the node is not part of a network. This command accepts options to control the network initialization.
        /// </summary>
        /// <param name="NetworkInitStruct">An sl_zigbee_network_init_struct_t containing the options for initialization.</param>
        /// <returns>An sl_status_t value that indicates one of the following: successful initialization, SL_STATUS_NOT_JOINED if the node is not part of a network, or the reason for failure.</returns>
        public async Task<Status> NetworkInit(ZigbeeNetworkInitStruct networkInitStruct, CancellationToken cancellationToken = default)
        {
            NetworkInitRequest request = new NetworkInitRequest();
            request.NetworkInitStruct = networkInitStruct;
            NetworkInitResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NetworkInitResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns a value indicating whether the node is joining, joined to, or leaving a network.
        /// </summary>
        /// <returns>An sl_zigbee_network_status_t value indicating the current join status.</returns>
        public async Task<ZigbeeNetworkStatus> NetworkState(CancellationToken cancellationToken = default)
        {
            NetworkStateRequest request = new NetworkStateRequest();
            NetworkStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NetworkStateResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked when the status of the stack changes. If the status parameter equals SL_STATUS_NETWORK_UP, then the &lt;i&gt;getNetworkParameters&lt;/i&gt; command can be called to obtain the new network parameters. If any of the parameters are being stored in nonvolatile memory by the Host, the stored values should be updated.
        /// </summary>
        /// <returns>Stack status</returns>
        public async Task<Status> StackStatusHandler(CancellationToken cancellationToken = default)
        {
            StackStatusHandlerRequest request = new StackStatusHandlerRequest();
            StackStatusHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StackStatusHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function will start a scan.
        /// </summary>
        /// <param name="ScanType">Indicates the type of scan to be performed. Possible values are: SL_ZIGBEE_EZSP_ENERGY_SCAN and SL_ZIGBEE_EZSP_ACTIVE_SCAN. For each type, the respective callback for reporting results is: energyScanResultHandler and networkFoundHandler. The energy scan and active scan report errors and completion via the scanCompleteHandler.</param>
        /// <param name="ChannelMask">Bits set as 1 indicate that this particular channel should be scanned. Bits set to 0 indicate that this particular channel should not be scanned. For example, a channelMask value of 0x00000001 would indicate that only channel 0 should be scanned. Valid channels range from 11 to 26 inclusive. This translates to a channel mask value of 0x07FFF800. As a convenience, a value of 0 is reinterpreted as the mask for the current channel.</param>
        /// <param name="Duration">Sets the exponent of the number of scan periods, where a scan period is 960 symbols. The scan will occur for ((2^duration) + 1) scan periods.</param>
        /// <returns>SL_STATUS_OK signals that the scan successfully started. Possible error responses and their meanings: SL_STATUS_MAC_SCANNING, we are already scanning; SL_STATUS_BAD_SCAN_DURATION, we have set a duration value that is not 0..14 inclusive; SL_STATUS_MAC_INCORRECT_SCAN_TYPE, we have requested an undefined scanning type; SL_STATUS_INVALID_CHANNEL_MASK, our channel mask did not specify any valid channels.</returns>
        public async Task<Status> StartScan(ZigbeeEzspNetworkScanType scanType, uint channelMask, byte duration, CancellationToken cancellationToken = default)
        {
            StartScanRequest request = new StartScanRequest();
            request.ScanType = scanType;
            request.ChannelMask = channelMask;
            request.Duration = duration;
            StartScanResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StartScanResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Reports the result of an energy scan for a single channel. The scan is not complete until the &lt;i&gt;scanCompleteHandler&lt;/i&gt; callback is called.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Channel: The 802.15.4 channel number that was scanned.
        /// - MaxRssiValue: The maximum RSSI value found on the channel.
        /// </returns>
        public async Task<EnergyScanResultHandler> EnergyScanResultHandler(CancellationToken cancellationToken = default)
        {
            EnergyScanResultHandlerRequest request = new EnergyScanResultHandlerRequest();
            EnergyScanResultHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as EnergyScanResultHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new EnergyScanResultHandler(response.Channel, response.MaxRssiValue);
        }

        /// <summary>
        /// Reports that a network was found as a result of a prior call to startScan. Gives the network parameters useful for deciding which network to join.
        /// </summary>
        /// <returns>A tuple containing:
        /// - NetworkFound: The parameters associated with the network found.
        /// - LastHopLqi: Link quality of incoming packet from network.
        /// - LastHopRssi: Power (in dBm) of incoming packet.
        /// </returns>
        public async Task<NetworkFoundHandler> NetworkFoundHandler(CancellationToken cancellationToken = default)
        {
            NetworkFoundHandlerRequest request = new NetworkFoundHandlerRequest();
            NetworkFoundHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NetworkFoundHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new NetworkFoundHandler(response.NetworkFound, response.LastHopLqi, response.LastHopRssi);
        }

        /// <summary>
        /// Returns the status of the current scan of type SL_ZIGBEE_EZSP_ENERGY_SCAN or SL_ZIGBEE_EZSP_ACTIVE_SCAN. SL_STATUS_OK signals that the scan has completed. Other error conditions signify a failure to scan on the channel specified.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Channel: The channel on which the current error occurred. Undefined for the case of SL_STATUS_OK.
        /// - Status: The error condition that occurred on the current channel. Value will be SL_STATUS_OK when the scan has completed.
        /// </returns>
        public async Task<(Status Status, byte Channel)> ScanCompleteHandler(CancellationToken cancellationToken = default)
        {
            ScanCompleteHandlerRequest request = new ScanCompleteHandlerRequest();
            ScanCompleteHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ScanCompleteHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Channel);
        }

        /// <summary>
        /// This function returns an unused panID and channel pair found via the find unused panId scan procedure.
        /// </summary>
        /// <returns>A tuple containing:
        /// - PanId: The unused panID which has been found.
        /// - Channel: The channel that the unused panID was found on.
        /// </returns>
        public async Task<UnusedPanIdFoundHandler> UnusedPanIdFoundHandler(CancellationToken cancellationToken = default)
        {
            UnusedPanIdFoundHandlerRequest request = new UnusedPanIdFoundHandlerRequest();
            UnusedPanIdFoundHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as UnusedPanIdFoundHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new UnusedPanIdFoundHandler(response.PanId, response.Channel);
        }

        /// <summary>
        /// This function starts a series of scans which will return an available panId.
        /// </summary>
        /// <param name="ChannelMask">The channels that will be scanned for available panIds.</param>
        /// <param name="Duration">The duration of the procedure.</param>
        /// <returns>The error condition that occurred during the scan. Value will be SL_STATUS_OK if there are no errors.</returns>
        public async Task<Status> FindUnusedPanId(uint channelMask, byte duration, CancellationToken cancellationToken = default)
        {
            FindUnusedPanIdRequest request = new FindUnusedPanIdRequest();
            request.ChannelMask = channelMask;
            request.Duration = duration;
            FindUnusedPanIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as FindUnusedPanIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Terminates a scan in progress.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> StopScan(CancellationToken cancellationToken = default)
        {
            StopScanRequest request = new StopScanRequest();
            StopScanResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StopScanResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Forms a new network by becoming the coordinator.
        /// </summary>
        /// <param name="Parameters">Specification of the new network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> FormNetwork(ZigbeeNetworkParameters parameters, CancellationToken cancellationToken = default)
        {
            FormNetworkRequest request = new FormNetworkRequest();
            request.Parameters = parameters;
            FormNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as FormNetworkResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Causes the stack to associate with the network using the specified network parameters. It can take several seconds for the stack to associate with the local network. Do not send messages until the &lt;i&gt;stackStatusHandler&lt;/i&gt; callback informs you that the stack is up.
        /// </summary>
        /// <param name="NodeType">Specification of the role that this node will have in the network. This role must not be SL_ZIGBEE_COORDINATOR. To be a coordinator, use the &lt;i&gt;formNetwork&lt;/i&gt; command.</param>
        /// <param name="Parameters">Specification of the network with which the node should associate.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> JoinNetwork(ZigbeeNodeType nodeType, ZigbeeNetworkParameters parameters, CancellationToken cancellationToken = default)
        {
            JoinNetworkRequest request = new JoinNetworkRequest();
            request.NodeType = nodeType;
            request.Parameters = parameters;
            JoinNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as JoinNetworkResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> JoinNetworkDirectly(ZigbeeNodeType localNodeType, ZigbeeBeaconData beacon, sbyte radioTxPower, bool clearBeaconsAfterNetworkUp, CancellationToken cancellationToken = default)
        {
            JoinNetworkDirectlyRequest request = new JoinNetworkDirectlyRequest();
            request.LocalNodeType = localNodeType;
            request.Beacon = beacon;
            request.RadioTxPower = radioTxPower;
            request.ClearBeaconsAfterNetworkUp = clearBeaconsAfterNetworkUp;
            JoinNetworkDirectlyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as JoinNetworkDirectlyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Causes the stack to leave the current network. This generates a &lt;i&gt;stackStatusHandler&lt;/i&gt; callback to indicate that the network is down. The radio will not be used until after sending a &lt;i&gt;formNetwork&lt;/i&gt; or &lt;i&gt;joinNetwork&lt;/i&gt; command.
        /// </summary>
        /// <param name="Options">This parameter gives options when leave network</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> LeaveNetwork(ZigbeeLeaveNetworkOption options, CancellationToken cancellationToken = default)
        {
            LeaveNetworkRequest request = new LeaveNetworkRequest();
            request.Options = options;
            LeaveNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as LeaveNetworkResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> FindAndRejoinNetwork(bool haveCurrentNetworkKey, uint channelMask, byte reason, byte nodeType, CancellationToken cancellationToken = default)
        {
            FindAndRejoinNetworkRequest request = new FindAndRejoinNetworkRequest();
            request.HaveCurrentNetworkKey = haveCurrentNetworkKey;
            request.ChannelMask = channelMask;
            request.Reason = reason;
            request.NodeType = nodeType;
            FindAndRejoinNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as FindAndRejoinNetworkResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Tells the stack to allow other nodes to join the network with this node as their parent. Joining is initially disabled by default.
        /// </summary>
        /// <param name="Duration">A value of 0x00 disables joining. A value of 0xFF enables joining. Any other value enables joining for that number of seconds.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> PermitJoining(byte duration, CancellationToken cancellationToken = default)
        {
            PermitJoiningRequest request = new PermitJoiningRequest();
            request.Duration = duration;
            PermitJoiningResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as PermitJoiningResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<ChildJoinHandler> ChildJoinHandler(CancellationToken cancellationToken = default)
        {
            ChildJoinHandlerRequest request = new ChildJoinHandlerRequest();
            ChildJoinHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ChildJoinHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new ChildJoinHandler(response.Index, response.Joining, response.ChildId, response.ChildEui64, response.ChildType);
        }

        /// <summary>
        /// Sends a ZDO energy scan request. This request may only be sent by the current network manager and must be unicast, not broadcast. See ezsp-utils.h for related macros sli_zigbee_stack_set_network_manager_request() and sl_zigbee_change_channel_request().
        /// </summary>
        /// <param name="Target">The network address of the node to perform the scan.</param>
        /// <param name="ScanChannels">A mask of the channels to be scanned</param>
        /// <param name="ScanDuration">How long to scan on each channel. Allowed values are 0..5, with the scan times as specified by 802.15.4 (0 = 31ms, 1 = 46ms, 2 = 77ms, 3 = 138ms, 4 = 261ms, 5 = 507ms).</param>
        /// <param name="ScanCount">The number of scans to be performed on each channel (1..8).</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> EnergyScanRequest(ushort target, uint scanChannels, byte scanDuration, ushort scanCount, CancellationToken cancellationToken = default)
        {
            EnergyScanRequestRequest request = new EnergyScanRequestRequest();
            request.Target = target;
            request.ScanChannels = scanChannels;
            request.ScanDuration = scanDuration;
            request.ScanCount = scanCount;
            EnergyScanRequestResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as EnergyScanRequestResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, GetNetworkParameters Result)> GetNetworkParameters(CancellationToken cancellationToken = default)
        {
            GetNetworkParametersRequest request = new GetNetworkParametersRequest();
            GetNetworkParametersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetNetworkParametersResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetNetworkParameters(response.NodeType, response.Parameters));
        }

        /// <summary>
        /// Returns the current radio parameters based on phy index.
        /// </summary>
        /// <param name="PhyIndex">Desired index of phy interface for radio parameters.</param>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - Parameters: The current radio parameters based on provided phy index.
        /// </returns>
        public async Task<(Status Status, ZigbeeMultiPhyRadioParameters Parameters)> GetRadioParameters(byte phyIndex, CancellationToken cancellationToken = default)
        {
            GetRadioParametersRequest request = new GetRadioParametersRequest();
            request.PhyIndex = phyIndex;
            GetRadioParametersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetRadioParametersResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<GetParentChildParameters> GetParentChildParameters(CancellationToken cancellationToken = default)
        {
            GetParentChildParametersRequest request = new GetParentChildParametersRequest();
            GetParentChildParametersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetParentChildParametersResponse;
            _logger.LogDebug(response?.ToString());
            return new GetParentChildParameters(response.ChildCount, response.ParentEui64, response.ParentNodeId);
        }

        /// <summary>
        /// Return the number of router children that the node currently has.
        /// </summary>
        /// <returns>The number of router children.</returns>
        public async Task<byte> RouterChildCount(CancellationToken cancellationToken = default)
        {
            RouterChildCountRequest request = new RouterChildCountRequest();
            RouterChildCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RouterChildCountResponse;
            _logger.LogDebug(response?.ToString());
            return response.RouterChildCount;
        }

        /// <summary>
        /// Return the maximum number of children for this node. The return value is undefined for nodes that are not joined to a network.
        /// </summary>
        /// <returns>The maximum number of children.</returns>
        public async Task<byte> MaxChildCount(CancellationToken cancellationToken = default)
        {
            MaxChildCountRequest request = new MaxChildCountRequest();
            MaxChildCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MaxChildCountResponse;
            _logger.LogDebug(response?.ToString());
            return response.MaxChildCount;
        }

        /// <summary>
        /// Return the maximum number of router children for this node. The return value is undefined for nodes that are not joined to a network.
        /// </summary>
        /// <returns>The maximum number of router children.</returns>
        public async Task<byte> MaxRouterChildCount(CancellationToken cancellationToken = default)
        {
            MaxRouterChildCountRequest request = new MaxRouterChildCountRequest();
            MaxRouterChildCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MaxRouterChildCountResponse;
            _logger.LogDebug(response?.ToString());
            return response.MaxRouterChildCount;
        }

        public async Task<uint> GetParentIncomingNwkFrameCounter(CancellationToken cancellationToken = default)
        {
            GetParentIncomingNwkFrameCounterRequest request = new GetParentIncomingNwkFrameCounterRequest();
            GetParentIncomingNwkFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetParentIncomingNwkFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
            return response.ParentIncomingNwkFrameCounter;
        }

        public async Task<Status> SetParentIncomingNwkFrameCounter(uint value, CancellationToken cancellationToken = default)
        {
            SetParentIncomingNwkFrameCounterRequest request = new SetParentIncomingNwkFrameCounterRequest();
            request.Value = value;
            SetParentIncomingNwkFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetParentIncomingNwkFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Return a bitmask indicating the stack&apos;s current tasks. The mask ::SL_ZIGBEE_HIGH_PRIORITY_TASKS defines which tasks are high priority.  Devices should not sleep if any high priority tasks are active. Active tasks that are not high priority are waiting for messages to arrive from other devices.  If there are active tasks, but no high priority ones, the device may sleep but should periodically wake up and call ::emberPollForData() in order to receive messages.  Parents will hold messages for ::SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT milliseconds before discarding them.
        /// </summary>
        /// <returns>A bitmask of the stack&apos;s active tasks.</returns>
        public async Task<ushort> CurrentStackTasks(CancellationToken cancellationToken = default)
        {
            CurrentStackTasksRequest request = new CurrentStackTasksRequest();
            CurrentStackTasksResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CurrentStackTasksResponse;
            _logger.LogDebug(response?.ToString());
            return response.ActiveTasks;
        }

        /// <summary>
        /// Indicate whether the stack is currently in a state where there are no high-priority tasks, allowing the device to sleep.
        /// There may be tasks expecting incoming messages, in which case the device should periodically wake up and call ::emberPollForData() in order to receive messages. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE
        /// </summary>
        /// <returns>True if the application may sleep but the stack may be expecting incoming messages.</returns>
        public async Task<bool> OkToNap(CancellationToken cancellationToken = default)
        {
            OkToNapRequest request = new OkToNapRequest();
            OkToNapResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as OkToNapResponse;
            _logger.LogDebug(response?.ToString());
            return response.Value;
        }

        /// <summary>
        /// Indicate whether the parent token has been set by association.
        /// </summary>
        /// <returns>True if the parent token has been set.</returns>
        public async Task<bool> ParentTokenSet(CancellationToken cancellationToken = default)
        {
            ParentTokenSetRequest request = new ParentTokenSetRequest();
            ParentTokenSetResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ParentTokenSetResponse;
            _logger.LogDebug(response?.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Indicate whether the stack currently has any tasks pending. If no tasks are pending, ::emberTick() does not need to be called until the next time a stack API function is called. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE.
        /// </summary>
        /// <returns>True if the application may sleep for as long as it wishes.</returns>
        public async Task<bool> OkToHibernate(CancellationToken cancellationToken = default)
        {
            OkToHibernateRequest request = new OkToHibernateRequest();
            OkToHibernateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as OkToHibernateResponse;
            _logger.LogDebug(response?.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Indicate whether the stack is currently in a state that does not require the application to periodically poll.
        /// </summary>
        /// <returns>True if the device may poll less frequently.</returns>
        public async Task<bool> OkToLongPoll(CancellationToken cancellationToken = default)
        {
            OkToLongPollRequest request = new OkToLongPollRequest();
            OkToLongPollResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as OkToLongPollResponse;
            _logger.LogDebug(response?.ToString());
            return response.Indicator;
        }

        /// <summary>
        /// Calling this function will render all other stack functions except sli_zigbee_stack_stack_power_up() non-functional until the radio is powered back on.
        /// </summary>
        /// <returns>The StackPowerDownResponse object from the NCP</returns>
        public async Task<StackPowerDownResponse> StackPowerDown(CancellationToken cancellationToken = default)
        {
            StackPowerDownRequest request = new StackPowerDownRequest();
            StackPowerDownResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StackPowerDownResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Initialize the radio.  Typically called coming out of deep sleep. For non-sleepy devices, also turns the radio on and leaves it in RX mode.
        /// </summary>
        /// <returns>The StackPowerUpResponse object from the NCP</returns>
        public async Task<StackPowerUpResponse> StackPowerUp(CancellationToken cancellationToken = default)
        {
            StackPowerUpRequest request = new StackPowerUpRequest();
            StackPowerUpResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as StackPowerUpResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeChildData ChildData)> GetChildData(byte index, CancellationToken cancellationToken = default)
        {
            GetChildDataRequest request = new GetChildDataRequest();
            request.Index = index;
            GetChildDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetChildDataResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.ChildData);
        }

        /// <summary>
        /// Sets child data to the child table token.
        /// </summary>
        /// <param name="Index">The index of the child of interest in the child table. Possible indexes range from zero to (SL_ZIGBEE_CHILD_TABLE_SIZE - 1).</param>
        /// <param name="ChildData">The data of the child.</param>
        /// <returns>SL_STATUS_OK if the child data is set successfully at &lt;i&gt;index&lt;/i&gt;. SL_STATUS_INVALID_INDEX if provided &lt;i&gt;index&lt;/i&gt; is out of range.</returns>
        public async Task<Status> SetChildData(byte index, ZigbeeChildData childData, CancellationToken cancellationToken = default)
        {
            SetChildDataRequest request = new SetChildDataRequest();
            request.Index = index;
            request.ChildData = childData;
            SetChildDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetChildDataResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Convert a child index to a node ID
        /// </summary>
        /// <param name="ChildIndex">The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.</param>
        /// <returns>The node ID of the child or SL_ZIGBEE_NULL_NODE_ID if there isn&apos;t a child at the childIndex specified</returns>
        public async Task<ushort> ChildId(byte childIndex, CancellationToken cancellationToken = default)
        {
            ChildIdRequest request = new ChildIdRequest();
            request.ChildIndex = childIndex;
            ChildIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ChildIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.ChildId;
        }

        /// <summary>
        /// Return radio power value of the child from the given childIndex
        /// </summary>
        /// <param name="ChildIndex">The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.</param>
        /// <returns>The power of the child or maximum radio power, which is the power value provided by the user while forming/joining a network if there isn&apos;t a child at the childIndex specified</returns>
        public async Task<sbyte> ChildPower(byte childIndex, CancellationToken cancellationToken = default)
        {
            ChildPowerRequest request = new ChildPowerRequest();
            request.ChildIndex = childIndex;
            ChildPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ChildPowerResponse;
            _logger.LogDebug(response?.ToString());
            return response.ChildPower;
        }

        /// <summary>
        /// Set the radio power value for a given child index.
        /// </summary>
        /// <param name="ChildIndex">The index.</param>
        /// <param name="NewPower">The new power value.</param>
        /// <returns>The SetChildPowerResponse object from the NCP</returns>
        public async Task<SetChildPowerResponse> SetChildPower(byte childIndex, sbyte newPower, CancellationToken cancellationToken = default)
        {
            SetChildPowerRequest request = new SetChildPowerRequest();
            request.ChildIndex = childIndex;
            request.NewPower = newPower;
            SetChildPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetChildPowerResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Convert a node ID to a child index
        /// </summary>
        /// <param name="ChildId">The node ID of the child</param>
        /// <returns>The child index or 0xFF if the node ID doesn&apos;t belong to a child</returns>
        public async Task<byte> ChildIndex(ushort childId, CancellationToken cancellationToken = default)
        {
            ChildIndexRequest request = new ChildIndexRequest();
            request.ChildId = childId;
            ChildIndexResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ChildIndexResponse;
            _logger.LogDebug(response?.ToString());
            return response.ChildIndex;
        }

        /// <summary>
        /// Returns the source route table total size.
        /// </summary>
        /// <returns>Total size of source route table.</returns>
        public async Task<byte> GetSourceRouteTableTotalSize(CancellationToken cancellationToken = default)
        {
            GetSourceRouteTableTotalSizeRequest request = new GetSourceRouteTableTotalSizeRequest();
            GetSourceRouteTableTotalSizeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetSourceRouteTableTotalSizeResponse;
            _logger.LogDebug(response?.ToString());
            return response.SourceRouteTableTotalSize;
        }

        /// <summary>
        /// Returns the number of filled entries in source route table.
        /// </summary>
        /// <returns>The number of filled entries in source route table.</returns>
        public async Task<byte> GetSourceRouteTableFilledSize(CancellationToken cancellationToken = default)
        {
            GetSourceRouteTableFilledSizeRequest request = new GetSourceRouteTableFilledSizeRequest();
            GetSourceRouteTableFilledSizeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetSourceRouteTableFilledSizeResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, GetSourceRouteTableEntry Result)> GetSourceRouteTableEntry(byte index, CancellationToken cancellationToken = default)
        {
            GetSourceRouteTableEntryRequest request = new GetSourceRouteTableEntryRequest();
            request.Index = index;
            GetSourceRouteTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetSourceRouteTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetSourceRouteTableEntry(response.Destination, response.CloserIndex));
        }

        /// <summary>
        /// Returns the neighbor table entry at the given index. The number of active neighbors can be obtained using the neighborCount command.
        /// </summary>
        /// <param name="Index">The index of the neighbor of interest. Neighbors are stored in ascending order by node id, with all unused entries at the end of the table.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_FAIL if the index is greater or equal to the number of active neighbors, or if the device is an end device. Returns SL_STATUS_OK otherwise.
        /// - Value: The contents of the neighbor table entry.
        /// </returns>
        public async Task<(Status Status, ZigbeeNeighborTableEntry Value)> GetNeighbor(byte index, CancellationToken cancellationToken = default)
        {
            GetNeighborRequest request = new GetNeighborRequest();
            request.Index = index;
            GetNeighborResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetNeighborResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, uint ReturnFrameCounter)> GetNeighborFrameCounter(byte[] eui64, CancellationToken cancellationToken = default)
        {
            GetNeighborFrameCounterRequest request = new GetNeighborFrameCounterRequest();
            request.Eui64 = eui64;
            GetNeighborFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetNeighborFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.ReturnFrameCounter);
        }

        /// <summary>
        /// Sets the frame counter for the neighbour or child.
        /// </summary>
        /// <param name="Eui64">eui64 of the node</param>
        /// <param name="FrameCounter">Return the frame counter of the node from the neighbor or child table</param>
        /// <returns>Return SL_STATUS_NOT_FOUND if the node is not found in the neighbor or child table. Returns SL_STATUS_OK otherwise</returns>
        public async Task<Status> SetNeighborFrameCounter(byte[] eui64, uint frameCounter, CancellationToken cancellationToken = default)
        {
            SetNeighborFrameCounterRequest request = new SetNeighborFrameCounterRequest();
            request.Eui64 = eui64;
            request.FrameCounter = frameCounter;
            SetNeighborFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetNeighborFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the routing shortcut threshold to directly use a neighbor instead of performing routing.
        /// </summary>
        /// <param name="CostThresh">The routing shortcut threshold to configure.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetRoutingShortcutThreshold(byte costThresh, CancellationToken cancellationToken = default)
        {
            SetRoutingShortcutThresholdRequest request = new SetRoutingShortcutThresholdRequest();
            request.CostThresh = costThresh;
            SetRoutingShortcutThresholdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetRoutingShortcutThresholdResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the routing shortcut threshold used to differentiate between directly using a neighbor vs. performing routing.
        /// </summary>
        /// <returns>The routing shortcut threshold</returns>
        public async Task<byte> GetRoutingShortcutThreshold(CancellationToken cancellationToken = default)
        {
            GetRoutingShortcutThresholdRequest request = new GetRoutingShortcutThresholdRequest();
            GetRoutingShortcutThresholdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetRoutingShortcutThresholdResponse;
            _logger.LogDebug(response?.ToString());
            return response.RoutingShortcutThresh;
        }

        /// <summary>
        /// Returns the number of active entries in the neighbor table.
        /// </summary>
        /// <returns>The number of active entries in the neighbor table.</returns>
        public async Task<byte> NeighborCount(CancellationToken cancellationToken = default)
        {
            NeighborCountRequest request = new NeighborCountRequest();
            NeighborCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as NeighborCountResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeRouteTableEntry Value)> GetRouteTableEntry(byte index, CancellationToken cancellationToken = default)
        {
            GetRouteTableEntryRequest request = new GetRouteTableEntryRequest();
            request.Index = index;
            GetRouteTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetRouteTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Sets the radio output power at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
        /// </summary>
        /// <param name="Power">Desired radio output power, in dBm.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> SetRadioPower(sbyte power, CancellationToken cancellationToken = default)
        {
            SetRadioPowerRequest request = new SetRadioPowerRequest();
            request.Power = power;
            SetRadioPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetRadioPowerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the channel to use for sending and receiving messages. For a list of available radio channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same channel.
        /// </summary>
        /// <param name="Channel">Desired radio channel.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> SetRadioChannel(byte channel, CancellationToken cancellationToken = default)
        {
            SetRadioChannelRequest request = new SetRadioChannelRequest();
            request.Channel = channel;
            SetRadioChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetRadioChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Gets the channel in use for sending and receiving messages.
        /// </summary>
        /// <returns>Current radio channel.</returns>
        public async Task<byte> GetRadioChannel(CancellationToken cancellationToken = default)
        {
            GetRadioChannelRequest request = new GetRadioChannelRequest();
            GetRadioChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetRadioChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Channel;
        }

        /// <summary>
        /// Set the configured 802.15.4 CCA mode in the radio.
        /// </summary>
        /// <param name="CcaMode">A RAIL_IEEE802154_CcaMode_t value.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> SetRadioIeee802154CcaMode(byte ccaMode, CancellationToken cancellationToken = default)
        {
            SetRadioIeee802154CcaModeRequest request = new SetRadioIeee802154CcaModeRequest();
            request.CcaMode = ccaMode;
            SetRadioIeee802154CcaModeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetRadioIeee802154CcaModeResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> SetConcentrator(bool on, ushort concentratorType, ushort minTime, ushort maxTime, byte routeErrorThreshold, byte deliveryFailureThreshold, byte maxHops, CancellationToken cancellationToken = default)
        {
            SetConcentratorRequest request = new SetConcentratorRequest();
            request.On = on;
            request.ConcentratorType = concentratorType;
            request.MinTime = minTime;
            request.MaxTime = maxTime;
            request.RouteErrorThreshold = routeErrorThreshold;
            request.DeliveryFailureThreshold = deliveryFailureThreshold;
            request.MaxHops = maxHops;
            SetConcentratorResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetConcentratorResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts periodic many-to-one route discovery. Periodic discovery is started by default on bootup, but this function may be used if discovery has been stopped by a call to ::emberConcentratorStopDiscovery().
        /// </summary>
        /// <returns>The ConcentratorStartDiscoveryResponse object from the NCP</returns>
        public async Task<ConcentratorStartDiscoveryResponse> ConcentratorStartDiscovery(CancellationToken cancellationToken = default)
        {
            ConcentratorStartDiscoveryRequest request = new ConcentratorStartDiscoveryRequest();
            ConcentratorStartDiscoveryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ConcentratorStartDiscoveryResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Stops periodic many-to-one route discovery.
        /// </summary>
        /// <returns>The ConcentratorStopDiscoveryResponse object from the NCP</returns>
        public async Task<ConcentratorStopDiscoveryResponse> ConcentratorStopDiscovery(CancellationToken cancellationToken = default)
        {
            ConcentratorStopDiscoveryRequest request = new ConcentratorStopDiscoveryRequest();
            ConcentratorStopDiscoveryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ConcentratorStopDiscoveryResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<ConcentratorNoteRouteErrorResponse> ConcentratorNoteRouteError(Status status, ushort nodeId, CancellationToken cancellationToken = default)
        {
            ConcentratorNoteRouteErrorRequest request = new ConcentratorNoteRouteErrorRequest();
            request.Status = status;
            request.NodeId = nodeId;
            ConcentratorNoteRouteErrorResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ConcentratorNoteRouteErrorResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Sets the error code that is sent back from a router with a broken route. 
        /// </summary>
        /// <param name="ErrorCode">Desired error code.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> SetBrokenRouteErrorCode(byte errorCode, CancellationToken cancellationToken = default)
        {
            SetBrokenRouteErrorCodeRequest request = new SetBrokenRouteErrorCodeRequest();
            request.ErrorCode = errorCode;
            SetBrokenRouteErrorCodeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetBrokenRouteErrorCodeResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> MultiPhyStart(byte phyIndex, byte page, byte channel, sbyte power, ZigbeeMultiPhyNwkConfig bitmask, CancellationToken cancellationToken = default)
        {
            MultiPhyStartRequest request = new MultiPhyStartRequest();
            request.PhyIndex = phyIndex;
            request.Page = page;
            request.Channel = channel;
            request.Power = power;
            request.Bitmask = bitmask;
            MultiPhyStartResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MultiPhyStartResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This causes to bring down the radio interface other than native.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MultiPhyStop(byte phyIndex, CancellationToken cancellationToken = default)
        {
            MultiPhyStopRequest request = new MultiPhyStopRequest();
            request.PhyIndex = phyIndex;
            MultiPhyStopResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MultiPhyStopResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the radio output power for desired phy interface at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this api on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <param name="Power">Desired radio output power, in dBm.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> MultiPhySetRadioPower(byte phyIndex, sbyte power, CancellationToken cancellationToken = default)
        {
            MultiPhySetRadioPowerRequest request = new MultiPhySetRadioPowerRequest();
            request.PhyIndex = phyIndex;
            request.Power = power;
            MultiPhySetRadioPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MultiPhySetRadioPowerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send Link Power Delta Request from a child to its parent
        /// </summary>
        /// <returns>An sl_status_t value indicating the success or failure of sending the request.</returns>
        public async Task<Status> SendLinkPowerDeltaRequest(CancellationToken cancellationToken = default)
        {
            SendLinkPowerDeltaRequestRequest request = new SendLinkPowerDeltaRequestRequest();
            SendLinkPowerDeltaRequestResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendLinkPowerDeltaRequestResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the channel for desired phy interface to use for sending and receiving messages. For a list of available radio pages and channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same page and channel.
        /// </summary>
        /// <param name="PhyIndex">Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.</param>
        /// <param name="Page">Desired radio channel page.</param>
        /// <param name="Channel">Desired radio channel.</param>
        /// <returns>An sl_status_t value indicating the success or failure of the command.</returns>
        public async Task<Status> MultiPhySetRadioChannel(byte phyIndex, byte page, byte channel, CancellationToken cancellationToken = default)
        {
            MultiPhySetRadioChannelRequest request = new MultiPhySetRadioChannelRequest();
            request.PhyIndex = phyIndex;
            request.Page = page;
            request.Channel = channel;
            MultiPhySetRadioChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MultiPhySetRadioChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Obtains the current duty cycle state.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating the success or failure of the command.
        /// - ReturnedState: The current duty cycle state in effect.
        /// </returns>
        public async Task<(Status Status, ZigbeeDutyCycleState ReturnedState)> GetDutyCycleState(CancellationToken cancellationToken = default)
        {
            GetDutyCycleStateRequest request = new GetDutyCycleStateRequest();
            GetDutyCycleStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetDutyCycleStateResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.ReturnedState);
        }

        /// <summary>
        /// Set the current duty cycle limits configuration. The Default limits set by stack if this call is not made.
        /// </summary>
        /// <param name="Limits">The duty cycle limits configuration to utilize.</param>
        /// <returns>SL_STATUS_OK  if the duty cycle limit configurations set successfully, SL_STATUS_INVALID_PARAMETER if set illegal value such as setting only one of the limits to default or violates constraints Susp &gt; Crit &gt; Limi, SL_STATUS_INVALID_STATE if device is operating on 2.4Ghz</returns>
        public async Task<Status> SetDutyCycleLimitsInStack(ZigbeeDutyCycleLimits limits, CancellationToken cancellationToken = default)
        {
            SetDutyCycleLimitsInStackRequest request = new SetDutyCycleLimitsInStackRequest();
            request.Limits = limits;
            SetDutyCycleLimitsInStackResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetDutyCycleLimitsInStackResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Obtains the current duty cycle limits that were previously set by a call to sli_zigbee_stack_set_duty_cycle_limits_in_stack(), or the defaults set by the stack if no set call was made.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating the success or failure of the command.
        /// - ReturnedLimits: Return current duty cycle limits if returnedLimits is not NULL
        /// </returns>
        public async Task<(Status Status, ZigbeeDutyCycleLimits ReturnedLimits)> GetDutyCycleLimits(CancellationToken cancellationToken = default)
        {
            GetDutyCycleLimitsRequest request = new GetDutyCycleLimitsRequest();
            GetDutyCycleLimitsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetDutyCycleLimitsResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte[] ArrayOfDeviceDutyCycles)> GetCurrentDutyCycle(byte maxDevices, CancellationToken cancellationToken = default)
        {
            GetCurrentDutyCycleRequest request = new GetCurrentDutyCycleRequest();
            request.MaxDevices = maxDevices;
            GetCurrentDutyCycleResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetCurrentDutyCycleResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<DutyCycleHandler> DutyCycleHandler(CancellationToken cancellationToken = default)
        {
            DutyCycleHandlerRequest request = new DutyCycleHandlerRequest();
            DutyCycleHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DutyCycleHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new DutyCycleHandler(response.ChannelPage, response.Channel, response.State, response.TotalDevices, response.ArrayOfDeviceDutyCycles);
        }

        /// <summary>
        /// Configure the number of beacons to store when issuing active scans for networks.
        /// </summary>
        /// <param name="NumBeacons">The number of beacons to cache when scanning.</param>
        /// <returns>SL_STATUS_INVALID_PARAMETER if numBeacons is greater than SL_ZIGBEE_MAX_BEACONS_TO_STORE, otherwise SL_STATUS_OK</returns>
        public async Task<Status> SetNumBeaconsToStore(byte numBeacons, CancellationToken cancellationToken = default)
        {
            SetNumBeaconsToStoreRequest request = new SetNumBeaconsToStoreRequest();
            request.NumBeacons = numBeacons;
            SetNumBeaconsToStoreResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetNumBeaconsToStoreResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeBeaconData Beacon)> GetStoredBeacon(byte beaconNumber, CancellationToken cancellationToken = default)
        {
            GetStoredBeaconRequest request = new GetStoredBeaconRequest();
            request.BeaconNumber = beaconNumber;
            GetStoredBeaconResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetStoredBeaconResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Beacon);
        }

        /// <summary>
        /// Returns the number of cached beacons that have been collected from a scan.
        /// </summary>
        /// <returns>The number of cached beacons that have been collected from a scan.</returns>
        public async Task<byte> GetNumStoredBeacons(CancellationToken cancellationToken = default)
        {
            GetNumStoredBeaconsRequest request = new GetNumStoredBeaconsRequest();
            GetNumStoredBeaconsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetNumStoredBeaconsResponse;
            _logger.LogDebug(response?.ToString());
            return response.NumBeacons;
        }

        /// <summary>
        /// Clears all cached beacons that have been collected from a scan.
        /// </summary>
        /// <returns></returns>
        public async Task<Status> ClearStoredBeacons(CancellationToken cancellationToken = default)
        {
            ClearStoredBeaconsRequest request = new ClearStoredBeaconsRequest();
            ClearStoredBeaconsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearStoredBeaconsResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call sets the radio channel in the stack and propagates the information to the hardware.
        /// </summary>
        /// <param name="RadioChannel">The radio channel to be set.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetLogicalAndRadioChannel(byte radioChannel, CancellationToken cancellationToken = default)
        {
            SetLogicalAndRadioChannelRequest request = new SetLogicalAndRadioChannelRequest();
            request.RadioChannel = radioChannel;
            SetLogicalAndRadioChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetLogicalAndRadioChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Form a new sleepy-to-sleepy network.  If the network is using security, the device must call sli_zigbee_stack_set_initial_security_state() first.
        /// </summary>
        /// <param name="Parameters">Specification of the new network.</param>
        /// <param name="Initiator">Whether this device is initiating or joining the network.</param>
        /// <returns>An sl_status_t value indicating success or a reason for failure.</returns>
        public async Task<Status> SleepyToSleepyNetworkStart(ZigbeeNetworkParameters parameters, bool initiator, CancellationToken cancellationToken = default)
        {
            SleepyToSleepyNetworkStartRequest request = new SleepyToSleepyNetworkStartRequest();
            request.Parameters = parameters;
            request.Initiator = initiator;
            SleepyToSleepyNetworkStartResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SleepyToSleepyNetworkStartResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send a Zigbee NWK Leave command to the destination.
        /// </summary>
        /// <param name="Destination">Node ID of the device being told to leave.</param>
        /// <param name="Flags">Bitmask indicating additional considerations for the leave request.</param>
        /// <returns>Status indicating success or a reason for failure. Call is invalid if destination is on network or is the local node.</returns>
        public async Task<Status> SendZigbeeLeave(ushort destination, ZigbeeLeaveRequestFlags flags, CancellationToken cancellationToken = default)
        {
            SendZigbeeLeaveRequest request = new SendZigbeeLeaveRequest();
            request.Destination = destination;
            request.Flags = flags;
            SendZigbeeLeaveResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendZigbeeLeaveResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicate the state of permit joining in MAC.
        /// </summary>
        /// <returns>Whether the current network permits joining.</returns>
        public async Task<bool> GetPermitJoining(CancellationToken cancellationToken = default)
        {
            GetPermitJoiningRequest request = new GetPermitJoiningRequest();
            GetPermitJoiningResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetPermitJoiningResponse;
            _logger.LogDebug(response?.ToString());
            return response.JoiningPermitted;
        }

        /// <summary>
        /// Get the 8-byte extended PAN ID of this node.
        /// </summary>
        /// <returns>Extended PAN ID of this node.  Valid only if it is currently on a network.</returns>
        public async Task<byte[]> GetExtendedPanId(CancellationToken cancellationToken = default)
        {
            GetExtendedPanIdRequest request = new GetExtendedPanIdRequest();
            GetExtendedPanIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetExtendedPanIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.ExtendedPanId;
        }

        /// <summary>
        /// Get the current network.
        /// </summary>
        /// <returns>Return the current network index.</returns>
        public async Task<byte> GetCurrentNetwork(CancellationToken cancellationToken = default)
        {
            GetCurrentNetworkRequest request = new GetCurrentNetworkRequest();
            GetCurrentNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetCurrentNetworkResponse;
            _logger.LogDebug(response?.ToString());
            return response.Index;
        }

        /// <summary>
        /// Set initial outgoing link cost for neighbor.
        /// </summary>
        /// <param name="Cost">The new default cost. Valid values are 0, 1, 3, 5, and 7.</param>
        /// <returns>Whether or not initial cost was successfully set.</returns>
        public async Task<Status> SetInitialNeighborOutgoingCost(byte cost, CancellationToken cancellationToken = default)
        {
            SetInitialNeighborOutgoingCostRequest request = new SetInitialNeighborOutgoingCostRequest();
            request.Cost = cost;
            SetInitialNeighborOutgoingCostResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetInitialNeighborOutgoingCostResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Get initial outgoing link cost for neighbor.
        /// </summary>
        /// <returns>The default cost associated with new neighbor&apos;s outgoing links.</returns>
        public async Task<byte> GetInitialNeighborOutgoingCost(CancellationToken cancellationToken = default)
        {
            GetInitialNeighborOutgoingCostRequest request = new GetInitialNeighborOutgoingCostRequest();
            GetInitialNeighborOutgoingCostResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetInitialNeighborOutgoingCostResponse;
            _logger.LogDebug(response?.ToString());
            return response.Cost;
        }

        /// <summary>
        /// Indicate whether a rejoining neighbor should have its incoming frame counter reset.
        /// </summary>
        /// <param name="Reset">Whether or not a neighbor&apos;s incoming FC should be reset upon rejoining (true or false).</param>
        /// <returns>The ResetRejoiningNeighborsFrameCounterResponse object from the NCP</returns>
        public async Task<ResetRejoiningNeighborsFrameCounterResponse> ResetRejoiningNeighborsFrameCounter(bool reset, CancellationToken cancellationToken = default)
        {
            ResetRejoiningNeighborsFrameCounterRequest request = new ResetRejoiningNeighborsFrameCounterRequest();
            request.Reset = reset;
            ResetRejoiningNeighborsFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ResetRejoiningNeighborsFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Check whether a rejoining neighbor will have its incoming frame counter reset based on the currently set policy.
        /// </summary>
        /// <returns>Whether or not a rejoining neighbor&apos;s incoming FC gets reset (true or false).</returns>
        public async Task<bool> IsResetRejoiningNeighborsFrameCounterEnabled(CancellationToken cancellationToken = default)
        {
            IsResetRejoiningNeighborsFrameCounterEnabledRequest request = new IsResetRejoiningNeighborsFrameCounterEnabledRequest();
            IsResetRejoiningNeighborsFrameCounterEnabledResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IsResetRejoiningNeighborsFrameCounterEnabledResponse;
            _logger.LogDebug(response?.ToString());
            return response.GetsReset;
        }

        /// <summary>
        /// Deletes all binding table entries.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ClearBindingTable(CancellationToken cancellationToken = default)
        {
            ClearBindingTableRequest request = new ClearBindingTableRequest();
            ClearBindingTableResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearBindingTableResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets an entry in the binding table.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <param name="Value">The contents of the binding entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetBinding(byte index, ZigbeeBindingTableEntry value, CancellationToken cancellationToken = default)
        {
            SetBindingRequest request = new SetBindingRequest();
            request.Index = index;
            request.Value = value;
            SetBindingResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetBindingResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeBindingTableEntry Value)> GetBinding(byte index, CancellationToken cancellationToken = default)
        {
            GetBindingRequest request = new GetBindingRequest();
            request.Index = index;
            GetBindingResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetBindingResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Deletes a binding table entry.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> DeleteBinding(byte index, CancellationToken cancellationToken = default)
        {
            DeleteBindingRequest request = new DeleteBindingRequest();
            request.Index = index;
            DeleteBindingResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DeleteBindingResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether any messages are currently being sent using this binding table entry. Note that this command does not indicate whether a binding is clear. To determine whether a binding is clear, check whether the type field of the sl_zigbee_binding_table_entry_t has the value SL_ZIGBEE_UNUSED_BINDING.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>True if the binding table entry is active, false otherwise.</returns>
        public async Task<bool> BindingIsActive(byte index, CancellationToken cancellationToken = default)
        {
            BindingIsActiveRequest request = new BindingIsActiveRequest();
            request.Index = index;
            BindingIsActiveResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as BindingIsActiveResponse;
            _logger.LogDebug(response?.ToString());
            return response.Active;
        }

        /// <summary>
        /// Returns the node ID for the binding&apos;s destination, if the ID is known. If a message is sent using the binding and the destination&apos;s ID is not known, the stack will discover the ID by broadcasting a ZDO address request. The application can avoid the need for this discovery by using &lt;i&gt;setBindingRemoteNodeId&lt;/i&gt; when it knows the correct ID via some other means. The destination&apos;s node ID is forgotten when the binding is changed, when the local node reboots or, much more rarely, when the destination node changes its ID in response to an ID conflict.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <returns>The short ID of the destination node or SL_ZIGBEE_NULL_NODE_ID if no destination is known.</returns>
        public async Task<ushort> GetBindingRemoteNodeId(byte index, CancellationToken cancellationToken = default)
        {
            GetBindingRemoteNodeIdRequest request = new GetBindingRemoteNodeIdRequest();
            request.Index = index;
            GetBindingRemoteNodeIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetBindingRemoteNodeIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.NodeId;
        }

        /// <summary>
        /// Set the node ID for the binding&apos;s destination. See &lt;i&gt;getBindingRemoteNodeId&lt;/i&gt; for a description.
        /// </summary>
        /// <param name="Index">The index of a binding table entry.</param>
        /// <param name="NodeId">The short ID of the destination node.</param>
        /// <returns>The SetBindingRemoteNodeIdResponse object from the NCP</returns>
        public async Task<SetBindingRemoteNodeIdResponse> SetBindingRemoteNodeId(byte index, ushort nodeId, CancellationToken cancellationToken = default)
        {
            SetBindingRemoteNodeIdRequest request = new SetBindingRemoteNodeIdRequest();
            request.Index = index;
            request.NodeId = nodeId;
            SetBindingRemoteNodeIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetBindingRemoteNodeIdResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status PolicyDecision, RemoteSetBindingHandler Result)> RemoteSetBindingHandler(CancellationToken cancellationToken = default)
        {
            RemoteSetBindingHandlerRequest request = new RemoteSetBindingHandlerRequest();
            RemoteSetBindingHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RemoteSetBindingHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.PolicyDecision, new RemoteSetBindingHandler(response.Entry, response.Index));
        }

        /// <summary>
        /// The NCP used the external binding modification policy to decide how to handle a remote delete binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Index: The index of the binding whose deletion was requested.
        /// - PolicyDecision: SL_STATUS_OK if the binding was removed from the table and any other status if not.
        /// </returns>
        public async Task<(Status PolicyDecision, byte Index)> RemoteDeleteBindingHandler(CancellationToken cancellationToken = default)
        {
            RemoteDeleteBindingHandlerRequest request = new RemoteDeleteBindingHandlerRequest();
            RemoteDeleteBindingHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RemoteDeleteBindingHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.PolicyDecision, response.Index);
        }

        /// <summary>
        /// Returns the maximum size of the payload. The size depends on the security level in use.
        /// </summary>
        /// <returns>The maximum APS payload length.</returns>
        public async Task<byte> MaximumPayloadLength(CancellationToken cancellationToken = default)
        {
            MaximumPayloadLengthRequest request = new MaximumPayloadLengthRequest();
            MaximumPayloadLengthResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MaximumPayloadLengthResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte Sequence)> SendUnicast(ZigbeeOutgoingMessageType type, ushort indexOrDestination, ZigbeeApsFrame apsFrame, ushort messageTag, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
        {
            SendUnicastRequest request = new SendUnicastRequest();
            request.Type = type;
            request.IndexOrDestination = indexOrDestination;
            request.ApsFrame = apsFrame;
            request.MessageTag = messageTag;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            SendUnicastResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendUnicastResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte ApsSequence)> SendBroadcast(ushort alias, ushort destination, byte nwkSequence, ZigbeeApsFrame apsFrame, byte radius, ushort messageTag, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
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
            SendBroadcastResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendBroadcastResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.ApsSequence);
        }

        /// <summary>
        /// Sends proxied broadcast message for another node in conjunction with sl_zigbee_proxy_broadcast where a long source is also specified in the NWK frame control.
        /// </summary>
        /// <param name="EuiSource">The long source from which to send the broadcast</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ProxyNextBroadcastFromLong(byte[] euiSource, CancellationToken cancellationToken = default)
        {
            ProxyNextBroadcastFromLongRequest request = new ProxyNextBroadcastFromLongRequest();
            request.EuiSource = euiSource;
            ProxyNextBroadcastFromLongResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ProxyNextBroadcastFromLongResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte Sequence)> SendMulticast(ZigbeeApsFrame apsFrame, byte hops, ushort broadcastAddr, ushort alias, byte nwkSequence, ushort messageTag, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
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
            SendMulticastResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendMulticastResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> SendReply(ushort sender, ZigbeeApsFrame apsFrame, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
        {
            SendReplyRequest request = new SendReplyRequest();
            request.Sender = sender;
            request.ApsFrame = apsFrame;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            SendReplyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendReplyResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, MessageSentHandler Result)> MessageSentHandler(CancellationToken cancellationToken = default)
        {
            MessageSentHandlerRequest request = new MessageSentHandlerRequest();
            MessageSentHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MessageSentHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new MessageSentHandler(response.Type, response.IndexOrDestination, response.ApsFrame, response.MessageTag, response.MessageLength, response.MessageContents));
        }

        /// <summary>
        /// Sends a route request packet that creates routes from every node in the network back to this node. This function should be called by an application that wishes to communicate with many nodes, for example, a gateway, central monitor, or controller. A device using this function was referred to as an &apos;aggregator&apos; in EmberZNet 2.x and earlier, and is referred to as a &apos;concentrator&apos; in the ZigBee specification and EmberZNet 3. &lt;p&gt; This function enables large scale networks, because the other devices do not have to individually perform bandwidth-intensive route discoveries. Instead, when a remote node sends an APS unicast to a concentrator, its network layer automatically delivers a special route record packet first, which lists the network ids of all the intermediate relays. The concentrator can then use source routing to send outbound APS unicasts. (A source routed message is one in which the entire route is listed in the network layer header.) This allows the concentrator to communicate with thousands of devices without requiring large route tables on neighboring nodes. &lt;p&gt; This function is only available in ZigBee Pro (stack profile 2), and cannot be called on end devices. Any router can be a concentrator (not just the coordinator), and there can be multiple concentrators on a network. &lt;p&gt; Note that a concentrator does not automatically obtain routes to all network nodes after calling this function. Remote applications must first initiate an inbound APS unicast. &lt;p&gt; Many-to-one routes are not repaired automatically. Instead, the concentrator application must call this function to rediscover the routes as necessary, for example, upon failure of a retried APS message. The reason for this is that there is no scalable one-size-fits-all route repair strategy. A common and recommended strategy is for the concentrator application to refresh the routes by calling this function periodically.
        /// </summary>
        /// <param name="ConcentratorType">Must be either SL_ZIGBEE_HIGH_RAM_CONCENTRATOR or SL_ZIGBEE_LOW_RAM_CONCENTRATOR. The former is used when the caller has enough memory to store source routes for the whole network. In that case, remote nodes stop sending route records once the concentrator has successfully received one. The latter is used when the concentrator has insufficient RAM to store all outbound source routes. In that case, route records are sent to the concentrator prior to every inbound APS unicast.</param>
        /// <param name="Radius">The maximum number of hops the route request will be relayed. A radius of zero is converted to SL_ZIGBEE_MAX_HOPS</param>
        /// <returns>SL_STATUS_OK if the route request was successfully submitted to the transmit queue, and SL_STATUS_FAIL otherwise.</returns>
        public async Task<Status> SendManyToOneRouteRequest(ushort concentratorType, byte radius, CancellationToken cancellationToken = default)
        {
            SendManyToOneRouteRequestRequest request = new SendManyToOneRouteRequestRequest();
            request.ConcentratorType = concentratorType;
            request.Radius = radius;
            SendManyToOneRouteRequestResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendManyToOneRouteRequestResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Periodically request any pending data from our parent. Setting &lt;i&gt;interval&lt;/i&gt; to 0 or &lt;i&gt;units&lt;/i&gt; to SL_ZIGBEE_EVENT_INACTIVE will generate a single poll.
        /// </summary>
        /// <param name="Interval">The time between polls. Note that the timer clock is free running and is not synchronized with this command. This means that the time will be between &lt;i&gt;interval&lt;/i&gt; and (&lt;i&gt;interval&lt;/i&gt; - 1). The maximum interval is 32767.</param>
        /// <param name="Units">The units for &lt;i&gt;interval&lt;/i&gt;.</param>
        /// <param name="FailureLimit">The number of poll failures that will be tolerated before a &lt;i&gt;pollCompleteHandler&lt;/i&gt; callback is generated. A value of zero will result in a callback for every poll. Any status value apart from SL_STATUS_OK and SL_STATUS_MAC_NO_DATA is counted as a failure.</param>
        /// <returns>The result of sending the first poll.</returns>
        public async Task<Status> PollForData(ushort interval, ZigbeeEventUnits units, byte failureLimit, CancellationToken cancellationToken = default)
        {
            PollForDataRequest request = new PollForDataRequest();
            request.Interval = interval;
            request.Units = units;
            request.FailureLimit = failureLimit;
            PollForDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as PollForDataResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates the result of a data poll to the parent of the local node.
        /// </summary>
        /// <returns>An sl_status_t value: SL_STATUS_OK - Data was received in response to the poll. SL_STATUS_MAC_NO_DATA - No data was pending. SL_STATUS_ZIGBEE_DELIVERY_FAILED - The poll message could not be sent. SL_STATUS_MAC_NO_ACK_RECEIVED - The poll message was sent but not acknowledged by the parent.</returns>
        public async Task<Status> PollCompleteHandler(CancellationToken cancellationToken = default)
        {
            PollCompleteHandlerRequest request = new PollCompleteHandlerRequest();
            PollCompleteHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as PollCompleteHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set a flag to indicate that a message is pending for a child. The next time that the child polls, it will be informed that it has a pending message. The message is sent from emberPollHandler, which is called when the child requests data.
        /// </summary>
        /// <param name="ChildId">The ID of the child that just polled for data.</param>
        /// <returns>SL_STATUS_OK - The next time that the child polls, it will be informed that it has pending data. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.</returns>
        public async Task<Status> SetMessageFlag(ushort childId, CancellationToken cancellationToken = default)
        {
            SetMessageFlagRequest request = new SetMessageFlagRequest();
            request.ChildId = childId;
            SetMessageFlagResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetMessageFlagResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clear a flag to indicate that there are no more messages for a child. The next time the child polls, it will be informed that it does not have any pending messages.
        /// </summary>
        /// <param name="ChildId">The ID of the child that no longer has pending messages.</param>
        /// <returns>SL_STATUS_OK - The next time that the child polls, it will be informed that it does not have any pending messages. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.</returns>
        public async Task<Status> ClearMessageFlag(ushort childId, CancellationToken cancellationToken = default)
        {
            ClearMessageFlagRequest request = new ClearMessageFlagRequest();
            request.ChildId = childId;
            ClearMessageFlagResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearMessageFlagResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates that the local node received a data poll from a child.
        /// </summary>
        /// <returns>A tuple containing:
        /// - ChildId: The node ID of the child that is requesting data.
        /// - TransmitExpected: True if transmit is expected, false otherwise.
        /// </returns>
        public async Task<PollHandler> PollHandler(CancellationToken cancellationToken = default)
        {
            PollHandlerRequest request = new PollHandlerRequest();
            PollHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as PollHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new PollHandler(response.ChildId, response.TransmitExpected);
        }

        /// <summary>
        /// Add a child to the child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely. If used appropriately, the application can maintain more than the maximum of children provided by the stack.
        /// </summary>
        /// <param name="ShortId">The preferred short ID of the node.</param>
        /// <param name="LongId">The long ID of the node.</param>
        /// <param name="NodeType">The nodetype e.g., SL_ZIGBEE_ROUTER defining, if this would be added to the child table or neighbor table.</param>
        /// <returns>SL_STATUS_OK - This node has been successfully added. SL_STATUS_FAIL - The child was not added to the child/neighbor table.</returns>
        public async Task<Status> AddChild(ushort shortId, byte[] longId, ZigbeeNodeType nodeType, CancellationToken cancellationToken = default)
        {
            AddChildRequest request = new AddChildRequest();
            request.ShortId = shortId;
            request.LongId = longId;
            request.NodeType = nodeType;
            AddChildResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as AddChildResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Remove a node from child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely.
        /// </summary>
        /// <param name="ChildEui64">The long ID of the node.</param>
        /// <returns>SL_STATUS_OK - This node has been successfully removed. SL_STATUS_FAIL - The node was not found in either of the child or neighbor tables.</returns>
        public async Task<Status> RemoveChild(byte[] childEui64, CancellationToken cancellationToken = default)
        {
            RemoveChildRequest request = new RemoveChildRequest();
            request.ChildEui64 = childEui64;
            RemoveChildResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RemoveChildResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Remove a neighbor from neighbor table only on SoC, allowing direct manipulation of neighbor table by the application. This can affect the network functionality, and needs to be used wisely.
        /// </summary>
        /// <param name="ShortId">The short ID of the neighbor.</param>
        /// <param name="LongId">The long ID of the neighbor.</param>
        /// <returns>The RemoveNeighborResponse object from the NCP</returns>
        public async Task<RemoveNeighborResponse> RemoveNeighbor(ushort shortId, byte[] longId, CancellationToken cancellationToken = default)
        {
            RemoveNeighborRequest request = new RemoveNeighborRequest();
            request.ShortId = shortId;
            request.LongId = longId;
            RemoveNeighborResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RemoveNeighborResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<IncomingMessageHandler> IncomingMessageHandler(CancellationToken cancellationToken = default)
        {
            IncomingMessageHandlerRequest request = new IncomingMessageHandlerRequest();
            IncomingMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingMessageHandler(response.Type, response.ApsFrame, response.PacketInfo, response.MessageLength, response.Message);
        }

        /// <summary>
        /// Sets source route discovery(MTORR) mode to on, off, reschedule
        /// </summary>
        /// <param name="Mode">Source route discovery mode: off:0, on:1, reschedule:2</param>
        /// <returns>Remaining time(ms) until next MTORR broadcast if the mode is on, MAX_INT32U_VALUE if the mode is off</returns>
        public async Task<uint> SetSourceRouteDiscoveryMode(byte mode, CancellationToken cancellationToken = default)
        {
            SetSourceRouteDiscoveryModeRequest request = new SetSourceRouteDiscoveryModeRequest();
            request.Mode = mode;
            SetSourceRouteDiscoveryModeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetSourceRouteDiscoveryModeResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<IncomingManyToOneRouteRequestHandler> IncomingManyToOneRouteRequestHandler(CancellationToken cancellationToken = default)
        {
            IncomingManyToOneRouteRequestHandlerRequest request = new IncomingManyToOneRouteRequestHandlerRequest();
            IncomingManyToOneRouteRequestHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingManyToOneRouteRequestHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingManyToOneRouteRequestHandler(response.Source, response.LongId, response.Cost);
        }

        /// <summary>
        /// A callback invoked when a route error message is received. The error indicates that a problem routing to or from the target node was encountered.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_ZIGBEE_SOURCE_ROUTE_FAILURE or SL_STATUS_ZIGBEE_MANY_TO_ONE_ROUTE_FAILURE.
        /// - Target: The short id of the remote node.
        /// </returns>
        public async Task<(Status Status, ushort Target)> IncomingRouteErrorHandler(CancellationToken cancellationToken = default)
        {
            IncomingRouteErrorHandlerRequest request = new IncomingRouteErrorHandlerRequest();
            IncomingRouteErrorHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingRouteErrorHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Target);
        }

        /// <summary>
        /// A callback invoked when a network status/route error message is received. The error indicates that there was a problem sending/receiving messages from the target node
        /// </summary>
        /// <returns>A tuple containing:
        /// - ErrorCode: One byte over-the-air error code from network status message
        /// - Target: The short ID of the remote node
        /// </returns>
        public async Task<IncomingNetworkStatusHandler> IncomingNetworkStatusHandler(CancellationToken cancellationToken = default)
        {
            IncomingNetworkStatusHandlerRequest request = new IncomingNetworkStatusHandlerRequest();
            IncomingNetworkStatusHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingNetworkStatusHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingNetworkStatusHandler(response.ErrorCode, response.Target);
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
        public async Task<IncomingRouteRecordHandler> IncomingRouteRecordHandler(CancellationToken cancellationToken = default)
        {
            IncomingRouteRecordHandlerRequest request = new IncomingRouteRecordHandlerRequest();
            IncomingRouteRecordHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingRouteRecordHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingRouteRecordHandler(response.Source, response.SourceEui, response.LastHopLqi, response.LastHopRssi, response.RelayCount, response.RelayList);
        }

        /// <summary>
        /// Supply a source route for the next outgoing message.
        /// </summary>
        /// <param name="Destination">The destination of the source route.</param>
        /// <param name="RelayCount">The number of relays in &lt;i&gt;relayList&lt;/i&gt;.</param>
        /// <param name="RelayList">The source route.</param>
        /// <returns>SL_STATUS_OK if the source route was successfully stored, and SL_STATUS_ALLOCATION_FAILED otherwise.</returns>
        public async Task<Status> SetSourceRoute(ushort destination, byte relayCount, ushort[] relayList, CancellationToken cancellationToken = default)
        {
            SetSourceRouteRequest request = new SetSourceRouteRequest();
            request.Destination = destination;
            request.RelayCount = relayCount;
            request.RelayList = relayList;
            SetSourceRouteResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetSourceRouteResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Send the network key to a destination.
        /// </summary>
        /// <param name="TargetShort">The destination node of the key.</param>
        /// <param name="TargetLong">The long address of the destination node.</param>
        /// <param name="ParentShortId">The parent node of the destination node.</param>
        /// <returns>SL_STATUS_OK if send was successful</returns>
        public async Task<Status> UnicastCurrentNetworkKey(ushort targetShort, byte[] targetLong, ushort parentShortId, CancellationToken cancellationToken = default)
        {
            UnicastCurrentNetworkKeyRequest request = new UnicastCurrentNetworkKeyRequest();
            request.TargetShort = targetShort;
            request.TargetLong = targetLong;
            request.ParentShortId = parentShortId;
            UnicastCurrentNetworkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as UnicastCurrentNetworkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether any messages are currently being sent using this address table entry. Note that this function does not indicate whether the address table entry is unused. To determine whether an address table entry is unused, check the remote node ID. The remote node ID will have the value SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID when the address table entry is not in use.
        /// </summary>
        /// <param name="AddressTableIndex">The index of an address table entry.</param>
        /// <returns>True if the address table entry is active, false otherwise.</returns>
        public async Task<bool> AddressTableEntryIsActive(byte addressTableIndex, CancellationToken cancellationToken = default)
        {
            AddressTableEntryIsActiveRequest request = new AddressTableEntryIsActiveRequest();
            request.AddressTableIndex = addressTableIndex;
            AddressTableEntryIsActiveResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as AddressTableEntryIsActiveResponse;
            _logger.LogDebug(response?.ToString());
            return response.Active;
        }

        /// <summary>
        /// Sets the EUI64 and short ID of an address table entry. Usually the application will not need to set the short ID in the address table. Once the remote EUI64 is set the stack is capable of figuring out the short ID on its own. However, in cases where the application does set the short ID, the application must set the remote EUI64 prior to setting the short ID. This function will also check other address table entries, the child table and the neighbor table to see if the node ID for the given EUI64 is already known. If known then this function will set node ID. If not known it will set the node ID to SL_ZIGBEE_UNKNOWN_NODE_ID.
        /// </summary>
        /// <param name="AddressTableIndex">The index of an address table entry.</param>
        /// <param name="Eui64">The EUI64 to use for the address table entry.</param>
        /// <param name="Id">The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index or SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID which indicates that the entry stored in the address table at the given index is not in use.</param>
        /// <returns>SL_STATUS_OK if the information was successfully set, and SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE otherwise.</returns>
        public async Task<Status> SetAddressTableInfo(byte addressTableIndex, byte[] eui64, ushort id, CancellationToken cancellationToken = default)
        {
            SetAddressTableInfoRequest request = new SetAddressTableInfoRequest();
            request.AddressTableIndex = addressTableIndex;
            request.Eui64 = eui64;
            request.Id = id;
            SetAddressTableInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetAddressTableInfoResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, GetAddressTableInfo Result)> GetAddressTableInfo(byte addressTableIndex, CancellationToken cancellationToken = default)
        {
            GetAddressTableInfoRequest request = new GetAddressTableInfoRequest();
            request.AddressTableIndex = addressTableIndex;
            GetAddressTableInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetAddressTableInfoResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new GetAddressTableInfo(response.NodeId, response.Eui64));
        }

        /// <summary>
        /// Tells the stack whether or not the normal interval between retransmissions of a retried unicast message should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. The interval needs to be increased when sending to a sleepy node so that the message is not retransmitted until the destination has had time to wake up and poll its parent. The stack will automatically extend the timeout: - For our own sleepy children. - When an address response is received from a parent on behalf of its child. - When an indirect transaction expiry route error is received. - When an end device announcement is received from a sleepy node.
        /// </summary>
        /// <param name="RemoteEui64">The address of the node for which the timeout is to be set.</param>
        /// <param name="ExtendedTimeout">true if the retry interval should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval should be used.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure  </returns>
        public async Task<Status> SetExtendedTimeout(byte[] remoteEui64, bool extendedTimeout, CancellationToken cancellationToken = default)
        {
            SetExtendedTimeoutRequest request = new SetExtendedTimeoutRequest();
            request.RemoteEui64 = remoteEui64;
            request.ExtendedTimeout = extendedTimeout;
            SetExtendedTimeoutResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetExtendedTimeoutResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Indicates whether or not the stack will extend the normal interval between retransmissions of a retried unicast message by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT.
        /// </summary>
        /// <param name="RemoteEui64">The address of the node for which the timeout is to be returned.</param>
        /// <returns>SL_STATUS_OK if the retry interval will be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT and SL_STATUS_FAIL if the normal retry interval will be used.</returns>
        public async Task<Status> GetExtendedTimeout(byte[] remoteEui64, CancellationToken cancellationToken = default)
        {
            GetExtendedTimeoutRequest request = new GetExtendedTimeoutRequest();
            request.RemoteEui64 = remoteEui64;
            GetExtendedTimeoutResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetExtendedTimeoutResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ReplaceAddressTableEntry Result)> ReplaceAddressTableEntry(byte addressTableIndex, byte[] newEui64, ushort newId, bool newExtendedTimeout, CancellationToken cancellationToken = default)
        {
            ReplaceAddressTableEntryRequest request = new ReplaceAddressTableEntryRequest();
            request.AddressTableIndex = addressTableIndex;
            request.NewEui64 = newEui64;
            request.NewId = newId;
            request.NewExtendedTimeout = newExtendedTimeout;
            ReplaceAddressTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ReplaceAddressTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new ReplaceAddressTableEntry(response.OldEui64, response.OldId, response.OldExtendedTimeout));
        }

        /// <summary>
        /// Returns the node ID that corresponds to the specified EUI64. The node ID is found by searching through all stack tables for the specified EUI64.
        /// </summary>
        /// <param name="Eui64">The EUI64 of the node to look up.</param>
        /// <returns>A tuple containing:
        /// - Status: SL_STATUS_OK if the short ID was found, SL_STATUS_FAIL if the short ID is not known.
        /// - NodeId: The short ID of the node or SL_ZIGBEE_NULL_NODE_ID if the short ID is not known.
        /// </returns>
        public async Task<(Status Status, ushort NodeId)> LookupNodeIdByEui64(byte[] eui64, CancellationToken cancellationToken = default)
        {
            LookupNodeIdByEui64Request request = new LookupNodeIdByEui64Request();
            request.Eui64 = eui64;
            LookupNodeIdByEui64Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as LookupNodeIdByEui64Response;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, byte[] Eui64)> LookupEui64ByNodeId(ushort nodeId, CancellationToken cancellationToken = default)
        {
            LookupEui64ByNodeIdRequest request = new LookupEui64ByNodeIdRequest();
            request.NodeId = nodeId;
            LookupEui64ByNodeIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as LookupEui64ByNodeIdResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeMulticastTableEntry Value)> GetMulticastTableEntry(byte index, CancellationToken cancellationToken = default)
        {
            GetMulticastTableEntryRequest request = new GetMulticastTableEntryRequest();
            request.Index = index;
            GetMulticastTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetMulticastTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Value);
        }

        /// <summary>
        /// Sets an entry in the multicast table.
        /// </summary>
        /// <param name="Index">The index of a multicast table entry</param>
        /// <param name="Value">The contents of the multicast entry.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetMulticastTableEntry(byte index, ZigbeeMulticastTableEntry value, CancellationToken cancellationToken = default)
        {
            SetMulticastTableEntryRequest request = new SetMulticastTableEntryRequest();
            request.Index = index;
            request.Value = value;
            SetMulticastTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetMulticastTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when an id conflict is discovered, that is, two different nodes in the network were found to be using the same short id. The stack automatically removes the conflicting short id from its internal tables (address, binding, route, neighbor, and child tables). The application should discontinue any other use of the id.
        /// </summary>
        /// <returns>The short id for which a conflict was detected</returns>
        public async Task<ushort> IdConflictHandler(CancellationToken cancellationToken = default)
        {
            IdConflictHandlerRequest request = new IdConflictHandlerRequest();
            IdConflictHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IdConflictHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Id;
        }

        /// <summary>
        /// Write the current node Id, PAN ID, or Node type to the tokens
        /// </summary>
        /// <param name="Erase">Erase the node type or not</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> WriteNodeData(bool erase, CancellationToken cancellationToken = default)
        {
            WriteNodeDataRequest request = new WriteNodeDataRequest();
            request.Erase = erase;
            WriteNodeDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as WriteNodeDataResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> SendRawMessage(byte messageLength, byte[] messageContents, byte priority, bool useCca, CancellationToken cancellationToken = default)
        {
            SendRawMessageRequest request = new SendRawMessageRequest();
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            request.Priority = priority;
            request.UseCca = useCca;
            SendRawMessageResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendRawMessageResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<MacPassthroughMessageHandler> MacPassthroughMessageHandler(CancellationToken cancellationToken = default)
        {
            MacPassthroughMessageHandlerRequest request = new MacPassthroughMessageHandlerRequest();
            MacPassthroughMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MacPassthroughMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new MacPassthroughMessageHandler(response.MessageType, response.PacketInfo, response.MessageLength, response.MessageContents);
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
        public async Task<MacFilterMatchMessageHandler> MacFilterMatchMessageHandler(CancellationToken cancellationToken = default)
        {
            MacFilterMatchMessageHandlerRequest request = new MacFilterMatchMessageHandlerRequest();
            MacFilterMatchMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MacFilterMatchMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new MacFilterMatchMessageHandler(response.FilterValueMatch, response.LegacyPassthroughType, response.PacketInfo, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a raw message.
        /// </summary>
        /// <returns>A tuple containing:
        /// - MessageLength: Length of the message that was transmitted.
        /// - MessageContents: The message that was transmitted.
        /// - Status: SL_STATUS_OK if the transmission was successful, or SL_STATUS_ZIGBEE_DELIVERY_FAILED if not
        /// </returns>
        public async Task<(Status Status, RawTransmitCompleteHandler Result)> RawTransmitCompleteHandler(CancellationToken cancellationToken = default)
        {
            RawTransmitCompleteHandlerRequest request = new RawTransmitCompleteHandlerRequest();
            RawTransmitCompleteHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RawTransmitCompleteHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new RawTransmitCompleteHandler(response.MessageLength, response.MessageContents));
        }

        /// <summary>
        /// This function is useful to sleepy end devices. This function will set the retry interval (in milliseconds) for mac data poll. This interval is the time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.
        /// </summary>
        /// <param name="WaitBeforeRetryIntervalMs">Time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.</param>
        /// <returns>The SetMacPollFailureWaitTimeResponse object from the NCP</returns>
        public async Task<SetMacPollFailureWaitTimeResponse> SetMacPollFailureWaitTime(uint waitBeforeRetryIntervalMs, CancellationToken cancellationToken = default)
        {
            SetMacPollFailureWaitTimeRequest request = new SetMacPollFailureWaitTimeRequest();
            request.WaitBeforeRetryIntervalMs = waitBeforeRetryIntervalMs;
            SetMacPollFailureWaitTimeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetMacPollFailureWaitTimeResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Returns the maximum number of no-ack retries that will be attempted
        /// </summary>
        /// <returns>Max MAC retries</returns>
        public async Task<byte> GetMaxMacRetries(CancellationToken cancellationToken = default)
        {
            GetMaxMacRetriesRequest request = new GetMaxMacRetriesRequest();
            GetMaxMacRetriesResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetMaxMacRetriesResponse;
            _logger.LogDebug(response?.ToString());
            return response.Retries;
        }

        /// <summary>
        /// Sets the priority masks and related variables for choosing the best beacon.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The attempt to set the pramaters returns SL_STATUS_OK
        /// - Param: Gets the beacon prioritization related variable
        /// </returns>
        public async Task<(Status Status, ZigbeeBeaconClassificationParams Param)> SetBeaconClassificationParams(CancellationToken cancellationToken = default)
        {
            SetBeaconClassificationParamsRequest request = new SetBeaconClassificationParamsRequest();
            SetBeaconClassificationParamsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetBeaconClassificationParamsResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Param);
        }

        /// <summary>
        /// Gets the priority masks and related variables for choosing the best beacon.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The attempt to get the pramaters returns SL_STATUS_OK
        /// - Param: Gets the beacon prioritization related variable
        /// </returns>
        public async Task<(Status Status, ZigbeeBeaconClassificationParams Param)> GetBeaconClassificationParams(CancellationToken cancellationToken = default)
        {
            GetBeaconClassificationParamsRequest request = new GetBeaconClassificationParamsRequest();
            GetBeaconClassificationParamsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetBeaconClassificationParamsResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Param);
        }

        /// <summary>
        /// Indicate whether there are pending messages in the APS retry queue.
        /// </summary>
        /// <returns>True if there is a pending message for this network in the APS retry queue, false if not.</returns>
        public async Task<bool> PendingAckedMessages(CancellationToken cancellationToken = default)
        {
            PendingAckedMessagesRequest request = new PendingAckedMessagesRequest();
            PendingAckedMessagesResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as PendingAckedMessagesResponse;
            _logger.LogDebug(response?.ToString());
            return response.PendingMessages;
        }

        /// <summary>
        /// Reschedule sending link status message, with first one being sent immediately.
        /// </summary>
        /// <returns></returns>
        public async Task<Status> RescheduleLinkStatusMsg(CancellationToken cancellationToken = default)
        {
            RescheduleLinkStatusMsgRequest request = new RescheduleLinkStatusMsgRequest();
            RescheduleLinkStatusMsgResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RescheduleLinkStatusMsgResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Set the network update ID to the desired value. Must be called before joining or forming the network.
        /// </summary>
        /// <param name="NwkUpdateId">Desired value of the network update ID.</param>
        /// <param name="SetWhenOnNetwork">Set to true in case change should also apply when on network.</param>
        /// <returns>Status of set operation for the network update ID.</returns>
        public async Task<Status> SetNwkUpdateId(byte nwkUpdateId, bool setWhenOnNetwork, CancellationToken cancellationToken = default)
        {
            SetNwkUpdateIdRequest request = new SetNwkUpdateIdRequest();
            request.NwkUpdateId = nwkUpdateId;
            request.SetWhenOnNetwork = setWhenOnNetwork;
            SetNwkUpdateIdResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetNwkUpdateIdResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the security state that will be used by the device when it forms or joins the network. This call &lt;b&gt;should not&lt;/b&gt; be used when restoring saved network state via networkInit as this will result in a loss of security data and will cause communication problems when the device re-enters the network.
        /// </summary>
        /// <param name="State">The security configuration to be set.</param>
        /// <returns>The success or failure code of the operation.</returns>
        public async Task<Status> SetInitialSecurityState(ZigbeeInitialSecurityState state, CancellationToken cancellationToken = default)
        {
            SetInitialSecurityStateRequest request = new SetInitialSecurityStateRequest();
            request.State = state;
            SetInitialSecurityStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetInitialSecurityStateResponse;
            _logger.LogDebug(response?.ToString());
            return response.Success;
        }

        /// <summary>
        /// Gets the current security state that is being used by a device that is joined in the network.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The success or failure code of the operation.
        /// - State: The security configuration in use by the stack.
        /// </returns>
        public async Task<(Status Status, ZigbeeCurrentSecurityState State)> GetCurrentSecurityState(CancellationToken cancellationToken = default)
        {
            GetCurrentSecurityStateRequest request = new GetCurrentSecurityStateRequest();
            GetCurrentSecurityStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetCurrentSecurityStateResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeSecManKey Key)> SecManExportKey(ZigbeeSecManContext context, CancellationToken cancellationToken = default)
        {
            SecManExportKeyRequest request = new SecManExportKeyRequest();
            request.Context = context;
            SecManExportKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManExportKeyResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Key);
        }

        /// <summary>
        /// Imports a key into security manager based on passed context.
        /// </summary>
        /// <param name="Context">Metadata to identify where the imported key should be stored.</param>
        /// <param name="Key">The key to be imported.</param>
        /// <returns>The success or failure code of the operation.</returns>
        public async Task<Status> SecManImportKey(ZigbeeSecManContext context, ZigbeeSecManKey key, CancellationToken cancellationToken = default)
        {
            SecManImportKeyRequest request = new SecManImportKeyRequest();
            request.Context = context;
            request.Key = key;
            SecManImportKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManImportKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to inform the application that the Network Key has been updated and the node has been switched over to use the new key. The actual key being used is not passed up, but the sequence number is.
        /// </summary>
        /// <returns>The sequence number of the new network key.</returns>
        public async Task<byte> SwitchNetworkKeyHandler(CancellationToken cancellationToken = default)
        {
            SwitchNetworkKeyHandlerRequest request = new SwitchNetworkKeyHandlerRequest();
            SwitchNetworkKeyHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SwitchNetworkKeyHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.SequenceNumber;
        }

        /// <summary>
        /// This function searches through the Key Table and tries to find the entry that matches the passed search criteria.
        /// </summary>
        /// <param name="Address">The address to search for. Alternatively, all zeros may be passed in to search for the first empty entry.</param>
        /// <param name="LinkKey">This indicates whether to search for an entry that contains a link key or a master key. true means to search for an entry with a Link Key.</param>
        /// <returns>This indicates the index of the entry that matches the search criteria. A value of 0xFF is returned if not matching entry is found.</returns>
        public async Task<byte> FindKeyTableEntry(byte[] address, bool linkKey, CancellationToken cancellationToken = default)
        {
            FindKeyTableEntryRequest request = new FindKeyTableEntryRequest();
            request.Address = address;
            request.LinkKey = linkKey;
            FindKeyTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as FindKeyTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response.Index;
        }

        /// <summary>
        /// This function sends an APS TransportKey command containing the current trust center link key. The node to which the command is sent is specified via the short and long address arguments.
        /// </summary>
        /// <param name="DestinationNodeId">The short address of the node to which this command will be sent</param>
        /// <param name="DestinationEui64">The long address of the node to which this command will be sent</param>
        /// <returns>An sl_status_t value indicating success of failure of the operation</returns>
        public async Task<Status> SendTrustCenterLinkKey(ushort destinationNodeId, byte[] destinationEui64, CancellationToken cancellationToken = default)
        {
            SendTrustCenterLinkKeyRequest request = new SendTrustCenterLinkKeyRequest();
            request.DestinationNodeId = destinationNodeId;
            request.DestinationEui64 = destinationEui64;
            SendTrustCenterLinkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendTrustCenterLinkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function erases the data in the key table entry at the specified index. If the index is invalid, false is returned.
        /// </summary>
        /// <param name="Index">This indicates the index of entry to erase.</param>
        /// <returns>The success or failure of the operation.</returns>
        public async Task<Status> EraseKeyTableEntry(byte index, CancellationToken cancellationToken = default)
        {
            EraseKeyTableEntryRequest request = new EraseKeyTableEntryRequest();
            request.Index = index;
            EraseKeyTableEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as EraseKeyTableEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function clears the key table of the current network.
        /// </summary>
        /// <returns>The success or failure of the operation.</returns>
        public async Task<Status> ClearKeyTable(CancellationToken cancellationToken = default)
        {
            ClearKeyTableRequest request = new ClearKeyTableRequest();
            ClearKeyTableResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearKeyTableResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function to request a Link Key from the Trust Center with another device on the Network (which could be the Trust Center). A Link Key with the Trust Center is possible but the requesting device cannot be the Trust Center. Link Keys are optional in ZigBee Standard Security and thus the stack cannot know whether the other device supports them. If SL_ZIGBEE_REQUEST_KEY_TIMEOUT is non-zero on the Trust Center and the partner device is not the Trust Center, both devices must request keys with their partner device within the time period. The Trust Center only supports one outstanding key request at a time and therefore will ignore other requests. If the timeout is zero then the Trust Center will immediately respond and not wait for the second request. The Trust Center will always immediately respond to requests for a Link Key with it. Sleepy devices should poll at a higher rate until a response is received or the request times out. The success or failure of the request is returned via sl_zigbee_ezsp_zigbee_key_establishment_handler(...)
        /// </summary>
        /// <param name="Partner">This is the IEEE address of the partner device that will share the link key.</param>
        /// <returns>The success or failure of sending the request. This is not the final result of the attempt. sl_zigbee_ezsp_zigbee_key_establishment_handler(...) will return that.</returns>
        public async Task<Status> RequestLinkKey(byte[] partner, CancellationToken cancellationToken = default)
        {
            RequestLinkKeyRequest request = new RequestLinkKeyRequest();
            request.Partner = partner;
            RequestLinkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RequestLinkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Requests a new link key from the Trust Center. This function starts by sending a Node Descriptor request to the Trust Center to verify its R21+ stack version compliance. A Request Key message will then be sent, followed by a Verify Key Confirm message.
        /// </summary>
        /// <param name="MaxAttempts">The maximum number of attempts a node should make when sending the Node Descriptor, Request Key, and Verify Key Confirm messages. The number of attempts resets for each message type sent (e.g., if maxAttempts is 3, up to 3 Node Descriptors are sent, up to 3 Request Keys, and up to 3 Verify Key Confirm messages are sent).</param>
        /// <returns>The success or failure of sending the request. If the Node Descriptor is successfully transmitted, sl_zigbee_ezsp_zigbee_key_establishment_handler(...) will be called at a later time with a final status result.</returns>
        public async Task<Status> UpdateTcLinkKey(byte maxAttempts, CancellationToken cancellationToken = default)
        {
            UpdateTcLinkKeyRequest request = new UpdateTcLinkKeyRequest();
            request.MaxAttempts = maxAttempts;
            UpdateTcLinkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as UpdateTcLinkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This is a callback that indicates the success or failure of an attempt to establish a key with a partner device.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Partner: This is the IEEE address of the partner that the device successfully established a key with. This value is all zeros on a failure.
        /// - Status: This is the status indicating what was established or why the key establishment failed.
        /// </returns>
        public async Task<ZigbeeKeyEstablishmentHandler> ZigbeeKeyEstablishmentHandler(CancellationToken cancellationToken = default)
        {
            ZigbeeKeyEstablishmentHandlerRequest request = new ZigbeeKeyEstablishmentHandlerRequest();
            ZigbeeKeyEstablishmentHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZigbeeKeyEstablishmentHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new ZigbeeKeyEstablishmentHandler(response.Partner, response.Status);
        }

        /// <summary>
        /// Clear all of the transient link keys from RAM.
        /// </summary>
        /// <returns>The ClearTransientLinkKeysResponse object from the NCP</returns>
        public async Task<ClearTransientLinkKeysResponse> ClearTransientLinkKeys(CancellationToken cancellationToken = default)
        {
            ClearTransientLinkKeysRequest request = new ClearTransientLinkKeysRequest();
            ClearTransientLinkKeysResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearTransientLinkKeysResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Retrieve information about the current and alternate network key, excluding their contents.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: Success or failure of retrieving network key info.
        /// - NetworkKeyInfo: Information about current and alternate network keys.
        /// </returns>
        public async Task<(Status Status, ZigbeeSecManNetworkKeyInfo NetworkKeyInfo)> SecManGetNetworkKeyInfo(CancellationToken cancellationToken = default)
        {
            SecManGetNetworkKeyInfoRequest request = new SecManGetNetworkKeyInfoRequest();
            SecManGetNetworkKeyInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManGetNetworkKeyInfoResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeSecManApsKeyMetadata KeyData)> SecManGetApsKeyInfo(ZigbeeSecManContext context, CancellationToken cancellationToken = default)
        {
            SecManGetApsKeyInfoRequest request = new SecManGetApsKeyInfoRequest();
            request.Context = context;
            SecManGetApsKeyInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManGetApsKeyInfoResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.KeyData);
        }

        /// <summary>
        /// Import an application link key into the key table.
        /// </summary>
        /// <param name="Index">Index where this key is to be imported to.</param>
        /// <param name="Address">EUI64 this key is associated with.</param>
        /// <param name="PlaintextKey">The key data to be imported.</param>
        /// <returns>Status of key import operation.</returns>
        public async Task<Status> SecManImportLinkKey(byte index, byte[] address, ZigbeeSecManKey plaintextKey, CancellationToken cancellationToken = default)
        {
            SecManImportLinkKeyRequest request = new SecManImportLinkKeyRequest();
            request.Index = index;
            request.Address = address;
            request.PlaintextKey = plaintextKey;
            SecManImportLinkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManImportLinkKeyResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, SecManExportLinkKeyByIndex Result)> SecManExportLinkKeyByIndex(byte index, CancellationToken cancellationToken = default)
        {
            SecManExportLinkKeyByIndexRequest request = new SecManExportLinkKeyByIndexRequest();
            request.Index = index;
            SecManExportLinkKeyByIndexResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManExportLinkKeyByIndexResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new SecManExportLinkKeyByIndex(response.Context, response.PlaintextKey, response.KeyData));
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
        public async Task<(Status Status, SecManExportLinkKeyByEui Result)> SecManExportLinkKeyByEui(byte[] eui, CancellationToken cancellationToken = default)
        {
            SecManExportLinkKeyByEuiRequest request = new SecManExportLinkKeyByEuiRequest();
            request.Eui = eui;
            SecManExportLinkKeyByEuiResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManExportLinkKeyByEuiResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new SecManExportLinkKeyByEui(response.Context, response.PlaintextKey, response.KeyData));
        }

        /// <summary>
        /// Check whether a key context can be used to load a valid key.
        /// </summary>
        /// <param name="Context">Context struct to check the validity of.</param>
        /// <returns>Validity of the checked context.</returns>
        public async Task<Status> SecManCheckKeyContext(ZigbeeSecManContext context, CancellationToken cancellationToken = default)
        {
            SecManCheckKeyContextRequest request = new SecManCheckKeyContextRequest();
            request.Context = context;
            SecManCheckKeyContextResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManCheckKeyContextResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Import a transient link key.
        /// </summary>
        /// <param name="Eui64">EUI64 associated with this transient key.</param>
        /// <param name="PlaintextKey">The key to import.</param>
        /// <returns>Status of key import operation.</returns>
        public async Task<Status> SecManImportTransientKey(byte[] eui64, ZigbeeSecManKey plaintextKey, CancellationToken cancellationToken = default)
        {
            SecManImportTransientKeyRequest request = new SecManImportTransientKeyRequest();
            request.Eui64 = eui64;
            request.PlaintextKey = plaintextKey;
            SecManImportTransientKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManImportTransientKeyResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, SecManExportTransientKeyByIndex Result)> SecManExportTransientKeyByIndex(byte index, CancellationToken cancellationToken = default)
        {
            SecManExportTransientKeyByIndexRequest request = new SecManExportTransientKeyByIndexRequest();
            request.Index = index;
            SecManExportTransientKeyByIndexResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManExportTransientKeyByIndexResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new SecManExportTransientKeyByIndex(response.Context, response.PlaintextKey, response.KeyData));
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
        public async Task<(Status Status, SecManExportTransientKeyByEui Result)> SecManExportTransientKeyByEui(byte[] eui, CancellationToken cancellationToken = default)
        {
            SecManExportTransientKeyByEuiRequest request = new SecManExportTransientKeyByEuiRequest();
            request.Eui = eui;
            SecManExportTransientKeyByEuiResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SecManExportTransientKeyByEuiResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new SecManExportTransientKeyByEui(response.Context, response.PlaintextKey, response.KeyData));
        }

        /// <summary>
        /// Set the incoming TC link key frame counter to desired value.
        /// </summary>
        /// <param name="FrameCounter">Value to set the frame counter to.</param>
        /// <returns>The SetIncomingTcLinkKeyFrameCounterResponse object from the NCP</returns>
        public async Task<SetIncomingTcLinkKeyFrameCounterResponse> SetIncomingTcLinkKeyFrameCounter(uint frameCounter, CancellationToken cancellationToken = default)
        {
            SetIncomingTcLinkKeyFrameCounterRequest request = new SetIncomingTcLinkKeyFrameCounterRequest();
            request.FrameCounter = frameCounter;
            SetIncomingTcLinkKeyFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetIncomingTcLinkKeyFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> ApsCryptMessage(bool encrypt, byte lengthCombinedArg, byte[] message, byte apsHeaderEndIndex, byte[] remoteEui64, CancellationToken cancellationToken = default)
        {
            ApsCryptMessageRequest request = new ApsCryptMessageRequest();
            request.Encrypt = encrypt;
            request.LengthCombinedArg = lengthCombinedArg;
            request.Message = message;
            request.ApsHeaderEndIndex = apsHeaderEndIndex;
            request.RemoteEui64 = remoteEui64;
            ApsCryptMessageResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ApsCryptMessageResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<TrustCenterPostJoinHandler> TrustCenterPostJoinHandler(CancellationToken cancellationToken = default)
        {
            TrustCenterPostJoinHandlerRequest request = new TrustCenterPostJoinHandlerRequest();
            TrustCenterPostJoinHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as TrustCenterPostJoinHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new TrustCenterPostJoinHandler(response.NewNodeId, response.NewNodeEui64, response.Status, response.PolicyDecision, response.ParentOfNewNodeId);
        }

        /// <summary>
        /// This function broadcasts a new encryption key, but does not tell the nodes in the network to start using it. To tell nodes to switch to the new key, use sl_zigbee_send_network_key_switch(). This is only valid for the Trust Center/Coordinator. It is up to the application to determine how quickly to send the Switch Key after sending the alternate encryption key.
        /// </summary>
        /// <param name="Key">An optional pointer to a 16-byte encryption key (SL_ZIGBEE_ENCRYPTION_KEY_SIZE). An all zero key may be passed in, which will cause the stack to randomly generate a new key.</param>
        /// <returns>sl_status_t value that indicates the success or failure of the command.</returns>
        public async Task<Status> BroadcastNextNetworkKey(ZigbeeKeyData key, CancellationToken cancellationToken = default)
        {
            BroadcastNextNetworkKeyRequest request = new BroadcastNextNetworkKeyRequest();
            request.Key = key;
            BroadcastNextNetworkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as BroadcastNextNetworkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This function broadcasts a switch key message to tell all nodes to change to the sequence number of the previously sent Alternate Encryption Key.
        /// </summary>
        /// <returns>sl_status_t value that indicates the success or failure of the command.</returns>
        public async Task<Status> BroadcastNetworkKeySwitch(CancellationToken cancellationToken = default)
        {
            BroadcastNetworkKeySwitchRequest request = new BroadcastNetworkKeySwitchRequest();
            BroadcastNetworkKeySwitchResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as BroadcastNetworkKeySwitchResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeAesMmoHashContext ReturnContext)> AesMmoHash(ZigbeeAesMmoHashContext context, bool finalize, byte length, byte[] data, CancellationToken cancellationToken = default)
        {
            AesMmoHashRequest request = new AesMmoHashRequest();
            request.Context = context;
            request.Finalize = finalize;
            request.Length = length;
            request.Data = data;
            AesMmoHashResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as AesMmoHashResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.ReturnContext);
        }

        /// <summary>
        /// This command sends an APS remove device using APS encryption to the destination indicating either to remove itself from the network, or one of its children.
        /// </summary>
        /// <param name="DestShort">The node ID of the device that will receive the message</param>
        /// <param name="DestLong">The long address (EUI64) of the device that will receive the message.</param>
        /// <param name="TargetLong">The long address (EUI64) of the device to be removed.</param>
        /// <returns>An sl_status_t value indicating success, or the reason for failure</returns>
        public async Task<Status> RemoveDevice(ushort destShort, byte[] destLong, byte[] targetLong, CancellationToken cancellationToken = default)
        {
            RemoveDeviceRequest request = new RemoveDeviceRequest();
            request.DestShort = destShort;
            request.DestLong = destLong;
            request.TargetLong = targetLong;
            RemoveDeviceResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as RemoveDeviceResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This command will send a unicast transport key message with a new NWK key to the specified device. APS encryption using the device&apos;s existing link key will be used.
        /// </summary>
        /// <param name="DestShort">The node ID of the device that will receive the message</param>
        /// <param name="DestLong">The long address (EUI64) of the device that will receive the message.</param>
        /// <param name="Key">The NWK key to send to the new device.</param>
        /// <returns>An sl_status_t value indicating success, or the reason for failure</returns>
        public async Task<Status> UnicastNwkKeyUpdate(ushort destShort, byte[] destLong, ZigbeeKeyData key, CancellationToken cancellationToken = default)
        {
            UnicastNwkKeyUpdateRequest request = new UnicastNwkKeyUpdateRequest();
            request.DestShort = destShort;
            request.DestLong = destLong;
            request.Key = key;
            UnicastNwkKeyUpdateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as UnicastNwkKeyUpdateResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call starts the generation of the ECC Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_handler().
        /// </summary>
        /// <returns></returns>
        public async Task<Status> GenerateCbkeKeys(CancellationToken cancellationToken = default)
        {
            GenerateCbkeKeysRequest request = new GenerateCbkeKeysRequest();
            GenerateCbkeKeysResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GenerateCbkeKeysResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback by the Crypto Engine indicating that a new ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The result of the CBKE operation.
        /// - EphemeralPublicKey: The generated ephemeral public key.
        /// </returns>
        public async Task<(Status Status, ZigbeePublicKeyData EphemeralPublicKey)> GenerateCbkeKeysHandler(CancellationToken cancellationToken = default)
        {
            GenerateCbkeKeysHandlerRequest request = new GenerateCbkeKeysHandlerRequest();
            GenerateCbkeKeysHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GenerateCbkeKeysHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.EphemeralPublicKey);
        }

        /// <summary>
        /// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE using the passed parameters and the stored public/private key pair previously generated with ezspGenerateKeysRetrieveCert(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
        /// </summary>
        /// <param name="AmInitiator">The role of this device in the Key Establishment protocol.</param>
        /// <param name="PartnerCertificate">The key establishment partner&apos;s implicit certificate.</param>
        /// <param name="PartnerEphemeralPublicKey">The key establishment partner&apos;s ephemeral public key</param>
        /// <returns></returns>
        public async Task<Status> CalculateSmacs(bool amInitiator, ZigbeeCertificateData partnerCertificate, ZigbeePublicKeyData partnerEphemeralPublicKey, CancellationToken cancellationToken = default)
        {
            CalculateSmacsRequest request = new CalculateSmacsRequest();
            request.AmInitiator = amInitiator;
            request.PartnerCertificate = partnerCertificate;
            request.PartnerEphemeralPublicKey = partnerEphemeralPublicKey;
            CalculateSmacsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CalculateSmacsResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, CalculateSmacsHandler Result)> CalculateSmacsHandler(CancellationToken cancellationToken = default)
        {
            CalculateSmacsHandlerRequest request = new CalculateSmacsHandlerRequest();
            CalculateSmacsHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CalculateSmacsHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new CalculateSmacsHandler(response.InitiatorSmac, response.ResponderSmac));
        }

        /// <summary>
        /// This call starts the generation of the ECC 283k1 curve Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_283k1_handler().
        /// </summary>
        /// <returns></returns>
        public async Task<Status> GenerateCbkeKeys283k1(CancellationToken cancellationToken = default)
        {
            GenerateCbkeKeys283k1Request request = new GenerateCbkeKeys283k1Request();
            GenerateCbkeKeys283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GenerateCbkeKeys283k1Response;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback by the Crypto Engine indicating that a new 283k1 ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: The result of the CBKE operation.
        /// - EphemeralPublicKey: The generated ephemeral public key.
        /// </returns>
        public async Task<(Status Status, ZigbeePublicKey283k1Data EphemeralPublicKey)> GenerateCbkeKeys283k1Handler(CancellationToken cancellationToken = default)
        {
            GenerateCbkeKeys283k1HandlerRequest request = new GenerateCbkeKeys283k1HandlerRequest();
            GenerateCbkeKeys283k1HandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GenerateCbkeKeys283k1HandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.EphemeralPublicKey);
        }

        /// <summary>
        /// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE for the 283k1 ECC curve using the passed parameters and the stored public/private key pair previously generated with sl_zigbee_ezsp_generate_keys_retrieve_cert_283k1(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
        /// </summary>
        /// <param name="AmInitiator">The role of this device in the Key Establishment protocol.</param>
        /// <param name="PartnerCertificate">The key establishment partner&apos;s implicit certificate.</param>
        /// <param name="PartnerEphemeralPublicKey">The key establishment partner&apos;s ephemeral public key</param>
        /// <returns></returns>
        public async Task<Status> CalculateSmacs283k1(bool amInitiator, ZigbeeCertificate283k1Data partnerCertificate, ZigbeePublicKey283k1Data partnerEphemeralPublicKey, CancellationToken cancellationToken = default)
        {
            CalculateSmacs283k1Request request = new CalculateSmacs283k1Request();
            request.AmInitiator = amInitiator;
            request.PartnerCertificate = partnerCertificate;
            request.PartnerEphemeralPublicKey = partnerEphemeralPublicKey;
            CalculateSmacs283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CalculateSmacs283k1Response;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, CalculateSmacs283k1Handler Result)> CalculateSmacs283k1Handler(CancellationToken cancellationToken = default)
        {
            CalculateSmacs283k1HandlerRequest request = new CalculateSmacs283k1HandlerRequest();
            CalculateSmacs283k1HandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as CalculateSmacs283k1HandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new CalculateSmacs283k1Handler(response.InitiatorSmac, response.ResponderSmac));
        }

        /// <summary>
        /// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
        /// </summary>
        /// <param name="StoreLinkKey">A bool indicating whether to store (true) or discard (false) the unverified link key derived when sl_zigbee_ezsp_calculate_smacs() was previously called.</param>
        /// <returns></returns>
        public async Task<Status> ClearTemporaryDataMaybeStoreLinkKey(bool storeLinkKey, CancellationToken cancellationToken = default)
        {
            ClearTemporaryDataMaybeStoreLinkKeyRequest request = new ClearTemporaryDataMaybeStoreLinkKeyRequest();
            request.StoreLinkKey = storeLinkKey;
            ClearTemporaryDataMaybeStoreLinkKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearTemporaryDataMaybeStoreLinkKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
        /// </summary>
        /// <param name="StoreLinkKey">A bool indicating whether to store (true) or discard (false) the unverified link key derived when sl_zigbee_ezsp_calculate_smacs() was previously called.</param>
        /// <returns></returns>
        public async Task<Status> ClearTemporaryDataMaybeStoreLinkKey283k1(bool storeLinkKey, CancellationToken cancellationToken = default)
        {
            ClearTemporaryDataMaybeStoreLinkKey283k1Request request = new ClearTemporaryDataMaybeStoreLinkKey283k1Request();
            request.StoreLinkKey = storeLinkKey;
            ClearTemporaryDataMaybeStoreLinkKey283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ClearTemporaryDataMaybeStoreLinkKey283k1Response;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Retrieves the certificate installed on the NCP.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: 
        /// - LocalCert: The locally installed certificate.
        /// </returns>
        public async Task<(Status Status, ZigbeeCertificateData LocalCert)> GetCertificate(CancellationToken cancellationToken = default)
        {
            GetCertificateRequest request = new GetCertificateRequest();
            GetCertificateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetCertificateResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.LocalCert);
        }

        /// <summary>
        /// Retrieves the 283k certificate installed on the NCP.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: 
        /// - LocalCert: The locally installed certificate.
        /// </returns>
        public async Task<(Status Status, ZigbeeCertificate283k1Data LocalCert)> GetCertificate283k1(CancellationToken cancellationToken = default)
        {
            GetCertificate283k1Request request = new GetCertificate283k1Request();
            GetCertificate283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetCertificate283k1Response;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.LocalCert);
        }

        /// <summary>
        /// LEGACY FUNCTION: This functionality has been replaced by a single bit in the sl_zigbee_aps_frame_t, SL_ZIGBEE_APS_OPTION_DSA_SIGN. Devices wishing to send signed messages should use that as it requires fewer function calls and message buffering. The dsaSignHandler response is still called when SL_ZIGBEE_APS_OPTION_DSA_SIGN is used. However, this function is still supported. This function begins the process of signing the passed message contained within the messageContents array. If no other ECC operation is going on, it will immediately return with SL_STATUS_IN_PROGRESS to indicate the start of ECC operation. It will delay a period of time to let APS retries take place, but then it will shut down the radio and consume the CPU processing until the signing is complete. This may take up to 1 second. The signed message will be returned in the dsaSignHandler response. Note that the last byte of the messageContents passed to this function has special significance. As the typical use case for DSA signing is to sign the ZCL payload of a DRLC Report Event Status message in SE 1.0, there is often both a signed portion (ZCL payload) and an unsigned portion (ZCL header). The last byte in the content of messageToSign is therefore used as a special indicator to signify how many bytes of leading data in the array should be excluded from consideration during the signing process. If the signature needs to cover the entire array (all bytes except last one), the caller should ensure that the last byte of messageContents is 0x00. When the signature operation is complete, this final byte will be replaced by the signature type indicator (0x01 for ECDSA signatures), and the actual signature will be appended to the original contents after this byte.
        /// </summary>
        /// <param name="MessageLength">The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.</param>
        /// <param name="MessageContents">The message contents for which to create a signature. Per above notes, this may include a leading portion of data not included in the signature, in which case the last byte of this array should be set to the index of the first byte to be considered for signing. Otherwise, the last byte of messageContents should be 0x00 to indicate that a signature should occur across the entire contents.</param>
        /// <returns>SL_STATUS_IN_PROGRESS if the stack has queued up the operation for execution. SL_STATUS_INVALID_STATE if the operation can&apos;t be performed in this context, possibly because another ECC operation is pending.</returns>
        public async Task<Status> DsaSign(byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
        {
            DsaSignRequest request = new DsaSignRequest();
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            DsaSignResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DsaSignResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, DsaSignHandler Result)> DsaSignHandler(CancellationToken cancellationToken = default)
        {
            DsaSignHandlerRequest request = new DsaSignHandlerRequest();
            DsaSignHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DsaSignHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new DsaSignHandler(response.MessageLength, response.MessageContents));
        }

        /// <summary>
        /// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
        /// </summary>
        /// <param name="Digest">The AES-MMO message digest of the signed data. If dsaSign command was used to generate the signature for this data, the final byte (replaced by signature type of 0x01) in the messageContents array passed to dsaSign is included in the hash context used for the digest calculation.</param>
        /// <param name="SignerCertificate">The certificate of the signer. Note that the signer&apos;s certificate and the verifier&apos;s certificate must both be issued by the same Certificate Authority, so they should share the same CA Public Key.</param>
        /// <param name="ReceivedSig">The signature of the signed data.</param>
        /// <returns></returns>
        public async Task<Status> DsaVerify(ZigbeeMessageDigest digest, ZigbeeCertificateData signerCertificate, ZigbeeSignatureData receivedSig, CancellationToken cancellationToken = default)
        {
            DsaVerifyRequest request = new DsaVerifyRequest();
            request.Digest = digest;
            request.SignerCertificate = signerCertificate;
            request.ReceivedSig = receivedSig;
            DsaVerifyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DsaVerifyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This callback is executed by the stack when the DSA verification has completed and has a result. If the result is SL_STATUS_OK, the signature is valid. If the result is SL_STATUS_ZIGBEE_SIGNATURE_VERIFY_FAILURE then the signature is invalid. If the result is anything else then the signature verify operation failed and the validity is unknown.
        /// </summary>
        /// <returns>The result of the DSA verification operation.</returns>
        public async Task<Status> DsaVerifyHandler(CancellationToken cancellationToken = default)
        {
            DsaVerifyHandlerRequest request = new DsaVerifyHandlerRequest();
            DsaVerifyHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DsaVerifyHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
        /// </summary>
        /// <param name="Digest">The AES-MMO message digest of the signed data. If dsaSign command was used to generate the signature for this data, the final byte (replaced by signature type of 0x01) in the messageContents array passed to dsaSign is included in the hash context used for the digest calculation.</param>
        /// <param name="SignerCertificate">The certificate of the signer. Note that the signer&apos;s certificate and the verifier&apos;s certificate must both be issued by the same Certificate Authority, so they should share the same CA Public Key.</param>
        /// <param name="ReceivedSig">The signature of the signed data.</param>
        /// <returns></returns>
        public async Task<Status> DsaVerify283k1(ZigbeeMessageDigest digest, ZigbeeCertificate283k1Data signerCertificate, ZigbeeSignature283k1Data receivedSig, CancellationToken cancellationToken = default)
        {
            DsaVerify283k1Request request = new DsaVerify283k1Request();
            request.Digest = digest;
            request.SignerCertificate = signerCertificate;
            request.ReceivedSig = receivedSig;
            DsaVerify283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DsaVerify283k1Response;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the device&apos;s CA public key, local certificate, and static private key on the NCP associated with this node.
        /// </summary>
        /// <param name="CaPublic">The Certificate Authority&apos;s public key.</param>
        /// <param name="MyCert">The node&apos;s new certificate signed by the CA.</param>
        /// <param name="MyKey">The node&apos;s new static private key.</param>
        /// <returns></returns>
        public async Task<Status> SetPreinstalledCbkeData(ZigbeePublicKeyData caPublic, ZigbeeCertificateData myCert, ZigbeePrivateKeyData myKey, CancellationToken cancellationToken = default)
        {
            SetPreinstalledCbkeDataRequest request = new SetPreinstalledCbkeDataRequest();
            request.CaPublic = caPublic;
            request.MyCert = myCert;
            request.MyKey = myKey;
            SetPreinstalledCbkeDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetPreinstalledCbkeDataResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the device&apos;s 283k1 curve CA public key, local certificate, and static private key on the NCP associated with this node.
        /// </summary>
        /// <returns></returns>
        public async Task<Status> SavePreinstalledCbkeData283k1(CancellationToken cancellationToken = default)
        {
            SavePreinstalledCbkeData283k1Request request = new SavePreinstalledCbkeData283k1Request();
            SavePreinstalledCbkeData283k1Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SavePreinstalledCbkeData283k1Response;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Activate use of mfglib test routines and enables the radio receiver to report packets it receives to the mfgLibRxHandler() callback. These packets will not be passed up with a CRC failure. All other mfglib functions will return an error until the mfglibInternalStart() has been called
        /// </summary>
        /// <param name="RxCallback">true to generate a mfglibRxHandler callback when a packet is received.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalStart(bool rxCallback, CancellationToken cancellationToken = default)
        {
            MfglibInternalStartRequest request = new MfglibInternalStartRequest();
            request.RxCallback = rxCallback;
            MfglibInternalStartResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalStartResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Deactivate use of mfglib test routines; restores the hardware to the state it was in prior to mfglibInternalStart() and stops receiving packets started by mfglibInternalStart() at the same time.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalEnd(CancellationToken cancellationToken = default)
        {
            MfglibInternalEndRequest request = new MfglibInternalEndRequest();
            MfglibInternalEndResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalEndResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts transmitting an unmodulated tone on the currently set channel and power level. Upon successful return, the tone will be transmitting. To stop transmitting tone, application must call mfglibInternalStopTone(), allowing it the flexibility to determine its own criteria for tone duration (time, event, etc.)
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalStartTone(CancellationToken cancellationToken = default)
        {
            MfglibInternalStartToneRequest request = new MfglibInternalStartToneRequest();
            MfglibInternalStartToneResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalStartToneResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Stops transmitting tone started by mfglibInternalStartTone().
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalStopTone(CancellationToken cancellationToken = default)
        {
            MfglibInternalStopToneRequest request = new MfglibInternalStopToneRequest();
            MfglibInternalStopToneResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalStopToneResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Starts transmitting a random stream of characters. This is so that the radio modulation can be measured.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalStartStream(CancellationToken cancellationToken = default)
        {
            MfglibInternalStartStreamRequest request = new MfglibInternalStartStreamRequest();
            MfglibInternalStartStreamResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalStartStreamResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Stops transmitting a random stream of characters started by mfglibInternalStartStream().
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalStopStream(CancellationToken cancellationToken = default)
        {
            MfglibInternalStopStreamRequest request = new MfglibInternalStopStreamRequest();
            MfglibInternalStopStreamResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalStopStreamResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sends a single packet consisting of the following bytes: packetLength, packetContents[0], ... , packetContents[packetLength - 3], CRC[0], CRC[1]. The total number of bytes sent is packetLength + 1. The radio replaces the last two bytes of packetContents[] with the 16-bit CRC for the packet.
        /// </summary>
        /// <param name="PacketLength">The length of the packetContents parameter in bytes. Must be greater than 3 and less than 123.</param>
        /// <param name="PacketContents">The packet to send. The last two bytes will be replaced with the 16-bit CRC.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalSendPacket(byte packetLength, byte[] packetContents, CancellationToken cancellationToken = default)
        {
            MfglibInternalSendPacketRequest request = new MfglibInternalSendPacketRequest();
            request.PacketLength = packetLength;
            request.PacketContents = packetContents;
            MfglibInternalSendPacketResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalSendPacketResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Sets the radio channel. Calibration occurs if this is the first time the channel has been used.
        /// </summary>
        /// <param name="Channel">The channel to switch to. Valid values are 11 to 26.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalSetChannel(byte channel, CancellationToken cancellationToken = default)
        {
            MfglibInternalSetChannelRequest request = new MfglibInternalSetChannelRequest();
            request.Channel = channel;
            MfglibInternalSetChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalSetChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns the current radio channel, as previously set via mfglibInternalSetChannel().
        /// </summary>
        /// <returns>The current channel.</returns>
        public async Task<byte> MfglibInternalGetChannel(CancellationToken cancellationToken = default)
        {
            MfglibInternalGetChannelRequest request = new MfglibInternalGetChannelRequest();
            MfglibInternalGetChannelResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalGetChannelResponse;
            _logger.LogDebug(response?.ToString());
            return response.Channel;
        }

        /// <summary>
        /// First select the transmit power mode, and then include a method for selecting the radio transmit power. The valid power settings depend upon the specific radio in use. Ember radios have discrete power settings, and then requested power is rounded to a valid power setting; the actual power output is available to the caller via mfglibInternalGetPower().
        /// </summary>
        /// <param name="TxPowerMode">Power mode. Refer to txPowerModes in stack/include/sl_zigbee_types.h for possible values.</param>
        /// <param name="Power">Power in units of dBm. Refer to radio data sheet for valid range.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> MfglibInternalSetPower(ushort txPowerMode, sbyte power, CancellationToken cancellationToken = default)
        {
            MfglibInternalSetPowerRequest request = new MfglibInternalSetPowerRequest();
            request.TxPowerMode = txPowerMode;
            request.Power = power;
            MfglibInternalSetPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalSetPowerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Returns the current radio power setting, as previously set via mfglibInternalSetPower().
        /// </summary>
        /// <returns>Power in units of dBm. Refer to radio data sheet for valid range.</returns>
        public async Task<sbyte> MfglibInternalGetPower(CancellationToken cancellationToken = default)
        {
            MfglibInternalGetPowerRequest request = new MfglibInternalGetPowerRequest();
            MfglibInternalGetPowerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibInternalGetPowerResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<MfglibRxHandler> MfglibRxHandler(CancellationToken cancellationToken = default)
        {
            MfglibRxHandlerRequest request = new MfglibRxHandlerRequest();
            MfglibRxHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfglibRxHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new MfglibRxHandler(response.LinkQuality, response.Rssi, response.PacketLength, response.PacketContents);
        }

        /// <summary>
        /// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
        /// </summary>
        /// <param name="Enabled">If true, launch the standalone bootloader. If false, do nothing.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> LaunchStandaloneBootloader(bool enabled, CancellationToken cancellationToken = default)
        {
            LaunchStandaloneBootloaderRequest request = new LaunchStandaloneBootloaderRequest();
            request.Enabled = enabled;
            LaunchStandaloneBootloaderResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as LaunchStandaloneBootloaderResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> SendBootloadMessage(bool broadcast, byte[] destEui64, byte messageLength, byte[] messageContents, CancellationToken cancellationToken = default)
        {
            SendBootloadMessageRequest request = new SendBootloadMessageRequest();
            request.Broadcast = broadcast;
            request.DestEui64 = destEui64;
            request.MessageLength = messageLength;
            request.MessageContents = messageContents;
            SendBootloadMessageResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SendBootloadMessageResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<GetStandaloneBootloaderVersionPlatMicroPhy> GetStandaloneBootloaderVersionPlatMicroPhy(CancellationToken cancellationToken = default)
        {
            GetStandaloneBootloaderVersionPlatMicroPhyRequest request = new GetStandaloneBootloaderVersionPlatMicroPhyRequest();
            GetStandaloneBootloaderVersionPlatMicroPhyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetStandaloneBootloaderVersionPlatMicroPhyResponse;
            _logger.LogDebug(response?.ToString());
            return new GetStandaloneBootloaderVersionPlatMicroPhy(response.BootloaderVersion, response.NodePlat, response.NodeMicro, response.NodePhy);
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
        public async Task<IncomingBootloadMessageHandler> IncomingBootloadMessageHandler(CancellationToken cancellationToken = default)
        {
            IncomingBootloadMessageHandlerRequest request = new IncomingBootloadMessageHandlerRequest();
            IncomingBootloadMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingBootloadMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingBootloadMessageHandler(response.LongId, response.PacketInfo, response.MessageLength, response.MessageContents);
        }

        /// <summary>
        /// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a bootload message.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value of SL_STATUS_OK if an ACK was received from the destination or SL_STATUS_ZIGBEE_DELIVERY_FAILED if no ACK was received.
        /// - MessageLength: The length of the <i>messageContents</i> parameter in bytes.
        /// - MessageContents: The message that was sent.
        /// </returns>
        public async Task<(Status Status, BootloadTransmitCompleteHandler Result)> BootloadTransmitCompleteHandler(CancellationToken cancellationToken = default)
        {
            BootloadTransmitCompleteHandlerRequest request = new BootloadTransmitCompleteHandlerRequest();
            BootloadTransmitCompleteHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as BootloadTransmitCompleteHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, new BootloadTransmitCompleteHandler(response.MessageLength, response.MessageContents));
        }

        /// <summary>
        /// Perform AES encryption on plaintext using key.
        /// </summary>
        /// <param name="Plaintext">16 bytes of plaintext.</param>
        /// <param name="Key">The 16-byte encryption key to use.</param>
        /// <returns>16 bytes of ciphertext.</returns>
        public async Task<byte[]> AesEncrypt(byte[] plaintext, byte[] key, CancellationToken cancellationToken = default)
        {
            AesEncryptRequest request = new AesEncryptRequest();
            request.Plaintext = plaintext;
            request.Key = key;
            AesEncryptResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as AesEncryptResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<IncomingMfgTestMessageHandler> IncomingMfgTestMessageHandler(CancellationToken cancellationToken = default)
        {
            IncomingMfgTestMessageHandlerRequest request = new IncomingMfgTestMessageHandlerRequest();
            IncomingMfgTestMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IncomingMfgTestMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new IncomingMfgTestMessageHandler(response.MessageType, response.DataLength, response.Data);
        }

        /// <summary>
        /// A function used on the Golden Node to switch between normal network operation (for testing) and manufacturing configuration. Like emberSleep(), it may not be possible to execute this command due to pending network activity. For the transition from normal network operation to manufacturing configuration, it is customary to loop, calling this function alternately with emberTick() until the mode change succeeds.
        /// </summary>
        /// <param name="BeginConfiguration">Determines the new mode of operation. true causes the node to enter manufacturing configuration. false causes the node to return to normal network operation.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSetPacketMode(bool beginConfiguration, CancellationToken cancellationToken = default)
        {
            MfgTestSetPacketModeRequest request = new MfgTestSetPacketModeRequest();
            request.BeginConfiguration = beginConfiguration;
            MfgTestSetPacketModeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSetPacketModeResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to send the DUT a reboot command. The usual practice is to execute this command at the end of manufacturing configuration, to place the DUT into normal network operation for testing. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the reboot command within 20 milliseconds and then reboots.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSendRebootCommand(CancellationToken cancellationToken = default)
        {
            MfgTestSendRebootCommandRequest request = new MfgTestSendRebootCommandRequest();
            MfgTestSendRebootCommandResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSendRebootCommandResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 8-byte EUI ID. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new EUI ID within 150 milliseconds.
        /// </summary>
        /// <param name="NewId">The 8-byte EUID for the DUT.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSendEui64(byte[] newId, CancellationToken cancellationToken = default)
        {
            MfgTestSendEui64Request request = new MfgTestSendEui64Request();
            request.NewId = newId;
            MfgTestSendEui64Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSendEui64Response;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 16-byte configuration string. This function executes only during manufacturing configuration mode and will return an error otherwise. If successful, the DUT will acknowledge the new string within 150 milliseconds.
        /// </summary>
        /// <param name="NewString">The 16-byte manufacturing string.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSendManufacturingString(byte[] newString, CancellationToken cancellationToken = default)
        {
            MfgTestSendManufacturingStringRequest request = new MfgTestSendManufacturingStringRequest();
            request.NewString = newString;
            MfgTestSendManufacturingStringResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSendManufacturingStringResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s radio parameters. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new parameters within 25 milliseconds.
        /// </summary>
        /// <param name="SupportedBands">Sets the radio band for the DUT. See ember-common.h for possible values.</param>
        /// <param name="CrystalOffset">Sets the CC1020 crystal offset. This parameter has no effect on the EM2420, and it may safely be set to 0 for this RFIC.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSendRadioParameters(byte supportedBands, sbyte crystalOffset, CancellationToken cancellationToken = default)
        {
            MfgTestSendRadioParametersRequest request = new MfgTestSendRadioParametersRequest();
            request.SupportedBands = supportedBands;
            request.CrystalOffset = crystalOffset;
            MfgTestSendRadioParametersResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSendRadioParametersResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A function used in each of the manufacturing configuration API calls. Most implementations will not need to call this function directly. See mfg-test.c for more detail. This function executes only during manufacturing configuration mode and returns an error otherwise.
        /// </summary>
        /// <param name="Command">A pointer to the outgoing command string.</param>
        /// <returns>An sl_status_t value indicating success or failure of the command.</returns>
        public async Task<Status> MfgTestSendCommand(byte[] command, CancellationToken cancellationToken = default)
        {
            MfgTestSendCommandRequest request = new MfgTestSendCommandRequest();
            request.Command = command;
            MfgTestSendCommandResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as MfgTestSendCommandResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A consolidation of ZLL network operations with similar signatures; specifically, forming and joining networks or touch-linking.
        /// </summary>
        /// <param name="NetworkInfo">Information about the network.</param>
        /// <param name="Op">Operation indicator.</param>
        /// <param name="RadioTxPower">Radio transmission power.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ZllNetworkOps(ZigbeeZllNetwork networkInfo, ZigbeeEzspZllNetworkOperation op, sbyte radioTxPower, CancellationToken cancellationToken = default)
        {
            ZllNetworkOpsRequest request = new ZllNetworkOpsRequest();
            request.NetworkInfo = networkInfo;
            request.Op = op;
            request.RadioTxPower = radioTxPower;
            ZllNetworkOpsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllNetworkOpsResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will cause the device to setup the security information used in its network. It must be called prior to forming, starting, or joining a network.
        /// </summary>
        /// <param name="NetworkKey">ZLL Network key.</param>
        /// <param name="SecurityState">Initial security state of the network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ZllSetInitialSecurityState(ZigbeeKeyData networkKey, ZigbeeZllInitialSecurityState securityState, CancellationToken cancellationToken = default)
        {
            ZllSetInitialSecurityStateRequest request = new ZllSetInitialSecurityStateRequest();
            request.NetworkKey = networkKey;
            request.SecurityState = securityState;
            ZllSetInitialSecurityStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetInitialSecurityStateResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will update ZLL security token information. Unlike sli_zigbee_stack_zll_set_initial_security_state, this can be called while a network is already established.
        /// </summary>
        /// <param name="SecurityState">Security state of the network.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ZllSetSecurityStateWithoutKey(ZigbeeZllInitialSecurityState securityState, CancellationToken cancellationToken = default)
        {
            ZllSetSecurityStateWithoutKeyRequest request = new ZllSetSecurityStateWithoutKeyRequest();
            request.SecurityState = securityState;
            ZllSetSecurityStateWithoutKeyResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetSecurityStateWithoutKeyResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will initiate a ZLL network scan on all the specified channels.
        /// </summary>
        /// <param name="ChannelMask">The range of channels to scan.</param>
        /// <param name="RadioPowerForScan">The radio output power used for the scan requests.</param>
        /// <param name="NodeType">The node type of the local device.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ZllStartScan(uint channelMask, sbyte radioPowerForScan, ZigbeeNodeType nodeType, CancellationToken cancellationToken = default)
        {
            ZllStartScanRequest request = new ZllStartScanRequest();
            request.ChannelMask = channelMask;
            request.RadioPowerForScan = radioPowerForScan;
            request.NodeType = nodeType;
            ZllStartScanResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllStartScanResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call will change the mode of the radio so that the receiver is on for a specified amount of time when the device is idle.
        /// </summary>
        /// <param name="DurationMs">The duration in milliseconds to leave the radio on.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> ZllSetRxOnWhenIdle(uint durationMs, CancellationToken cancellationToken = default)
        {
            ZllSetRxOnWhenIdleRequest request = new ZllSetRxOnWhenIdleRequest();
            request.DurationMs = durationMs;
            ZllSetRxOnWhenIdleResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetRxOnWhenIdleResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<ZllNetworkFoundHandler> ZllNetworkFoundHandler(CancellationToken cancellationToken = default)
        {
            ZllNetworkFoundHandlerRequest request = new ZllNetworkFoundHandlerRequest();
            ZllNetworkFoundHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllNetworkFoundHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new ZllNetworkFoundHandler(response.NetworkInfo, response.IsDeviceInfoNull, response.DeviceInfo, response.PacketInfo);
        }

        /// <summary>
        /// This call is fired when a ZLL network scan is complete.
        /// </summary>
        /// <returns>Status of the operation.</returns>
        public async Task<Status> ZllScanCompleteHandler(CancellationToken cancellationToken = default)
        {
            ZllScanCompleteHandlerRequest request = new ZllScanCompleteHandlerRequest();
            ZllScanCompleteHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllScanCompleteHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// This call is fired when network and group addresses are assigned to a remote mode in a network start or network join request.
        /// </summary>
        /// <returns>A tuple containing:
        /// - AddressInfo: Address assignment information.
        /// - PacketInfo: Information about the incoming packet.
        /// </returns>
        public async Task<ZllAddressAssignmentHandler> ZllAddressAssignmentHandler(CancellationToken cancellationToken = default)
        {
            ZllAddressAssignmentHandlerRequest request = new ZllAddressAssignmentHandlerRequest();
            ZllAddressAssignmentHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllAddressAssignmentHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return new ZllAddressAssignmentHandler(response.AddressInfo, response.PacketInfo);
        }

        /// <summary>
        /// This call is fired when the device is a target of a touch link.
        /// </summary>
        /// <returns>Information about the network.</returns>
        public async Task<ZigbeeZllNetwork> ZllTouchLinkTargetHandler(CancellationToken cancellationToken = default)
        {
            ZllTouchLinkTargetHandlerRequest request = new ZllTouchLinkTargetHandlerRequest();
            ZllTouchLinkTargetHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllTouchLinkTargetHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return response.NetworkInfo;
        }

        /// <summary>
        /// Get the ZLL tokens.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Data: Data token return value.
        /// - Security: Security token return value.
        /// </returns>
        public async Task<ZllGetTokens> ZllGetTokens(CancellationToken cancellationToken = default)
        {
            ZllGetTokensRequest request = new ZllGetTokensRequest();
            ZllGetTokensResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllGetTokensResponse;
            _logger.LogDebug(response?.ToString());
            return new ZllGetTokens(response.Data, response.Security);
        }

        /// <summary>
        /// Set the ZLL data token.
        /// </summary>
        /// <param name="Data">Data token to be set.</param>
        /// <returns>The ZllSetDataTokenResponse object from the NCP</returns>
        public async Task<ZllSetDataTokenResponse> ZllSetDataToken(ZigbeeTokTypeStackZllData data, CancellationToken cancellationToken = default)
        {
            ZllSetDataTokenRequest request = new ZllSetDataTokenRequest();
            request.Data = data;
            ZllSetDataTokenResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetDataTokenResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Set the ZLL data token bitmask to reflect the ZLL network state.
        /// </summary>
        /// <returns>The ZllSetNonZllNetworkResponse object from the NCP</returns>
        public async Task<ZllSetNonZllNetworkResponse> ZllSetNonZllNetwork(CancellationToken cancellationToken = default)
        {
            ZllSetNonZllNetworkRequest request = new ZllSetNonZllNetworkRequest();
            ZllSetNonZllNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetNonZllNetworkResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Is this a ZLL network?
        /// </summary>
        /// <returns>ZLL network?</returns>
        public async Task<bool> IsZllNetwork(CancellationToken cancellationToken = default)
        {
            IsZllNetworkRequest request = new IsZllNetworkRequest();
            IsZllNetworkResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as IsZllNetworkResponse;
            _logger.LogDebug(response?.ToString());
            return response.IsZllNetwork;
        }

        /// <summary>
        /// This call sets the radio&apos;s default idle power mode.
        /// </summary>
        /// <param name="Mode">The power mode to be set.</param>
        /// <returns>The ZllSetRadioIdleModeResponse object from the NCP</returns>
        public async Task<ZllSetRadioIdleModeResponse> ZllSetRadioIdleMode(ZigbeeRadioPowerMode mode, CancellationToken cancellationToken = default)
        {
            ZllSetRadioIdleModeRequest request = new ZllSetRadioIdleModeRequest();
            request.Mode = mode;
            ZllSetRadioIdleModeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllSetRadioIdleModeResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// This call gets the radio&apos;s default idle power mode.
        /// </summary>
        /// <returns>The current power mode.</returns>
        public async Task<byte> ZllGetRadioIdleMode(CancellationToken cancellationToken = default)
        {
            ZllGetRadioIdleModeRequest request = new ZllGetRadioIdleModeRequest();
            ZllGetRadioIdleModeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllGetRadioIdleModeResponse;
            _logger.LogDebug(response?.ToString());
            return response.RadioIdleMode;
        }

        /// <summary>
        /// This call sets the default node type for a factory new ZLL device.
        /// </summary>
        /// <param name="NodeType">The node type to be set.</param>
        /// <returns>The SetZllNodeTypeResponse object from the NCP</returns>
        public async Task<SetZllNodeTypeResponse> SetZllNodeType(ZigbeeNodeType nodeType, CancellationToken cancellationToken = default)
        {
            SetZllNodeTypeRequest request = new SetZllNodeTypeRequest();
            request.NodeType = nodeType;
            SetZllNodeTypeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetZllNodeTypeResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// This call sets additional capability bits in the ZLL state.
        /// </summary>
        /// <param name="State">A mask with the bits to be set or cleared.</param>
        /// <returns>The SetZllAdditionalStateResponse object from the NCP</returns>
        public async Task<SetZllAdditionalStateResponse> SetZllAdditionalState(ushort state, CancellationToken cancellationToken = default)
        {
            SetZllAdditionalStateRequest request = new SetZllAdditionalStateRequest();
            request.State = state;
            SetZllAdditionalStateResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetZllAdditionalStateResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Is there a ZLL (Touchlink) operation in progress?
        /// </summary>
        /// <returns>ZLL operation in progress?</returns>
        public async Task<bool> ZllOperationInProgress(CancellationToken cancellationToken = default)
        {
            ZllOperationInProgressRequest request = new ZllOperationInProgressRequest();
            ZllOperationInProgressResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllOperationInProgressResponse;
            _logger.LogDebug(response?.ToString());
            return response.ZllOperationInProgress;
        }

        /// <summary>
        /// Is the ZLL radio on when idle mode is active?
        /// </summary>
        /// <returns>ZLL radio on when idle mode is active?</returns>
        public async Task<bool> ZllRxOnWhenIdleGetActive(CancellationToken cancellationToken = default)
        {
            ZllRxOnWhenIdleGetActiveRequest request = new ZllRxOnWhenIdleGetActiveRequest();
            ZllRxOnWhenIdleGetActiveResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllRxOnWhenIdleGetActiveResponse;
            _logger.LogDebug(response?.ToString());
            return response.ZllRxOnWhenIdleGetActive;
        }

        /// <summary>
        /// Informs the ZLL API that application scanning is complete
        /// </summary>
        /// <returns>The ZllScanningCompleteResponse object from the NCP</returns>
        public async Task<ZllScanningCompleteResponse> ZllScanningComplete(CancellationToken cancellationToken = default)
        {
            ZllScanningCompleteRequest request = new ZllScanningCompleteRequest();
            ZllScanningCompleteResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllScanningCompleteResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Get the primary ZLL (touchlink) channel mask.
        /// </summary>
        /// <returns>The primary ZLL channel mask</returns>
        public async Task<uint> GetZllPrimaryChannelMask(CancellationToken cancellationToken = default)
        {
            GetZllPrimaryChannelMaskRequest request = new GetZllPrimaryChannelMaskRequest();
            GetZllPrimaryChannelMaskResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetZllPrimaryChannelMaskResponse;
            _logger.LogDebug(response?.ToString());
            return response.ZllPrimaryChannelMask;
        }

        /// <summary>
        /// Get the secondary ZLL (touchlink) channel mask.
        /// </summary>
        /// <returns>The secondary ZLL channel mask</returns>
        public async Task<uint> GetZllSecondaryChannelMask(CancellationToken cancellationToken = default)
        {
            GetZllSecondaryChannelMaskRequest request = new GetZllSecondaryChannelMaskRequest();
            GetZllSecondaryChannelMaskResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetZllSecondaryChannelMaskResponse;
            _logger.LogDebug(response?.ToString());
            return response.ZllSecondaryChannelMask;
        }

        /// <summary>
        /// Set the primary ZLL (touchlink) channel mask
        /// </summary>
        /// <param name="ZllPrimaryChannelMask">The primary ZLL channel mask</param>
        /// <returns>The SetZllPrimaryChannelMaskResponse object from the NCP</returns>
        public async Task<SetZllPrimaryChannelMaskResponse> SetZllPrimaryChannelMask(uint zllPrimaryChannelMask, CancellationToken cancellationToken = default)
        {
            SetZllPrimaryChannelMaskRequest request = new SetZllPrimaryChannelMaskRequest();
            request.ZllPrimaryChannelMask = zllPrimaryChannelMask;
            SetZllPrimaryChannelMaskResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetZllPrimaryChannelMaskResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Set the secondary ZLL (touchlink) channel mask.
        /// </summary>
        /// <param name="ZllSecondaryChannelMask">The secondary ZLL channel mask</param>
        /// <returns>The SetZllSecondaryChannelMaskResponse object from the NCP</returns>
        public async Task<SetZllSecondaryChannelMaskResponse> SetZllSecondaryChannelMask(uint zllSecondaryChannelMask, CancellationToken cancellationToken = default)
        {
            SetZllSecondaryChannelMaskRequest request = new SetZllSecondaryChannelMaskRequest();
            request.ZllSecondaryChannelMask = zllSecondaryChannelMask;
            SetZllSecondaryChannelMaskResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetZllSecondaryChannelMaskResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Clear ZLL stack tokens.
        /// </summary>
        /// <returns>The ZllClearTokensResponse object from the NCP</returns>
        public async Task<ZllClearTokensResponse> ZllClearTokens(CancellationToken cancellationToken = default)
        {
            ZllClearTokensRequest request = new ZllClearTokensRequest();
            ZllClearTokensResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ZllClearTokensResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<bool> GpProxyTableProcessGpPairing(uint options, ZigbeeGpAddress addr, byte commMode, ushort sinkNetworkAddress, ushort sinkGroupId, ushort assignedAlias, byte[] sinkIeeeAddress, ZigbeeKeyData gpdKey, uint gpdSecurityFrameCounter, byte forwardingRadius, CancellationToken cancellationToken = default)
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
            GpProxyTableProcessGpPairingResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpProxyTableProcessGpPairingResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> DGpSend(bool action, bool useCca, ZigbeeGpAddress addr, byte gpdCommandId, byte gpdAsduLength, byte[] gpdAsdu, byte gpepHandle, ushort gpTxQueueEntryLifetimeMs, CancellationToken cancellationToken = default)
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
            DGpSendResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DGpSendResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// A callback to the GP endpoint to indicate the result of the GPDF transmission.
        /// </summary>
        /// <returns>A tuple containing:
        /// - Status: An sl_status_t value indicating success or the reason for failure.
        /// - GpepHandle: The handle of the GPDF.
        /// </returns>
        public async Task<(Status Status, byte GpepHandle)> DGpSentHandler(CancellationToken cancellationToken = default)
        {
            DGpSentHandlerRequest request = new DGpSentHandlerRequest();
            DGpSentHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as DGpSentHandlerResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.GpepHandle);
        }

        /// <summary>
        /// A callback invoked by the ZigBee GP stack when a GPDF is received.
        /// </summary>
        /// <returns>GP parameters list represented as a macro for GP endpoint incoming message handler and callbacks prototypes.</returns>
        public async Task<ZigbeeGpParams> GpepIncomingMessageHandler(CancellationToken cancellationToken = default)
        {
            GpepIncomingMessageHandlerRequest request = new GpepIncomingMessageHandlerRequest();
            GpepIncomingMessageHandlerResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpepIncomingMessageHandlerResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeGpProxyTableEntry Entry)> GpProxyTableGetEntry(byte proxyIndex, CancellationToken cancellationToken = default)
        {
            GpProxyTableGetEntryRequest request = new GpProxyTableGetEntryRequest();
            request.ProxyIndex = proxyIndex;
            GpProxyTableGetEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpProxyTableGetEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Entry);
        }

        /// <summary>
        /// Finds the index of the passed address in the gp table.
        /// </summary>
        /// <param name="Addr">The address to search for</param>
        /// <returns>The index, or 0xFF for not found</returns>
        public async Task<byte> GpProxyTableLookup(ZigbeeGpAddress addr, CancellationToken cancellationToken = default)
        {
            GpProxyTableLookupRequest request = new GpProxyTableLookupRequest();
            request.Addr = addr;
            GpProxyTableLookupResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpProxyTableLookupResponse;
            _logger.LogDebug(response?.ToString());
            return response.Index;
        }

        /// <summary>
        /// Removes the proxy table entry stored at the passed index.
        /// </summary>
        /// <param name="ProxyIndex">The index of the requested proxy table entry.</param>
        /// <returns>The GpProxyTableRemoveEntryResponse object from the NCP</returns>
        public async Task<GpProxyTableRemoveEntryResponse> GpProxyTableRemoveEntry(byte proxyIndex, CancellationToken cancellationToken = default)
        {
            GpProxyTableRemoveEntryRequest request = new GpProxyTableRemoveEntryRequest();
            request.ProxyIndex = proxyIndex;
            GpProxyTableRemoveEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpProxyTableRemoveEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Clear the entire proxy table
        /// </summary>
        /// <returns>The GpClearProxyTableResponse object from the NCP</returns>
        public async Task<GpClearProxyTableResponse> GpClearProxyTable(CancellationToken cancellationToken = default)
        {
            GpClearProxyTableRequest request = new GpClearProxyTableRequest();
            GpClearProxyTableResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpClearProxyTableResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeGpSinkTableEntry Entry)> GpSinkTableGetEntry(byte sinkIndex, CancellationToken cancellationToken = default)
        {
            GpSinkTableGetEntryRequest request = new GpSinkTableGetEntryRequest();
            request.SinkIndex = sinkIndex;
            GpSinkTableGetEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableGetEntryResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.Entry);
        }

        /// <summary>
        /// Finds the index of the passed address in the gp table.
        /// </summary>
        /// <param name="Addr">The address to search for.</param>
        /// <returns>The index, or 0xFF for not found</returns>
        public async Task<byte> GpSinkTableLookup(ZigbeeGpAddress addr, CancellationToken cancellationToken = default)
        {
            GpSinkTableLookupRequest request = new GpSinkTableLookupRequest();
            request.Addr = addr;
            GpSinkTableLookupResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableLookupResponse;
            _logger.LogDebug(response?.ToString());
            return response.Index;
        }

        /// <summary>
        /// Retrieves the sink table entry stored at the passed index.
        /// </summary>
        /// <param name="SinkIndex">The index of the requested sink table entry.</param>
        /// <param name="Entry">An sl_zigbee_gp_sink_table_entry_t struct containing a copy of the sink entry to be updated.</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> GpSinkTableSetEntry(byte sinkIndex, ZigbeeGpSinkTableEntry entry, CancellationToken cancellationToken = default)
        {
            GpSinkTableSetEntryRequest request = new GpSinkTableSetEntryRequest();
            request.SinkIndex = sinkIndex;
            request.Entry = entry;
            GpSinkTableSetEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableSetEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Removes the sink table entry stored at the passed index.
        /// </summary>
        /// <param name="SinkIndex">The index of the requested sink table entry.</param>
        /// <returns>The GpSinkTableRemoveEntryResponse object from the NCP</returns>
        public async Task<GpSinkTableRemoveEntryResponse> GpSinkTableRemoveEntry(byte sinkIndex, CancellationToken cancellationToken = default)
        {
            GpSinkTableRemoveEntryRequest request = new GpSinkTableRemoveEntryRequest();
            request.SinkIndex = sinkIndex;
            GpSinkTableRemoveEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableRemoveEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Finds or allocates a sink entry
        /// </summary>
        /// <param name="Addr">An sl_zigbee_gp_address_t struct containing a copy of the gpd address to be found.</param>
        /// <returns>An index of found or allocated sink or 0xFF if failed.</returns>
        public async Task<byte> GpSinkTableFindOrAllocateEntry(ZigbeeGpAddress addr, CancellationToken cancellationToken = default)
        {
            GpSinkTableFindOrAllocateEntryRequest request = new GpSinkTableFindOrAllocateEntryRequest();
            request.Addr = addr;
            GpSinkTableFindOrAllocateEntryResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableFindOrAllocateEntryResponse;
            _logger.LogDebug(response?.ToString());
            return response.Index;
        }

        /// <summary>
        /// Clear the entire sink table
        /// </summary>
        /// <returns>The GpSinkTableClearAllResponse object from the NCP</returns>
        public async Task<GpSinkTableClearAllResponse> GpSinkTableClearAll(CancellationToken cancellationToken = default)
        {
            GpSinkTableClearAllRequest request = new GpSinkTableClearAllRequest();
            GpSinkTableClearAllResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableClearAllResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Iniitializes Sink Table
        /// </summary>
        /// <returns>The GpSinkTableInitResponse object from the NCP</returns>
        public async Task<GpSinkTableInitResponse> GpSinkTableInit(CancellationToken cancellationToken = default)
        {
            GpSinkTableInitRequest request = new GpSinkTableInitRequest();
            GpSinkTableInitResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableInitResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Sets security framecounter in the sink table
        /// </summary>
        /// <param name="Index">Index to the Sink table</param>
        /// <param name="Sfc">Security Frame Counter</param>
        /// <returns>The GpSinkTableSetSecurityFrameCounterResponse object from the NCP</returns>
        public async Task<GpSinkTableSetSecurityFrameCounterResponse> GpSinkTableSetSecurityFrameCounter(byte index, uint sfc, CancellationToken cancellationToken = default)
        {
            GpSinkTableSetSecurityFrameCounterRequest request = new GpSinkTableSetSecurityFrameCounterRequest();
            request.Index = index;
            request.Sfc = sfc;
            GpSinkTableSetSecurityFrameCounterResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableSetSecurityFrameCounterResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<Status> GpSinkCommission(byte options, ushort gpmAddrForSecurity, ushort gpmAddrForPairing, byte sinkEndpoint, CancellationToken cancellationToken = default)
        {
            GpSinkCommissionRequest request = new GpSinkCommissionRequest();
            request.Options = options;
            request.GpmAddrForSecurity = gpmAddrForSecurity;
            request.GpmAddrForPairing = gpmAddrForPairing;
            request.SinkEndpoint = sinkEndpoint;
            GpSinkCommissionResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkCommissionResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Clears all entries within the translation table.
        /// </summary>
        /// <returns>The GpTranslationTableClearResponse object from the NCP</returns>
        public async Task<GpTranslationTableClearResponse> GpTranslationTableClear(CancellationToken cancellationToken = default)
        {
            GpTranslationTableClearRequest request = new GpTranslationTableClearRequest();
            GpTranslationTableClearResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpTranslationTableClearResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Return number of active entries in sink table.
        /// </summary>
        /// <returns>Number of active entries in sink table.</returns>
        public async Task<byte> GpSinkTableGetNumberOfActiveEntries(CancellationToken cancellationToken = default)
        {
            GpSinkTableGetNumberOfActiveEntriesRequest request = new GpSinkTableGetNumberOfActiveEntriesRequest();
            GpSinkTableGetNumberOfActiveEntriesResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSinkTableGetNumberOfActiveEntriesResponse;
            _logger.LogDebug(response?.ToString());
            return response.NumberOfEntries;
        }

        /// <summary>
        /// Gets the total number of tokens.
        /// </summary>
        /// <returns>Total number of tokens.</returns>
        public async Task<uint> GetTokenCount(CancellationToken cancellationToken = default)
        {
            GetTokenCountRequest request = new GetTokenCountRequest();
            GetTokenCountResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTokenCountResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeTokenInfo TokenInfo)> GetTokenInfo(byte index, CancellationToken cancellationToken = default)
        {
            GetTokenInfoRequest request = new GetTokenInfoRequest();
            request.Index = index;
            GetTokenInfoResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTokenInfoResponse;
            _logger.LogDebug(response?.ToString());
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
        public async Task<(Status Status, ZigbeeTokenData TokenData)> GetTokenData(uint token, uint index, CancellationToken cancellationToken = default)
        {
            GetTokenDataRequest request = new GetTokenDataRequest();
            request.Token = token;
            request.Index = index;
            GetTokenDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GetTokenDataResponse;
            _logger.LogDebug(response?.ToString());
            return (response.Status, response.TokenData);
        }

        /// <summary>
        /// Sets the token data for a single token with provided key
        /// </summary>
        /// <param name="Token">Key of the token in the token table for which data is to be set.</param>
        /// <param name="Index">Index in case of the indexed token.</param>
        /// <param name="TokenData">Token Data</param>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> SetTokenData(uint token, uint index, ZigbeeTokenData tokenData, CancellationToken cancellationToken = default)
        {
            SetTokenDataRequest request = new SetTokenDataRequest();
            request.Token = token;
            request.Index = index;
            request.TokenData = tokenData;
            SetTokenDataResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as SetTokenDataResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Reset the node by calling halReboot.
        /// </summary>
        /// <returns>The ResetNodeResponse object from the NCP</returns>
        public async Task<ResetNodeResponse> ResetNode(CancellationToken cancellationToken = default)
        {
            ResetNodeRequest request = new ResetNodeRequest();
            ResetNodeResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as ResetNodeResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }

        /// <summary>
        /// Run GP security test vectors.
        /// </summary>
        /// <returns>An sl_status_t value indicating success or the reason for failure.</returns>
        public async Task<Status> GpSecurityTestVectors(CancellationToken cancellationToken = default)
        {
            GpSecurityTestVectorsRequest request = new GpSecurityTestVectorsRequest();
            GpSecurityTestVectorsResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as GpSecurityTestVectorsResponse;
            _logger.LogDebug(response?.ToString());
            return response.Status;
        }

        /// <summary>
        /// Factory reset all configured zigbee tokens
        /// </summary>
        /// <param name="ExcludeOutgoingFC">Exclude network and APS outgoing frame counter tokens.</param>
        /// <param name="ExcludeBootCounter">Exclude stack boot counter token.</param>
        /// <returns>The TokenFactoryResetResponse object from the NCP</returns>
        public async Task<TokenFactoryResetResponse> TokenFactoryReset(bool excludeOutgoingFC, bool excludeBootCounter, CancellationToken cancellationToken = default)
        {
            TokenFactoryResetRequest request = new TokenFactoryResetRequest();
            request.ExcludeOutgoingFC = excludeOutgoingFC;
            request.ExcludeBootCounter = excludeBootCounter;
            TokenFactoryResetResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as TokenFactoryResetResponse;
            _logger.LogDebug(response?.ToString());
            return response;
        }
    }
}
#endif
#if VERSION_2025_6_2
/// <summary>
/// GP parameters list.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeGpParams
{
	/// <summary>
	/// The status of the GPDF receive.
	/// </summary>
	public ZigbeeGpStatus status;

	/// <summary>
	/// The gpdLink value of the received GPDF.
	/// </summary>
	public byte gpdLink;

	/// <summary>
	/// The GPDF sequence number.
	/// </summary>
	public byte sequenceNumber;

	/// <summary>
	/// The address of the source GPD.
	/// </summary>
	public ZigbeeGpAddress addr;

	/// <summary>
	/// The security level of the received GPDF.
	/// </summary>
	public ZigbeeGpSecurityLevel gpdfSecurityLevel;

	/// <summary>
	/// The securityKeyType used to decrypt/authenticate the incoming GPDF.
	/// </summary>
	public ZigbeeGpKeyType gpdfSecurityKeyType;

	/// <summary>
	/// Whether the incoming GPDF had the auto-commissioning bit set.
	/// </summary>
	public bool autoCommissioning;

	/// <summary>
	/// Bidirectional information represented in bitfields, where bit0 holds the rxAfterTx of incoming GPDF and bit1 holds if TX queue is available for outgoing GPDF.
	/// </summary>
	public byte bidirectionalInfo;

	/// <summary>
	/// The security frame counter of the incoming GPDF.
	/// </summary>
	public uint gpdSecurityFrameCounter;

	/// <summary>
	/// The gpdCommandId of the incoming GPDF.
	/// </summary>
	public byte gpdCommandId;

	/// <summary>
	/// The received MIC of the GPDF.
	/// </summary>
	public uint mic;

	/// <summary>
	/// The proxy table index of the corresponding proxy table entry to the incoming GPDF.
	/// </summary>
	public byte proxyTableIndex;

	/// <summary>
	/// The length of the GPD command payload.
	/// </summary>
	public byte gpdCommandPayloadLength;

	/// <summary>
	/// The GPD command payload.
	/// </summary>
	// Array field with symbolic size: SL_ZIGBEE_GP_MAX_APPLICATION_PAYLOAD
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
	public byte[] gpdCommandPayload;
	/// <summary>
	/// Rx packet information.
	/// </summary>
	public ZigbeeRxPacketInfo packetInfo;

}


#endif
#if VERSION_2025_6_2
/// <summary>
/// ZigBee APS frame parameters.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeApsFrame
{
	/// <summary>
	/// The application profile ID that describes the format of the message.
	/// </summary>
	public ushort profileId;

	/// <summary>
	/// The cluster ID for this message.
	/// </summary>
	public ushort clusterId;

	/// <summary>
	/// The source endpoint.
	/// </summary>
	public byte sourceEndpoint;

	/// <summary>
	/// The destination endpoint.
	/// </summary>
	public byte destinationEndpoint;

	/// <summary>
	/// A bitmask of options.
	/// </summary>
	public ZigbeeApsOption options;

	/// <summary>
	/// The group ID for this message, if it is multicast mode.
	/// </summary>
	public ushort groupId;

	/// <summary>
	/// The sequence number.
	/// </summary>
	public byte sequence;

	/// <summary>
	/// The radius of the message. Note that in context of use of this in a send API, it gets updated internally based on message type and radius supplied in the API.
	/// </summary>
	public byte radius;

}


#endif
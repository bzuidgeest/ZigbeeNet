#if VERSION_2025_6_2
/// <summary>
/// Information of a token in the token table.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeTokenInfo
{
	/// <summary>
	/// NVM3 key of the token
	/// </summary>
	public uint nvm3Key;

	/// <summary>
	/// Token is a counter type
	/// </summary>
	public bool isCnt;

	/// <summary>
	/// Token is an indexed token
	/// </summary>
	public bool isIdx;

	/// <summary>
	/// Size of the token
	/// </summary>
	public byte size;

	/// <summary>
	/// Array size of the token
	/// </summary>
	public byte arraySize;

}


#endif
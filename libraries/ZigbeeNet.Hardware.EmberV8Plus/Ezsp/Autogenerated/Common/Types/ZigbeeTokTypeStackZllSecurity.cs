/// <summary>
/// Public API for ZLL stack security token.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeTokTypeStackZllSecurity
{
	/// <summary>
	/// Token bitmask.
	/// </summary>
	public uint bitmask;

	/// <summary>
	/// Key index.
	/// </summary>
	public byte keyIndex;

	/// <summary>
	/// Encryption key.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] encryptionKey;
	/// <summary>
	/// Preconfigured key.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] preconfiguredKey;
}


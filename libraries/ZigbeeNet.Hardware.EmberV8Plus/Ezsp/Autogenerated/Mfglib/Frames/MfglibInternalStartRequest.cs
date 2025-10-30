using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Activate use of mfglib test routines and enables the radio receiver to report packets it receives to the mfgLibRxHandler() callback. These packets will not be passed up with a CRC failure. All other mfglib functions will return an error until the mfglibInternalStart() has been called
/// Frame value: 0x0083
/// </summary>
public class MfglibInternalStartRequest : EzspFrameRequest
{
    /// <summary>
    /// true to generate a mfglibRxHandler callback when a packet is received.
    /// </summary>
    public bool rxCallback { get; set; }


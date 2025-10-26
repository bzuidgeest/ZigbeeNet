using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Deactivate use of mfglib test routines; restores the hardware to the state it was in prior to mfglibInternalStart() and stops receiving packets started by mfglibInternalStart() at the same time.
/// Frame value: 0x0084
/// </summary>
public class mfglibInternalEnd : EzspFrameRequest
{
}

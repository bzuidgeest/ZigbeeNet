using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// First select the transmit power mode, and then include a method for selecting the radio transmit power. The valid power settings depend upon the specific radio in use. Ember radios have discrete power settings, and then requested power is rounded to a valid power setting; the actual power output is available to the caller via mfglibInternalGetPower().
/// Frame value: 0x008c
/// </summary>
public class MfglibInternalSetPowerRequest : EzspFrameRequest
{
    /// <summary>
    /// Power mode. Refer to txPowerModes in stack/include/sl_zigbee_types.h for possible values.
    /// </summary>
    public ushort txPowerMode { get; set; }

    /// <summary>
    /// Power in units of dBm. Refer to radio data sheet for valid range.
    /// </summary>
    public sbyte power { get; set; }


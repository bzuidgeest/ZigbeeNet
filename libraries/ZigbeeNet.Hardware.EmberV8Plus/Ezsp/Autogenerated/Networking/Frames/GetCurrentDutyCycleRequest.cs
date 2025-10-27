using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the duty cycle of the stack&apos;s connected children that are being monitored, up to maxDevices. It indicates the amount of overall duty cycle they have consumed (up to the suspend limit). The first entry is always the local stack&apos;s nodeId, and thus the total aggregate duty cycle for the device. The passed pointer arrayOfDeviceDutyCycles MUST have space for maxDevices.
/// Frame value: 0x004C
/// </summary>
public class GetCurrentDutyCycleRequest : EzspFrameRequest
{
    /// <summary>
    /// Number of devices to retrieve consumed duty cycle.
    /// </summary>
    public byte maxDevices { get; set; }

}

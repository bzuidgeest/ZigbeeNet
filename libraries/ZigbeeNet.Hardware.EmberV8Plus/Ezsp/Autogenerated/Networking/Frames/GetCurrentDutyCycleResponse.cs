using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the duty cycle of the stack&apos;s connected children that are being monitored, up to maxDevices. It indicates the amount of overall duty cycle they have consumed (up to the suspend limit). The first entry is always the local stack&apos;s nodeId, and thus the total aggregate duty cycle for the device. The passed pointer arrayOfDeviceDutyCycles MUST have space for maxDevices.
/// Frame value: 0x004C
/// </summary>
public class GetCurrentDutyCycleResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK  if the duty cycles were read successfully, SL_STATUS_INVALID_PARAMETER maxDevices is greater than SL_ZIGBEE_MAX_END_DEVICE_CHILDREN + 1.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Consumed duty cycles up to maxDevices. When the number of children that are being monitored is less than maxDevices, the sl_802154_short_addr_t element in the sl_zigbee_per_device_duty_cycle_t will be 0xFFFF.
    /// </summary>
    public uint8_t[134] arrayOfDeviceDutyCycles { get; set; }

}

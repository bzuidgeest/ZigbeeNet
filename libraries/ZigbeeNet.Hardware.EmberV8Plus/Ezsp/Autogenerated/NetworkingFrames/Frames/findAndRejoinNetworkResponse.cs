using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// The application may call this function when contact with the network has been lost. The most common usage case is when an end device can no longer communicate with its parent and wishes to find a new one. Another case is when a device has missed a Network Key update and no longer has the current Network Key. &lt;p&gt; The stack will call &lt;i&gt;sl_zigbee_ezsp_stack_status_handler&lt;/i&gt; to indicate that the network is down, then try to re-establish contact with the network by performing an active scan, choosing a network with matching extended pan id, and sending a ZigBee network rejoin request. A second call to the &lt;i&gt;sl_zigbee_ezsp_stack_status_handler&lt;/i&gt; callback indicates either the success or the failure of the attempt. The process takes approximately 150 milliseconds per channel to complete. &lt;p&gt;
/// Frame value: 0x0021
/// </summary>
public class findAndRejoinNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}

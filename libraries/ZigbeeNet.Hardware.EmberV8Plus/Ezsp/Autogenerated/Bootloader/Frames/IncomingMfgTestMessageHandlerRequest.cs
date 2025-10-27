using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A callback to be implemented on the Golden Node to process acknowledgements. If you supply a custom version of this handler, you must define SL_ZIGBEE_APPLICATION_HAS_INCOMING_MFG_TEST_MESSAGE_HANDLER in your application&apos;s CONFIGURATION_HEADER
/// Frame value: 0x0147
/// </summary>
public class IncomingMfgTestMessageHandlerRequest : EzspFrameRequest
{
}

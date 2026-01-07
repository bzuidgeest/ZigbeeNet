namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Types;

internal record struct EmberResponseHeader(byte SequenceNumber, ushort FrameControl, ushort FrameId);

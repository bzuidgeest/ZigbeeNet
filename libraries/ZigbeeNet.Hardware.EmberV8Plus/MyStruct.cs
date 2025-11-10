csharp AutoCode\Examples\ByteToStructHelpers.cs
using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.MemoryMarshal;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct MyStruct
{
    public byte A;
    public ushort B;
    public uint C;
}

public static class StructParser
{
    // Fast, safe for blittable types
    public static T ReadStruct<T>(ReadOnlySpan<byte> data) where T : struct
    {
        int size = Marshal.SizeOf<T>();
        if (data.Length < size)
            throw new ArgumentException($"Not enough data to read {typeof(T).Name}: need {size} bytes.");

        return MemoryMarshal.Read<T>(data);
    }

    // Pin an array and use Marshal (works when you can't use Span APIs)
    public static T ReadStructWithMarshal<T>(byte[] data, int offset = 0) where T : struct
    {
        int size = Marshal.SizeOf<T>();
        if (data.Length - offset < size)
            throw new ArgumentException($"Not enough data to read {typeof(T).Name}: need {size} bytes.");

        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try
        {
            IntPtr ptr = handle.AddrOfPinnedObject() + offset;
            return Marshal.PtrToStructure<T>(ptr)!;
        }
        finally
        {
            handle.Free();
        }
    }

    // Manual parsing with endianness control (recommended when wire format is little/big endian)
    public static MyStruct ParseMyStruct(ReadOnlySpan<byte> data)
    {
        const int size = 1 + 2 + 4;
        if (data.Length < size) throw new ArgumentException($"Not enough data to read MyStruct (need {size} bytes)");

        MyStruct s;
        s.A = data[0];
        s.B = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(1, 2));
        s.C = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(3, 4));
        return s;
    }
}
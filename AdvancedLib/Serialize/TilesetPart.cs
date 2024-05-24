using BinarySerializer;

namespace AdvancedLib.Serialize;

class TilesetPart : BinarySerializable {
    byte[] indicies {get; set;}
    public override void SerializeImpl(SerializerObject s)
    {
        s.SerializeArray<byte>(indicies, 4096, nameof(indicies));
    }
}
using BinarySerializer;
using BinarySerializer.GBA;
using System.Linq;

namespace AdvancedLib.Serialize;

class Tileset : BinarySerializable
{
    Pointer?[] tilePointers { get; set; }
    byte[] indicies { 
        get {
            byte[] a = new byte[4096 * 4];
            Array.Copy(tileParts[0], 0, a, 4096 * 0, 4096);
            Array.Copy(tileParts[1], 0, a, 4096 * 1, 4096);
            Array.Copy(tileParts[2], 0, a, 4096 * 2, 4096);
            Array.Copy(tileParts[3], 0, a, 4096 * 3, 4096);
            return a; 
        } set {
            if (value.Length != 4096 * 4) return;
            tileParts[0] = value[(4096 * 0)..(4096 * 1)];
            tileParts[1] = value[(4096 * 1)..(4096 * 2)];
            tileParts[2] = value[(4096 * 2)..(4096 * 3)];
            tileParts[3] = value[(4096 * 3)..(4096 * 4)];
        } }
    byte[][] tileParts = new byte[4][];
    public override void SerializeImpl(SerializerObject s)
    {
        Pointer basePointer = s.CurrentPointer;
        tilePointers = s.SerializePointerArray(tilePointers,4,PointerSize.Pointer16, basePointer);
        for (int i = 0; i < tilePointers.Length; i++)
        {
            s.DoAtEncoded(tilePointers[i],new LZSSEncoder(), () => {
                tileParts[i] = s.SerializeArray<byte>(tileParts[i], 4096, "tilePart");
            });
        }
    }
}
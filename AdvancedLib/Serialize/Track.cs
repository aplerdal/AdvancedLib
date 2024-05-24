using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySerializer;
using BinarySerializer.GBA;

namespace AdvancedLib.Serialize
{
    public class Track : BinarySerializable
    {
        #pragma warning disable
        uint magic;
        byte trackWidth {get; set;}
        byte trackHeight {get; set;}
        uint tileLookback {get; set;}
        Pointer trackLayoutPointer {get; set;}
        Pointer tilesetPartsPointer { get; set; }
        Pointer[] tilesetPartPointers {get; set;}
        Pointer palettePointer {get; set;}
        Pointer tileBehaviorsPointer {get; set;}
        Pointer objectsPointer {get; set;}
        Pointer overlayPointer {get; set;}
        Pointer itemBoxPointer {get; set;}
        Pointer finishLinePointer {get; set;}
        uint data0;
        uint trackRoutine {get; set;}
        Pointer minimapPointer {get; set;}
        Pointer aiZonesPointer {get; set;}
        Pointer objectGfxPointer {get; set;}
        Pointer objectPalettePointer {get; set;}

        // Decompressed Values
        TilesetPart[] tilesetParts {get; set;}
        

        #pragma warning enable
        public override void SerializeImpl(SerializerObject s)
        {
            Pointer basePointer = s.CurrentPointer;
            s.Serialize<uint>(magic, nameof(magic));
            s.Serialize<byte>(trackWidth, nameof(trackWidth));
            s.Serialize<byte>(trackHeight, nameof(trackHeight));
            s.SerializePadding(42);
            s.Serialize<uint>(tileLookback,nameof(tileLookback));
            s.SerializePadding(12);
            s.SerializePointer(trackLayoutPointer, PointerSize.Pointer32, basePointer);
            s.SerializePadding(60);
            s.SerializePointer(tilesetPartsPointer, PointerSize.Pointer32, basePointer);
            s.SerializePointer(palettePointer, PointerSize.Pointer32, basePointer, name: nameof(palettePointer));
            s.SerializePointer(tileBehaviorsPointer, PointerSize.Pointer32, basePointer, name: nameof(tileBehaviorsPointer));
            s.SerializePointer(objectsPointer, PointerSize.Pointer32, basePointer, name: nameof(objectsPointer));
            s.SerializePointer(overlayPointer, PointerSize.Pointer32, basePointer, name: nameof(overlayPointer));
            s.SerializePointer(itemBoxPointer, PointerSize.Pointer32, basePointer, name: nameof(itemBoxPointer));
            s.SerializePointer(finishLinePointer, PointerSize.Pointer32, basePointer, name: nameof(finishLinePointer));
            s.Serialize<uint>(data0, name: nameof(data0));
            s.SerializePadding(32);
            s.Serialize<uint>(trackRoutine, nameof(trackRoutine));
            s.SerializePointer(minimapPointer, PointerSize.Pointer32, basePointer, name: nameof(minimapPointer));
            s.SerializePadding(4);
            s.SerializePointer(aiZonesPointer, PointerSize.Pointer32, basePointer, name: nameof(aiZonesPointer));
            s.SerializePadding(20);
            s.SerializePointer(objectGfxPointer, PointerSize.Pointer32, basePointer, name: nameof(objectGfxPointer));
            s.SerializePointer(objectPalettePointer, PointerSize.Pointer32, basePointer, name: nameof(objectPalettePointer));
            s.SerializePadding(20);

            s.DoAt(tilesetPartsPointer, () => {
                s.SerializePointerArray(tilesetPartPointers,4,PointerSize.Pointer16, tilesetPartsPointer, name: nameof(tilesetPartPointers));
                for (int i = 0; i< tilesetPartPointers.Length; i++){
                    s.DoAtEncoded(tilesetPartPointers[i], new LZSSEncoder(), () => {
                        tilesetParts[i] = new TilesetPart();
                        tilesetParts[i].SerializeImpl(s);
                    });
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySerializer;
using BinarySerializer.GBA;

namespace AdvancedLib.Serialize;

public class Track : BinarySerializable
{
    #pragma warning disable
    uint magic;
    byte trackWidth { get; set; }
    byte trackHeight { get; set; }
    uint tileLookback { get; set; }
    Pointer layoutPointers { get; set; }
    Pointer tilesetPartsPointer { get; set; }
    Pointer[] tilesetPartPointers { get; set; }
    Pointer palettePointer { get; set; }
    Pointer tileBehaviorsPointer { get; set; }
    Pointer objectsPointer { get; set; }
    Pointer overlayPointer { get; set; }
    Pointer itemBoxPointer { get; set; }
    Pointer finishLinePointer { get; set; }
    uint data0;
    uint trackRoutine { get; set; }
    Pointer minimapPointer { get; set; }
    Pointer aiZonesPointer { get; set; }
    Pointer objectGfxPointer { get; set; }
    Pointer objectPalettePointer { get; set; }

    Tileset tileset { get; set; }
    Layout layout { get; set; }


#pragma warning enable
    public override void SerializeImpl(SerializerObject s)
    {
        Pointer basePointer = s.CurrentPointer;
        magic = s.Serialize<uint>(magic, nameof(magic));
        trackWidth = s.Serialize<byte>(trackWidth, nameof(trackWidth));
        trackHeight = s.Serialize<byte>(trackHeight, nameof(trackHeight));

        s.SerializePadding(42);
        
        tileLookback = s.Serialize<uint>(tileLookback,nameof(tileLookback));

        s.SerializePadding(12);
        
        s.SerializePointer(layoutPointers, PointerSize.Pointer32, basePointer, name: nameof(layoutPointers));
        
        s.SerializePadding(60);

        tilesetPartsPointer = s.SerializePointer(tilesetPartsPointer, PointerSize.Pointer32, basePointer, name: nameof(tilesetPartPointers));
        palettePointer = s.SerializePointer(palettePointer, PointerSize.Pointer32, basePointer, name: nameof(palettePointer));
        tileBehaviorsPointer = s.SerializePointer(tileBehaviorsPointer, PointerSize.Pointer32, basePointer, name: nameof(tileBehaviorsPointer));
        objectsPointer = s.SerializePointer(objectsPointer, PointerSize.Pointer32, basePointer, name: nameof(objectsPointer));
        overlayPointer = s.SerializePointer(overlayPointer, PointerSize.Pointer32, basePointer, name: nameof(overlayPointer));
        overlayPointer = s.SerializePointer(overlayPointer, PointerSize.Pointer32, basePointer, name: nameof(itemBoxPointer));
        finishLinePointer = s.SerializePointer(finishLinePointer, PointerSize.Pointer32, basePointer, name: nameof(finishLinePointer));
        data0 = s.Serialize<uint>(data0, name: nameof(data0));
        
        s.SerializePadding(32);

        trackRoutine = s.Serialize<uint>(trackRoutine, nameof(trackRoutine));
        minimapPointer = s.SerializePointer(minimapPointer, PointerSize.Pointer32, basePointer, name: nameof(minimapPointer));
        
        s.SerializePadding(4);

        aiZonesPointer = s.SerializePointer(aiZonesPointer, PointerSize.Pointer32, basePointer, name: nameof(aiZonesPointer));
        
        s.SerializePadding(20);

        objectGfxPointer = s.SerializePointer(objectGfxPointer, PointerSize.Pointer32, basePointer, name: nameof(objectGfxPointer));
        objectPalettePointer = s.SerializePointer(objectPalettePointer, PointerSize.Pointer32, basePointer, name: nameof(objectPalettePointer));
        
        s.SerializePadding(20);

        // TODO: Implement tileset lookback
        if (tilesetPartsPointer.AbsoluteOffset != palettePointer.AbsoluteOffset)
        {
            s.DoAt(tilesetPartsPointer, () =>
            {
                tileset = s.SerializeObject<Tileset>(tileset, name: nameof(tileset));
            });
        }
        else { }
        

        s.DoAt(layoutPointers, ()=> { 
            layout = s.SerializeObject<Layout>(layout, name: nameof(layout));
        });
    }
}

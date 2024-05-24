using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySerializer;

namespace AdvancedLib.Track
{
    public class Track : BinarySerializable
    {
        int RepeatTiles { get; set; }
        int TilesetPointerTable { get; set; }
        int LayoutPointerTable { get; set; }
        int PalettePointer { get; set; }
        int TileBehaviours { get; set; }
        int ObjectsPointer { get; set; }
        int OverlayPointer { get; set; }
        int MinimapPointer { get; set; }
        int AiZonesPointer { get; set; }
        int ItemBoxesPointer { get; set; }
        int EndlinePointer { get; set; }
        int TreeGfx { get; set; }
        int ObjectPalette { get; set; }


        public override void SerializeImpl(SerializerObject s)
        {

        }
    }
}

namespace AdvancedLib.Types
{
    /// <summary>
    /// GBA native BGR color format (Blue Green Red)
    /// where each color is 5 bits
    /// </summary>
    public struct BgrColor
    {
        public byte b;
        public byte g;
        public byte r;
        public BgrColor(ushort bgr555)
        {
            this.b = (byte)((bgr555 & 0b01111100_00000000) >> 10);
            this.g = (byte)((bgr555 & 0b00000011_11100000) >> 5);
            this.r = (byte)((bgr555 & 0b00000000_00011111) >> 0);
        }
        public BgrColor(byte b, byte g, byte r)
        {
            this.b = (byte)((byte)(b/8f) & 0b00011111);
            this.g = (byte)((byte)(g/8f) & 0b00011111);
            this.r = (byte)((byte)(r/8f) & 0b00011111);
        }
        public override string ToString()
        {
            // Return BGR string with fancy colors
            return $"B({b}) G({g}) R({r}) \x1b[38;2;{r*8};{g*8};{b*8}m██\x1b[m";
        }
    }
}
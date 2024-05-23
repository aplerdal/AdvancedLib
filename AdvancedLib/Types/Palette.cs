namespace AdvancedLib.Types{
    /// <summary>
    /// Palette of BGR555 colors
    /// </summary>
    public struct Palette
    {
        public BgrColor[] palette;
        public Palette(BgrColor[] pal)
        {
            this.palette = pal;
        }
        public Palette(byte[] rawPalette)
        {
            BgrColor[] pal = new BgrColor[rawPalette.Length / 2];
            for (int i = 0; i < rawPalette.Length; i += 2)
            {
                ushort color = (ushort)((rawPalette[i + 1] << 8) | rawPalette[i]);
                pal[i / 2] = new BgrColor(color);
            }
            this.palette = pal;
        }
        public BgrColor this[int i]
        {
            get { 
                if (i > palette.Length) i = i % palette.Length;
                return this.palette[i]; 
            }
            set { 
                this.palette[i] = value; 
            }
        }

    }
}
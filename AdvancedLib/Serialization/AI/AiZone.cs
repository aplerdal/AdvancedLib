using AuroraLib.Core.IO;

namespace AdvancedLib.Serialization.AI;

public enum ZoneShape
{
    Rectangle,
    TriangleTopLeft,
    TriangleTopRight,
    TriangleBottomRight,
    TriangleBottomLeft,
}

public class AiZone : ISerializable, IEquatable<AiZone>
{
    public static int Precision = 2;
    public static int Size = 12;
    public ZoneShape Shape { get; set; }
    public ushort X { get; set; }
    public ushort Y { get; set; }
    public ushort Width { get; set; }
    public ushort Height { get; set; }
    
    public void Serialize(Stream stream)
    {
        stream.Write((byte)Shape);
        stream.Write(X);
        stream.Write(Y);
        stream.Write(Width);
        stream.Write(Height);
        stream.Write([0,0,0]);
    }

    public void Deserialize(Stream stream)
    {
        Shape = (ZoneShape)stream.ReadUInt8();
        X = stream.ReadUInt16();
        Y = stream.ReadUInt16();
        Width = stream.ReadUInt16();
        Height = stream.ReadUInt16();
        stream.Skip(3);
    }

    public void WriteZoneMap(byte index, ref byte[,] map)
    {
        var mapSize = new Vec2I(map.GetLength(0), map.GetLength(1));
        switch (Shape)
        {
            case ZoneShape.Rectangle:
            {
                for (int y = 0; y <= Height; y++)
                {
                    if (Y + y >= mapSize.Y) continue;
                    for (int x = 0; x <= Width; x++)
                    {
                        if (X + x >= mapSize.X) continue;
                        map[X + x, Y + y] = index;
                    }
                }
            } break;
            case ZoneShape.TriangleTopLeft:
            {
                var size = Width;
                for (int y = 0; y < size; y++)
                {
                    if (Y + y >= mapSize.Y) continue;
                    for (int x = 0; x < size - y; x++)
                    {
                        if (X + x >= mapSize.X) continue;
                        map[X + x, Y + y] = index;
                    }
                }
                
            } break;
            case ZoneShape.TriangleTopRight:
            {
                var size = Width;
                for (int y = 0; y < size; y++)
                {
                    if (Y + y >= mapSize.Y) continue;
                    for (int x = size - y; x > 0; x--)
                    {
                        if (X + x >= mapSize.X) continue;
                        map[X + x, Y + y] = index;
                    }
                }
            } break;
            case ZoneShape.TriangleBottomRight:
            {
                var size = Width;
                for (int y = size; y >= 0; y--)
                {
                    if (Y + y >= mapSize.Y) continue;
                    for (int x = size - y; x > 0; x--)
                    {
                        if (X + x >= mapSize.X) continue;
                        map[X + x, Y + y] = index;
                    }
                }
            } break;
            case ZoneShape.TriangleBottomLeft:
            {
                var size = Width;
                for (int y = size; y >= 0; y--)
                {
                    if (Y + y >= mapSize.Y) continue;
                    for (int x = 0; x < size - y; x++)
                    {
                        if (X + x >= mapSize.X) continue;
                        map[X + x, Y + y] = index;
                    }
                }
            } break;
        }
    } 

    public bool Equals(AiZone other)
    {
        return Shape == other.Shape && X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
    }

    public override bool Equals(object? obj)
    {
        return obj is AiZone other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Shape, X, Y, Width, Height);
    }
}
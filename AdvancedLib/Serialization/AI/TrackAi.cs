using AdvancedLib.Serialization.Tracks;

namespace AdvancedLib.Serialization.AI;

public class TrackAi : ISerializable
{
    private const int DefaultSets = 3;
    public List<AiZone> Zones { get; set; } = new();
    public List<AiTarget[]> TargetSets { get; set; } = new();
    public void Serialize(Stream stream)
    {
        var header = new AiHeader
        {
            ZoneCount = (byte)Zones.Count,
            ZonesOffset = 5,
            TargetsOffset = (ushort)(5 + AiZone.Size * Zones.Count)
        };
        stream.Write(header);
        foreach (var zone in Zones)
            stream.Write(zone);
        foreach (var set in TargetSets)
        foreach (var target in set)
            stream.Write(target);
    }

    public void Deserialize(Stream stream)
    {
        var basePos = stream.Position;
        var header = stream.Read<AiHeader>();
        stream.Seek(basePos + header.ZonesOffset, SeekOrigin.Begin);
        for (int i = 0; i < header.ZoneCount; i++)
            Zones.Add(stream.Read<AiZone>());
        stream.Seek(basePos + header.TargetsOffset, SeekOrigin.Begin);
        for (int i = 0; i < DefaultSets; i++)
        {
            var set = new AiTarget[header.ZoneCount];
            for (var j = 0; j < set.Length; j++)
            {
                set[j] = stream.Read<AiTarget>();
            }
            TargetSets.Add(set);
        }
    }

    public byte[,] GenerateZoneMap(TrackHeader header)
    {
        var aiMapSize = header.TrackWidth * 64;
        var aiMap = new byte[aiMapSize, aiMapSize];
        for (var i = 0; i < Zones.Count; i++)
        {
            Zones[i].WriteZoneMap((byte)i, ref aiMap);
        }
        return aiMap;
    }
}
using System.Text.Json;
using AdvancedLib.Game;
using AdvancedLib.Graphics;
using AdvancedLib.Serialization;
using AdvancedLib.Serialization.AI;
using AdvancedLib.Serialization.Objects;
using AdvancedLib.Serialization.Tracks;
using AuroraLib.Core.IO;

namespace AdvancedLib.Project;

public class ProjectTrack
{
    public string Folder { get; set; }
    private readonly string _configPath, _tilesetPath, _tilemapPath, _minimapPath, _obstacleGraphicsPath, _objectsPath, _aiPath;

    public ProjectTrack(string folder)
    {
        Folder = folder;
        _configPath = Path.Combine(Folder, "track.json");
        _tilesetPath = Path.Combine(Folder, "tileset.chr");
        _tilemapPath = Path.Combine(Folder, "tilemap.scr");
        _minimapPath = Path.Combine(Folder, "minimap.chr");
        _obstacleGraphicsPath = Path.Combine(Folder, "obstacles.chr");
        _objectsPath = Path.Combine(Folder, "objects.json");
        _aiPath = Path.Combine(Folder, "ai.json");
        if (!Directory.Exists(Folder))
            Directory.CreateDirectory(Folder);
    }
    
    public void SaveTrackData(Track track)
    {
        using var configStream = File.Create(_configPath);
        using var tilesetStream = File.Create(_tilesetPath);
        using var tilemapStream = File.Create(_tilemapPath);
        using var minimapStream = File.Create(_minimapPath);
        using var obstacleGfxStream = File.Create(_obstacleGraphicsPath);
        using var objectsStream = File.Create(_objectsPath);
        using var aiStream = File.Create(_aiPath);
        
        JsonSerializer.Serialize(configStream, track.TrackConfig);
        track.Tileset.Write(tilesetStream);
        track.Tilemap.Write(tilemapStream);
        track.Minimap.Write(minimapStream);
        track.ObstacleGfx.Write(obstacleGfxStream);
        JsonSerializer.Serialize(objectsStream, track.Objects);
        JsonSerializer.Serialize(aiStream, track.Ai);
    }
    public void SaveTrackDataAsync(Track track)
    {
        using var configStream = File.Create(_configPath);
        using var tilesetStream = File.Create(_tilesetPath);
        using var tilemapStream = File.Create(_tilemapPath);
        using var minimapStream = File.Create(_minimapPath);
        using var obstacleGfxStream = File.Create(_obstacleGraphicsPath);
        using var objectsStream = File.Create(_objectsPath);
        using var aiStream = File.Create(_aiPath);
        
        Task.WaitAll(
            JsonSerializer.SerializeAsync(configStream, track.TrackConfig), 
            track.Tileset.WriteAsync(tilesetStream), 
            track.Tilemap.WriteAsync(tilemapStream), 
            track.Minimap.WriteAsync(minimapStream), 
            track.ObstacleGfx.WriteAsync(obstacleGfxStream), 
            JsonSerializer.SerializeAsync(objectsStream, track.Objects), 
            JsonSerializer.SerializeAsync(aiStream, track.Ai)
        );
    }

    public Track LoadTrackData()
    {
        using var configStream = File.Create(_configPath);
        using var tilesetStream = File.Create(_tilesetPath);
        using var tilemapStream = File.Create(_tilemapPath);
        using var minimapStream = File.Create(_minimapPath);
        using var obstacleGfxStream = File.Create(_obstacleGraphicsPath);
        using var objectsStream = File.Create(_objectsPath);
        using var aiStream = File.Create(_aiPath);

        var trackConfig = JsonSerializer.Deserialize<TrackConfig>(configStream) ?? throw new NullReferenceException("track.json was null");
        return new Track
        {
            TrackConfig = trackConfig,
            Tileset = new Tileset(tilesetStream, 256, PixelFormat.Bpp8),
            Tilemap = new AffineTilemap(tilemapStream, trackConfig.Size.X * 128, trackConfig.Size.Y * 128),
            Minimap = new Tileset(minimapStream, 64, PixelFormat.Bpp4),
            ObstacleGfx = new Tileset(obstacleGfxStream, 256, PixelFormat.Bpp4),
            Objects = JsonSerializer.Deserialize<TrackObjects>(_objectsPath) ?? throw new NullReferenceException("objects.json was null"),
            Ai = JsonSerializer.Deserialize<TrackAi>(_objectsPath) ?? throw new NullReferenceException("ai.json was null"),
        };
    }
}
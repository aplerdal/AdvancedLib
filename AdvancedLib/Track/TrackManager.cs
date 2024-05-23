using AdvancedLib.Serialize;

namespace AdvancedLib.Track;

public class TrackManager{
    public static readonly int trackTablePointer = 0x250000;
    public static readonly int trackCount = 48;

    public Pointer?[] trackPointers = new Pointer?[trackCount];
    public TrackManager(BinaryReader reader) {
        
        for (int i = 0; i < trackCount; i++){
            trackPointers[i] = reader.ReadUInt32;
        }
    }
    public void DecompressTracks(){
        for (int i = 0; i < trackCount; i++){

        }
    }
}
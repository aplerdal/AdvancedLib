using System.IO;
using BinarySerializer;
using BinarySerializer.GBA;

/*
Notes:
Raw (usually) refers to the binary form of something
*/

namespace AdvancedLib;
public class Manager
{
    FileStream? romStream;
    BinaryReader? romReader;
    Context context = new Context("");

    Region region;

    ROMHeader header = new ROMHeader();

    public Manager() {
        context.AddPreDefinedPointers(new Dictionary<Region, long>()
        {
            [Region.USA] = 0x258000,
            [Region.JPN] = 0x258000, // Unknown
            [Region.PAL] = 0x258000, // Unknown
        });
    }

    /// <summary>
    /// Opens filestream to 
    /// </summary>
    /// <param name="path">Path the file</param>
    /// <returns>Open was a success?</returns>
    public bool Open(string path){
        context.AddFile(new LinearFile(context, path, Endian.Little));

        BinaryDeserializer s = new BinaryDeserializer(context);

        header.SerializeImpl(s);
        switch (header.GameCode)
        {
            case "AMKE":
                region = Region.USA;
                return true;
            case "AMKJ":
                region = Region.JPN;
                return true;
            case "AMKP":
                region = Region.PAL;
                return true;
            default:
                return false;
        }

    }
    /// <summary>
    /// Close reference to ROM
    /// </summary>
    public void Close(){
        if (romStream is not null)
            romStream.Close();
    }
    /// <summary>
    /// Decompress entire ROM
    /// </summary>
    public void Decompress(){

    }
    /// <summary>
    /// Decompress entire ROM and output current progress in `progress`
    /// </summary>
    /// <param name="progress">0.0-1.0</param>
    public void Decompress(ref float progress){
        
    }
}

public enum Region
{
    USA,
    JPN,
    PAL,
}

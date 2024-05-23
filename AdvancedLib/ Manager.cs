using System.IO;

/*
Notes:
Raw (usually) refers to the binary form of something
*/


namespace AdvancedLib;
public class Manager
{
    FileStream? rom;

    public Manager() {}

    /// <summary>
    /// Opens filestream to 
    /// </summary>
    /// <param name="path">Path the file</param>
    /// <returns>Open was a success?</returns>
    public bool Open(string path){
        if (!File.Exists(path)) return false;
        try {
            var options = new FileStreamOptions(){
                Access = FileAccess.ReadWrite,
                Share = FileShare.ReadWrite,
            };
            
            rom = File.Open(path, options);
            return true;
        } catch (Exception e) {
            #if DEBUG
            Console.WriteLine(e);
            #endif
            return false;
        }
    }
    /// <summary>
    /// Close reference to ROM
    /// </summary>
    public void Close(){
        if (rom is not null)
            rom.Close();
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

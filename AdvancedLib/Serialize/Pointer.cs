namespace AdvancedLib.Serialize;

/// <summary>
/// Little Endian Pointer
/// </summary>
public class Pointer{
    public int baseAddress;
    private int _target;
    public int target {
        get => _target;
    }
    public Pointer(int baseAddress, BinaryReader br, int size){
        this.baseAddress = baseAddress;
        List<byte> temp = new List<byte>();
        for (int i = 0; i < size; i++){
            temp.Add(br.ReadByte());
        }
        while (temp.Count < 4){
            temp.Add(0x00);
        }
        
        var rawPointer = temp.ToArray();
        // Make sure the conversion works on Big Endian systems
        if (!BitConverter.IsLittleEndian) Array.Reverse(rawPointer);
        int pointer = BitConverter.ToInt32(rawPointer);
        this._target = baseAddress + pointer;
    }
}
namespace AdvancedLib.Serialize;

/// <summary>
/// Little Endian Pointer
/// </summary>
public class Pointer{
    public uint baseAddress;
    private uint _target;
    public uint target {
        get => _target;
    }
    public Pointer(uint baseAddress, BinaryReader br, int size){
        this.baseAddress = baseAddress;
        switch (size){
            case 2:
                this._target = baseAddress + br.ReadUInt16();
                break;
            case 4:
                this._target = baseAddress + br.ReadUInt32();
                break;
            default:
                throw new Exception("Invalid Size!");
        }
    }
}
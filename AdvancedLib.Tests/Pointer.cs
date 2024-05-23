using AdvancedLib.Serialize;
using AdvancedLib.Types;

namespace AdvancedLib.Tests.Serialize;

public class PointerTest
{
    [Fact]
    public void Test(){
        MemoryStream stream = new MemoryStream(new byte[]{0x18,0x93,0x84,0xf0,0x5a});
        BinaryReader br = new BinaryReader(stream);
        Assert.Equal("a",$"{br.ReadUInt32():X}");
    }
}
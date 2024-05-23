using AdvancedLib.Serialize;
using AdvancedLib.Types;

namespace AdvancedLib.Tests.Serialize;

public class PointerTest
{
    [Fact]
    public void PointerTest_i32()
    {
        Pointer pntr = new Pointer(0x1D3A7F, new byte[] {0xA3,0x37,0x4F,0x02});
        Assert.Equal(0x1D3A7F + 0x024F37A3, pntr.target);
    }
    [Fact]
    public void PointerTest_i16(){
        Pointer pntr = new Pointer(0x1D3A7F, new byte[] {0xA3,0x37});
        Assert.Equal(0x1D3A7F + 0x37A3, pntr.target);
    }
}
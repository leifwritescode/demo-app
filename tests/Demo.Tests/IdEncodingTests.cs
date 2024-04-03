using Demo.Core.Ids;

namespace Demo.Tests;

public class IdEncodingTests
{
    public static TheoryData<IId, string> TestData = new()
    {
        { SiteId.From(123), "SiteId:123" },
        { TagId.From(321), "TagId:321" },
    };
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestEncodeSuccess(IId id, string encoded)
    {
        Assert.Equal(encoded, IdEncoding.Encode(id));
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestDecodeSuccess(IId id, string encoded)
    {
        Assert.Equal(id, IdEncoding.Decode(encoded));
    }

    [Theory]
    [InlineData("xxx")]
    [InlineData("xxx:xxx")]
    [InlineData("xxx:123")]
    public void TestDecodeFailure(string encoded)
    {
        Assert.Throws<ArgumentException>(() => IdEncoding.Decode(encoded));
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void TestTypedDecode(IId id, string encoded)
    {
        var siteId = IdEncoding.Decode<SiteId>(encoded);
        if (id is SiteId)
        {
            Assert.Equal(id, siteId);
        }
        else
        {
            Assert.Null(siteId);
        }
    }
}
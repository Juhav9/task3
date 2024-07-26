using EFApp.Data;
using Microsoft.EntityFrameworkCore;
using Savonia.xUnit.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace tests;

public class UnitTest : AppTestBase
{
    private readonly AdventureWorksLTContext db;

    public UnitTest(ITestOutputHelper output) : base(output)
    {
        string connectionstring = "Server=tcp:sav-mik-devsql.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=ets730021s;Password=qMA9m7b4ZGfEgWsM;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        var optionsBuilder = new DbContextOptionsBuilder<AdventureWorksLTContext>();
        optionsBuilder.UseSqlServer(connectionstring);

        db = new AdventureWorksLTContext(optionsBuilder.Options);

    }

    [Theory]
    [JsonFileData("testdata.json", typeof(int), typeof(string))]    
    public async Task Checkpoint04(int data, string expected)
    {
        List<string> actual = await db.ProductCategoryListAsync();
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(41, actual.Count);
        Assert.Equal(expected, actual[data]);
    }
}
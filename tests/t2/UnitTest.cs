using EFApp.Data;
using Microsoft.EntityFrameworkCore;
using Savonia.xUnit.Helpers;
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
    [JsonFileData("testdata.json", typeof(int), typeof(decimal))]    
    public async Task Checkpoint02(int data, decimal expected)
    {
        decimal actual = await db.ProductCategoryItemsListPriceAverageAsync(data);
        Assert.Equal(expected, actual, 2);
    }
}
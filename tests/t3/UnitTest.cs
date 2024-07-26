using EFApp.Data;
using Microsoft.EntityFrameworkCore;
using Savonia.xUnit.Helpers;
using System.Collections.Generic;
using System.Linq;
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
    [JsonFileData("testdata.json", typeof(string), typeof(IEnumerable<string>))]    
    public async Task Checkpoint03(string data, IEnumerable<string> expected)
    {
        IEnumerable<Address>? actual = await db.CustomerAddressesAsync(data);
        Assert.NotNull(actual);
        Assert.Equal(expected.Count(), actual.Count());
        Assert.Equal(expected.OrderBy(i => i), actual.Select(a => a.AddressLine1).OrderBy(i => i));
    }
}
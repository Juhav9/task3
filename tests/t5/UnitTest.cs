using EFApp.Data;
using EFApp.Models;
using Microsoft.EntityFrameworkCore;
using Savonia.xUnit.Helpers;
using System;
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
    [JsonFileData("testdata.json", typeof(Tuple<int, string>), typeof(Tuple<bool, string?, string?, string?, string?>))]    
    public async Task Checkpoint05(Tuple<int, string> data, Tuple<bool, string?, string?, string?, string?> expected)
    {
        CustomerInfo actual = await db.GetCustomerInfoAsync(data.Item1, data.Item2);
        Assert.Equal(expected.Item1, null != actual);
        Assert.Equal(expected.Item2, actual?.Firstname);
        Assert.Equal(expected.Item3, actual?.Lastname);
        Assert.Equal(expected.Item4, actual?.AddressLine1);
        Assert.Equal(expected.Item5, actual?.City);
    }
}
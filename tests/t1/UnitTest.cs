using Microsoft.EntityFrameworkCore.Storage;
using Savonia.xUnit.Helpers;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace tests;

public class UnitTest : AppTestBase
{
    public UnitTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [JsonFileData("testdata.json", "files", typeof(string), typeof(bool))]
    public void Checkpoint01_Files(string data, bool expected)
    {
        FileInfo fi = new FileInfo($"{data}");
        Assert.NotNull(fi);
        Assert.Equal(expected, fi.Exists);
    }

    [Theory]
    [JsonFileData("testdata.json", "objects", typeof(string), typeof(Tuple<bool, int>))]
    public void Checkpoint01_Objects(string data, Tuple<bool, int> expected)
    {
        var classHandle = Activator.CreateInstance("EFApp", data);
        Assert.NotNull(classHandle);
        var obj = classHandle.Unwrap();
        Assert.NotNull(obj);
        Type t = obj.GetType();
        var properties = t.GetProperties();
        Assert.NotEmpty(properties);
        if (expected.Item1)
        {
            Assert.Equal(expected.Item2, properties.Length);
        }
    }

    [Theory]
    [JsonFileData("testdata.json", "constructors", typeof(string), typeof(Tuple<bool, int>))]
    public void Checkpoint01_Constructors(string data, Tuple<bool, int> expected)
    {
        var classHandle = Activator.CreateInstance("EFApp", data);
        Assert.NotNull(classHandle);
        var obj = classHandle.Unwrap();
        Assert.NotNull(obj);
        Type t = obj.GetType();
        var constructors = t.GetConstructors();
        Assert.NotEmpty(constructors);
        if (expected.Item1)
        {
            Assert.Equal(expected.Item2, constructors.Length);
        }
    }

    [Theory]
    [JsonFileData("testdata.json", "sensitive", typeof(string), typeof(string))]
    public void Checkpoint01_SensitiveInformation(string data, string expected)
    {
        var content = base.GetFileContent(data);
        Assert.True(content.fileExists);
        Assert.NotNull(content.content);
        Assert.NotEmpty(content.content);
        Assert.DoesNotContain(expected, content.content, StringComparison.OrdinalIgnoreCase);
    }

}
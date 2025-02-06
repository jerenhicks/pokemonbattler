using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;

public class TypeRepositoryTests : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public TypeRepositoryTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void LoadTypesFromFile_CorrectlyParsesTypes()
    {

        var result = TypeRepository.GetAllTypes();

        // Assert
        Assert.Equal(TypeRepository.GetAllTypes().Count(), result.Count());
        foreach (var expectedType in TypeRepository.GetAllTypes())
        {
            Assert.Contains(result, t => t.Name == expectedType.Name);
        }

    }

    [Fact]
    public void GetType_ReturnsCorrectType()
    {
        // Act
        var result = TypeRepository.GetType("Fire");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("fire", result.Name);

    }

    [Fact]
    public void GetAllTypes_ReturnsAllTypes()
    {
        // Act
        var result = TypeRepository.GetAllTypes();

        // Assert
        Assert.Equal(TypeRepository.GetAllTypes().Count(), result.Count());
        foreach (var expectedType in TypeRepository.GetAllTypes())
        {
            Assert.Contains(result, t => t.Name == expectedType.Name);
        }

    }

    [Fact]
    public void SavePokedexToFile_SavesCorrectData()
    {
        var saveFilePath = "saved_types.json";

        // Act
        TypeRepository.SavePokedexToFile(saveFilePath);
        var savedJsonData = File.ReadAllText(saveFilePath);
        var savedTypes = JsonConvert.DeserializeObject<List<Type>>(savedJsonData);

        // Assert
        Assert.Equal(TypeRepository.GetAllTypes().Count(), savedTypes.Count);
        foreach (var expectedType in TypeRepository.GetAllTypes())
        {
            Assert.Contains(savedTypes, t => t.Name == expectedType.Name);
        }

        // Clean up
        File.Delete(saveFilePath);
    }
}
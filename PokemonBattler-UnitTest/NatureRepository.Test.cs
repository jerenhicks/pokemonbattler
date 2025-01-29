using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class NatureRepositoryTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public NatureRepositoryTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestAddNature()
    {
        // Arrange
        var nature = new Nature("CustomNature", 1.1, 0.9, 1.0, 1.0, 1.0);

        // Act
        NatureRepository.AddNature(nature);
        var retrievedNature = NatureRepository.GetNature("CustomNature");

        // Assert
        Assert.NotNull(retrievedNature);
        Assert.Equal("CustomNature", retrievedNature.Name);
        Assert.Equal(1.1, retrievedNature.AttackModifier);
        Assert.Equal(1.0, retrievedNature.SpecialAttackModifier);
        Assert.Equal(0.9, retrievedNature.DefenseModifier);
        Assert.Equal(1.0, retrievedNature.SpecialDefenseModifier);
        Assert.Equal(1.0, retrievedNature.SpeedModifier);
    }

    [Fact]
    public void TestGetNature_NotFound()
    {
        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => NatureRepository.GetNature("NonExistentNature"));
    }

    [Fact]
    public void TestLoadNaturesFromFile()
    {
        // Arrange
        var filePath = "../../../../PokemonBattler/data/test_natures.json";
        var natures = new List<Nature>
        {
            new Nature("Bold", 0.9, 1.0, 1.1, 1.0, 1.0),
            new Nature("Timid", 1.0, 1.0, 1.0, 1.0, 1.1)
        };



        var jsonData = JsonConvert.SerializeObject(natures, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);


        // Act
        NatureRepository.LoadNaturesFromFile(filePath);
        var boldNature = NatureRepository.GetNature("Bold");
        var timidNature = NatureRepository.GetNature("Timid");

        // Assert
        Assert.NotNull(boldNature);
        Assert.Equal("Bold", boldNature.Name);
        Assert.Equal(0.9, boldNature.AttackModifier);
        Assert.Equal(1.0, boldNature.SpecialAttackModifier);
        Assert.Equal(1.1, boldNature.DefenseModifier);
        Assert.Equal(1.0, boldNature.SpecialDefenseModifier);
        Assert.Equal(1.0, boldNature.SpeedModifier);

        Assert.NotNull(timidNature);
        Assert.Equal("Timid", timidNature.Name);
        Assert.Equal(0.9, timidNature.AttackModifier);
        Assert.Equal(1.0, timidNature.SpecialAttackModifier);
        Assert.Equal(1.0, timidNature.DefenseModifier);
        Assert.Equal(1.0, timidNature.SpecialDefenseModifier);
        Assert.Equal(1.1, timidNature.SpeedModifier);
    }
}
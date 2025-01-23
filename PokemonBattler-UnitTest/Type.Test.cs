using Xunit;
using System.Collections.Generic;

public class TypeTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public TypeTest(TestFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void TestTypeInitialization()
    {
        // Arrange
        var type = new Type("Fire");

        // Assert
        Assert.Equal("Fire", type.Name);
        Assert.Empty(type.SuperEffectiveAgainst);
        Assert.Empty(type.NotEffectiveAgainst);
        Assert.Empty(type.NoEffectAgainst);
    }

    [Fact]
    public void TestAddSuperEffectiveAgainst()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var grassType = TypeRepository.GetType("Grass");


        // Assert
        Assert.Contains(grassType, fireType.SuperEffectiveAgainst);
    }

    [Fact]
    public void TestAddNotEffectiveAgainst()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var waterType = TypeRepository.GetType("water");

        // Assert
        Assert.Contains(waterType, fireType.NotEffectiveAgainst);
    }

    [Fact]
    public void TestAddNoEffectAgainst()
    {
        // Arrange
        var normalType = TypeRepository.GetType("Normal");
        var ghostType = TypeRepository.GetType("Ghost");

        // Assert
        Assert.Contains(ghostType, normalType.NoEffectAgainst);
    }

    [Fact]
    public void TestGetEffectiveness_SuperEffective()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var grassType = TypeRepository.GetType("Grass");


        // Act
        var effectiveness = fireType.GetEffectiveness(grassType, null);

        // Assert
        Assert.Equal(2.0, effectiveness);
    }

    [Fact]
    public void TestGetEffectiveness_NotEffective()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var waterType = TypeRepository.GetType("Water");

        // Act
        var effectiveness = fireType.GetEffectiveness(waterType, null);

        // Assert
        Assert.Equal(0.5, effectiveness);
    }

    [Fact]
    public void TestGetEffectiveness_NoEffect()
    {
        // Arrange
        var normalType = TypeRepository.GetType("Normal");
        var ghostType = TypeRepository.GetType("Ghost");

        // Act
        var effectiveness = normalType.GetEffectiveness(ghostType, null);

        // Assert
        Assert.Equal(0.0, effectiveness);
    }

    [Fact]
    public void TestGetEffectiveness_Neutral()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var electricType = TypeRepository.GetType("Electric");

        // Act
        var effectiveness = fireType.GetEffectiveness(electricType, null);

        // Assert
        Assert.Equal(1.0, effectiveness);
    }

    [Fact]
    public void TestGetEffectiveness_DualType()
    {
        // Arrange
        var fireType = TypeRepository.GetType("Fire");
        var grassType = TypeRepository.GetType("Grass");
        var waterType = TypeRepository.GetType("Water");

        // Act
        var effectiveness = fireType.GetEffectiveness(grassType, waterType);

        // Assert
        Assert.Equal(1.0, effectiveness); // 2.0 (super effective) * 0.5 (not effective) = 1.0
    }

    [Fact]
    public void TestGetEffectiveness_Immune()
    {
        // Arrange
        var groundType = TypeRepository.GetType("Ground");
        var FlyingType = TypeRepository.GetType("Flying");
        var rockType = TypeRepository.GetType("Rock");

        // Act
        var effectiveness = groundType.GetEffectiveness(FlyingType, rockType);

        // Assert
        Assert.Equal(0.0, effectiveness);


        var ghostType = TypeRepository.GetType("Ghost");
        var normalType = TypeRepository.GetType("Normal");
        var effectiveness2 = normalType.GetEffectiveness(ghostType, null);
        Assert.Equal(0.0, effectiveness2);
    }
}
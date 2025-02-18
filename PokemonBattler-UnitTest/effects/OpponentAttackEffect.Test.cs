using Xunit;

public class OpponentAttackEffectTest : IClassFixture<TestFixture>
{

    private readonly TestFixture _fixture;

    public OpponentAttackEffectTest(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void SeChance_SetsCorrectValue()
    {
        // Arrange
        var growlEffect = new OpponentAttackEffect();
        double modifier = 0.33;

        // Act
        growlEffect.SetChance(modifier);

        // Assert
        Assert.Equal(modifier, growlEffect.Chance);
    }

    [Fact]
    public void TestDoEffect()
    {
        // Arrange
        var growlEffect = new OpponentAttackEffect();
        growlEffect.SetChance(1.0);
        growlEffect.SetModifier(-1);
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Initial AtkStage of defender
        var initialAtkStage = defender.StatModifiers.AtkStage;

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Equal(initialAtkStage - 1, defender.StatModifiers.AtkStage);
    }

    [Fact]
    public void CheckAllConditions()
    {
        // Arrange
        var growlEffect = new OpponentAttackEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");


        growlEffect.SetModifier(-2);

        // Act
        var result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack fell sharply"));

        growlEffect.SetModifier(-1);

        // Act
        result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack fell"));

        growlEffect.SetModifier(1);

        // Act
        result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack rose"));

        growlEffect.SetModifier(2);

        // Act
        result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack rose sharply"));

        growlEffect.SetModifier(12);

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move, 0);
        result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack can't go any higher"));

        growlEffect.SetModifier(-12);

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move, 0);
        result = growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Contains(result, message => message.Contains("attack can't go any lower"));

    }

    [Fact]
    public void TestDoEffect_MinAtkStage()
    {
        // Arrange
        var growlEffect = new OpponentAttackEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Set defender's AtkStage to minimum
        defender.StatModifiers.ChangeAtkStage(-6);

        // Act
        growlEffect.PostDamageEffect(attacker, defender, move, 0);

        // Assert
        Assert.Equal(-6, defender.StatModifiers.AtkStage); // AtkStage should not go below -6
    }

    [Fact]
    public void PreDamageEffect_ReturnsEmptyList()
    {
        // Arrange
        var growlEffect = new OpponentAttackEffect();
        var attacker = PokedexRepository.CreatePokemon("" + 129, NatureRepository.GetNature("adamant")); // Magikarp
        var defender = PokedexRepository.CreatePokemon("" + 596, NatureRepository.GetNature("adamant")); // Galvantula
        var move = MoveRepository.GetMove("growl");

        // Act
        var result = growlEffect.PreDamageEffect(attacker, defender, move);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SetRandom()
    {
        // Arrange
        var OpponentAttackEffect = new OpponentAttackEffect();
        var random = new Random();
        OpponentAttackEffect.SetRandom(random);

        Assert.Equal(random, OpponentAttackEffect.Random);
    }

}
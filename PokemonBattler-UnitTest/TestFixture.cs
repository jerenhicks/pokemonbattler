using System;

public class TestFixture
{
    private static readonly object _lock = new object();
    private static bool _initialized = false;

    public TestFixture()
    {
        lock (_lock)
        {
            if (!_initialized)
            {
                // Load natures from CSV file
                NatureRepository.LoadNaturesFromFile("../../../../PokemonBattler/data/natures.csv");
                TypeRepository.LoadTypesFromFile("../../../../PokemonBattler/data/types.csv");
                MoveRepository.LoadMovesFromFile("../../../../PokemonBattler/data/moves.csv");
                PokedexRepository.LoadPokedexFromFile("../../../../PokemonBattler/data/pokedex.csv");

                _initialized = true;
            }
        }
    }
}
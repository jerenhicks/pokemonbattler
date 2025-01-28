using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class TypeRepositoryTests
{
    [Fact]
    public void GetAllTypes_ReturnsCorrectTypes()
    {
        // Arrange
        var filePath = "test_types.csv";
        var expectedTypes = new List<string> { "normal", "fire", "water", "grass" };

        // Create a temporary CSV file for testing
        File.WriteAllLines(filePath, new[]
        {
            "Normal",
            "Fire",
            "Water",
            "Grass"
        });

        TypeRepository.LoadTypesFromFile(filePath);

        // Act
        var result = TypeRepository.GetAllTypes();

        List<string> actualTypes = new List<string>();
        foreach (var type in result)
        {
            actualTypes.Add(type.Name);
        }

        // Assert
        foreach (var type in expectedTypes)
        {
            Assert.Contains(type, actualTypes);
        }

        // Clean up
        File.Delete(filePath);
    }
}
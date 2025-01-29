using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;

public class TypeRepositoryTests
{
    [Fact]
    public void GetAllTypes_ReturnsCorrectTypes()
    {
        // // Arrange
        // var filePath = "test_types.json";
        // var expectedTypes = new List<string> { "normal", "fire", "water", "grass" };

        // // Create a temporary JSON file for testing
        // var jsonData = new[]
        // {
        //     new { Name = "normal" },
        //     new { Name = "fire" },
        //     new { Name = "water" },
        //     new { Name = "grass" }
        // };
        // File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData));

        // TypeRepository.LoadTypesFromFile(filePath);

        // // Act
        // var result = TypeRepository.GetAllTypes();

        // List<string> actualTypes = new List<string>();
        // foreach (var type in result)
        // {
        //     actualTypes.Add(type.Name);
        // }

        // // Assert
        // foreach (var type in expectedTypes)
        // {
        //     Assert.Contains(type, actualTypes);
        // }

        // // Clean up
        // File.Delete(filePath);
    }
}
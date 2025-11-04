using System;
using MiJuego.Domain.Interfaces;
using Moq;

namespace MiJuego.Tests.Mocks;

public static class MockCard
{
    private static readonly Random Rand = new();
    private static string Name => $"Card {Rand.Next(1000, 9999)}";

    public static Mock<ICard> CreateMockCardRandom()
    {
        var mock = new Mock<ICard>();
        var random = new Random();
        var typeCards = Enum.GetValues(typeof(CardType));

        mock.SetupAllProperties(); 

        mock.Object.Name = $"Card {Rand.Next(1000, 9999)}";
        mock.Object.Description = Rand.Next(1000, 9999).ToString();
        mock.Object.Type = (CardType)typeCards.GetValue(random.Next(typeCards.Length))!;
        mock.Object.Value = Rand.Next(5, 15);
        
            
        return mock;
    }
}

using MiJuego.Domain.Interfaces;
using Moq;

namespace MiJuego.Tests.Mocks;

public static class MockPlayer
{
    private static readonly Random Rand = new();
    public static Mock<IPlayer> CreateMockPlayerRandom()
    {
        var mock = new Mock<IPlayer>();

        mock.SetupAllProperties();

        mock.Object.Name = $"Player {Rand.Next(1000, 9999)}";
        mock.Object.Attack = Rand.Next(5, 15);
        mock.Object.AttackBase = mock.Object.Attack;
        mock.Object.Defense = Rand.Next(1, 10);
        mock.Object.DefenseBase = mock.Object.Defense;
        mock.Object.HealthMax = Rand.Next(20, 30);
        mock.Object.HealthCurrent = mock.Object.HealthMax;
        mock.Object.NegativeEffects = new List<IEffectCard>();
        mock.Object.Hand = new List<ICard>()
        .Select(_ => MockCard.CreateMockCardRandom().Object)
        .ToList();
        mock.Object.Deck = Enumerable.Range(0, 10)
        .Select(_ => MockCard.CreateMockCardRandom().Object)
        .ToList();


        return mock;
    }
}
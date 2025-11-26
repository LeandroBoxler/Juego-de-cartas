using MiJuego.Domain.Services;
using MiJuego.Domain.UseCases;
using MiJuego.Tests.Mocks;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Tests.UseCases;

public class DrawCardUseCaseTests
{
    private readonly DeckService _deckService;

    public DrawCardUseCaseTests()
    {
        _deckService = new DeckService();
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_WhenPlayerHasCardsInDeck()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 5);

        // Act
        var result = useCase.Execute(player);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldRespectCustomMaxHandSize()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        int customMaxHandSize = 3;
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: customMaxHandSize);

        // Act
        var result = useCase.Execute(player);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(customMaxHandSize, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldUseDefaultMaxHandSize_WhenNotSpecified()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService); // Default es 5

        // Act
        var result = useCase.Execute(player);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldNotDrawCards_WhenHandIsFull()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        // Llenar la mano manualmente
        for (int i = 0; i < 5; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }

        int initialDeckSize = player.Deck.Count;
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 5);

        // Act
        var result = useCase.Execute(player);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
        Assert.Equal(initialDeckSize, player.Deck.Count); // No se robaron cartas
    }

    [Fact]
    public void Execute_ShouldHandleMultipleCalls_Correctly()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 3);

        // Act
        var result1 = useCase.Execute(player);
        var result2 = useCase.Execute(player); // Segunda llamada no debe agregar más

        // Assert
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.Equal(3, player.Hand.Count); // Sigue siendo 3
    }
}

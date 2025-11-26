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
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 5);

        var result = useCase.Execute(player);

        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldRespectCustomMaxHandSize()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        int customMaxHandSize = 3;
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: customMaxHandSize);


        var result = useCase.Execute(player);


        Assert.True(result.IsSuccess);
        Assert.Equal(customMaxHandSize, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldUseDefaultMaxHandSize_WhenNotSpecified()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService);

        var result = useCase.Execute(player);

        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldNotDrawCards_WhenHandIsFull()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        for (int i = 0; i < 5; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }

        int initialDeckSize = player.Deck.Count;
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 5);

        var result = useCase.Execute(player);

        Assert.True(result.IsSuccess);
        Assert.Equal(5, player.Hand.Count);
        Assert.Equal(initialDeckSize, player.Deck.Count); 
    }

    [Fact]
    public void Execute_ShouldHandleMultipleCalls_Correctly()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        var useCase = new DrawCardUseCase(_deckService, maxHandSize: 3);

        var result1 = useCase.Execute(player);
        var result2 = useCase.Execute(player); 

  
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.Equal(3, player.Hand.Count);
    }
}

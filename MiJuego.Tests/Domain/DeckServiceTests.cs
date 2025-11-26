using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Services;
using MiJuego.Tests.Mocks;

namespace MiJuego.Tests.Domain;

public class DeckServiceTests
{
    private readonly DeckService _deckService;

    public DeckServiceTests()
    {
        _deckService = new DeckService();
    }

    [Fact]
    public void DrawCards_ShouldDrawCardsUpToMaxHandSize()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        int maxHandSize = 5;

        _deckService.DrawCards(player, maxHandSize);

        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotDrawMoreThanMaxHandSize()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        for (int i = 0; i < 3; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }
        
        int maxHandSize = 5;

        _deckService.DrawCards(player, maxHandSize);

        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotDrawWhenHandIsFull()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        int maxHandSize = 3;
        for (int i = 0; i < maxHandSize; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }

        _deckService.DrawCards(player, maxHandSize);

        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotExceedMaxHandSize_EvenIfDeckHasMoreCards()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        int maxHandSize = 3;

        _deckService.DrawCards(player, maxHandSize);

        Assert.Equal(maxHandSize, player.Hand.Count);
        Assert.True(player.Deck.Count > 0); 
    }

    [Fact]
    public void DrawCards_ShouldThrowException_WhenPlayerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => 
            _deckService.DrawCards(null, 5));
    }

    [Fact]
    public void CreateDeck_ShouldAddCardsToPlayerDeck()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Deck.Clear();
        
        var cards = new List<ICard>
        {
            MockCard.CreateMockCardRandom().Object,
            MockCard.CreateMockCardRandom().Object,
            MockCard.CreateMockCardRandom().Object
        };

        _deckService.CreateDeck(player, cards);

        Assert.Equal(3, player.Deck.Count);
    }

    [Fact]
    public void CreateDeck_ShouldThrowException_WhenPlayerIsNull()
    {
        var cards = new List<ICard> { MockCard.CreateMockCardRandom().Object };

        Assert.Throws<ArgumentNullException>(() => 
            _deckService.CreateDeck(null, cards));
    }

    [Fact]
    public void CreateDeck_ShouldThrowException_WhenCardsIsNull()
    {
        var player = MockPlayer.CreateMockPlayerRandom().Object;

        Assert.Throws<ArgumentNullException>(() => 
            _deckService.CreateDeck(player, null));
    }
}

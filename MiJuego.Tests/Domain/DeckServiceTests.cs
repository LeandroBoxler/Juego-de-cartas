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
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        int maxHandSize = 5;

        // Act
        _deckService.DrawCards(player, maxHandSize);

        // Assert
        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotDrawMoreThanMaxHandSize()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        // Añadir 3 cartas manualmente
        for (int i = 0; i < 3; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }
        
        int maxHandSize = 5;

        // Act
        _deckService.DrawCards(player, maxHandSize);

        // Assert
        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotDrawWhenHandIsFull()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        int maxHandSize = 3;
        for (int i = 0; i < maxHandSize; i++)
        {
            player.Hand.Add(MockCard.CreateMockCardRandom().Object);
        }

        // Act
        _deckService.DrawCards(player, maxHandSize);

        // Assert
        Assert.Equal(maxHandSize, player.Hand.Count);
    }

    [Fact]
    public void DrawCards_ShouldNotExceedMaxHandSize_EvenIfDeckHasMoreCards()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Hand.Clear();
        
        // Asegurar que hay más cartas en el mazo que el maxHandSize
        int maxHandSize = 3;

        // Act
        _deckService.DrawCards(player, maxHandSize);

        // Assert
        Assert.Equal(maxHandSize, player.Hand.Count);
        Assert.True(player.Deck.Count > 0); // Aún quedan cartas en el mazo
    }

    [Fact]
    public void DrawCards_ShouldThrowException_WhenPlayerIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _deckService.DrawCards(null, 5));
    }

    [Fact]
    public void CreateDeck_ShouldAddCardsToPlayerDeck()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;
        player.Deck.Clear();
        
        var cards = new List<ICard>
        {
            MockCard.CreateMockCardRandom().Object,
            MockCard.CreateMockCardRandom().Object,
            MockCard.CreateMockCardRandom().Object
        };

        // Act
        _deckService.CreateDeck(player, cards);

        // Assert
        Assert.Equal(3, player.Deck.Count);
    }

    [Fact]
    public void CreateDeck_ShouldThrowException_WhenPlayerIsNull()
    {
        // Arrange
        var cards = new List<ICard> { MockCard.CreateMockCardRandom().Object };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _deckService.CreateDeck(null, cards));
    }

    [Fact]
    public void CreateDeck_ShouldThrowException_WhenCardsIsNull()
    {
        // Arrange
        var player = MockPlayer.CreateMockPlayerRandom().Object;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _deckService.CreateDeck(player, null));
    }
}

using MiJuego.Application.Services;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Services;
using MiJuego.Domain.UseCases;
using MiJuego.Infrastructure.Repositories;
using MiJuego.Tests.Mocks;

namespace MiJuego.Tests.Application;

public class BattleServiceTests
{
    private readonly BattleService _battleService;
    private readonly IDeckService _deckService;
    private readonly ICardRepository _cardRepository;

    public BattleServiceTests()
    {
        _cardRepository = new CardRepository();
        _deckService = new DeckService();
        
        var drawCardUseCase = new DrawCardUseCase(_deckService);
        var activateCardUseCase = new ActivateCardUseCase();
        var getCardsUseCase = new GetCardsUseCase(_cardRepository);
        
        _battleService = new BattleService(
            drawCardUseCase,
            activateCardUseCase,
            getCardsUseCase,
            _deckService);
    }

    [Fact]
    public void InitializePlayerDeck_ShouldAddCardsToPlayerDeck()
    {
        // Arrange
        var mockPlayer = MockPlayer.CreateMockPlayerRandom();
        mockPlayer.Object.Deck.Clear();

        // Act
        _battleService.InitializePlayerDeck(mockPlayer.Object);

        // Assert
        Assert.NotEmpty(mockPlayer.Object.Deck);
    }

    [Fact]
    public void DrawCards_ShouldReturnSuccess_WhenPlayerHasCards()
    {
        // Arrange
        var mockPlayer = MockPlayer.CreateMockPlayerRandom();
        mockPlayer.Object.Hand.Clear();

        // Act
        var result = _battleService.DrawCards(mockPlayer.Object);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(5, mockPlayer.Object.Hand.Count);
    }

    [Fact]
    public void ActivateCard_ShouldReturnSuccess_WhenCardIsInHand()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();
        
        playerMock.Object.Hand.Add(cardMock.Object);
        int initialHandSize = playerMock.Object.Hand.Count;

        // Act
        var result = _battleService.ActivateCard(playerMock.Object, targetMock.Object, cardMock.Object);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(initialHandSize - 1, playerMock.Object.Hand.Count);
        Assert.DoesNotContain(cardMock.Object, playerMock.Object.Hand);
    }

    [Fact]
    public void ActivateCard_ShouldReturnError_WhenCardNotInHand()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        // Act
        var result = _battleService.ActivateCard(playerMock.Object, targetMock.Object, cardMock.Object);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Card not found in player's hand.", result.Errors);
    }

    [Fact]
    public void GetAllCards_ShouldReturnListOfCards()
    {
        // Act
        var result = _battleService.GetAllCards();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotEmpty(result.Value);
    }

    [Fact]
    public void ActivateCard_ShouldReturnError_WhenPlayerIsNull()
    {
        // Arrange
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        // Act
        var result = _battleService.ActivateCard(null, targetMock.Object, cardMock.Object);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Player cannot be null.", result.Errors);
    }

    [Fact]
    public void ActivateCard_ShouldReturnError_WhenTargetIsNull()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        // Act
        var result = _battleService.ActivateCard(playerMock.Object, null, cardMock.Object);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Target cannot be null.", result.Errors);
    }
}

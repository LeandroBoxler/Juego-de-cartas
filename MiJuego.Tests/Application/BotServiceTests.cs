using MiJuego.Application.Services;
using MiJuego.Domain.Interfaces;
using MiJuego.Tests.Mocks;

namespace MiJuego.Tests.Application;

public class BotServiceTests
{
    private readonly BotService _botService;

    public BotServiceTests()
    {
        _botService = new BotService();
    }

    [Fact]
    public void SelectCard_ShouldReturnLethalCard_WhenBotCanKillPlayer()
    {
        // Arrange
        var botMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        
        botMock.Object.Attack = 10;
        targetMock.Object.HealthCurrent = 25;
        
        var lethalCard = MockCard.CreateMockCardRandom();
        lethalCard.Object.Type = CardType.Attack;
        lethalCard.Object.Value = 20; // 10 (attack) + 20 (card) = 30 > 25 (health)
        
        var healCard = MockCard.CreateMockCardRandom();
        healCard.Object.Type = CardType.Health;
        
        botMock.Object.Hand.Clear();
        botMock.Object.Hand.Add(healCard.Object);
        botMock.Object.Hand.Add(lethalCard.Object);

        // Act
        var selectedCard = _botService.SelectCard(botMock.Object, targetMock.Object);

        // Assert
        Assert.Equal(lethalCard.Object, selectedCard);
    }

    [Fact]
    public void SelectCard_ShouldReturnHealCard_WhenBotHealthIsLow()
    {
        // Arrange
        var botMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        
        botMock.Object.HealthMax = 100;
        botMock.Object.HealthCurrent = 10; // 10% de vida (< 15%)
        
        var healCard = MockCard.CreateMockCardRandom();
        healCard.Object.Type = CardType.Health;
        
        var attackCard = MockCard.CreateMockCardRandom();
        attackCard.Object.Type = CardType.Attack;
        attackCard.Object.Value = 5;
        
        botMock.Object.Hand.Clear();
        botMock.Object.Hand.Add(attackCard.Object);
        botMock.Object.Hand.Add(healCard.Object);

        // Act
        var selectedCard = _botService.SelectCard(botMock.Object, targetMock.Object);

        // Assert
        Assert.Equal(CardType.Health, selectedCard.Type);
    }

    [Fact]
    public void SelectCard_ShouldReturnRandomCard_WhenNoStrategyApplies()
    {
        // Arrange
        var botMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        
        botMock.Object.HealthMax = 100;
        botMock.Object.HealthCurrent = 80;
        botMock.Object.Attack = 5;
        targetMock.Object.HealthCurrent = 100;
        
        var card = MockCard.CreateMockCardRandom();
        card.Object.Type = CardType.Attack;
        card.Object.Value = 10;
        
        botMock.Object.Hand.Clear();
        botMock.Object.Hand.Add(card.Object);

        // Act
        var selectedCard = _botService.SelectCard(botMock.Object, targetMock.Object);

        // Assert
        Assert.NotNull(selectedCard);
        Assert.Contains(selectedCard, botMock.Object.Hand);
    }

    [Fact]
    public void SelectCard_ShouldThrowException_WhenBotHasNoCards()
    {
        // Arrange
        var botMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        
        botMock.Object.Hand.Clear();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => 
            _botService.SelectCard(botMock.Object, targetMock.Object));
    }

    [Fact]
    public void SelectCard_ShouldThrowException_WhenBotPlayerIsNull()
    {
        // Arrange
        var targetMock = MockPlayer.CreateMockPlayerRandom();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _botService.SelectCard(null, targetMock.Object));
    }

    [Fact]
    public void SelectCard_ShouldThrowException_WhenTargetPlayerIsNull()
    {
        // Arrange
        var botMock = MockPlayer.CreateMockPlayerRandom();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            _botService.SelectCard(botMock.Object, null));
    }
}

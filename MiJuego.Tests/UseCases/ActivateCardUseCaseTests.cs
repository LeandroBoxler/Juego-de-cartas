using MiJuego.Domain.UseCases;
using MiJuego.Domain.Interfaces;
using MiJuego.Tests.Mocks;
using Moq;

namespace MiJuego.Tests.UseCases;

public class ActivateCardUseCaseTests
{
    private readonly ActivateCardUseCase _useCase;

    public ActivateCardUseCaseTests()
    {
        _useCase = new ActivateCardUseCase();
    }

    [Fact]
    public void Execute_ShouldActivateCard_AndRemoveFromHand_WhenValidInputs()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        playerMock.Object.Hand.Add(cardMock.Object);
        int initialHandSize = playerMock.Object.Hand.Count;

        // Act
        var result = _useCase.Execute(playerMock.Object, targetMock.Object, cardMock.Object);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.DoesNotContain(cardMock.Object, playerMock.Object.Hand);
        Assert.Equal(initialHandSize - 1, playerMock.Object.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCardIsNotInPlayerHand()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var anotherCard = MockCard.CreateMockCardRandom().Object;

        // Act
        var result = _useCase.Execute(playerMock.Object, targetMock.Object, anotherCard);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Card not found in player's hand.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenPlayerIsNull()
    {
        // Arrange
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        // Act
        var result = _useCase.Execute(null, targetMock.Object, cardMock.Object);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Player cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenTargetIsNull()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        // Act
        var result = _useCase.Execute(playerMock.Object, null, cardMock.Object);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Target cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCardIsNull()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();

        // Act
        var result = _useCase.Execute(playerMock.Object, targetMock.Object, null);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Card cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnMultipleErrors_WhenMultipleValidationsFail()
    {
        // Act
        var result = _useCase.Execute(null, null, null);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(3, result.Errors.Count);
        Assert.Contains("Player cannot be null.", result.Errors);
        Assert.Contains("Target cannot be null.", result.Errors);
        Assert.Contains("Card cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldApplyCardEffect_WhenActivated()
    {
        // Arrange
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        playerMock.Object.Hand.Add(cardMock.Object);
        int effectCallCount = 0;
        
        // Configurar el mock para verificar que se llama ApplyEffect
        cardMock.Setup(c => c.ApplyEffect(
            It.IsAny<IPlayer>(), 
            It.IsAny<IPlayer>()))
            .Callback(() => effectCallCount++);

        // Act
        var result = _useCase.Execute(playerMock.Object, targetMock.Object, cardMock.Object);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, effectCallCount);
    }
}

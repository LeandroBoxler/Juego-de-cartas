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
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        playerMock.Object.Hand.Add(cardMock.Object);
        int initialHandSize = playerMock.Object.Hand.Count;

        var result = _useCase.Execute(playerMock.Object, targetMock.Object, cardMock.Object);

        Assert.True(result.IsSuccess);
        Assert.DoesNotContain(cardMock.Object, playerMock.Object.Hand);
        Assert.Equal(initialHandSize - 1, playerMock.Object.Hand.Count);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCardIsNotInPlayerHand()
    {
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var anotherCard = MockCard.CreateMockCardRandom().Object;

        var result = _useCase.Execute(playerMock.Object, targetMock.Object, anotherCard);

        Assert.False(result.IsSuccess);
        Assert.Contains("Card not found in player's hand.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenPlayerIsNull()
    {
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        var result = _useCase.Execute(null, targetMock.Object, cardMock.Object);

        Assert.False(result.IsSuccess);
        Assert.Contains("Player cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenTargetIsNull()
    {
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        var result = _useCase.Execute(playerMock.Object, null, cardMock.Object);

        Assert.False(result.IsSuccess);
        Assert.Contains("Target cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnError_WhenCardIsNull()
    {
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();

        var result = _useCase.Execute(playerMock.Object, targetMock.Object, null);

        Assert.False(result.IsSuccess);
        Assert.Contains("Card cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnMultipleErrors_WhenMultipleValidationsFail()
    {
        var result = _useCase.Execute(null, null, null);

        Assert.False(result.IsSuccess);
        Assert.Equal(3, result.Errors.Count);
        Assert.Contains("Player cannot be null.", result.Errors);
        Assert.Contains("Target cannot be null.", result.Errors);
        Assert.Contains("Card cannot be null.", result.Errors);
    }

    [Fact]
    public void Execute_ShouldApplyCardEffect_WhenActivated()
    {
        var playerMock = MockPlayer.CreateMockPlayerRandom();
        var targetMock = MockPlayer.CreateMockPlayerRandom();
        var cardMock = MockCard.CreateMockCardRandom();

        playerMock.Object.Hand.Add(cardMock.Object);
        int effectCallCount = 0;
        
        cardMock.Setup(c => c.ApplyEffect(
            It.IsAny<IPlayer>(), 
            It.IsAny<IPlayer>()))
            .Callback(() => effectCallCount++);

        var result = _useCase.Execute(playerMock.Object, targetMock.Object, cardMock.Object);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, effectCallCount);
    }
}

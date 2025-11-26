using MiJuego.Domain.UseCases;
using MiJuego.Domain.Interfaces;
using MiJuego.Infrastructure.Repositories;

namespace MiJuego.Tests.UseCases;

public class GetCardsUseCaseTests
{
    private readonly GetCardsUseCase _useCase;
    private readonly ICardRepository _cardRepository;

    public GetCardsUseCaseTests()
    {
        _cardRepository = new CardRepository();
        _useCase = new GetCardsUseCase(_cardRepository);
    }

    [Fact]
    public void Execute_ShouldReturnListOfCards_WhenValidInputs()
    {
        var result = _useCase.Execute();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsType<OperationResult<List<ICard>>>(result);
        Assert.IsType<List<ICard>>(result.Value);
        Assert.NotEmpty(result.Value);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_Always()
    {
        var result = _useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnExpectedNumberOfCards()
    {
        var result = _useCase.Execute();

        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public void Execute_ShouldReturnCardsWithValidProperties()
    {
        var result = _useCase.Execute();

        foreach (var card in result.Value)
        {
            Assert.NotNull(card);
            Assert.False(string.IsNullOrWhiteSpace(card.Name));
            Assert.False(string.IsNullOrWhiteSpace(card.Description));
            Assert.NotNull(card.Value);
        }
    }

    [Fact]
    public void Execute_ShouldReturnFireballCard()
    {
        var result = _useCase.Execute();

        Assert.Contains(result.Value, c => c.Name == "Fireball");
    }

    [Fact]
    public void Execute_ShouldReturnHealingCard()
    {
        var result = _useCase.Execute();

        Assert.Contains(result.Value, c => c.Name == "Healing");
    }

    [Fact]
    public void Execute_ShouldReturnDifferentInstancesOnMultipleCalls()
    {
        var result1 = _useCase.Execute();
        var result2 = _useCase.Execute();

        Assert.NotSame(result1, result2);
        Assert.NotSame(result1.Value, result2.Value);
    }
}

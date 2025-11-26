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
        // Act
        var result = _useCase.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsType<OperationResult<List<ICard>>>(result);
        Assert.IsType<List<ICard>>(result.Value);
        Assert.NotEmpty(result.Value);
    }

    [Fact]
    public void Execute_ShouldReturnSuccess_Always()
    {
        // Act
        var result = _useCase.Execute();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Execute_ShouldReturnExpectedNumberOfCards()
    {
        // Act
        var result = _useCase.Execute();

        // Assert
        Assert.Equal(2, result.Value.Count); // Actualmente hay 2 cartas
    }

    [Fact]
    public void Execute_ShouldReturnCardsWithValidProperties()
    {
        // Act
        var result = _useCase.Execute();

        // Assert
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
        // Act
        var result = _useCase.Execute();

        // Assert
        Assert.Contains(result.Value, c => c.Name == "Bola de fuego");
    }

    [Fact]
    public void Execute_ShouldReturnHealingCard()
    {
        // Act
        var result = _useCase.Execute();

        // Assert
        Assert.Contains(result.Value, c => c.Name == "Curación");
    }

    [Fact]
    public void Execute_ShouldReturnDifferentInstancesOnMultipleCalls()
    {
        // Act
        var result1 = _useCase.Execute();
        var result2 = _useCase.Execute();

        // Assert
        Assert.NotSame(result1, result2);
        Assert.NotSame(result1.Value, result2.Value);
    }
}

using MiJuego.Infrastructure.Repositories;

namespace MiJuego.Tests.Infrastructure;

public class CardRepositoryTests
{
    private readonly CardRepository _cardRepository;

    public CardRepositoryTests()
    {
        _cardRepository = new CardRepository();
    }

    [Fact]
    public void GetAllCards_ShouldReturnNonEmptyList()
    {
        // Act
        var cards = _cardRepository.GetAllCards();

        // Assert
        Assert.NotNull(cards);
        Assert.NotEmpty(cards);
    }

    [Fact]
    public void GetAllCards_ShouldReturnCardsWithValidProperties()
    {
        // Act
        var cards = _cardRepository.GetAllCards();

        // Assert
        foreach (var card in cards)
        {
            Assert.NotNull(card);
            Assert.False(string.IsNullOrWhiteSpace(card.Name));
            Assert.False(string.IsNullOrWhiteSpace(card.Description));
            Assert.NotNull(card.Value);
        }
    }

    [Fact]
    public void GetAllCards_ShouldReturnExpectedNumberOfCards()
    {
        // Act
        var cards = _cardRepository.GetAllCards();

        // Assert
        Assert.Equal(2, cards.Count); // Actualmente hay 2 cartas: Bola de fuego y Curación
    }

    [Fact]
    public void GetAllCards_ShouldContainFireballCard()
    {
        // Act
        var cards = _cardRepository.GetAllCards();

        // Assert
        Assert.Contains(cards, c => c.Name == "Bola de fuego");
    }

    [Fact]
    public void GetAllCards_ShouldContainHealingCard()
    {
        // Act
        var cards = _cardRepository.GetAllCards();

        // Assert
        Assert.Contains(cards, c => c.Name == "Curación");
    }

    [Fact]
    public void GetAllCards_ShouldReturnNewListEachTime()
    {
        // Act
        var cards1 = _cardRepository.GetAllCards();
        var cards2 = _cardRepository.GetAllCards();

        // Assert
        Assert.NotSame(cards1, cards2);
    }
}

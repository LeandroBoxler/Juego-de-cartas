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
        var cards = _cardRepository.GetAllCards();

        Assert.NotNull(cards);
        Assert.NotEmpty(cards);
    }

    [Fact]
    public void GetAllCards_ShouldReturnCardsWithValidProperties()
    {
        var cards = _cardRepository.GetAllCards();

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
        var cards = _cardRepository.GetAllCards();

        Assert.Equal(2, cards.Count);
    }

    [Fact]
    public void GetAllCards_ShouldContainFireballCard()
    {
        var cards = _cardRepository.GetAllCards();

        Assert.Contains(cards, c => c.Name == "Fireball");
    }

    [Fact]
    public void GetAllCards_ShouldContainHealingCard()
    {
        var cards = _cardRepository.GetAllCards();

        Assert.Contains(cards, c => c.Name == "Healing");
    }

    [Fact]
    public void GetAllCards_ShouldReturnNewListEachTime()
    {
        var cards1 = _cardRepository.GetAllCards();
        var cards2 = _cardRepository.GetAllCards();

        Assert.NotSame(cards1, cards2);
    }
}

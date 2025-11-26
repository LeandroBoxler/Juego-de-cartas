namespace MiJuego.Domain.Interfaces;

public interface IDeckService
{
    void DrawCards(IPlayer player, int maxHandSize);
    void CreateDeck(IPlayer player, System.Collections.Generic.List<ICard> cards);
}

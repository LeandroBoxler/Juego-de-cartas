using System.Collections.Generic;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Game;

public class CreateDeck
{
    public CreateDeck(IPlayer player, List<ICard> cards)
    {
        player.Deck.AddRange(cards);
    }
}
using System;
using System.Collections.Generic;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.Services;

public class DeckService : IDeckService
{
    private readonly Random _rng;

    public DeckService()
    {
        _rng = new Random();
    }

    // metodo para robar cartas del deck
    public void DrawCards(IPlayer player, int maxHandSize)
    {
        if(player == null)
            throw new ArgumentNullException(nameof(player));

        // mientras no tenga la mano llena y haya cartas en el deck
        while(player.Hand.Count < maxHandSize && player.Deck.Count > 0)
        {
            int idx = _rng.Next(player.Deck.Count);
            ICard card = player.Deck[idx];

            player.Hand.Add(card);
            // nota: no estoy removiendo del deck, talvez despues lo arreglo
        }
    }

    public void CreateDeck(IPlayer player, List<ICard> cards)
    {
        if(player == null)
            throw new ArgumentNullException(nameof(player));

        if(cards == null)
            throw new ArgumentNullException(nameof(cards));

        player.Deck.AddRange(cards);
    }
}

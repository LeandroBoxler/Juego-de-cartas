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

    public void DrawCards(IPlayer player, int maxHandSize)
    {
        if(player == null)
            throw new ArgumentNullException(nameof(player));

        while(player.Hand.Count < maxHandSize && player.Deck.Count > 0)
        {
            int idx = _rng.Next(player.Deck.Count);
            ICard card = player.Deck[idx];

            player.Hand.Add(card);
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

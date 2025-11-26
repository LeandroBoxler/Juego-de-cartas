using System;
using System.Collections.Generic;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Factories;

namespace MiJuego.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    public List<ICard> GetAllCards()
    {
        var cards = new List<ICard>();
        
        cards.Add(new CardFactory("Fireball", "Inflicts damage to enemy", CardType.Attack,
            (player, target) =>
            {
                target.HealthCurrent -= player.Attack + 20;
            }, 20, "fireball").Card);

        cards.Add(new CardFactory("Healing", "Recovers 40 health points", CardType.Health,
            (player, target) =>
            {
                player.HealthCurrent = Math.Min(player.HealthCurrent + 40, player.HealthMax);
            }, 40, "card").Card);
            
        return cards;
    }
}

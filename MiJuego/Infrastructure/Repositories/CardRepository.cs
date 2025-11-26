using System;
using System.Collections.Generic;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Factories;

namespace MiJuego.Infrastructure.Repositories;

// repositorio de cartas del juego
public class CardRepository : ICardRepository
{
    public List<ICard> GetAllCards()
    {
        // lista de todas las cartas disponibles
        var cards = new List<ICard>();
        
        // carta de ataque
        cards.Add(new CardFactory("Bola de fuego", "Inflige daño al enemigo", CardType.Attack,
            (player, target) =>
            {
                target.HealthCurrent -= player.Attack + 20;
            }, 20, "fireball").Card);

        // carta de curacion
        cards.Add(new CardFactory("Curación", "Recupera 40 puntos de vida", CardType.Health,
            (player, target) =>
            {
                player.HealthCurrent = Math.Min(player.HealthCurrent + 40, player.HealthMax);
            }, 40, "card").Card);
            
        return cards;
    }
}

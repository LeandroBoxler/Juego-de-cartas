using System;
using System.Collections.Generic;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Factories;

namespace MiJuego.Content;

    public class CardList
    {
        public List<ICard> AllCards => new()
        {
            new CardFactory("Bola de fuego", "Inflige daño al enemigo", CardType.Attack,
                (player, target) =>
                {
                    target.HealthCurrent -= player.Attack + 20;
                }, 20,"fireball").Card,

            new CardFactory("Curación", "Recupera 40 puntos de vida", CardType.Health,
                (player, target) =>
                {
                    player.HealthCurrent = Math.Min(player.HealthCurrent + 40, player.HealthMax);
                }, 40,"card").Card,

        };
    }

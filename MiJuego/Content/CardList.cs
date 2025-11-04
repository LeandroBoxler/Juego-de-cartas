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
                }, 20,"card").Card,

            new CardFactory("Curación", "Recupera 40 puntos de vida", CardType.Health,
                (player, target) =>
                {
                    player.HealthCurrent = Math.Min(player.HealthCurrent + 40, player.HealthMax);
                }, 40,"card").Card,

            new CardFactory("Rompe escudos", "Baja su defensa en 3 puntos", CardType.Debuff,
                (player, target) =>
                {
                    target.Defense -= 3;
                }, 3,"card").Card,

            new CardFactory("Espada maestra", "Aumenta tu ataque en 3 puntos", CardType.Buff,
                (player, target) =>
                {
                    player.Attack += 3;
                }, 3,"card").Card,

            new CardFactory("Armadura", "Aumenta tu defensa en 3 puntos", CardType.Buff,
                (player, target) =>
                {
                    player.Defense += 3;
                }, 3,"card").Card,

            new CardFactory("Picadura de serpiente", "Causa 2 de daño cada turno", CardType.Attack,
                (player, target) =>
                {
                    target.HealthCurrent -= player.Attack + 5;
                    IEffectCard poison = new NegativeEffectFactory(3, 2, NegativeEffect.Poison);
                    target.NegativeEffects.Add(poison);
                }, 5,"card").Card,

            new CardFactory("Corte profundo", "Causa sangrado por 2 turnos", CardType.Attack,
                (player, target) =>
                {
                    target.HealthCurrent -= player.Attack + 5;
                    IEffectCard bleed = new NegativeEffectFactory(3, 2, NegativeEffect.Bleed);
                    target.NegativeEffects.Add(bleed);
                }, 5,"card").Card
        };
    }

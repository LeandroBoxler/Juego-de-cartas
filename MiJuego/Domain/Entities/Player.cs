using System.Collections.Generic;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.Entities;

// clase del jugador
public class Player: IPlayer
{
        public string Name { get; set; }
        public int Attack { get; set; }
        public int AttackBase { get; set; }
        public int Defense { get; set; }
        public int DefenseBase { get; set; }
        public int HealthMax { get; set; }
        public int HealthCurrent { get; set; }
        public List<IEffectCard> NegativeEffects { get; set; }
        public List<ICard> Hand { get; set; }  // cartas en la mano
        public List<ICard> Deck { get; set; }  // mazo del jugador

    public Player(string name, int attackBase, int defenseBase, int healthMax)
    {
        Name = name;
        AttackBase = attackBase;
        DefenseBase = defenseBase;
        HealthMax = healthMax;
        // inicializar stats
        Attack = attackBase;
        Defense = defenseBase;
        HealthCurrent = healthMax;
        NegativeEffects = new List<IEffectCard>();
        Hand = new List<ICard>();
        Deck = new List<ICard>();
    }
    
}
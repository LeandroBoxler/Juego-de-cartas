using System.Collections.Generic;

namespace MiJuego.Domain.Interfaces;
    public interface IPlayer
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int AttackBase { get; set; }
        public int Defense { get; set; }
        public int DefenseBase { get; set; }
        public int HealthMax { get; set; }
        public int HealthCurrent { get; set; }
        public List<IEffectCard> NegativeEffects { get; set; }
        public List<ICard> Hand { get; set; }
        public List<ICard> Deck { get; set; }
        
}

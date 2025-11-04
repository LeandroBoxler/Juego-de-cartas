

using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.Factories;
    public class NegativeEffectFactory : IEffectCard
    {
        public int Value { get; set; }
        public int Duration { get; set; }
        public NegativeEffect NegativeEffect { get; set; }
        public NegativeEffectFactory(int value, int duration, NegativeEffect negativeEffect)
        {
            Value = value;
            Duration = duration;
            NegativeEffect = negativeEffect;
        }
    }

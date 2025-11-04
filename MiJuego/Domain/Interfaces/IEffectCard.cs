
namespace MiJuego.Domain.Interfaces;


public interface IEffectCard
{
    public int Value { get; set; }
    public int Duration { get; set; }
    public NegativeEffect  NegativeEffect { get; set; }

}


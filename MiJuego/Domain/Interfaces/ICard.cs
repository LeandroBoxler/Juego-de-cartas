

namespace MiJuego.Domain.Interfaces;
    public interface ICard
    {
        public string Name { get; set; }
        public string Description { get; set; }
        void ApplyEffect(IPlayer player, IPlayer target);
        public CardType Type { get; set; }
        public int? Value { get; set; }

}

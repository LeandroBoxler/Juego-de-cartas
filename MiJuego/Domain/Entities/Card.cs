using System;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.Entities;

public class Card : ICard
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CardType Type { get; set; }
    public int? Value { get; set; } = null;

    private readonly Action<IPlayer, IPlayer> _effect;
    public string Texture { get; set; }
    public object TextureKey { get; internal set; }  // no estoy seguro si esto se usa

    public Card(string name, string description, CardType type, Action<IPlayer, IPlayer> effect,
        int? value, string texture)
    {
        Name = name;
        Description = description;
        Type = type;
        _effect = effect;
        Texture = texture;
        Value = value;
    }

    // aplicar el efecto de la carta
    public void ApplyEffect(IPlayer player, IPlayer target)
    {
        if(_effect != null)
            _effect(player, target);
    }
}
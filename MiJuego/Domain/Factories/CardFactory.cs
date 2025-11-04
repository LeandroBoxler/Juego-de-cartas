using System;
using MiJuego.Domain.Entities;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.Factories;

    public class CardFactory
    {
        public ICard Card { get; }
        

        public CardFactory(string name, string description, CardType type, Action<IPlayer, IPlayer> effect, int? value, string cardTexture)
        {
            Card = new Card(name, description, type, effect, value, cardTexture);
        }
    }


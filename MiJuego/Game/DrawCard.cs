using System;
using MiJuego.Domain.Interfaces;
namespace MiJuego.Game;

    public static class DrawCard
    {
        private const int _maxHand = 5;
        private static Random _rng = new Random(); 

        public static void DrawCards(IPlayer player)
        {
            while (player.Hand.Count < _maxHand && player.Deck.Count > 0)
            {
                int numberRandom = _rng.Next(player.Deck.Count);
                ICard drawnCard = player.Deck[numberRandom];

                player.Hand.Add(drawnCard);
            }
        }
    }

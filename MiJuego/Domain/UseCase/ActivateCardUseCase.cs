using System.Collections.Generic;
using MiJuego.Domain.Interfaces;
using MiJuego.Game;

namespace MiJuego.Domain.UseCases;

    public class ActivateCardUseCase
    {
        public OperationResult<bool> Execute(IPlayer player, IPlayer target, ICard card)
        {
            var errors = new List<string>();

            if (player == null)
                errors.Add("Player cannot be null.");

            if (target == null)
                errors.Add("Target cannot be null.");

            if (card == null)
                errors.Add("Card cannot be null.");

            if (player != null && card != null && !player.Hand.Contains(card))
                errors.Add("Card not found in player's hand.");

            if (errors.Count > 0)
                return new OperationResult<bool>(errors);

            ActivateCard.Activate(player, target, card);
            player.Hand.Remove(card);

            return new OperationResult<bool>(true);
        }
    }

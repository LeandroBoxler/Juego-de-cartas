using System.Collections.Generic;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.UseCases;

public class ActivateCardUseCase
{
    public OperationResult<bool> Execute(IPlayer player, IPlayer target, ICard card)
    {
        var errors = new List<string>();

        // Validaciones basicas
        if(player == null)
            errors.Add("Player cannot be null.");

        if(target == null)
            errors.Add("Target cannot be null.");

        if(card == null)
            errors.Add("Card cannot be null.");

        // verificar que la carta este en la mano del jugador
        if(player != null && card != null && !player.Hand.Contains(card))
            errors.Add("Card not found in player's hand.");

        if(errors.Count > 0)
            return new OperationResult<bool>(errors);
            
        // aplicar el efecto de la carta
        card.ApplyEffect(player, target);
        // remover de la mano
        player.Hand.Remove(card);

        return new OperationResult<bool>(true);
    }
}

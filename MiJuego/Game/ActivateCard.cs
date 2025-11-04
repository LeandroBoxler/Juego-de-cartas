using MiJuego.Domain.Interfaces;

namespace MiJuego.Game;

public static class ActivateCard
{
    public static void Activate(IPlayer player, IPlayer target, ICard card)
    {
        card.ApplyEffect(player, target);
        player.Hand.Remove(card);
    }
}
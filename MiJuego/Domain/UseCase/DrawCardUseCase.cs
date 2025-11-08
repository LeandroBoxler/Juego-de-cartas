using System.Linq;
using MiJuego.Domain.Interfaces;
using MiJuego.Game;
namespace MiJuego.Domain.UseCases;

class DrawCardUseCase
{
    public OperationResult<ICard> Execute(IPlayer player)
    {
        if (player == null)
            return new OperationResult<ICard>("Player cannot be null.");

        var card = player.Deck.FirstOrDefault();
        if (card == null)
            return new OperationResult<ICard>("No cards left in deck.");

        DrawCard.DrawCards(player);

        return new OperationResult<ICard>(card);
    }
}

using System.Linq;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.UseCases;

public class DrawCardUseCase
{
    private readonly IDeckService _deckService;
    private readonly int _maxHandSize;

    public DrawCardUseCase(IDeckService deckService, int maxHandSize = 5)
    {
        _deckService = deckService;
        _maxHandSize = maxHandSize;
    }

    public OperationResult<bool> Execute(IPlayer player)
    {
        if(player == null)
            return new OperationResult<bool>("Player cannot be null.");

        if(player.Deck.Count == 0)
            return new OperationResult<bool>("No cards left in deck.");

        _deckService.DrawCards(player, _maxHandSize);

        return new OperationResult<bool>(true);
    }
}

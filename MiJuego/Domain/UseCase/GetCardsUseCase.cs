using System.Collections.Generic;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Domain.UseCases;

public class GetCardsUseCase
{
    private readonly ICardRepository _cardRepository;

    public GetCardsUseCase(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public OperationResult<List<ICard>> Execute()
    {
        var cards = _cardRepository.GetAllCards();
        return new OperationResult<List<ICard>>(cards);
    }
}

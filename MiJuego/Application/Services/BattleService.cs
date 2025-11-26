using MiJuego.Domain.Interfaces;
using MiJuego.Domain.UseCases;
using System;
using System.Collections.Generic;

namespace MiJuego.Application.Services;

// Servicio principal para manejar la batalla
public class BattleService
{
    private readonly DrawCardUseCase _drawCardUseCase;
    private readonly ActivateCardUseCase _activateCardUseCase;
    private readonly GetCardsUseCase _getCardsUseCase;
    private readonly IDeckService _deckService;

    public BattleService(
        DrawCardUseCase drawCardUseCase,
        ActivateCardUseCase activateCardUseCase,
        GetCardsUseCase getCardsUseCase,
        IDeckService deckService)
    {
        _drawCardUseCase = drawCardUseCase;
        _activateCardUseCase = activateCardUseCase;
        _getCardsUseCase = getCardsUseCase;
        _deckService = deckService;
    }

    public OperationResult<bool> DrawCards(IPlayer player)
    {
        return _drawCardUseCase.Execute(player);
    }

    public OperationResult<bool> ActivateCard(IPlayer player, IPlayer target, ICard card)
    {
        return _activateCardUseCase.Execute(player, target, card);
    }

    public OperationResult<List<ICard>> GetAllCards()
    {
        return _getCardsUseCase.Execute();
    }

    public void InitializePlayerDeck(IPlayer player)
    {
        var cardsResult = GetAllCards();
        if(cardsResult.IsSuccess)
        {
            _deckService.CreateDeck(player, cardsResult.Value);
        }
    }
}

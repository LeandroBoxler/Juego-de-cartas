using MiJuego.Application.Services;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Services;
using MiJuego.Domain.UseCases;
using MiJuego.Infrastructure.Repositories;

namespace MiJuego.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    private static ICardRepository _cardRepository;
    private static IDeckService _deckService;
    private static BattleService _battleService;
    private static BotService _botService;

    public static void Initialize()
    {
        _cardRepository = new CardRepository();

        _deckService = new DeckService();

        var drawCardUseCase = new DrawCardUseCase(_deckService);
        var activateCardUseCase = new ActivateCardUseCase();
        var getCardsUseCase = new GetCardsUseCase(_cardRepository);

        _battleService = new BattleService(
            drawCardUseCase,
            activateCardUseCase,
            getCardsUseCase,
            _deckService);

        _botService = new BotService();
    }

    public static BattleService GetBattleService()
    {
        if (_battleService == null)
            Initialize();
        return _battleService;
    }

    public static BotService GetBotService()
    {
        if (_botService == null)
            Initialize();
        return _botService;
    }

    public static ICardRepository GetCardRepository()
    {
        if (_cardRepository == null)
            Initialize();
        return _cardRepository;
    }

    public static IDeckService GetDeckService()
    {
        if (_deckService == null)
            Initialize();
        return _deckService;
    }
}

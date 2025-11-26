using MiJuego.Application.Services;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.Services;
using MiJuego.Domain.UseCases;
using MiJuego.Infrastructure.Repositories;

namespace MiJuego.Infrastructure.DependencyInjection;

// contenedor de servicios - talvez deberia usar un DI container de verdad pero esto funciona
public static class ServiceContainer
{
    private static ICardRepository _cardRepository;
    private static IDeckService _deckService;
    private static BattleService _battleService;
    private static BotService _botService;

    // inicializar todos los servicios
    public static void Initialize()
    {
        // repositorios
        _cardRepository = new CardRepository();

        // servicios de dominio
        _deckService = new DeckService();

        // casos de uso
        var drawCardUseCase = new DrawCardUseCase(_deckService);
        var activateCardUseCase = new ActivateCardUseCase();
        var getCardsUseCase = new GetCardsUseCase(_cardRepository);

        // servicios de aplicacion
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

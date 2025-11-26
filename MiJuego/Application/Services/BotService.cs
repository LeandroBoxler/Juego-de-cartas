using System;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Application.Services;

public class BotService
{
    private readonly Random _random;

    public BotService()
    {
        _random = new Random();
    }

    // logica para que el bot seleccione una carta
    public ICard SelectCard(IPlayer botPlayer, IPlayer targetPlayer)
    {
        if(botPlayer == null || targetPlayer == null)
            throw new ArgumentNullException("Players cannot be null");

        if(botPlayer.Hand.Count == 0)
            throw new InvalidOperationException("Bot has no cards in hand");

        // Prioridad 1: ver si puede matar al jugador
        for(int i = 0; i < botPlayer.Hand.Count; i++)
        {
            if(botPlayer.Hand[i].Type == CardType.Attack &&
                botPlayer.Hand[i].Value + botPlayer.Attack >= targetPlayer.HealthCurrent)
            {
                return botPlayer.Hand[i];
            }
        }

        // Prioridad 2: si el bot tiene poca vida, curarse
        if(botPlayer.HealthCurrent <= botPlayer.HealthMax * 0.15)
        {
            for(int i = 0; i < botPlayer.Hand.Count; i++)
            {
                if(botPlayer.Hand[i].Type == CardType.Health)
                {
                    return botPlayer.Hand[i];
                }
            }
        }
        
        // si no hay estrategia clara, elegir random
        return botPlayer.Hand[_random.Next(0, botPlayer.Hand.Count)];
    }
}

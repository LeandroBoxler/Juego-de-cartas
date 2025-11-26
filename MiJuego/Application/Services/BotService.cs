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

    public ICard SelectCard(IPlayer botPlayer, IPlayer targetPlayer)
    {
        if(botPlayer == null || targetPlayer == null)
            throw new ArgumentNullException("Players cannot be null");

        if(botPlayer.Hand.Count == 0)
            throw new InvalidOperationException("Bot has no cards in hand");

        for(int i = 0; i < botPlayer.Hand.Count; i++)
        {
            if(botPlayer.Hand[i].Type == CardType.Attack &&
                botPlayer.Hand[i].Value + botPlayer.Attack >= targetPlayer.HealthCurrent)
            {
                return botPlayer.Hand[i];
            }
        }

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
        
        return botPlayer.Hand[_random.Next(0, botPlayer.Hand.Count)];
    }
}

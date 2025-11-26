using System;
using MiJuego.Domain.Interfaces;


namespace MiJuego.Game;

public class BotController
{
    private IPlayer _botPlayer;
    private IPlayer _player;

    Random random = new Random();
    public BotController(IPlayer BotPlayer, IPlayer player)
    {
        _botPlayer = BotPlayer;
        _player = player;
        
    }

    public ICard SelectedCard()
    {
        for (int i = 0; i < _botPlayer.Hand.Count; i++)
        {
            if (_botPlayer.Hand[i].Type == CardType.Attack &&
                _botPlayer.Hand[i].Value + _botPlayer.Attack >= _player.HealthCurrent)
            {
                return _botPlayer.Hand[i];
            }
            if (_botPlayer.HealthCurrent <= _botPlayer.HealthMax * 0.15)
            {
                if (_botPlayer.Hand[i].Type == CardType.Health)
                {
                    return _botPlayer.Hand[i];
                }
            }
        }
        
        return _botPlayer.Hand[random.Next(0, _botPlayer.Hand.Count)];

    }
}

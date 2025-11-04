using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Domain.Entities;
using MiJuego.Domain.Interfaces;
using MiJuego.Helpers;
using MiJuego.Views;

public class CardsHandView
{
    private readonly IPlayer _player;
    private List<CardView> _cardViews = new();
    private readonly Vector2 _startPosition;

    public CardsHandView(IPlayer player, Vector2 startPosition)
    {
        _player = player;
        _startPosition = startPosition;
        Refresh();
    }

    public void Refresh()
    {
        _cardViews.Clear();
        for (int i = 0; i < _player.Hand.Count; i++)
        {
            if (_player.Hand[i] is Card card)
            {
                var texture = SpriteHelper.Load(card.Texture);
                var position = _startPosition + new Vector2(i * (texture.Width + 10), 0);
                _cardViews.Add(new CardView(card, position, texture));
            }
        }
    }

    public List<CardView> GetCardViews() => _cardViews;

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var cv in _cardViews)
        {
            cv.Draw(spriteBatch);
        }
    }
}

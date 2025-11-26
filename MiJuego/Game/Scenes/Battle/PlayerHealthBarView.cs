using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Domain.Interfaces;
using MiJuego.Views;

public class PlayerHealthBarView
{
    private IPlayer _player;
    private HealthBar _bar;

    public PlayerHealthBarView(IPlayer player, Vector2 position, int width, int height, GraphicsDevice graphicsDevice)
    {
        _player = player;
        _bar = new HealthBar(graphicsDevice, position, width, height);

    }

public void Draw(SpriteBatch spriteBatch)
{
    _bar.Draw(spriteBatch, _player.HealthCurrent, _player.HealthMax);
}

}

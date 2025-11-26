using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MiJuego.Views;

   public class HealthBar
{
    private Texture2D _texture;
    private Vector2 _position;
    private int _width;
    private int _height;

    public HealthBar(GraphicsDevice graphicsDevice, Vector2 position, int width, int height)
    {
        _texture = new Texture2D(graphicsDevice, 1, 1);
        _texture.SetData(new[] { Color.White });
        _position = position;
        _width = width;
        _height = height;
    }

    public void Draw(SpriteBatch spriteBatch, int currentValue, int maxValue)
    {
        float percent = (float)currentValue / maxValue;
        int currentWidth = (int)(_width * percent);

        spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.Gray);

        spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, currentWidth, _height), Color.Green);
    }
}

    


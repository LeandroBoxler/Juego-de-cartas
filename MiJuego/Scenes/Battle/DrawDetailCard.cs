using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Scenes.Battle;
public class DrawDetailCard
{
    private SpriteFont _font;
    public string Text;
    private Vector2 _centerPosition;
    private Color _textColor;
    private Texture2D _rectTexture;
    private Color _backgroundColor;

    public DrawDetailCard( SpriteFont font, ICard? card, Vector2 centerPosition, Color textColor, Color backgroundColor, GraphicsDevice graphicsDevice)
    {
      
        _font = font;
        Text = card?.Description ?? "No hay carta seleccionada";
        _centerPosition = centerPosition;
        _textColor = textColor;
        _backgroundColor = backgroundColor;

        _rectTexture = new Texture2D(graphicsDevice, 1, 1);
        _rectTexture.SetData(new[] { Color.White });
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 textSize = _font.MeasureString(Text);
        Vector2 position = new Vector2(
            _centerPosition.X - textSize.X / 2,
            _centerPosition.Y - textSize.Y / 2
        );


        int padding = 10;

        spriteBatch.Draw(
            _rectTexture,
            new Rectangle(
                (int)(position.X - padding / 2),
                (int)(position.Y - padding / 2),
                (int)(textSize.X + padding),
                (int)(textSize.Y + padding)
            ),
            _backgroundColor
        );
        spriteBatch.DrawString(_font, Text, position, _textColor);
    }
}

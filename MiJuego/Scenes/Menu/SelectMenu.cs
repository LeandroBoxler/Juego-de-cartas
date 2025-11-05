
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MiJuego.Scenes.Menu;
public class SelectMenu
{
    private SpriteFont _font;
    public string Text;
    public Vector2 Position; 
    public Color BackgoundColor = Color.Black;
    public Color HoverColor = Color.DarkGray;
    public Color TextColor = Color.White;

    private MouseState _previousMouse;
    private Texture2D _texture; 

    public SelectMenu(SpriteFont font, Vector2 position, string text)
    {
        _font = font;
        Position = position;
        Text = text;
    }

    private Rectangle TextBounds => new Rectangle(
        (int)Position.X,
        (int)Position.Y,
        (int)_font.MeasureString(Text).X,
        (int)_font.MeasureString(Text).Y
    );

    public bool Update()
    {
        var mouse = Mouse.GetState();
        bool hovering = TextBounds.Contains(mouse.X, mouse.Y);

        if (hovering)
            Mouse.SetCursor(MouseCursor.Hand);
        else
            Mouse.SetCursor(MouseCursor.Arrow);

        bool clicked = hovering &&
                       mouse.LeftButton == ButtonState.Pressed &&
                       _previousMouse.LeftButton == ButtonState.Released;

        _previousMouse = mouse;
        return clicked;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var mouse = Mouse.GetState();
        bool hovering = TextBounds.Contains(mouse.X, mouse.Y);
        var color = hovering ? HoverColor : BackgoundColor;

        if (_texture != null)
            spriteBatch.Draw(_texture, TextBounds, color);

        spriteBatch.DrawString(_font, Text, Position, TextColor);
    }
}

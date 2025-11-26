
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
    private readonly Texture2D _texture; 

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

    public Texture2D Texture => _texture;

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

        if (Texture != null)
            spriteBatch.Draw(Texture, TextBounds, color);

        spriteBatch.DrawString(_font, Text, Position, TextColor);
    }
}

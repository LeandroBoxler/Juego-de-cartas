
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MiJuego.Scenes.Menu;
public class SelectMenu
{
    private Texture2D _texture;
    public string Text;
    private SpriteFont _font;
    public Rectangle Bounds;
    public Color BackgoundColor = Color.Black;
    public Color HoverColor = Color.DarkGray;
    public Color TextColor = Color.White;

    private MouseState _previousMouse;

    public SelectMenu(SpriteFont font, Rectangle bounds, string text)
    {
        _font = font;
        Bounds = bounds;
        Text = text;

    }
    public bool Update()
    {
        var mouse = Mouse.GetState();
        bool hovering = Bounds.Contains(mouse.X, mouse.Y);
        bool clicked = hovering && mouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

        _previousMouse = mouse;

        return clicked;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        var mouse = Mouse.GetState();
        bool hovering = Bounds.Contains(mouse.X, mouse.Y);
        var color = hovering ? HoverColor : BackgoundColor;
        var textSize = _font.MeasureString(Text);
        var textPosition = new Vector2(Bounds.X + (Bounds.Width - textSize.X) / 2,
        Bounds.Y + (Bounds.Height - textSize.Y) / 2);
        spriteBatch.DrawString(_font, Text, textPosition, TextColor);

    }
    
}
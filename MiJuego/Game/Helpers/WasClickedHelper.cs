using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace MiJuego.Helpers;
public static class WasClickedHelper
{
    public static bool WasClicked(Rectangle bounds, MouseState mouse, bool lastClick)
    {
        return mouse.LeftButton == ButtonState.Pressed &&
               bounds.Contains(mouse.X, mouse.Y) &&
               !lastClick;
    }
}

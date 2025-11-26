using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Domain.Entities;

public interface ICardView
{
    Card Card { get; }
    void Draw(SpriteBatch spriteBatch);
    bool WasClicked(MouseState mouseState, bool lastClickState);
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Helpers;

namespace MiJuego.Scenes.Battle;
public class ActivateButton
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _scale;

    public Rectangle Bounds
        {
            get
            {
                float scaledWidth = _texture.Width * _scale * ResolutionHelper.ScaleX;
                float scaledHeight = _texture.Height * _scale * ResolutionHelper.ScaleY;
                return new Rectangle(
                    (int)(_position.X * ResolutionHelper.ScaleX),
                    (int)(_position.Y * ResolutionHelper.ScaleY),
                    (int)scaledWidth,
                    (int)scaledHeight
                );
            }
        }

    public ActivateButton(Texture2D texture, Vector2 position, float scale)
    {
        _texture = texture;
        _position = position;
        _scale = scale;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, new Vector2(
                    _position.X * ResolutionHelper.ScaleX,
                    _position.Y * ResolutionHelper.ScaleY
                ),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                _scale * MathF.Min(ResolutionHelper.ScaleX, ResolutionHelper.ScaleY),
                SpriteEffects.None,
                0f);
    }
    public bool WasClicked(MouseState mouse, bool lastClick)
    {
    return mouse.LeftButton == ButtonState.Pressed &&
    Bounds.Contains(mouse.X, mouse.Y) &&
    !lastClick;
    }
}
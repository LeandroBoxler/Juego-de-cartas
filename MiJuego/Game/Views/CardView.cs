using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Domain.Entities;
using MiJuego.Helpers;


namespace MiJuego.Views;

    public class CardView
    {
        public Card Card { get; }
        public Texture2D Texture { get; }
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; }
        public float Scale { get; set; } = 0.68f;

        public float EffectiveScale => Scale * MathF.Min(ResolutionHelper.ScaleX, ResolutionHelper.ScaleY);
private const float HitboxScale = 0.85f;

public Rectangle Bounds => new(
    (int)((Position.X + (Texture.Width * Scale * (1 - HitboxScale) / 2)) * ResolutionHelper.ScaleX),
    (int)((Position.Y + (Texture.Height * Scale * (1 - HitboxScale) / 2)) * ResolutionHelper.ScaleY),
    (int)(Texture.Width * Scale * HitboxScale * ResolutionHelper.ScaleX),
    (int)(Texture.Height * Scale * HitboxScale * ResolutionHelper.ScaleY)
);


        public CardView(Card card, Vector2 position, Texture2D texture)
        {
            Card = card;
            Texture = texture;
            Position = position;
            OriginalPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                new Vector2(Position.X * ResolutionHelper.ScaleX, Position.Y * ResolutionHelper.ScaleY),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                EffectiveScale,
                SpriteEffects.None,
                0f
            );
        }
        public void MoveTo(Vector2 newPosition) => Position = newPosition;
    public void ResetPosition() => Position = OriginalPosition;
        public bool WasClicked(MouseState mouse, bool lastClick)
{
    return mouse.LeftButton == ButtonState.Pressed  
           && Bounds.Contains(mouse.X, mouse.Y)     
           && !lastClick;                           
}

    }

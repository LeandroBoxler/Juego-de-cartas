
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class DrawHelper
{
    public static void DrawCenteredText(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 centerPosition, Color color)
    {
        Vector2 textSize = font.MeasureString(text);
        Vector2 position = new Vector2(
            centerPosition.X - textSize.X / 2,
            centerPosition.Y - textSize.Y / 2
        );

        spriteBatch.DrawString(font, text, position, color);
    }
    
    
}
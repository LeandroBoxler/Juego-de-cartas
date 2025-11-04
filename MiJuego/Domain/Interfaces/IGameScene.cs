using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiJuego.Domain.Interfaces;
public interface IGameScene
{
    void LoadContent();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
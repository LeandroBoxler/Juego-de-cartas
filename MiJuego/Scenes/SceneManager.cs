using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Domain.Interfaces;

namespace MiJuego.Scenes;

public static class SceneManager
{
    private static IGameScene _currentScene;

    public static void ChangeScene(IGameScene newScene)
    {
        _currentScene = newScene;
        _currentScene.LoadContent();
    }

    public static void Update(GameTime gameTime)
    {
        _currentScene?.Update(gameTime);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _currentScene?.Draw(spriteBatch);
    }
}
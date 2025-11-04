using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Domain.Factories;
using MiJuego.Domain.Interfaces;
using MiJuego.Helpers;
using MiJuego.Scenes.Battle;


namespace MiJuego.Scenes;

public class MenuScene : IGameScene
{
    private SpriteFont _font;
    private GameState  _gameState;
    
    public MenuScene(GameState gameState)
    {
        _gameState = gameState;
    }
    public void LoadContent()
    {
        _font = GameServices.Content.Load<SpriteFont>("DefaultFont");
    }

    public void Update(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();
        if (keyboard.IsKeyDown(Keys.Enter))
        {
            _gameState.Player = PlayerFactory.Create("Juan", 5, 2, 30);
            SceneManager.ChangeScene(new BattleScene(_gameState));
        }
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(_font, "Scena 1 precione enter para pasar a la 2", new Vector2(100, 100), Color.White);
        spriteBatch.End();
    }
}
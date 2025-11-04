using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Domain.Factories;
using MiJuego.Domain.Interfaces;
using MiJuego.Helpers;
using MiJuego.Scenes.Battle;
using MiJuego.Scenes.Menu;
namespace MiJuego.Scenes.Menu;

public class MenuScene : IGameScene
{
    private SpriteFont _font;
    private GameState _gameState;

    private SelectMenu _selectMenu;
    public MenuScene(GameState gameState)
    {
        _gameState = gameState;
    }
    public void LoadContent()
    {
        SpriteHelper.Initialize(GameServices.Content);
        _font = GameServices.Content.Load<SpriteFont>("DefaultFont");
       _selectMenu = new SelectMenu( _font, new Rectangle(100, 100, 200, 50), "Start Game");
       
    }

    public void Update(GameTime gameTime)
    {
        if (_selectMenu.Update())
        {
            _gameState.Player = PlayerFactory.Create("Juan", 5, 2, 30);
            SceneManager.ChangeScene(new BattleScene(_gameState));
        }

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
        spriteBatch.DrawString(_font, "My Game", new Vector2(300, 100), Color.White);
        _selectMenu.Draw(spriteBatch);
        spriteBatch.End();
    }
}
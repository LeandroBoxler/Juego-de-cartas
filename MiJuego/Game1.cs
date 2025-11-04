using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Helpers;
using MiJuego.Scenes;

namespace MiJuego;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState _gameState;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        _gameState = new GameState();
        base.Initialize();
        SceneManager.ChangeScene(new MenuScene(_gameState)); 
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        GameServices.Content = Content;
        ResolutionHelper.UpdateScale(GraphicsDevice);
        GameServices.GraphicsDevice = GraphicsDevice;
       


    }

    protected override void Update(GameTime gameTime)
    {
        SceneManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SceneManager.Draw(_spriteBatch);
        base.Draw(gameTime);
    }
}
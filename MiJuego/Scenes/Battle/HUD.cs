using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiJuego.Domain.Interfaces;
using MiJuego.Helpers;


namespace MiJuego.Scenes.Battle;
public class HUD
{
    private PlayerHealthBarView _healthBarPlayer;
    private PlayerHealthBarView _healthBarEnemy;
    private ActivateButton _activateButton;


    public HUD(IPlayer player, IPlayer enemy, GraphicsDevice graphicsDevice)
    {
        _healthBarPlayer = new PlayerHealthBarView(player, new Vector2(10, 10), 200, 20, graphicsDevice);
        _healthBarEnemy = new PlayerHealthBarView(enemy, new Vector2(10, 40), 200, 20, graphicsDevice);
        _activateButton = new ActivateButton(GameServices.Content.Load<Texture2D>("button-icon"), new Vector2(350, 70), 0.3f);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _healthBarPlayer.Draw(spriteBatch);
        _healthBarEnemy.Draw(spriteBatch);
        _activateButton.Draw(spriteBatch);
    }
}
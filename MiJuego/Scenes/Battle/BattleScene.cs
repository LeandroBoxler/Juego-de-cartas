using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Content;
using MiJuego.Domain.Entities;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.UseCases;
using MiJuego.Helpers;
using MiJuego.Views;
using MiJuego.Scenes.Menu;

namespace MiJuego.Scenes.Battle;
    public class BattleScene : IGameScene
    {
        private SpriteFont _font;
        public IPlayer Player { get; set; }
        public IPlayer Enemy { get; set; }
        private readonly GameState _gameState;
        private CardList _cardList;
        private CardsHandView _cardsHandView;
        private bool _lastClick;
        public List<CardView> CardViews { get; set; } = new();

        private CardView CardSelected = null;
        private ActivateButton _activateCardButton;
        
        private Vector2 _cardStartPosition = new (0, 400);
        private CardSelector _cardSelector;
        private HUD _hud;
        private DrawDetailCard _drawDetailView;
        public bool turn = true;
        
        public Random Random = new Random();

        public BattleScene(GameState gameState)
        {
            _gameState = gameState;
            Player = gameState.Player;
            Enemy = new Player("pedro", 5, 2, 30);
            _cardList = new CardList();
            _cardSelector = new CardSelector();
            
        }

        public void LoadContent()
    {
  
            SpriteHelper.Initialize(GameServices.Content);
            Player.Deck.AddRange(_cardList.AllCards);
            Enemy.Deck.AddRange(_cardList.AllCards);
            _hud = new HUD(Player, Enemy, GameServices.GraphicsDevice);
            _font = GameServices.Content.Load<SpriteFont>("DefaultFont");

        new DrawCardUseCase().Execute(Player);
        new DrawCardUseCase().Execute(Enemy);

        _cardsHandView = new CardsHandView(
            Player,
            _cardStartPosition
        );
        
        CardViews = _cardsHandView.GetCardViews();
            _activateCardButton = new ActivateButton(
                SpriteHelper.Load("button-icon"),
                new Vector2(350, 70),
                0.3f
            );

        _drawDetailView = new DrawDetailCard(
            GameServices.Content.Load<SpriteFont>("DefaultFont"),
            CardSelected?.Card,
            new Vector2(GameServices.GraphicsDevice.Viewport.Width - 150, GameServices.GraphicsDevice.Viewport.Height - 500),
            Color.White,
            Color.Black * 0.5f,
            GameServices.GraphicsDevice
        );

        }

    public void Update(GameTime gameTime)
    {
        MouseState mouse = Mouse.GetState();

        CardSelected = _cardSelector.Update(CardViews, mouse, _lastClick, new Vector2(300, 200));   

        if (Enemy.HealthCurrent <= 0)
        {
            SceneManager.ChangeScene(new MenuScene(_gameState));
        }
        if (!turn)
        {
            System.Threading.Thread.Sleep(1000);
            new ActivateCardUseCase().Execute(Enemy, Player, Enemy.Hand[Random.Next(Enemy.Hand.Count)]);
            new DrawCardUseCase().Execute(Enemy);
            turn = true;            
        }


        if (turn)
        {
            if (_activateCardButton.WasClicked(mouse, _lastClick) && CardSelected != null)
            {
                new ActivateCardUseCase().Execute(Player, Enemy, CardSelected.Card);
                _cardSelector.ClearSelection();
                _cardsHandView.Refresh();
                CardSelected = null;
                new DrawCardUseCase().Execute(Player);
                turn = false;
            }

            _lastClick = mouse.LeftButton == ButtonState.Pressed;
        }
    }

        public void Draw(SpriteBatch spriteBatch)
    {
        /////////////////// ESTO SIEMPRE ARRIBA 
        spriteBatch.Begin();
        ////////////////////////////////

            _drawDetailView.Draw(spriteBatch);
            _hud.Draw(spriteBatch);
            _activateCardButton.Draw(spriteBatch);
            _cardsHandView.Draw(spriteBatch);
        foreach (CardView cardView in CardViews)
        {
            cardView.Draw(spriteBatch);
        }
            
            if (CardSelected != null)
            {
                spriteBatch.DrawString(_font, $"Carta Seleccionada: {CardSelected.Card.Name}", new Vector2(150, 300), Color.Yellow);
            }

        spriteBatch.End();
        }
    }

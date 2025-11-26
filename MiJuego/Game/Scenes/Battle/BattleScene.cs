using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Domain.Entities;
using MiJuego.Domain.Interfaces;
using MiJuego.Helpers;
using MiJuego.Views;
using MiJuego.Scenes.Menu;
using MiJuego.Application.Services;

namespace MiJuego.Scenes.Battle;
    public class BattleScene : IGameScene
    {
        private SpriteFont _font;
        public IPlayer Player { get; set; }
        public IPlayer Enemy { get; set; }
        private readonly GameState _gameState;
        private readonly BattleService _battleService;
        private readonly BotService _botService;
        private CardsHandView _cardsHandView;
        private bool _lastClick;
        public List<CardView> CardViews { get; set; } = new();

        private CardView CardSelected = null;
        private ActivateButton _activateCardButton;
        
        private Vector2 _cardStartPosition = new (0, 400);
        private CardSelector _cardSelector;
        private HUD _hud;
        private DrawDetailCardView _drawDetailView;
        public bool turn = true;
        
        public Random Random = new Random();

        public BattleScene(GameState gameState, BattleService battleService, BotService botService)
        {
            _gameState = gameState;
            _battleService = battleService;
            _botService = botService;
            Player = gameState.Player;
            Enemy = new Player("pedro", 5, 2, 30);
            _cardSelector = new CardSelector();
            
        }

        public void LoadContent()
    {
            SpriteHelper.Initialize(GameServices.Content);
            
            _battleService.InitializePlayerDeck(Player);
            _battleService.InitializePlayerDeck(Enemy);
            
            _hud = new HUD(Player, Enemy, GameServices.GraphicsDevice);
            _font = GameServices.Content.Load<SpriteFont>("DefaultFont");

            _battleService.DrawCards(Player);
            _battleService.DrawCards(Enemy);

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

        _drawDetailView = new DrawDetailCardView(
            GameServices.Content.Load<SpriteFont>("DefaultFont"),
            CardSelected?.Card.Description,
            new Vector2(GameServices.GraphicsDevice.Viewport.Width - 150, GameServices.GraphicsDevice.Viewport.Height - 500),
            Color.White,
            Color.Black * 0.5f,
            GameServices.GraphicsDevice
        );
        }

    public void Update(GameTime gameTime)
    {
        _drawDetailView.Text = CardSelected?.Card.Description ?? "No hay carta seleccionada";

        MouseState mouse = Mouse.GetState();

        CardSelected = _cardSelector.Update(CardViews, mouse, _lastClick, new Vector2(300, 200));   

        if(Enemy.HealthCurrent <= 0)
        {
            SceneManager.ChangeScene(new MenuScene(_gameState));
        }
        

        if(!turn)
        {
            System.Threading.Thread.Sleep(500);
            
            var botSelectedCard = _botService.SelectCard(Enemy, Player);
            _battleService.ActivateCard(Enemy, Player, botSelectedCard);
            _battleService.DrawCards(Enemy);
            
            turn = true;            
        }

        if(turn)
        {
            if(_activateCardButton.WasClicked(mouse, _lastClick) && CardSelected != null)
            {
                _battleService.ActivateCard(Player, Enemy, CardSelected.Card);
                _cardSelector.ClearSelection();
                _cardsHandView.Refresh();
                CardSelected = null;
                _battleService.DrawCards(Player);
                turn = false;
            }

            _lastClick = mouse.LeftButton == ButtonState.Pressed;
        }
    }

        public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

            
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
                _drawDetailView.Draw(spriteBatch);
            }

        spriteBatch.End();
        }
    }

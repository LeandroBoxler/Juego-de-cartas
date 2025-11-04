using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiJuego.Content;
using MiJuego.Domain.Entities;
using MiJuego.Domain.Interfaces;
using MiJuego.Domain.UseCases;
using MiJuego.Game;
using MiJuego.Helpers;

using MiJuego.Views;

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
        private Vector2 _cardStartPosition = new (50, 400);
        private CardSelector _cardSelector;
        private HUD _hud;

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
            _hud = new HUD(Player, Enemy, GameServices.GraphicsDevice);
            _font = GameServices.Content.Load<SpriteFont>("DefaultFont");

            DrawCard.DrawCards(Player);

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
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            CardSelected = _cardSelector.Update(CardViews, mouse, _lastClick, new Vector2(300, 200));

            if (Enemy.HealthCurrent <= 0)
            {
                SceneManager.ChangeScene(new MenuScene(_gameState));
            }

            if (_activateCardButton.WasClicked(mouse, _lastClick) && CardSelected != null)
            {
                new ActivateCardUseCase().Execute(Player, Enemy, CardSelected.Card);
                Console.WriteLine($"Hiciste click en {CardSelected.Card.Name}");
                
                _cardSelector.ClearSelection();
                
                _cardsHandView.Refresh();
                CardSelected = null;
            }

            _lastClick = mouse.LeftButton == ButtonState.Pressed;
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
            }

            spriteBatch.DrawString(_font, "Escena 2 - Presione enter para pasar a la 1", new Vector2(150, 100), Color.Red);
            spriteBatch.End();
        }
    }

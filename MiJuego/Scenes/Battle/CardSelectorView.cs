using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiJuego.Helpers;
using MiJuego.Views;
namespace MiJuego.Scenes.Battle;

public class CardSelector
{
    private CardView _selectedCard { get; set; }

    public CardView Update(List<CardView> CardViews, MouseState mouse, bool lastClick, Vector2 centeredPosition)
    {
        if (_selectedCard != null && !CardViews.Contains(_selectedCard))
        {
            _selectedCard = null;
        }

        for (int i = 0; i < CardViews.Count; i++)
        {
            if (WasClickedHelper.WasClicked(CardViews[i].Bounds, mouse, lastClick))
            {
                _selectedCard?.ResetPosition();
                _selectedCard = CardViews[i];
                _selectedCard.MoveTo(centeredPosition);
                return _selectedCard;
            }
        }
        
        return _selectedCard;
    }

    public void ClearSelection()
    {
        _selectedCard?.ResetPosition();
        _selectedCard = null;
    }
}
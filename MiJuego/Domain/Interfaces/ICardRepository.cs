using System.Collections.Generic;

namespace MiJuego.Domain.Interfaces;

public interface ICardRepository
{
    List<ICard> GetAllCards();
}

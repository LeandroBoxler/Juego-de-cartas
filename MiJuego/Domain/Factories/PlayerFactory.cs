

using MiJuego.Domain.Entities;

namespace MiJuego.Domain.Factories;

    public static class PlayerFactory
    {
        public static Player Create(string name, int attackBase, int defenseBase, int healthMax)
        {
            return new Player(name, attackBase, defenseBase, healthMax);
        }
    }

using MiJuego.Domain.Factories;
using MiJuego.Domain.Interfaces;
using MiJuego.Game;
using MiJuego.Tests.Mocks;
using Moq;

namespace MiJuego.Tests.Factories;

public class CardFactoryTest
{
[Fact]
public void Factory_Card()
{
   
    var cardFactory = new CardFactory(
        "fire ball",
        "the fire ball",
        CardType.Attack,
        (player, target) => target.HealthCurrent -= 10,
        10,   
        "texture_fire_ball"  
    );

    var card = cardFactory.Card;

    var mockPlayer = MockPlayer.CreateMockPlayerRandom();
    var mockEnemy = MockPlayer.CreateMockPlayerRandom();

    mockEnemy.SetupAllProperties();
    mockEnemy.Object.HealthMax = 20;
    mockEnemy.Object.HealthCurrent = 20;

    IPlayer player = mockPlayer.Object;
    IPlayer enemy = mockEnemy.Object;

  
    card.ApplyEffect(player, enemy);

    Assert.Equal("fire ball", card.Name);
    Assert.Equal("the fire ball", card.Description);
    Assert.Equal(CardType.Attack, card.Type);
    Assert.Equal(10, card.Value);
    Assert.Equal(10, enemy.HealthCurrent);
}

}
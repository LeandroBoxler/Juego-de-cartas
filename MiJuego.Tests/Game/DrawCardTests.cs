using MiJuego.Game;
using MiJuego.Tests.Mocks;


namespace MiJuego.Tests;

public class DrawCardTests
{
    [Fact]
    public void Draw_Cards()
    {
        var mockPlayer = MockPlayer.CreateMockPlayerRandom();

        mockPlayer.Object.Hand.Clear();

        DrawCard.DrawCards(mockPlayer.Object);

        Assert.Equal(5, mockPlayer.Object.Hand.Count);
    }

}
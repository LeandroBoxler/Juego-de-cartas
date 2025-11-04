using MiJuego.Tests.Mocks;
using MiJuego.Domain.UseCases;

namespace MiJuego.Tests.UseCases
{
    public class ActivateCardUseCaseTests
    {
        [Fact]
        public void Execute_ShouldActivateCard_WhenValidInputs()
        {
            var playerMock = MockPlayer.CreateMockPlayerRandom();
            var targetMock = MockPlayer.CreateMockPlayerRandom();
            var cardMock = MockCard.CreateMockCardRandom();

            playerMock.Object.Hand.Add(cardMock.Object);

            var useCase = new ActivateCardUseCase();

            var result = useCase.Execute(playerMock.Object, targetMock.Object, cardMock.Object);


            Assert.True(result.IsSuccess);
            Assert.DoesNotContain(cardMock.Object, playerMock.Object.Hand);
        }

        [Fact]
        public void Execute_ShouldReturnError_WhenCardNotInHand()
        {
            var playerMock = MockPlayer.CreateMockPlayerRandom();
            var targetMock = MockPlayer.CreateMockPlayerRandom();

            var anotherCard = MockCard.CreateMockCardRandom().Object;

            var useCase = new ActivateCardUseCase();

            var result = useCase.Execute(playerMock.Object, targetMock.Object, anotherCard);

            Assert.False(result.IsSuccess);
            Assert.Contains("Card not found in player's hand.", result.Errors);
        }
    }
}

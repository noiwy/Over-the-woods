namespace SlayTheSpireMechanics.Actions
{
    public class DrawMultipleCardsGA : IAction
    {
        public int CardsDrawn { get; set; }

        public DrawMultipleCardsGA(int cardsDrawn)
        {
            CardsDrawn = cardsDrawn;
        }
    }
}
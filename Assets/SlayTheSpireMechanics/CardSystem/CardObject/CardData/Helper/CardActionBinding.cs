

using SlayTheSpireMechanics.VisualLogic.CardActionsCode;

namespace SlayTheSpireMechanics.VisualLogic.Card
{
    public static class CardActionBinding
    {
        public static ICardAction GetActionFromCardSetting(CardSettings cardSettings)
        {
            CardActionSetting setting = cardSettings.ActionSetting;
            switch (cardSettings.ActionType)
            {
                case ActionType.Damage:
                    DamageCASetting dsa = (DamageCASetting)setting;
                    return new DamageCA(dsa.isGlobal,dsa.Damage, dsa.Repeat);
            }
            return null;
        }
        
    }
}
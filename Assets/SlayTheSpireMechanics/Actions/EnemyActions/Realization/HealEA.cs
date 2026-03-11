namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions
{
    public class HealEA : IEnemyAction
    {
        public int Heal { get; set; }
        public int Repeat {get; set;}

        public HealEA(int heal, int repeat = 1)
        {
            Heal = heal;
            Repeat = repeat;
        }
    }
}
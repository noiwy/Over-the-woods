namespace SlayTheSpireMechanics.VisualLogic.Enemies.EnemyActions
{
    public class DamageEA : IEnemyAction
    {
        public int Damage { get; set; }
        public int Repeat {get; set;}

        public DamageEA(int damage, int repeat = 1)
        {
            Damage = damage;
            Repeat = repeat;
        }
    }
}
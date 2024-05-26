namespace MushroomPocket
{
    public class Wario : Character
    {
        public Wario(int hp, int exp) : base(CharacterName.Wario, hp, exp)
        {
            Skill = "Strength";
            Attack = 90;
            Defense = 25;
            CriticalMultiplier = 0.1;
            CriticalChance = 0.1;
        }
    }
}

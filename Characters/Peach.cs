namespace MushroomPocket
{
    public class Peach : Character
    {
        public Peach(int hp, int exp) : base(CharacterName.Peach, hp, exp)
        {
            Skill = "Magic Abilities";
            Attack = 50;
            Defense = 25;
            CriticalMultiplier = 0.5;
            CriticalChance = 0.2;
        }
    }
}

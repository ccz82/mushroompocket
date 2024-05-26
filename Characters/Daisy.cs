namespace MushroomPocket
{
    public class Daisy : Character
    {
        public Daisy(int hp, int exp) : base(CharacterName.Daisy, hp, exp)
        {
            Skill = "Leadership";
            Attack = 60;
            Defense = 20;
            CriticalMultiplier = 0.4;
            CriticalChance = 0.2;
        }
    }
}

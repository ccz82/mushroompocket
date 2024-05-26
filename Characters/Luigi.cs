namespace MushroomPocket
{
    public class Luigi : Character
    {
        public Luigi(int hp, int exp) : base(CharacterName.Luigi, hp, exp)
        {
            Skill = "Precision and Accuracy";
            Attack = 70;
            Defense = 10;
            CriticalMultiplier = 0.2;
            CriticalChance = 0.4;
        }
    }
}

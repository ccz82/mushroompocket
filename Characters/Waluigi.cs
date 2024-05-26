namespace MushroomPocket
{
    public class Waluigi : Character
    {
        public Waluigi(int hp, int exp) : base(CharacterName.Waluigi, hp, exp)
        {
            Skill = "Agility";
            Attack = 70;
            Defense = 10;
            CriticalMultiplier = 0.3;
            CriticalChance = 0.3;
        }
    }
}

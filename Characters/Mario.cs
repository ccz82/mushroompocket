namespace MushroomPocket
{
    public class Mario : Character
    {
        public Mario(int hp, int exp) : base(CharacterName.Mario, hp, exp)
        {
            Skill = "Combat Skills";
            Attack = 80;
            Defense = 30;
            CriticalMultiplier = 0.1;
            CriticalChance = 0.2;
        }
    }
}

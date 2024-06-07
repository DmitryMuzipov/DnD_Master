namespace DnD_Master.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DexterityModifier { get; set; }  // Модификатор ловкости
        public int Initiative { get; set; }  // Инициатива, рассчитанная автоматически

        public void RollInitiative()
        {
            Random random = new Random();
            Initiative = random.Next(1, 21) + DexterityModifier;  // Бросок d20 + модификатор ловкости
        }
    }
}

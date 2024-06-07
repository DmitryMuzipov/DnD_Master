using DnD_Master.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DnD_Master.Controllers
{
    public class InitiativeController : Controller
    {
        // Списки персонажей и монстров. Обычно это будет из базы данных.
        private static List<Character> characters = CharacterController.characters;
        private static List<Monster> monsters = MonsterController.monsters;

        // Метод для отображения списка инициативы
        public IActionResult Index()
        {
            // Объединяем списки персонажей и монстров
            var initiativeList = characters.Cast<object>()
                                           .Concat(monsters)
                                           .OrderByDescending(c => (c is Character character ? character.Initiative : ((Monster)c).Initiative))
                                           .ToList();

            return View(initiativeList);
        }
    }
}

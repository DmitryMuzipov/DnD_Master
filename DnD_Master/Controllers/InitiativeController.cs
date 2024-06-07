using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;
using System.Collections.Generic;
using System.Linq;

namespace DnD_Master.Controllers
{
    public class InitiativeController : Controller
    {
        private static List<Character> characters = CharacterController.characters;
        private static List<Monster> monsters = MonsterController.monsters;

        public IActionResult Index()
        {
            var initiativeList = characters.Cast<object>()
                                           .Concat(monsters)
                                           .OrderByDescending(c => (c is Character character ? character.Initiative : ((Monster)c).Initiative))
                                           .ToList();
            return View(initiativeList);
        }

        [HttpPost]
        public IActionResult StartCombat()
        {
            // Перебрасываем инициативу для всех монстров
            foreach (var monster in monsters)
            {
                monster.RollInitiative();
            }

            // Перенаправляем на Index для отображения обновленного списка инициатив
            return RedirectToAction("Index");
        }
    }
}

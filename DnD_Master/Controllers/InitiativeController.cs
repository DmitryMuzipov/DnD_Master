using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DnD_Master.Controllers
{
    public class InitiativeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InitiativeController(ApplicationDbContext context)
        {
            _context = context;
        }


        //private static List<Characters> characters = _context.Characters.ToList();
        //private static List<Monster> monsters = MonsterController.monsters;

        public IActionResult Index()
        {
            var characters = _context.Characters.ToList();
            var monsters = _context.Monsters.ToList();

            var initiativeList = characters.Cast<object>()
                                           .Concat(monsters)
                                           .OrderByDescending(c => (c is Character character ? character.Initiative : ((Monster)c).Initiative))
                                           .ToList();
            return View(initiativeList);
        }

        [HttpPost]
        public IActionResult StartCombat(Dictionary<string, int> InitiativeValues, List<string> DeadCharacters, List<string> DeadMonsters)
        {
            var charInd = 0;
            var monsterInd = 0;
            var characters = _context.Characters.ToList();
            var monsters = _context.Monsters.ToList();

            // Обновляем инициативу персонажей на основе данных из формы
            foreach (var charItem in characters)
            {
                if (DeadCharacters.Contains(charItem.Name))
                {
                    var existingCharacher = _context.Characters.FirstOrDefault(m => m.Id == characters[charInd].Id);
                    if (existingCharacher != null)
                    {
                        existingCharacher.Name = characters[charInd].Name;
                        existingCharacher.Initiative = 0;
                        existingCharacher.Dead = true;
                    }
                    
                }
                else
                {
                    var existingCharacher = _context.Characters.FirstOrDefault(m => m.Id == characters[charInd].Id);
                    charItem.Initiative = InitiativeValues[charItem.Name];
                    existingCharacher.Dead = false;
                }
                _context.SaveChanges();
                charInd++;
                //if (InitiativeValues.ContainsKey(charItem.Name))
                //{
                //    charItem.Initiative = InitiativeValues[charItem.Name];
                //}
            }

            // Перебрасываем инициативу для всех монстров
            foreach (var monsterItem in monsters)
            {
                if (DeadMonsters.Contains(monsterItem.Name))
                {
                    var existingMonster = _context.Monsters.FirstOrDefault(m => m.Id == monsters[monsterInd].Id);
                    if (existingMonster != null)
                    {
                        existingMonster.Name = monsters[monsterInd].Name;
                        existingMonster.Initiative = 0;
                        existingMonster.Dead = true;
                    }

                }
                else
                {
                    var existingMonster = _context.Monsters.FirstOrDefault(m => m.Id == monsters[monsterInd].Id);

                    if (existingMonster != null)
                    {
                        existingMonster.Name = monsters[monsterInd].Name;
                        existingMonster.DexterityModifier = monsters[monsterInd].DexterityModifier;
                        existingMonster.RollInitiative();  // Перерасчет инициативы при редактировании

                        //monsterItem.Initiative = InitiativeValues[monsterItem.Name];
                        existingMonster.Dead = false;
                    }    
                }

                _context.SaveChanges();

                monsterInd++;
            }

            // Перенаправляем на Index для отображения обновленного списка инициатив
            return RedirectToAction("Index");
        }
    }
}

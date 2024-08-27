﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult StartCombat(Dictionary<string, int> InitiativeValues)
        {
            var i = 0;
            var characters = _context.Characters.ToList();
            var monsters = _context.Monsters.ToList();

            // Обновляем инициативу персонажей на основе данных из формы
            foreach (var charItem in characters)
            {
                if (InitiativeValues.ContainsKey(charItem.Name))
                {
                    charItem.Initiative = InitiativeValues[charItem.Name];
                }
            }

            // Перебрасываем инициативу для всех монстров
            foreach (var monsterItem in monsters)
            {
                
                monsterItem.RollInitiative();

                var existingMonster = _context.Monsters.FirstOrDefault(m => m.Id == monsters[i].Id);
                if (existingMonster != null)
                {
                    existingMonster.Name = monsters[i].Name;
                    existingMonster.DexterityModifier = monsters[i].DexterityModifier;
                    existingMonster.RollInitiative();  // Перерасчет инициативы при редактировании
                }

                _context.SaveChanges();

                i++;
            }

            // Перенаправляем на Index для отображения обновленного списка инициатив
            return RedirectToAction("Index");
        }
    }
}

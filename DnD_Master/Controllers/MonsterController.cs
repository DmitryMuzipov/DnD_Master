using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;
using System.Collections.Generic;

namespace DnD_Master.Controllers
{
    public class MonsterController : Controller
    {
        public static List<Monster> monsters = new List<Monster>();

        public IActionResult Index()
        {
            return View(monsters);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Monster monster)
        {
            monster.RollInitiative();  // Автоматический расчет инициативы
            monsters.Add(monster);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var monster = monsters.Find(m => m.Id == id);
            return View(monster);
        }

        [HttpPost]
        public IActionResult Edit(Monster monster)
        {
            var existingMonster = monsters.Find(m => m.Id == monster.Id);
            if (existingMonster != null)
            {
                existingMonster.Name = monster.Name;
                existingMonster.DexterityModifier = monster.DexterityModifier;
                existingMonster.RollInitiative();  // Перерасчет инициативы при редактировании
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var monster = monsters.Find(m => m.Id == id);
            if (monster != null)
            {
                monsters.Remove(monster);
            }
            return RedirectToAction("Index");
        }
    }
}

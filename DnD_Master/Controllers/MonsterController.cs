using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;
using System.Collections.Generic;

namespace DnD_Master.Controllers
{
    public class MonsterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonsterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Monster> monsters = new List<Monster>();

        public IActionResult Index()
        {
            var monsters = _context.Monsters.ToList();

            return View(monsters);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Monster monster)
        {
            if (ModelState.IsValid)
            {
                _context.Monsters.Add(monster);
                _context.SaveChanges();
            }
            monster.RollInitiative();  // Автоматический расчет инициативы

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var monster = _context.Monsters.FirstOrDefault(m => m.Id == id);
            return View(monster);
        }

        [HttpPost]
        public IActionResult Edit(Monster monster)
        {
            var existingMonster = _context.Monsters.FirstOrDefault(m => m.Id == monster.Id);
            if (existingMonster != null)
            {
                existingMonster.Name = monster.Name;
                existingMonster.DexterityModifier = monster.DexterityModifier;
                existingMonster.RollInitiative();  // Перерасчет инициативы при редактировании
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Метод для удаления Монстра
        public IActionResult Delete(int id)
        {
            var monster = _context.Monsters.FirstOrDefault(m => m.Id == id);
            if (monster != null)
            {
                _context.Monsters.Remove(monster);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

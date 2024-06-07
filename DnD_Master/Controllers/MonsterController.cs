using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;

namespace DnD_Master.Controllers
{
    public class MonsterController : Controller
    {
        public static List<Monster> monsters = new List<Monster>();

        public IActionResult Index()
        {
            return View(monsters);
        }

        // Метод для отображения формы создания нового монстра
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Метод для обработки данных формы создания нового монстра
        [HttpPost]
        public IActionResult Create(Monster monster)
        {
            monsters.Add(monster);
            return RedirectToAction("Index");
        }

        // Метод для отображения формы редактирования монстра
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var monster = monsters.FirstOrDefault(m => m.Id == id);
            if (monster == null)
            {
                return NotFound();
            }
            return View(monster);
        }

        // Метод для обработки данных формы редактирования монстра
        [HttpPost]
        public IActionResult Edit(Monster monster)
        {
            var existingMonster = monsters.FirstOrDefault(m => m.Id == monster.Id);
            if (existingMonster == null)
            {
                return NotFound();
            }
            existingMonster.Name = monster.Name;
            existingMonster.Initiative = monster.Initiative;
            return RedirectToAction("Index");
        }

        // Метод для удаления монстра
        public IActionResult Delete(int id)
        {
            var monster = monsters.FirstOrDefault(m => m.Id == id);
            if (monster != null)
            {
                monsters.Remove(monster);
            }
            return RedirectToAction("Index");
        }
    }
}

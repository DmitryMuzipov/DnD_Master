using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;

namespace DnD_Master.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterController (ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Character> characters = new List<Character>();

        // Метод для отображения списка персонажей
        public IActionResult Index()
        {
            var characters = _context.Characters.ToList();

            return View(characters);
        }

        // Метод для отображения формы создания нового персонажа
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Метод для обработки данных формы создания нового персонажа запись строки в базу
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Characters.Add(character);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(characters);
        }

        // Метод для отображения формы редактирования персонажа
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            return View(character);
        }

        // Метод для обработки данных формы редактирования персонажа
        [HttpPost]
        public IActionResult Edit(Character character)
        {
            var existingCharacter = _context.Characters.FirstOrDefault(c => c.Id == character.Id);
            if (existingCharacter == null)
            {
                return NotFound();
            }
            existingCharacter.Name = character.Name;
            existingCharacter.Initiative = character.Initiative;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Метод для удаления персонажа
        public IActionResult Delete(int id)
        {
            var character = _context.Characters.FirstOrDefault(c => c.Id == id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

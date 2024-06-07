using Microsoft.AspNetCore.Mvc;
using DnD_Master.Models;

namespace DnD_Master.Controllers
{
    public class CharacterController : Controller
    {
        public static List<Character> characters = new List<Character>();

        // Метод для отображения списка персонажей
        public IActionResult Index()
        {
            return View(characters);
        }

        // Метод для отображения формы создания нового персонажа
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Метод для обработки данных формы создания нового персонажа
        [HttpPost]
        public IActionResult Create(Character character)
        {
            characters.Add(character);
            return RedirectToAction("Index");
        }

        // Метод для отображения формы редактирования персонажа
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
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
            var existingCharacter = characters.FirstOrDefault(c => c.Id == character.Id);
            if (existingCharacter == null)
            {
                return NotFound();
            }
            existingCharacter.Name = character.Name;
            existingCharacter.Initiative = character.Initiative;
            return RedirectToAction("Index");
        }

        // Метод для удаления персонажа
        public IActionResult Delete(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (character != null)
            {
                characters.Remove(character);
            }
            return RedirectToAction("Index");
        }
    }
}

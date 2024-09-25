using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnD_Master.Models;

namespace DnD_Master.Controllers
{
    public class ScenesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Возвращает совпавшие значения из базы 
        [HttpGet]
        public JsonResult SearchParticipants(string searchTerm)
        {
            var participants = _context.Monsters
                .Where(p => p.Name.Contains(searchTerm))
                .Select(p => new
                {
                    Item = p.Name // Переименование SceneName на Item
                })
                .ToList();

            return Json(participants);
        }

        // GET: Scenes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Scene.ToListAsync());
        }

        // GET: Scenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene
                .FirstOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // GET: Scenes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SceneId,SceneName,Participant,Quantity")] Scene scene)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scene);
        }

        // GET: Scenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }
            return View(scene);
        }

        // POST: Scenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SceneId,SceneName,Participant,Quantity")] Scene scene)
        {
            if (id != scene.SceneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneExists(scene.SceneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(scene);
        }

        // GET: Scenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scene
                .FirstOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // POST: Scenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scene = await _context.Scene.FindAsync(id);
            if (scene != null)
            {
                _context.Scene.Remove(scene);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SceneExists(int id)
        {
            return _context.Scene.Any(e => e.SceneId == id);
        }
    }
}

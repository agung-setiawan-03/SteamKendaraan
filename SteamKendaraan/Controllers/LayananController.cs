using Microsoft.AspNetCore.Mvc;
using SteamKendaraan.Models;
using SteamKendaraan.Repositories;

namespace SteamKendaraan.Controllers
{
    public class LayananController : Controller
    {
        private readonly IMasterLayanan layananRepository;

        public LayananController(IMasterLayanan layananRepository)
        {
            this.layananRepository = layananRepository;
        }

        public async Task<IActionResult> Index()
        {
            var layanan = await layananRepository.Get();
            return View(layanan);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var layanan = await layananRepository.Find(id);
            if (layanan == null)
            {
                return NotFound();
            }

            return View(layanan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MasterLayanan model)
        {
            if (ModelState.IsValid)
            {
                await layananRepository.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var layanan = await layananRepository.Find(id);
            if (layanan == null)
            {
                return NotFound();
            }

            return View(layanan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MasterLayanan model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await layananRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var layanan = await layananRepository.Find(id);
            if (layanan == null)
            {
                return BadRequest();
            }

            return View(layanan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var layanan = await layananRepository.Find(id);
            if (layanan != null)
            {
                await layananRepository.Remove(layanan);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

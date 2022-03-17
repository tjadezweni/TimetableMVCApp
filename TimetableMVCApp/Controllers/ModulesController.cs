#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimetableMVCApp.Models;
using TimetableMVCApp.Uow;

namespace TimetableMVCApp.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModulesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork._moduleRepository.ListAsync(module => true));
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _unitOfWork._moduleRepository.GetAsync(module => module.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,Name,Description")] Module @module)
        {
            @module.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                @module = await _unitOfWork._moduleRepository.AddAsync(@module);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _unitOfWork._moduleRepository.GetAsync(module => module.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }
            return View(@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleId,Name,Description")] Module @module)
        {
            if (id != @module.ModuleId)
            {
                return NotFound();
            }
            @module.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork._moduleRepository.UpdateAsync(@module);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.ModuleId))
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
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _unitOfWork._moduleRepository.GetAsync(module => module.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _unitOfWork._moduleRepository.GetAsync(module => module.ModuleId == id);
            await _unitOfWork._moduleRepository.DeleteAsync(@module);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _unitOfWork._moduleRepository.GetAsync(module => module.ModuleId == id) is not null;
        }
    }
}

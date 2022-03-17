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
    public class TimesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Times
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork._timeRepository.ListAsync(time => true));
        }

        // GET: Times/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _unitOfWork
                                ._timeRepository
                                .GetAsync(time => time.TimeId == id);
            if (time == null)
            {
                return NotFound();
            }

            return View(time);
        }

        // GET: Times/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Times/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeId,StartTime,EndTime")] Time time)
        {
            time.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _unitOfWork._timeRepository.AddAsync(time);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(time);
        }

        // GET: Times/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _unitOfWork._timeRepository.GetAsync(time => time.TimeId == id);
            if (time == null)
            {
                return NotFound();
            }
            return View(time);
        }

        // POST: Times/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimeId,StartTime,EndTime")] Time time)
        {
            if (id != time.TimeId)
            {
                return NotFound();
            }
            time.LastModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork._timeRepository.UpdateAsync(time);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeExists(time.TimeId))
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
            return View(time);
        }

        // GET: Times/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var time = await _unitOfWork._timeRepository.GetAsync(time => time.TimeId == id);
            if (time == null)
            {
                return NotFound();
            }

            return View(time);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var time = await _unitOfWork._timeRepository.GetAsync(time => time.TimeId == id);
            await _unitOfWork._timeRepository.DeleteAsync(time);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeExists(int id)
        {
            return _unitOfWork._timeRepository.GetAsync(time => time.TimeId == id) is not null;
        }
    }
}

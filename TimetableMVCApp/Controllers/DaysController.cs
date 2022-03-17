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
    public class DaysController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DaysController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: Days
        public async Task<IActionResult> Index()
        {
            var days = await _unitOfWork._dayRepository.GetDaysWithTimeAsync(day => true);
            return View(days);
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _unitOfWork._dayRepository.GetDayAsync(day => day.DayId == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // GET: Days/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TimeId"] = new SelectList(await _unitOfWork._timeRepository.ListAsync(time => true), "TimeId", "TimeId");
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,Name,TimeId")] Day day)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork._dayRepository.AddAsync(day);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TimeId"] = new SelectList(await _unitOfWork._timeRepository.ListAsync(time => true), "TimeId", "TimeId", day.TimeId);
            return View(day);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _unitOfWork._dayRepository.GetAsync(day => day.DayId == id);
            if (day == null)
            {
                return NotFound();
            }
            ViewData["TimeId"] = new SelectList(await _unitOfWork._timeRepository.ListAsync(time => true), "TimeId", "TimeId", day.TimeId);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DayId,Name,TimeId")] Day day)
        {
            if (id != day.DayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork._dayRepository.UpdateAsync(day);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayExists(day.DayId))
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
            ViewData["TimeId"] = new SelectList(await _unitOfWork._timeRepository.ListAsync(time => true), "TimeId", "TimeId", day.TimeId);
            return View(day);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _unitOfWork._dayRepository.GetAsync(day => day.DayId == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var day = await _unitOfWork._dayRepository.GetAsync(day => day.DayId == id);
            await _unitOfWork._dayRepository.DeleteAsync(day);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayExists(int id)
        {
            return _unitOfWork._dayRepository.GetAsync(day => day.DayId == id) is not null;
        }
    }
}

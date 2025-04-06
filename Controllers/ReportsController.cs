using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvironmentalMunicipality.Models;
using EvironmentalMunicipality.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EvironmentalMunicipality.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports
                .Include(r => r.Citizen)
                .ToListAsync());
        }

        // GET: Reports/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Citizens = await _context.Citizen.OrderBy(c => c.CitizenID).ToListAsync();
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report)
        {
            Debug.WriteLine($"Received CitizenID: {report.CitizenID}");
            Debug.WriteLine($"Received ReportType: {report.ReportType}");
            Debug.WriteLine($"Received Details: {report.Details}");

            try
            {
                // Manual validation
                var validationErrors = new List<string>();

                if (report.CitizenID <= 0)
                {
                    validationErrors.Add("Please enter a valid Citizen ID");
                    ModelState.AddModelError("CitizenID", "Invalid Citizen ID");
                }

                if (string.IsNullOrWhiteSpace(report.ReportType))
                {
                    validationErrors.Add("Report Type is required");
                    ModelState.AddModelError("ReportType", "Required");
                }

                if (string.IsNullOrWhiteSpace(report.Details))
                {
                    validationErrors.Add("Details are required");
                    ModelState.AddModelError("Details", "Required");
                }

                if (validationErrors.Any())
                {
                    TempData["Error"] = string.Join("<br>", validationErrors);
                }
                else
                {
                    report.SubmissionDate = DateTime.Now;
                    report.Status = "Under Review";
                    _context.Reports.Add(report);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Report created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Database error: " + ex.InnerException?.Message ?? ex.Message);
                TempData["Error"] = "Failed to save to database. Please try again.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                TempData["Error"] = "An unexpected error occurred.";
            }

            ViewBag.Citizens = await _context.Citizen.OrderBy(c => c.CitizenID).ToListAsync();
            return View(report);
        }

        // GET: Reports/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Citizen)
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(
            int ReportID,
            string CitizenID,
            string ReportType,
            string Details,
            string Status)

        {
            var report = await _context.Reports.FindAsync(ReportID);
            if (report == null)
            {
                return NotFound();
            }

            report.ReportType = ReportType;
            report.Status = Status;

            try
            {
                _context.Update(report);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(ReportID))
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

        // GET: Reports/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Citizen)
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Report deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportID == id);
        }
    }
}
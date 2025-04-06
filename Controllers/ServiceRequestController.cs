using System.Diagnostics;
using EvironmentalMunicipality.Data;
using EvironmentalMunicipality.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ServiceRequestController : Controller
{
    private readonly AppDbContext _context;

    public ServiceRequestController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Only include Citizen, no Staff
        return View(await _context.ServiceRequests
            .Include(sr => sr.Citizen)
            .ToListAsync());
    }

    // GET: ServiceRequest/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Citizens = await _context.Citizen.OrderBy(c => c.CitizenID).ToListAsync();
        return View();
    }

    // POST: ServiceRequest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceRequest serviceRequests)
    {
        Debug.WriteLine($"Received CitizenID: {serviceRequests.CitizenID}");
        Debug.WriteLine($"Received ServiceType: {serviceRequests.ServiceType}");
        Debug.WriteLine($"Received Status: {serviceRequests.Status}");

        try
        {
            // Manual validation
            var validationErrors = new List<string>();

            if (serviceRequests.CitizenID <= 0)
            {
                validationErrors.Add("Please enter a valid Citizen ID");
                ModelState.AddModelError("CitizenID", "Invalid Citizen ID");
            }

            if (string.IsNullOrWhiteSpace(serviceRequests.ServiceType))
            {
                validationErrors.Add("Service Type is required");
                ModelState.AddModelError("ServiceType", "Required");
            }

            if (string.IsNullOrWhiteSpace(serviceRequests.Status))
            {
                validationErrors.Add("Status are required");
                ModelState.AddModelError("Status", "Required");
            }

            if (validationErrors.Any())
            {
                TempData["Error"] = string.Join("<br>", validationErrors);
            }
            else
            {
                serviceRequests.RequestDate = DateTime.Now;
                serviceRequests.Status = "Under Review";
                _context.ServiceRequests.Add(serviceRequests);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Service REquest created successfully!";
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
        return View(serviceRequests);
    }

    // GET: ServiceRequest/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var serviceRequest = await _context.ServiceRequests
            .Include(r => r.Citizen)
            .FirstOrDefaultAsync(m => m.RequestID == id);
        if (serviceRequest == null)
        {
            return NotFound();
        }

        return View(serviceRequest);
    }

    // GET: ServiceRequest/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var serviceRequest = await _context.ServiceRequests.FindAsync(id);
        if (serviceRequest == null)
        {
            return NotFound();
        }
        return View(serviceRequest);
    }

    // POST: ServiceRequest/Edit
    [HttpPost]
    public async Task<IActionResult> Edit(
        int RequestID,
        int CitizenID,
        string ServiceType,
        string Status)
    {
        var serviceRequest = await _context.ServiceRequests.FindAsync(RequestID);
        if (serviceRequest == null)
        {
            return NotFound();
        }

        serviceRequest.CitizenID = CitizenID;
        serviceRequest.ServiceType = ServiceType;
        serviceRequest.Status = Status;

        try
        {
            _context.Update(serviceRequest);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ServiceRequestExists(RequestID))
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

    // GET: ServiceRequest/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var serviceRequest = await _context.ServiceRequests
            .Include(r => r.Citizen)
            .FirstOrDefaultAsync(m => m.RequestID == id);
        if (serviceRequest == null)
        {
            return NotFound();
        }

        return View(serviceRequest);
    }

    // POST: ServiceRequest/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var serviceRequest = await _context.ServiceRequests.FindAsync(id);
        if (serviceRequest != null)
        {
            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Report deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ServiceRequestExists(int id)
    {
        return _context.ServiceRequests.Any(e => e.RequestID == id);
    }
}

using FitnessTracker.Data.Models;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Pages;

public class EditModel : PageModel
{
    private readonly Data.ExerciseLogDbContext _context;

    public EditModel(Data.ExerciseLogDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LogEntryData LogEntryData { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var logentrydata = await _context.ExcerciseLogEntries.FirstOrDefaultAsync(m => m.Id == id);
        if (logentrydata == null)
        {
            return NotFound();
        }
        LogEntryData = logentrydata;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (await ExerciseApiService.GetExerciseById(LogEntryData.ExerciseId) == null)
        {
            ModelState.AddModelError("LogEntryData.ExerciseName", "Please choose an exercise from the dropdown list.");
        }

        if (LogEntryData.IsTimeBasedExercise)
        {
            if (LogEntryData.Time == null || LogEntryData.Time <= 0)
            {
                ModelState.AddModelError("LogEntryData.Time", "Time cannot be empty and must be greater than 0.");
            }

            LogEntryData.Reps = null;
        }
        else
        {
            if (LogEntryData.Reps == null || LogEntryData.Reps <= 0)
            {
                ModelState.AddModelError("LogEntryData.Reps", "Reps cannot be empty and must be greater than 0.");
            }

            LogEntryData.Time = null;
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(LogEntryData).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LogEntryDataExists(LogEntryData.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    public async Task<JsonResult> OnGetSearchAsync(string query)
    {
        var results = await ExerciseApiService.GetFuzzySearchResults(query);
        return new JsonResult(results);
    }

    private bool LogEntryDataExists(int id)
    {
        return _context.ExcerciseLogEntries.Any(e => e.Id == id);
    }
}

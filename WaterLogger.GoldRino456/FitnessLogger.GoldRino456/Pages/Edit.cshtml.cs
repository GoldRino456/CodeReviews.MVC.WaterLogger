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
    public LogEntryData LogEntryData { get; set; } = new LogEntryData
    {
        Date = DateTime.MinValue,
        ExerciseName = "",
        Sets = 0,
        Reps = 0,
        Time = 0.0f,
        IsTimeBasedExercise = false
    };
    public string? ErrorMessage { get; private set; } = null;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        LogEntryData? logentrydata;

        try
        {
           logentrydata = await _context.ExcerciseLogEntries.FirstOrDefaultAsync(m => m.Id == id);
        }
        catch
        {
            ErrorMessage = "ERROR: Trouble communicating with database. Please try reloading the page.";
            return Page();
        }


        if (logentrydata == null)
        {
            return NotFound();
        }
        LogEntryData = logentrydata;
        return Page();
    }

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

        try
        {
            _context.Attach(LogEntryData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch
        {
            if (!LogEntryDataExists(LogEntryData.Id))
            {
                return NotFound();
            }
            else
            {
                ErrorMessage = "ERROR: Could not update entry in database. Please try refreshing the page.";
                return Page();
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

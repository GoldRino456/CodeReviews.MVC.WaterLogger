using FitnessTracker.Data;
using FitnessTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Pages;

public class DeleteModel : PageModel
{
    private readonly ExerciseLogDbContext _context;

    public DeleteModel(ExerciseLogDbContext context)
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

        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        LogEntryData? logentrydata;

        try
        {
            logentrydata = await _context.ExcerciseLogEntries.FindAsync(id);
        }
        catch
        {
            ErrorMessage = "ERROR: Trouble communicating with database. Please try reloading the page.";
            return Page();
        }

        if (logentrydata != null)
        {
            LogEntryData = logentrydata;

            try
            {
                _context.ExcerciseLogEntries.Remove(LogEntryData);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ErrorMessage = "ERROR: Could not delete record from database. Please try refreshing the page.";
                return Page();
            }
        }

        return RedirectToPage("./Index");
    }
}

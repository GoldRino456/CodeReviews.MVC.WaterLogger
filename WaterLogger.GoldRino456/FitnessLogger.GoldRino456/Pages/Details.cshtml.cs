using FitnessTracker.Data;
using FitnessTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Pages;

public class DetailsModel : PageModel
{
    private readonly ExerciseLogDbContext _context;

    public DetailsModel(ExerciseLogDbContext context)
    {
        _context = context;
    }

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

        try
        {
            var logentrydata = await _context.ExcerciseLogEntries.FirstOrDefaultAsync(m => m.Id == id);

            if (logentrydata is not null)
            {
                LogEntryData = logentrydata;

                return Page();
            }
        }
        catch
        {
            ErrorMessage = "ERROR: Could not retrieve entry from database. Please try refreshing the page.";
            return Page();
        }

        return NotFound();
    }
}

using FitnessTracker.Data.Models;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessTracker.Pages;

public class CreateModel : PageModel
{
    private readonly Data.ExerciseLogDbContext _context;

    [BindProperty]
    public LogEntryData LogEntryData { get; set; } = default!;

    public CreateModel(Data.ExerciseLogDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
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

        _context.ExcerciseLogEntries.Add(LogEntryData);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    public async Task<JsonResult> OnGetSearchAsync(string query)
    {
        var results = await ExerciseApiService.GetFuzzySearchResults(query);
        return new JsonResult(results);
    }
}

using FitnessTracker.Data.Models;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessTracker.Pages;

public class ExerciseDisplayModel : PageModel
{
    public string SearchQuery { get; set; } = String.Empty;
    public ExerciseData ExerciseData { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string? id, string? query)
    {
        if (id == null)
        {
            return NotFound();
        }

        if (query != null)
        {
            SearchQuery = query;
        }

        var exerciseData = await ExerciseApiService.GetExerciseById(id);

        if (exerciseData is not null)
        {
            ExerciseData = exerciseData;
            return Page();
        }

        return NotFound();
    }
}

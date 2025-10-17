using FitnessTracker.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Pages;

public class IndexModel : PageModel
{
    private readonly Data.ExerciseLogDbContext _context;
    public IList<LogEntryData> LogEntryData { get; set; } = [];
    public string? ErrorMessage { get; private set; } = null;

    public IndexModel(Data.ExerciseLogDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        try
        {
            LogEntryData = await _context.ExcerciseLogEntries.OrderByDescending(x => x.Date).ToListAsync();
        }
        catch
        {
            ErrorMessage = "ERROR: Could not load data from the database. Please try refreshing the page.";
        }
    }
}

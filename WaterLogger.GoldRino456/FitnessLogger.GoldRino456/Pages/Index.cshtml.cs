using FitnessTracker.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Pages;

public class IndexModel : PageModel
{
    private readonly Data.ExerciseLogDbContext _context;
    public IList<LogEntryData> LogEntryData { get; set; } = default!;

    public IndexModel(Data.ExerciseLogDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        LogEntryData = await _context.ExcerciseLogEntries.OrderByDescending(x => x.Date).ToListAsync();
    }
}

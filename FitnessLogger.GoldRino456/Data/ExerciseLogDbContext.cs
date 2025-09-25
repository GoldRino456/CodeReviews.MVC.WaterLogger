using FitnessTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data;

public class ExerciseLogDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<LogEntryData> ExcerciseLogEntries { get; set; }
}

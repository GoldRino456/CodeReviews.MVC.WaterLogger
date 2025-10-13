using System.ComponentModel;

namespace FitnessTracker.Data.Models;

public class ExerciseData
{
    public string exerciseId { get; set; }

    [DisplayName("Exercise Name")]
    public string name { get; set; }

    [DisplayName("Demonstration")]
    public string gifUrl { get; set; }

    [DisplayName("Target Muscles")]
    public List<string> targetMuscles { get; set; }

    [DisplayName("Body Parts")]
    public List<string> bodyParts { get; set; }

    [DisplayName("Equipment Used")]
    public List<string> equipments { get; set; }

    [DisplayName("Secondary Muscles")]
    public List<string> secondaryMuscles { get; set; }

    [DisplayName("Instructions")]
    public List<string> instructions { get; set; }
}

public class Metadata
{
    public int totalExercises { get; set; }
    public int totalPages { get; set; }
    public int currentPage { get; set; }
    public string previousPage { get; set; }
    public string nextPage { get; set; }
}

public class multiExerciseJsonResponse
{
    public bool success { get; set; }
    public Metadata metadata { get; set; }
    public List<ExerciseData> data {  get; set; }
}

public class singleExerciseJsonResponse
{
    public bool success { get; set; }
    public ExerciseData data { get; set; }
}

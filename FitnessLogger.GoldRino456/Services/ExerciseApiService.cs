using FitnessTracker.Data.Models;
using System.Net.Http.Headers;

namespace FitnessTracker.Services;

public static class ExerciseApiService
{
    private static readonly HttpClient _client = new();

    static ExerciseApiService()
    {
        _client.BaseAddress = new("https://www.exercisedb.dev/api/v1/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<List<ExerciseData>?> GetFuzzySearchResults(string query)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"exercises/search?q={query}");
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadFromJsonAsync<multiExerciseJsonResponse>().Result;
            return jsonResult.data;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to retrieve any exercise data from source. \nError Message: {e.Message}");
            return null;
        }
    }

    public static async Task<ExerciseData?> GetExerciseById(string id)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"exercises/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadFromJsonAsync<singleExerciseJsonResponse>().Result;
            return jsonResult.data;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to retrieve specified exercise data from source. \nError Message: {e.Message}");
            return null;
        }
    }
}

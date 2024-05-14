using OnlineJudgeBackend.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OnlineJudgeBackend.Services;

public interface ISubmission
{
    Task<string> SubmitCodeAsync(SubmitRequest data);
    Task<string> GetResultAsync(string id);
}

public class Submission(IConfiguration configuration, HttpClient client) : ISubmission
{
    public async Task<string> SubmitCodeAsync(SubmitRequest data)
    {
        const string endpoint = "https://judge0-ce.p.rapidapi.com/submissions?base64_encoded=true&fields=*";
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint));

        request.Headers.Add("X-RapidAPI-Key", configuration["RapidApiKey"]);
        request.Headers.Add("X-RapidAPI-Host", "judge0-ce.p.rapidapi.com");

        var payload = new Dictionary<string, dynamic> {
            { "language_id", data.Id },
            { "source_code", data.Code },
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
        request.Content = content;

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetResultAsync(string id)
    {
        var endpoint = $"https://judge0-ce.p.rapidapi.com/submissions/{id}?base64_encoded=true&fields=*";
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(endpoint));

        request.Headers.Add("X-RapidAPI-Key", configuration["RapidApiKey"]);
        request.Headers.Add("X-RapidAPI-Host", "judge0-ce.p.rapidapi.com");

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}

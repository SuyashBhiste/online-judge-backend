using Microsoft.AspNetCore.Mvc;
using OnlineJudgeBackend.Models;
using OnlineJudgeBackend.Services;

namespace OnlineJudgeBackend.Controllers;

public static class SubmissionController
{
    public static IServiceCollection RegisterSubmissionController(this IServiceCollection services)
    {
        services.AddSingleton<ISubmission, Submission>();
        services.AddHttpClient<Submission>();
        return services;
    }

    public static IEndpointRouteBuilder MapSubmissionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/submit", 
            async ([FromBody] SubmitRequest request, ISubmission submission) => 
            await submission.SubmitCodeAsync(request));

        endpoints.MapGet("/result", 
            async (string id, ISubmission submission) =>
                await submission.GetResultAsync(id));

        return endpoints;
    }
}
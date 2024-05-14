using System.Text.Json.Serialization;
using OnlineJudgeBackend.Helper;

namespace OnlineJudgeBackend.Models;

public record SubmitRequest(string Code)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SupportedLanguage Language { get; set; }

    public int Id => 92;
}

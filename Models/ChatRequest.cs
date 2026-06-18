using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SemanticKernelGeminiDemo.Models;

public class ChatRequest
{
    public string SessionId { get; set; } = "default";
    public string Message { get; set; } = string.Empty;
}
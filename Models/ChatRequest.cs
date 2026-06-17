using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SemanticKernelGeminiDemo.Models;

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
}
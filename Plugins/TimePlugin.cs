using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelGeminiDemo.Plugins;


public class TimePlugin
{
    [KernelFunction]
    [Description("Vraca trenutno vreme na serveru")]    
    public string GetCurrentTime()
    {
        return DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
    }
}
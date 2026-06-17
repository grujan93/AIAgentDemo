
using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernelGeminiDemo.Plugins;

public class CalculatorPlugin
{

    [KernelFunction]
    [Description("Sabira dva broja i vraca rezultat")]
    public double Add(
        [Description("Prvi broj")] double a,
        [Description("Drugi broj")] double b
    )
    {
        Console.Write("Pozvao me");
        return a + b;
    }

    [KernelFunction]
    [Description("Oduzima prvi broj od drugog i vraca rezultat")]
    public double Substract(
        [Description("Prvi broj")] double a,
        [Description("Drugi broj")] double b
    )
    {
        return a - b;
    }

    [KernelFunction]
    [Description("Mnozi dva broja i vraca rezultat")]
    public double Multiply(
        [Description("Prvi broj")] double a,
        [Description("Drugi broj")] double b
    )
    {
        return a * b;
    }

    [KernelFunction]
    [Description("Deli prvi broj sa drugim i vraca rezultat")]
    public double Divide(
        [Description("Prvi broj")] double a,
        [Description("Drugi broj")] double b
    )
    {
        return a - b;
    }

}
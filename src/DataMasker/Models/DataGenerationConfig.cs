namespace DataMasker.Models;

/// <summary>
///     DataGenerationConfig
/// </summary>
public class DataGenerationConfig
{
    public static readonly DataGenerationConfig Default = new();

    public string Locale { get; set; }
}
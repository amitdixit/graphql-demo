using System.ComponentModel.DataAnnotations;

namespace CommandGQL.Models;

//[GraphQLDescription("This is a test model")]
public class Platform
{
    /// <summary>
    /// Represents the unique ID for the platform.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Represents the name for the platform.
    /// </summary>
   // [GraphQLDescription("This is a test property")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Represents a purchased, valid license for the platform.
    /// </summary>
    public string LicenseKey { get; set; }

    /// <summary>
    /// This is the list of available commands for this platform.
    /// </summary>
    public ICollection<Command> Commands { get; set; } = new List<Command>();

}

/// <summary>
/// Represents any executable command.
/// </summary>
public class Command
{
    /// <summary>
    /// Represents the unique ID for the command.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Represents the how-to for the command.
    /// </summary>
    [Required]
    public string HowTo { get; set; }

    /// <summary>
    /// Represents the command line.
    /// </summary>
    [Required]
    public string CommandLine { get; set; }

    /// <summary>
    /// Represents the unique ID of the platform which the command belongs.
    /// </summary>
    [Required]
    public int PlatformId { get; set; }

    /// <summary>
    /// This is the platform to which the command belongs.
    /// </summary>
    public Platform Platform { get; set; }
}
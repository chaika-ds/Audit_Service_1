namespace KIT.Minio.Commands.SaveFileWithSharing.Models;

/// <summary>
///     Model to save file with sharing
/// </summary>
/// <param name="Stream">File stream</param>
/// <param name="FileName">File name(File name must be unique)</param>
/// <param name="ContentType">File content type</param>
/// <param name="ExpirationInSeconds">Shared file expiration in seconds(optional parameter)</param>
public record SaveFileWithSharingModel(Stream Stream, string FileName, string ContentType, int? ExpirationInSeconds = null);
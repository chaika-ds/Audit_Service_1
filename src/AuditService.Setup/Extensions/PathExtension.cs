namespace AuditService.Setup.Extensions;

public static class PathExtension
{
    public static string GetPathByApplicationLayer(this string path, string applicationLayer)
    {
        return Directory.GetParent(Environment.CurrentDirectory) + $"/{applicationLayer}/" + path;
    }
}
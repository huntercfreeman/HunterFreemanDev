namespace HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

public interface IFileSystemDrive
{
    public string DriveNameAsIdentifier { get; }
    public string DriveNameAsPath { get; }
}
﻿namespace HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

public interface IAbsoluteFilePath : IFilePath
{
    /// <summary>
    /// Only non null when specifying a drive to mount
    ///
    /// Example: "C:\\" gets stored as "C"
    ///
    /// To get the guaranteed non null root directory
    /// use GetRootDirectory
    /// </summary>
    public IFileSystemDrive? RootDrive { get; }
    /// <summary>
    /// Returns either System.IO.Path.DirectorySeparatorChar
    ///
    /// OR
    ///
    /// Returns $"{RootDrive}:{System.IO.Path.DirectorySeparatorChar}"
    /// </summary>
    public string GetRootDirectory { get; }
    public string GetAbsoluteFilePathString();
}
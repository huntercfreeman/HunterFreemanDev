using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.FolderExplorer;

public partial class FolderExplorerDisplay : ComponentBase
{
    

    protected override void OnInitialized()
    {
        var rootDirectoryAbsoluteFilePath = new AbsoluteFilePath(System.IO.Path.DirectorySeparatorChar.ToString(), true);

        var rootDirectoryDirectoryFiles =
            System.IO.Directory.GetDirectories(rootDirectoryAbsoluteFilePath.GetAbsoluteFilePathString());

        var rootDirectoryFileFiles =
            System.IO.Directory.GetFiles(rootDirectoryAbsoluteFilePath.GetAbsoluteFilePathString());

        base.OnInitialized();
    }
}
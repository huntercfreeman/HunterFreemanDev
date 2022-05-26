using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.InputFile;

public partial class InputFileDialogDisplay : ComponentBase
{
    private DirectoryFileTreeViewRecord _rootDirectoryFileTreeViewRecord  =
            new DirectoryFileTreeViewRecord(
                new AbsoluteFilePath(System.IO.Path.DirectorySeparatorChar.ToString(), true));
}
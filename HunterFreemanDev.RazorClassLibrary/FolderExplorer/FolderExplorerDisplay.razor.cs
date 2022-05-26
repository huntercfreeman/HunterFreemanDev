using HunterFreemanDev.ClassLibrary.Errors;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.FolderExplorer;

public partial class FolderExplorerDisplay : ComponentBase
{
    private IAbsoluteFilePath? _workspaceAbsoluteFilePath;
    private RichErrorModel? _onFileSelectedRichErrorModel;

    protected override void OnInitialized()
    {
        var rootDirectoryAbsoluteFilePath = new AbsoluteFilePath(System.IO.Path.DirectorySeparatorChar.ToString(), true);

        var rootDirectoryDirectoryFiles =
            System.IO.Directory.GetDirectories(rootDirectoryAbsoluteFilePath.GetAbsoluteFilePathString());

        var rootDirectoryFileFiles =
            System.IO.Directory.GetFiles(rootDirectoryAbsoluteFilePath.GetAbsoluteFilePathString());

        base.OnInitialized();
    }

    private void OnFileSelected(IAbsoluteFilePath absoluteFilePath)
    {
        if (absoluteFilePath.IsDirectory)
        {
            _workspaceAbsoluteFilePath = absoluteFilePath;
        }
        else
        {
            _onFileSelectedRichErrorModel = new RichErrorModel($"{nameof(absoluteFilePath)} must be a directory.",
                "Reopen the input file dialog and select a directory.");
        }

        InvokeAsync(StateHasChanged);
    }
}
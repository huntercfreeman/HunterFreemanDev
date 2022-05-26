using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.InputFile;

public partial class InputFileDialogDisplay : ComponentBase
{
    private DirectoryFileTreeViewRecord _rootDirectoryFileTreeViewRecord  =
            new DirectoryFileTreeViewRecord(
                new AbsoluteFilePath(System.IO.Path.DirectorySeparatorChar.ToString(), true));

    private TreeViewRecordBase<IAbsoluteFilePath> _activeTreeViewRecord;

    protected override void OnInitialized()
    {
        _activeTreeViewRecord = _rootDirectoryFileTreeViewRecord;

        base.OnInitialized();
    }

    private void SetActiveTreeViewRecordAction(TreeViewRecordBase<IAbsoluteFilePath> treeViewRecord)
    {
        _activeTreeViewRecord = treeViewRecord;

        InvokeAsync(StateHasChanged);
    }
}
using Fluxor;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.InputFile;

public partial class InputFileDialogDisplay : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    
    [Parameter, EditorRequired]
    public Action<IAbsoluteFilePath> OnFileSelectedAction { get; set; } = null!;
    [Parameter, EditorRequired]
    public Guid InputFileDialogRecordId { get; set; }

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

    private void ConfirmWorkspaceDirectoryOnClick()
    {
        OnFileSelectedAction.Invoke(_activeTreeViewRecord.Data);

        var action = new UnregisterDialogAction(InputFileDialogRecordId);

        Dispatcher.Dispatch(action);
    }
}
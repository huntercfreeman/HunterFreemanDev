using Fluxor;
using HunterFreemanDev.ClassLibrary.Errors;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.Grid;
using HunterFreemanDev.ClassLibrary.Store.FileBuffer;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using HunterFreemanDev.ClassLibrary.TreeView;
using HunterFreemanDev.RazorClassLibrary.Html;
using HunterFreemanDev.RazorClassLibrary.PlainTextEditor;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.FolderExplorer;

public partial class FolderExplorerDisplay : ComponentBase
{
    [Inject]
    private FileSystemAccessSettings FileSystemAccessSettings { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public GridItemRecordKey GridItemRecordKey { get; set; } = null!;

    private IAbsoluteFilePath? _workspaceAbsoluteFilePath;
    private RichErrorModel? _onFileSelectedRichErrorModel;
    private TreeViewRecordBase<IAbsoluteFilePath> _activeTreeViewRecord;
    private DirectoryFileTreeViewRecord _workspaceTreeView;

    private void OnFileSelected(IAbsoluteFilePath absoluteFilePath)
    {
        if (absoluteFilePath.IsDirectory)
        {
            _workspaceAbsoluteFilePath = absoluteFilePath;

            _workspaceTreeView = new DirectoryFileTreeViewRecord(_workspaceAbsoluteFilePath);

            _activeTreeViewRecord = _workspaceTreeView;
        }
        else
        {
            _onFileSelectedRichErrorModel = new RichErrorModel($"{nameof(absoluteFilePath)} must be a directory.",
                "Reopen the input file dialog and select a directory.");
        }

        InvokeAsync(StateHasChanged);
    }

    private void SetActiveTreeViewRecordAction(TreeViewRecordBase<IAbsoluteFilePath> treeViewRecord)
    {
        _activeTreeViewRecord = treeViewRecord;

        InvokeAsync(StateHasChanged);
    }

    private void DefaultFileOnDoubleClick(TreeViewRecordBase<IAbsoluteFilePath> treeViewRecord)
    {
        var fileDescriptorRecordKey = new FileDescriptorRecordKey(Guid.NewGuid());

        var parametersForPlainTextEditorDisplay = new Dictionary<string, object>
        {
            { "FileDescriptorRecordKey", fileDescriptorRecordKey }
        };

        var addGridTabRecordAction = new AddGridTabRecordAction(GridItemRecordKey,
            new GridTabRecord(new GridTabRecordKey(Guid.NewGuid()),
                              true,
                              typeof(PlainTextEditorDisplay),
                              nameof(PlainTextEditorDisplay),
                              parametersForPlainTextEditorDisplay),
            0);

        Dispatcher.Dispatch(addGridTabRecordAction);

        var registerFileDescriptorRecord = new RegisterFileDescriptorRecordAction(fileDescriptorRecordKey, 
            treeViewRecord.Data);

        Dispatcher.Dispatch(registerFileDescriptorRecord);
    }
}
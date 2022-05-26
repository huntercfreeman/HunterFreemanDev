using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class DirectoryFileTreeViewDisplay : ComponentBase
{
    [CascadingParameter(Name="SetActiveTreeViewRecordAction")]
    public Action<TreeViewRecordBase<IAbsoluteFilePath>> SetActiveTreeViewRecordAction { get; set; } = null!;
    [CascadingParameter(Name="ActiveTreeViewRecord")]
    public TreeViewRecordBase<IAbsoluteFilePath> ActiveTreeViewRecord { get; set; } = null!;

    [Parameter, EditorRequired]
    public DirectoryFileTreeViewRecord DirectoryFileTreeViewRecord { get; set; } = null!;

    private string IsActiveTreeViewRecordCss => ActiveTreeViewRecord.Data.GetAbsoluteFilePathString() ==
                                                DirectoryFileTreeViewRecord.Data.GetAbsoluteFilePathString()
                                              ? "hfd_active"
                                              : string.Empty;

    private void OnToggleIsExpandedEventCallback()
    {
        DirectoryFileTreeViewRecord.IsExpanded = !DirectoryFileTreeViewRecord.IsExpanded;

        if(!DirectoryFileTreeViewRecord.GetChildTreeViewRecords.Any())
            DirectoryFileTreeViewRecord.LoadChildTreeViewRecords();
    }
}
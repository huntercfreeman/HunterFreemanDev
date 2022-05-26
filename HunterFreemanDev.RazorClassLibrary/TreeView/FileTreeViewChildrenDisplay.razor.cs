using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.Errors;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class FileTreeViewChildrenDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public ImmutableArray<TreeViewRecordBase<IAbsoluteFilePath>> ChildFileTreeViewRecordBases { get; set; }
    [Parameter, EditorRequired]
    public RichErrorModel? RichErrorModel { get; set; }
}
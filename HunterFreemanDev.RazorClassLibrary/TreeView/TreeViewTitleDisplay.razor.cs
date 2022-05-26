using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using HunterFreemanDev.ClassLibrary.TreeView;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class TreeViewTitleDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public string Title { get; set; } = null!;
    [Parameter, EditorRequired]
    public Action SetActiveTreeViewRecordAction { get; set; } = null!;
    [Parameter, EditorRequired]
    public string Classes { get; set; } = null!;
    [Parameter]
    public Action? DefaultFileOnDoubleClick { get; set; } = null!;

    private void FireDefaultFileOnDoubleClick()
    {
        if(DefaultFileOnDoubleClick is not null)
            DefaultFileOnDoubleClick.Invoke();
    }
}
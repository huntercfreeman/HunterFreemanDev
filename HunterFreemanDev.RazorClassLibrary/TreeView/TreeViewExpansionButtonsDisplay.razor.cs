using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.TreeView;

public partial class TreeViewExpansionButtonsDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public bool IsExpanded { get; set; }
    [Parameter, EditorRequired]
    public EventCallback OnToggleIsExpandedEventCallback { get; set; }
        
    private void FireOnToggleIsExpandedEventCallback()
    {
        if (OnToggleIsExpandedEventCallback.HasDelegate)
            OnToggleIsExpandedEventCallback.InvokeAsync();
    }
}
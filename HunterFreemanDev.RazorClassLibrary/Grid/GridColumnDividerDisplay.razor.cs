using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridColumnDividerDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public GridRecord LeftElementRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public GridRecord RightElementRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(GridRecord LeftElementRecord, GridRecord RightElementRecord)> OnMouseDownEventCallback { get; set; }

    private void FireOnMouseDownEventCallback()
    {
        if (OnMouseDownEventCallback.HasDelegate)
            OnMouseDownEventCallback.InvokeAsync((LeftElementRecord, RightElementRecord));
    }
}
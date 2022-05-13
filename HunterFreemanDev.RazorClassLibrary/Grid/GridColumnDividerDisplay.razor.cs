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
    public ElementRecord LeftElementRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public ElementRecord RightElementRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public EventCallback<(ElementRecord LeftElementRecord, ElementRecord RightElementRecord)> OnMouseDownEventCallback { get; set; }

    private void FireOnMouseDownEventCallback()
    {
        if (OnMouseDownEventCallback.HasDelegate)
            OnMouseDownEventCallback.InvokeAsync((LeftElementRecord, RightElementRecord));
    }
}
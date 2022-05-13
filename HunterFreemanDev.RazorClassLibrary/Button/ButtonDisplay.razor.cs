using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HunterFreemanDev.RazorClassLibrary.Button;

public partial class ButtonDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;
    [Parameter]
    public EventCallback OnClickEventCallback { get; set; }
    [Parameter]
    public string? Title { get; set; }
    [Parameter]
    public string? Class { get; set; }

    private void FireOnClickEventCallback()
    {
        if (OnClickEventCallback.HasDelegate)
            OnClickEventCallback.InvokeAsync();
    }
}
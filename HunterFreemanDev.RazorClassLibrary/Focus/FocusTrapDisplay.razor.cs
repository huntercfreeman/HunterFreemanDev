using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

namespace HunterFreemanDev.RazorClassLibrary.Focus;

public partial class FocusTrapDisplay : ComponentBase
{
    private bool _initializedFocusTrap;
    private ElementReference _focusTrap;

    public ElementReference GetFocusTrapElementReference() => _focusTrap;

    [Parameter]
    public Func<Task> FocusOut { get; set; } = null!;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _initializedFocusTrap = true;
        }

        base.OnAfterRender(firstRender);
    }

    protected override bool ShouldRender() => !_initializedFocusTrap;
}
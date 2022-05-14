using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using Microsoft.JSInterop;
using HunterFreemanDev.ClassLibrary.KeyDown;
using HunterFreemanDev.ClassLibrary.Store.KeyDown;

namespace HunterFreemanDev.RazorClassLibrary.KeyDownEvent;

public partial class KeyDownEventProviderDisplay : ComponentBase
{
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            JsRuntime.InvokeVoidAsync("hunterFreemanDevRazorClassLibrary.initializeOnKeyDownEventProvider",
                DotNetObjectReference.Create(this));
        }

        return base.OnAfterRenderAsync(firstRender);
    }

    [JSInvokable]
    public void DispatchOnKeyDownEventAction(KeyDownEventRecord onKeyDownEventRecord)
    {
        var action = new KeyDownEventAction(onKeyDownEventRecord);

        Dispatcher.Dispatch(action);
    }
}
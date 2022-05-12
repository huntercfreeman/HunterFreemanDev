using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.Counter;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HunterFreemanDev.RazorClassLibrary.Counter;

public partial class CounterDisplay : FluxorComponent
{
    [Inject]
    private IState<CounterState> CounterState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    private void DispatchIncrementCounterAction()
    {
        var action = new IncrementCounterAction();

        Dispatcher.Dispatch(action);
    }
}
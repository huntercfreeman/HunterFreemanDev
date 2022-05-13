using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor.Blazor.Web.Components;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Theme;

namespace HunterFreemanDev.RazorClassLibrary.Theme;

public partial class ThemeSelectDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemesProvider> ThemesProvider { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private void OnSelectChangedEvent(ChangeEventArgs changeEventArgs)
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Theme;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Fluxor.Blazor.Web.Components;

namespace HunterFreemanDev.RazorClassLibrary.Theme;

public partial class ThemeDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public ThemeRecord ThemeRecord { get; set; } = null!;

    private string IsActiveThemeStateCssClass => ThemeState.Value.ThemeRecord.ThemeId == ThemeRecord.ThemeId
        ? "hfd_active"
        : string.Empty;

    private void SetThemeStateOnClick()
    {
        var action = new SetThemeStateAction(ThemeRecord);

        Dispatcher.Dispatch(action);
    }
}
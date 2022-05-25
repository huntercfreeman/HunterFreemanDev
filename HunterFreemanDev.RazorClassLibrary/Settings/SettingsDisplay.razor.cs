using Microsoft.AspNetCore.Components;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

namespace HunterFreemanDev.RazorClassLibrary.Settings;

public partial class SettingsDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IState<ThemesProvider> ThemesProvider { get; set; } = null!;
    [Inject]
    private IState<DebugCssClassesState> DebugCssClassesState { get; set; } = null!;
}
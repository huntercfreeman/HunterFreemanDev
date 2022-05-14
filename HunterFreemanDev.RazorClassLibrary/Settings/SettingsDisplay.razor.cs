using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace HunterFreemanDev.RazorClassLibrary.Settings;

public partial class SettingsDisplay : FluxorComponent
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
    [Inject]
    private IState<ThemesProvider> ThemesProvider { get; set; } = null!;


}
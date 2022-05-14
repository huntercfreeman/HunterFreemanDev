using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Store.Theme;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary.Layout;

public partial class DocumentLayoutDisplay : FluxorLayout
{
    [Inject]
    private IState<ThemeState> ThemeState { get; set; } = null!;
}

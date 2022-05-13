using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using Fluxor;
using Fluxor.Blazor.Web.Components;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogTaskBarDisplay : FluxorComponent
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
}
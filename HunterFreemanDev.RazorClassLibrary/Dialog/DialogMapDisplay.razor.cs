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
using HunterFreemanDev.ClassLibrary.Store.Dialog;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogMapDisplay : FluxorComponent
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
}
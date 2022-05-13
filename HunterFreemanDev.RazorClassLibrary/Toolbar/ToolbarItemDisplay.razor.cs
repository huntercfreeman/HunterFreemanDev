using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Toolbar;

namespace HunterFreemanDev.RazorClassLibrary.Toolbar;

public partial class ToolbarItemDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public IToolbarRecordUntyped ToolbarRecordUntyped { get; set; } = null!;
}
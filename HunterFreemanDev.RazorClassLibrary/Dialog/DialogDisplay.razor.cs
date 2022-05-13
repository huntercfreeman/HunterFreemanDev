using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Dialog;

namespace HunterFreemanDev.RazorClassLibrary.Dialog;

public partial class DialogDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public DialogRecord DialogRecord { get; set; } = null!;
}
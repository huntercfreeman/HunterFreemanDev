using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class StartOfRowTextSyntaxRecordDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public StartOfRowTextSyntaxRecord StartOfRowTextSyntaxRecord { get; set; } = null!;
}
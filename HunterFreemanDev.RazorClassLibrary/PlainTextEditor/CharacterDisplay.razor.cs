using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

namespace HunterFreemanDev.RazorClassLibrary.PlainTextEditor;

public partial class CharacterDisplay : ComponentBase
{
    /// <summary>
    /// Some Characters look like '&nbsp;' as they're HTML escaped
    /// and require a string to hold them.
    /// </summary>
    [Parameter, EditorRequired]
    public string HtmlCharacter { get; set; } = null!;
    [Parameter, EditorRequired]
    public bool DisplayCursor { get; set; }
}
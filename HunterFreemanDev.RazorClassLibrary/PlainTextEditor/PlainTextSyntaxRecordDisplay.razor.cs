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

public partial class PlainTextSyntaxRecordDisplay : ComponentBase
{
    [Parameter, EditorRequired]
    public PlainTextSyntaxRecord PlainTextSyntaxRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public Guid CurrentTextSyntaxRecordId { get; set; }

    private bool ShouldDisplayCursor(int characterIndex)
    {
        return CurrentTextSyntaxRecordId == PlainTextSyntaxRecord.TextSyntaxRecordId &&
            PlainTextSyntaxRecord.IndexInContent == characterIndex;

    }
}
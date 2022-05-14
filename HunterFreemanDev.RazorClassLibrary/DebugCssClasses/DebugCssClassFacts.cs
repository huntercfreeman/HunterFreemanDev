using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.DebugCssClasses;

public static class DebugCssClassFacts
{
    public static readonly DebugCssClassRecord GlobalDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-global", false);
    public static readonly DebugCssClassRecord PlainTextEditorDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-plain-text-editor", false);
    public static readonly DebugCssClassRecord ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-start-of-row-text-syntax-record-displays", true);
    public static readonly DebugCssClassRecord ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-plain-text-syntax-record-displays", true);
    public static readonly DebugCssClassRecord ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-whitespace-text-syntax-record-displays", true);

    public static readonly ImmutableArray<DebugCssClassRecord> AllDebugCssClassRecords = new DebugCssClassRecord[]
    {
        GlobalDebugCssClassRecord,
        PlainTextEditorDebugCssClassRecord,
        ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord,
        ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord,
        ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord
    }.ToImmutableArray();
}

using System.Collections.Immutable;

namespace HunterFreemanDev.ClassLibrary.DebugCssClasses;

public static class DebugCssClassInitialStates
{
    public static readonly DebugCssClassRecord GlobalDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-global", "Global Debug Css", false);
    public static readonly DebugCssClassRecord PlainTextEditorDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-plain-text-editor", "Plain Text Editor General Debug Css", false);
    public static readonly DebugCssClassRecord ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord 
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-start-of-row-text-syntax-record-displays", "Plain Text Editor Color Start of Rows", true);
    public static readonly DebugCssClassRecord ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-plain-text-syntax-record-displays", "Plain Text Editor Color Plain Text", true);
    public static readonly DebugCssClassRecord ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-colored-whitespace-text-syntax-record-displays", "Plain Text Editor Color Whitespace", true);

    public static readonly ImmutableArray<DebugCssClassRecord> AllDebugCssClassRecords = new DebugCssClassRecord[]
    {
        GlobalDebugCssClassRecord,
        PlainTextEditorDebugCssClassRecord,
        ColoredStartOfRowTextSyntaxRecordDisplaysDebugCssClassRecord,
        ColoredPlainTextSyntaxRecordDisplaysDebugCssClassRecord,
        ColoredWhitespaceTextSyntaxRecordDisplaysDebugCssClassRecord
    }.ToImmutableArray();
}

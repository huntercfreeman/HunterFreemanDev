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
        = new DebugCssClassRecord(Guid.NewGuid(), "hfd_debug-plain-text-editor", true);

    public static readonly ImmutableArray<DebugCssClassRecord> AllDebugCssClassRecords = new DebugCssClassRecord[]
    {
        GlobalDebugCssClassRecord,
        PlainTextEditorDebugCssClassRecord
    }.ToImmutableArray();
}

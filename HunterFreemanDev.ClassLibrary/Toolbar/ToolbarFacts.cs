using HunterFreemanDev.ClassLibrary.Dropdown;
using HunterFreemanDev.ClassLibrary.IconButton;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Toolbar;

public static class ToolbarFacts
{
    public static readonly ToolbarRecord<IconButtonRecord> SettingsDialogEntryPoint = 
        new ToolbarRecord<IconButtonRecord>(Guid.NewGuid(),
            new IconButtonRecord(Guid.NewGuid(), Icon.IconKind.Settings),
            nameof(IconButtonRecord));

    public static ImmutableArray<IToolbarRecordUntyped> AllToolbarRecordsUntyped = new IToolbarRecordUntyped[]
    {
        SettingsDialogEntryPoint
    }.ToImmutableArray();
}

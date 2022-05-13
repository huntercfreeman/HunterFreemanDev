using Fluxor;
using HunterFreemanDev.ClassLibrary.Dropdown;
using HunterFreemanDev.ClassLibrary.Toolbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Toolbar;

[FeatureState]
public record ToolbarState(Dictionary<Guid, ToolbarRecord> MainRowToolbarRecordMap,
    Dictionary<Guid, ToolbarRecord> SecondaryRowToolbarRecordMap)
{
    public ToolbarState() : this(new Dictionary<Guid, ToolbarRecord>(), new Dictionary<Guid, ToolbarRecord>())
    {

    }
}

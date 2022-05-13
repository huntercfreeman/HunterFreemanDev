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
public record ToolbarState(Dictionary<Guid, IToolbarRecordUntyped> MainRowToolbarRecordMap,
    Dictionary<Guid, IToolbarRecordUntyped> SecondaryRowToolbarRecordMap)
{
    public ToolbarState() : this(new Dictionary<Guid, IToolbarRecordUntyped>(), new Dictionary<Guid, IToolbarRecordUntyped>())
    {

    }
}

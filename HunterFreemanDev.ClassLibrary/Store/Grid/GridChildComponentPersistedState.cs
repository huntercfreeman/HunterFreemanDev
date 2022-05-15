using HunterFreemanDev.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record GridChildComponentPersistedState(Dictionary<GridRecord, object?> GridChildComponentPersistedStateMap)
{
    public GridChildComponentPersistedState() : this(new Dictionary<GridRecord, object?>())
    {

    }
}

using Fluxor;
using HunterFreemanDev.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public class GridReducers
{
    [ReducerMethod]
    public static GridState ReduceAddGridStateAction(GridState previousGridState,
        AddGridStateAction addGridStateAction)
    {
        var nextRowsList = new List<List<Element.ElementRecord>>();

        foreach(var row in previousGridState.ElementRecords)
        {
            nextRowsList.Add(new List<ElementRecord>(row));
        }

        switch (addGridStateAction.ArgumentTuple.CardinalDirectionKind)
        {
            case Direction.CardinalDirectionKind.North:
                if (addGridStateAction.ArgumentTuple.GridRowIndex == 0)
                    nextRowsList.Insert(0, new());

                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.ElementRecord);
                break;
            case Direction.CardinalDirectionKind.East:
                break;
            case Direction.CardinalDirectionKind.South:
                break;
            case Direction.CardinalDirectionKind.West:
                break;
        }

        return new GridState(nextRowsList);
    }
}

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
                nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex, new());

                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.ElementRecord);
                break;
            case Direction.CardinalDirectionKind.East:
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.ElementRecord);
                break;
            case Direction.CardinalDirectionKind.South:
                nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex + 1, new());
                
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex + 1].Add(addGridStateAction.ElementRecord);
                break;
            case Direction.CardinalDirectionKind.West:
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex]
                    .Insert(addGridStateAction.ArgumentTuple.GridColumnIndex, 
                        addGridStateAction.ElementRecord);
                break;
        }

        return new GridState(nextRowsList);
    }
}

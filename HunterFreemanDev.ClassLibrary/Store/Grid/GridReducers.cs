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
        AddGridRecordAction addGridStateAction)
    {
        // Replace GridRecords Lists
        var nextRowsList = new List<List<GridRecord>>();

        foreach(var row in previousGridState.GridRecords)
        {
            nextRowsList.Add(new List<GridRecord>(row));
        }

        switch (addGridStateAction.ArgumentTuple.CardinalDirectionKind)
        {
            case Direction.CardinalDirectionKind.North:
                nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex, new());

                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.GridRecord);
                break;
            case Direction.CardinalDirectionKind.East:
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex].Add(addGridStateAction.GridRecord);
                break;
            case Direction.CardinalDirectionKind.South:
                nextRowsList.Insert(addGridStateAction.ArgumentTuple.GridRowIndex + 1, new());
                
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex + 1].Add(addGridStateAction.GridRecord);
                break;
            case Direction.CardinalDirectionKind.West:
                nextRowsList[addGridStateAction.ArgumentTuple.GridRowIndex]
                    .Insert(addGridStateAction.ArgumentTuple.GridColumnIndex, 
                        addGridStateAction.GridRecord);
                break;
        }

        // Replace GridRecordMap
        var nextGridRecordMap = new Dictionary<Guid, object?>(previousGridState.GridRecordMap);

        nextGridRecordMap.Add(addGridStateAction.GridRecord.GridRecordId, addGridStateAction.GridRecordChildComponentState);

        return new GridState(nextGridRecordMap, nextRowsList);
    }
    
    [ReducerMethod]
    public static GridState ReduceReplaceGridRecordAction(GridState previousGridState,
        ReplaceGridRecordAction replaceGridRecordAction)
    {
        // Replace GridRecordMap
        var nextGridRecordMap = new Dictionary<Guid, object?>(previousGridState.GridRecordMap);

        nextGridRecordMap.Remove(replaceGridRecordAction.GridRecord.GridRecordId);

        nextGridRecordMap.Add(replaceGridRecordAction.GridRecord.GridRecordId, replaceGridRecordAction.GridRecordChildComponentState);

        return previousGridState with
        {
            GridRecordMap = nextGridRecordMap
        };
    }
}

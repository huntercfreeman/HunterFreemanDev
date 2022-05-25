using Fluxor;
using HunterFreemanDev.ClassLibrary.ConstructorAction;
using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

[FeatureState]
public record GridRecordsState
{
    private Dictionary<GridRecordKey, GridBoardRecord> _gridRecordItemContainerMap;

    public GridRecordsState()
    {
        _gridRecordItemContainerMap = new();
    }
    
    public GridRecordsState(GridRecordsState otherGridRecordsState,
        GridRecordKey gridRecordKey)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);

        _gridRecordItemContainerMap.Add(gridRecordKey, new());
    }
    
    public GridRecordsState(GridRecordsState otherGridRecordsState,
        GridRecordKey gridRecordKey,
        int rowIndex)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);
        
        var previousGridRecordItemContainerMap = _gridRecordItemContainerMap[gridRecordKey];

        _gridRecordItemContainerMap[gridRecordKey] = new GridBoardRecord(previousGridRecordItemContainerMap,
            rowIndex);
    }
    
    public GridRecordsState(GridRecordsState otherGridRecordsState,
        GridRecordKey gridRecordKey,
        int rowIndex,
        int gridItemIndex)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);
        
        var previousGridRecordItemContainerMap = _gridRecordItemContainerMap[gridRecordKey];

        _gridRecordItemContainerMap[gridRecordKey] = new GridBoardRecord(previousGridRecordItemContainerMap,
            rowIndex,
            gridItemIndex);
    }
    
    public GridRecordsState(GridRecordsState otherGridRecordsState,
        GridRecordKey gridRecordKey,
        GridItemRecord? gridItemRecord,
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);

        if (gridItemRecord is null)
            return;

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Add:
                PerformAdd(gridRecordKey,
                    gridItemRecord,
                    constructorActionKind,
                    cardinalDirectionKind,
                    rowIndexRelativeTo,
                    columnIndexRelativeTo);
                break;
            case ConstructorActionKind.Replace:
                PerformReplace(gridRecordKey,
                    gridItemRecord,
                    constructorActionKind,
                    cardinalDirectionKind,
                    rowIndexRelativeTo,
                    columnIndexRelativeTo);
                break;
        }

        var nextGridBoard = new GridBoardRecord(_gridRecordItemContainerMap[gridRecordKey],
            gridItemRecord,
            cardinalDirectionKind,
            rowIndexRelativeTo,
            columnIndexRelativeTo);

        _gridRecordItemContainerMap[gridRecordKey] = nextGridBoard;
    }

    public GridRecordsState(GridRecordsState otherGridRecordsState, 
        GridRecordKey gridRecordKey, 
        GridItemRecord gridItemRecord,
        CardinalDirectionKind cardinalDirectionKind,
        int? rowIndex,
        int? activeGridItemRecordIndex) 
    {
        _gridRecordItemContainerMap = new(otherGridRecordsState._gridRecordItemContainerMap);

        var previousGridBoard = _gridRecordItemContainerMap[gridRecordKey];

        var nextGridBoard = new GridBoardRecord(previousGridBoard, 
            gridItemRecord, 
            cardinalDirectionKind, 
            rowIndex,
            activeGridItemRecordIndex);

        _gridRecordItemContainerMap[gridRecordKey] = nextGridBoard;
    }

    private void PerformAdd(GridRecordKey gridRecordKey,
        GridItemRecord gridItemRecord,
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        var gridBoard = new GridBoardRecord();

        _gridRecordItemContainerMap.Add(gridRecordKey, gridBoard);
    }

    private void PerformReplace(GridRecordKey gridRecordKey,
        GridItemRecord gridItemRecord,
        ConstructorActionKind constructorActionKind,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        throw new NotImplementedException();
    }

    public GridBoardRecord LookupGridBoard(GridRecordKey gridRecordKey) =>
        _gridRecordItemContainerMap[gridRecordKey];
}

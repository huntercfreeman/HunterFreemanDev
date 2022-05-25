using Fluxor;
using HunterFreemanDev.ClassLibrary.ConstructorAction;
using HunterFreemanDev.ClassLibrary.Grid;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

[FeatureState]
public record GridItemRecordsState
{
    private Dictionary<GridItemRecordKey, GridTabContainerRecord> _gridTabContainerRecordMap;

    public GridItemRecordsState()
    {
        _gridTabContainerRecordMap = new();
    }

    public GridItemRecordsState(GridItemRecordsState otherGridItemRecordsState, GridItemRecordKey gridItemRecordKey)
    {
        _gridTabContainerRecordMap = new(otherGridItemRecordsState._gridTabContainerRecordMap);

        _gridTabContainerRecordMap.Add(gridItemRecordKey, new());
    }

    public GridItemRecordsState(GridItemRecordsState otherGridItemsState,
        GridItemRecordKey gridItemRecordKey,
        GridTabRecord gridTabRecord,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        _gridTabContainerRecordMap = new(otherGridItemsState._gridTabContainerRecordMap);

        if(!_gridTabContainerRecordMap.TryGetValue(gridItemRecordKey, out var previousGridTabContainerRecord))
        {
            previousGridTabContainerRecord = new();
            _gridTabContainerRecordMap.Add(gridItemRecordKey, previousGridTabContainerRecord);
        }

        var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
            gridTabRecord,
            tabToSetAsActive ?? previousGridTabContainerRecord.ActiveGridTabIndex + 1,
            constructorActionKind);

        _gridTabContainerRecordMap[gridItemRecordKey] = nextGridTabContainerRecord;
    }
    
    public GridItemRecordsState(GridItemRecordsState otherGridItemsState,
        GridItemRecordKey gridItemRecordKey,
        GridTabRecordKey gridTabRecordKey,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        _gridTabContainerRecordMap = new(otherGridItemsState._gridTabContainerRecordMap);

        if(_gridTabContainerRecordMap.TryGetValue(gridItemRecordKey, out var previousGridTabContainerRecord))
        {
            var nextGridTabContainerRecord = new GridTabContainerRecord(previousGridTabContainerRecord,
                gridTabRecordKey,
                tabToSetAsActive,
                constructorActionKind);

            _gridTabContainerRecordMap[gridItemRecordKey] = nextGridTabContainerRecord;
        }
    }

    public GridTabContainerRecord LookupGridTabContainer(GridItemRecordKey gridItemRecordKey) =>
        _gridTabContainerRecordMap[gridItemRecordKey];
}

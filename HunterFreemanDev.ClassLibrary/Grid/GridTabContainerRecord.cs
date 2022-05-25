using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.ConstructorAction;

namespace HunterFreemanDev.ClassLibrary.Grid;

public record GridTabContainerRecord
{
    private readonly Dictionary<GridTabRecordKey, GridTabRecord> _gridTabRecordMap;

    public GridTabContainerRecord()
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new();
    }

    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecord gridTabRecord,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new(previousGridTabContainerRecord._gridTabRecordMap);

        try
        {
            _gridTabRecordMap[gridTabRecord.GridTabRecordKey] = gridTabRecord;
        }
        catch (KeyNotFoundException)
        {
            _gridTabRecordMap.Add(gridTabRecord.GridTabRecordKey, gridTabRecord);
        }
        
        ActiveGridTabIndex = tabToSetAsActive ?? _gridTabRecordMap.Values.ToList().IndexOf(gridTabRecord);
    }
    
    public GridTabContainerRecord(GridTabContainerRecord previousGridTabContainerRecord, 
        GridTabRecordKey gridTabRecordKey,
        int? tabToSetAsActive,
        ConstructorActionKind constructorActionKind)
    {
        GridTabContainerRecordSequence = Guid.NewGuid();

        _gridTabRecordMap = new(previousGridTabContainerRecord._gridTabRecordMap);

        switch (constructorActionKind)
        {
            case ConstructorActionKind.Remove:
                _gridTabRecordMap.Remove(gridTabRecordKey);

                if (tabToSetAsActive is not null)
                {
                    ActiveGridTabIndex = tabToSetAsActive;
                }
                else
                {
                    ActiveGridTabIndex = previousGridTabContainerRecord.ActiveGridTabIndex;

                    if (ActiveGridTabIndex >= _gridTabRecordMap.Values.Count)
                    {
                        if (_gridTabRecordMap.Values.Any())
                            ActiveGridTabIndex = _gridTabRecordMap.Values.Count - 1;
                        else
                            ActiveGridTabIndex = null;
                    }
                }

                break;
            case ConstructorActionKind.Replace:
                ActiveGridTabIndex = tabToSetAsActive ?? previousGridTabContainerRecord.ActiveGridTabIndex;
                break;
        }
    }

    public int? ActiveGridTabIndex { get; }
    public Guid GridTabContainerRecordSequence { get; }
    public ImmutableArray<GridTabRecord> GridTabRecords => _gridTabRecordMap.Values
        .ToImmutableArray();
}

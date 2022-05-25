using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.Direction;

namespace HunterFreemanDev.ClassLibrary.Grid;

public record GridRowRecord
{
    private List<GridItemRecord> _gridItemRecords;

    public GridRowRecord()
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new();
    }

    public GridRowRecord(GridItemRecord gridItemRecord)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new();

        _gridItemRecords.Add(gridItemRecord);
    }
    
    public GridRowRecord(GridRowRecord previousGridRowRecord, GridItemRecord gridItemRecord)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new(previousGridRowRecord._gridItemRecords);

        _gridItemRecords.Add(gridItemRecord);
    }
    
    public GridRowRecord(GridRowRecord previousGridRowRecord, GridItemRecordKey gridItemRecordKey)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new(previousGridRowRecord._gridItemRecords);

        _gridItemRecords = _gridItemRecords
            .Where(x => x.GridItemRecordKey != gridItemRecordKey)
            .ToList();
    }
    
    public GridRowRecord(GridRowRecord previousGridRowRecord,
        int gridItemIndex)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new(previousGridRowRecord._gridItemRecords);

        _gridItemRecords.RemoveAt(gridItemIndex);
    }
    
    public GridRowRecord(GridRowRecord previousGridRowRecord, 
        GridItemRecord gridItemRecord,
        int indexOfInsertion)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new(previousGridRowRecord._gridItemRecords);

        _gridItemRecords.Insert(indexOfInsertion, gridItemRecord);
    }

    public GridRowRecord(GridRowRecord otherGridRowRecord, 
        GridItemRecord gridItemRecord,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        GridRowSequence = Guid.NewGuid();

        _gridItemRecords = new(otherGridRowRecord._gridItemRecords);
    }

    public ImmutableArray<GridItemRecord> GridItemRecords => _gridItemRecords.ToImmutableArray();

    public Guid GridRowSequence { get; }
}
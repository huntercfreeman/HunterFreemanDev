using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.Direction;

namespace HunterFreemanDev.ClassLibrary.Grid;

public record GridBoardRecord
{
    private readonly List<GridRowRecord> _gridRows;

    public GridBoardRecord()
    {
        GridBoardSequence = Guid.NewGuid();

        _gridRows = new();
    }

    public GridBoardRecord(GridBoardRecord previousGridBoardRecord, GridItemRecord gridItemRecord)
    {
        GridBoardSequence = Guid.NewGuid();

        _gridRows = new(previousGridBoardRecord._gridRows);

        if (!_gridRows.Any())
            _gridRows.Add(new(gridItemRecord));
    }
    
    public GridBoardRecord(GridBoardRecord previousGridBoardRecord,
        int rowIndex)
    {
        GridBoardSequence = Guid.NewGuid();

        _gridRows = new(previousGridBoardRecord._gridRows);

        _gridRows.RemoveAt(rowIndex);
    }
    
    public GridBoardRecord(GridBoardRecord previousGridBoardRecord,
        int rowIndex,
        int gridItemIndex)
    {
        GridBoardSequence = Guid.NewGuid();

        _gridRows = new(previousGridBoardRecord._gridRows);

        var previousRow = _gridRows[rowIndex];
            
        _gridRows[rowIndex] = new GridRowRecord(previousRow, gridItemIndex);
    }

    public GridBoardRecord(GridBoardRecord otherGridBoardRecord, 
        GridItemRecord gridItemRecord,
        CardinalDirectionKind? cardinalDirectionKind,
        int? rowIndexRelativeTo,
        int? columnIndexRelativeTo)
    {
        GridBoardSequence = Guid.NewGuid();

        _gridRows = new(otherGridBoardRecord._gridRows);

        // No GridItemRecords are on the board yet so cardinalDirectionKind is not relevant
        if (rowIndexRelativeTo is null && 
            columnIndexRelativeTo is null)
        {
            if (!_gridRows.Any())
                _gridRows.Add(new(gridItemRecord));
            else
                _gridRows[0] = new(gridItemRecord);
        }
        else
        {
            GridRowRecord? newlyConstructedGridRow = null;
            GridRowRecord? previousGridRowRecord = null;
            
            switch (cardinalDirectionKind)
            {
                case CardinalDirectionKind.North:
                    newlyConstructedGridRow = new GridRowRecord(gridItemRecord);
                    _gridRows.Insert(rowIndexRelativeTo ?? 0, newlyConstructedGridRow);
                    break;
                case CardinalDirectionKind.East:
                    previousGridRowRecord = _gridRows[rowIndexRelativeTo ?? 0];
                    
                    _gridRows[rowIndexRelativeTo ?? 0] = new GridRowRecord(previousGridRowRecord,
                        gridItemRecord,
                        (columnIndexRelativeTo ?? 0) + 1);
                    break;
                case CardinalDirectionKind.South:
                    newlyConstructedGridRow = new GridRowRecord(gridItemRecord);
                    _gridRows.Insert((rowIndexRelativeTo ?? 0) + 1, newlyConstructedGridRow);
                    break;
                case CardinalDirectionKind.West:
                    previousGridRowRecord = _gridRows[rowIndexRelativeTo ?? 0];
                    
                    _gridRows[rowIndexRelativeTo ?? 0] = new GridRowRecord(previousGridRowRecord,
                        gridItemRecord,
                        columnIndexRelativeTo ?? 0);
                    break;
            }
        }
    }

    public ImmutableArray<GridRowRecord> GridRowRecords => _gridRows.ToImmutableArray();

    public Guid GridBoardSequence { get; }
}

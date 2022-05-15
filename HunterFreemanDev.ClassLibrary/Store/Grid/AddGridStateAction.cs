using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record AddGridRecordAction((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) ArgumentTuple, 
    GridRecord GridRecord);

public record RegisterGridRecordAction((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) ArgumentTuple,
    GridRecord ElementRecord);

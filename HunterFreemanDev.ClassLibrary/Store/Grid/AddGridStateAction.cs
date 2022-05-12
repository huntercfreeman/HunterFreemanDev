using HunterFreemanDev.ClassLibrary.Direction;
using HunterFreemanDev.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

public record AddGridStateAction((CardinalDirectionKind CardinalDirectionKind, int GridColumnIndex, int GridRowIndex) ArgumentTuple, 
    ElementRecord ElementRecord);

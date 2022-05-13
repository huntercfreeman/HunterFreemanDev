using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Dimension;

public record DimensionsRecord(DimensionValuedUnit Width, DimensionValuedUnit Height, DimensionValuedUnit Left, DimensionValuedUnit Top);

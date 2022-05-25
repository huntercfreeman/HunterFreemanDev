using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.DebugCssClasses;

public record DebugCssClassRecord(Guid DebugCssClassId, string CssClassString, string DisplayName, bool IsEnabled);
using HunterFreemanDev.ClassLibrary.DebugCssClasses;
using System.Collections.Immutable;

namespace HunterFreemanDev.ClassLibrary.Store.DebugCssClasses;

public record InitializeDebugCssClassesStateAction(ImmutableArray<DebugCssClassRecord> DebugCssClassRecords);

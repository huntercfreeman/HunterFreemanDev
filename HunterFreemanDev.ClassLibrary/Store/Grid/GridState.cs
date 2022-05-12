using Fluxor;
using HunterFreemanDev.ClassLibrary.Element;

namespace HunterFreemanDev.ClassLibrary.Store.Grid;

[FeatureState]
public record GridState(List<List<ElementRecord>> ElementRecords)
{
    private GridState() : this(new List<List<ElementRecord>> { new List<ElementRecord> { ElementRecord.Uninitialized } })
    {
    }
}

using Fluxor;
using HunterFreemanDev.ClassLibrary.Dialog;

namespace HunterFreemanDev.ClassLibrary.Store.Dialog;

[FeatureState]
public record DialogStates(Dictionary<Guid, DialogRecord> DialogRecordMap)
{
    public DialogStates() : this(new Dictionary<Guid, DialogRecord>())
    {

    }
}
using Fluxor;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;

namespace HunterFreemanDev.ClassLibrary.Store.PlainTextEditor;

[FeatureState]
public record PlainTextEditorsState(Dictionary<Guid, PlainTextEditorRecord> PlainTextEditorMap)
{
    public PlainTextEditorsState() 
        : this(new Dictionary<Guid, PlainTextEditorRecord>())
    {

    }
}

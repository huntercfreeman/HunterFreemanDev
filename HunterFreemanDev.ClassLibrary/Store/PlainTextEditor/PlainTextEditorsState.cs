using Fluxor;
using HunterFreemanDev.ClassLibrary.PlainTextEditor;
using HunterFreemanDev.ClassLibrary.Store.Focus;
using HunterFreemanDev.ClassLibrary.Store.KeyDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.PlainTextEditor;

[FeatureState]
public record PlainTextEditorsState(Dictionary<Guid, PlainTextEditorRecord> PlainTextEditorMap)
{
    public PlainTextEditorsState() 
        : this(new Dictionary<Guid, PlainTextEditorRecord>())
    {

    }
}

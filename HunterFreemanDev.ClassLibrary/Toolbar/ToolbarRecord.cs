using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Toolbar;

public record ToolbarRecord<T>(Guid ToolbarRecordId, T? Item, string RenderedTypeParameterNameOf) : IToolbarRecordTyped<T>, IToolbarRecordUntyped
{
    public Type RenderedType => typeof(T);
    public object? UntypedItemParameter => Item;
    public T? TypedItemParameter => Item;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Toolbar;

public interface IToolbarRecordUntyped
{
    public Guid ToolbarRecordId { get; }
    public string RenderedTypeParameterNameOf { get; }
    public object? UntypedItemParameter { get; }
}

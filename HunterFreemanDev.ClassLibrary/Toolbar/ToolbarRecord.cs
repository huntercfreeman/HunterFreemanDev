using HunterFreemanDev.ClassLibrary.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Toolbar;

public record ToolbarRecord(Guid ToolbarRecordId,
    Type RenderedContentType)
{
}

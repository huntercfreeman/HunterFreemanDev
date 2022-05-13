using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Dropdown;

public record DropdownRecord(Guid DropdownId, List<DropdownRecord> ChildDropdownRecords);

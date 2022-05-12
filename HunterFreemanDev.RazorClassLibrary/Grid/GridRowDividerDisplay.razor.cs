using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridRowDividerDisplay : ComponentBase
{
    [Parameter]
    public int MyProperty { get; set; }
}

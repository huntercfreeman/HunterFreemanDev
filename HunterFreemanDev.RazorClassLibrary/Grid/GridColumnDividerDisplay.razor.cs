using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridColumnDividerDisplay : ComponentBase
{
    [Parameter]
    public int MyProperty { get; set; }
}
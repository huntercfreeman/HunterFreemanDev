using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

public class IconBase : ComponentBase
{
    [Parameter]
    public double Width { get; set; } = 16;
    [Parameter]
    public double Height { get; set; } = 16;
}

using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

public class IconBase : ComponentBase
{
    [Parameter]
    public double Width { get; set; } = 16;
    [Parameter]
    public double Height { get; set; } = 16;
}

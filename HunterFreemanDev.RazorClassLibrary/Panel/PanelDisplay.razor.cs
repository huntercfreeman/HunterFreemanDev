using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;

namespace HunterFreemanDev.RazorClassLibrary.Panel;

public partial class PanelDisplay : ComponentBase
{
    /// <summary>
    /// Accepts a parameter of bool to indicate the _isExpanded state
    /// </summary>
    [Parameter, EditorRequired]
    public RenderFragment<bool> PanelTitleRenderFragment { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment PanelBodyRenderFragment { get; set; } = null!;

    private bool _isExpanded;

    public static RenderFragment GetDefaultExpansionIcons(bool isExpanded)
    {
        int sequence = 0;

        return (builder) =>
        {
            if(isExpanded)
            {
                builder.OpenComponent<IconChevronDown>(sequence++);
                builder.CloseComponent();
            }
            else
            {
                builder.OpenComponent<IconChevronRight>(sequence++);
                builder.CloseComponent();
            }
        };
    }

    private void ToggleIsExpanded()
    {
        _isExpanded = !_isExpanded;
    }
}
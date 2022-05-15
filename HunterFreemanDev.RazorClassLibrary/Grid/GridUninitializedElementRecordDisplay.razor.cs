using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.Element;
using HunterFreemanDev.ClassLibrary.Store.Grid;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary.Grid;

public partial class GridUninitializedElementRecordDisplay : FluxorComponent
{
    [Inject]
    private IState<GridState> GridState { get; set; } = null!;

    [Parameter, EditorRequired]
    public GridRecord ElementRecord { get; set; } = null!;
}

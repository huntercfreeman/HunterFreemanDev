using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using HunterFreemanDev.ClassLibrary.Store.Toolbar;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.Toolbar;
using HunterFreemanDev.RazorClassLibrary.Settings;

namespace HunterFreemanDev.RazorClassLibrary.Toolbar;

public partial class ToolbarDisplay : FluxorComponent
{
    [Inject]
    private IState<ToolbarState> ToolbarState { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    [Inject]
    private FileSystemAccessSettings FileSystemAccessSettings { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            var action = new RegisterMainRowToolbarRecordAction(new ToolbarRecord(Guid.NewGuid(), typeof(SettingsDialogEntryPointDisplay)));

            Dispatcher.Dispatch(action);
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}
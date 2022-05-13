using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HunterFreemanDev.RazorClassLibrary.Button;
using HunterFreemanDev.RazorClassLibrary.Icons.Codicon;
using Fluxor;
using HunterFreemanDev.ClassLibrary.Store.Dialog;
using HunterFreemanDev.ClassLibrary.Dialog;
using HunterFreemanDev.ClassLibrary.Dimension;

namespace HunterFreemanDev.RazorClassLibrary.Settings;

public partial class SettingsDialogEntryPointDisplay : ComponentBase
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;
    [Inject]
    private IViewportDimensionsService ViewportDimensionsService { get; set;} = null!;

    private Guid _settingsDialogRecordId = Guid.NewGuid();
    private Type _settingsDialogRecordType = typeof(SettingsDisplay);

    private async Task DispatchConstructDialogOnClick()
    {
        if (!DialogStates.Value.DialogRecordMap.ContainsKey(_settingsDialogRecordId))
        {
            var defaultDimensionsRecordForDialog = await DialogRecord.ConstructDefaultDimensionsRecord(ViewportDimensionsService);

            var action = new RegisterDialogAction(new DialogRecord(_settingsDialogRecordId,
                _settingsDialogRecordType,
                defaultDimensionsRecordForDialog));

            Dispatcher.Dispatch(action);
        }
    }
}
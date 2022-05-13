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

namespace HunterFreemanDev.RazorClassLibrary.Settings;

public partial class SettingsDialogEntryPointDisplay : ComponentBase
{
    [Inject]
    private IState<DialogStates> DialogStates { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    private DialogRecord _settingsDialogRecord = new(Guid.NewGuid(), typeof(SettingsDisplay));

    private void DispatchConstructDialogOnClick()
    {
        if (!DialogStates.Value.DialogRecordMap.ContainsKey(_settingsDialogRecord.DialogRecordId))
        {
            var action = new RegisterDialogAction(_settingsDialogRecord);

            Dispatcher.Dispatch(action);
        }
    }
}
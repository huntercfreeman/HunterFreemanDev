﻿using HunterFreemanDev.ClassLibrary.Errors;
using Microsoft.AspNetCore.Components;

namespace HunterFreemanDev.RazorClassLibrary.Errors;

public partial class RichErrorDisplay : ComponentBase
{
    [Parameter]
    public RichErrorModel RichErrorModel { get; set; } = null!;

    protected override void OnAfterRender(bool firstRender)
    {
        if (RichErrorModel.IsResolved is not null && RichErrorModel.IsResolved() &&
            RichErrorModel.OnIsResolvedAction is not null)
            RichErrorModel.OnIsResolvedAction(RichErrorModel);

        base.OnAfterRender(firstRender);
    }
}
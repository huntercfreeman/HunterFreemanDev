using HunterFreemanDev.ClassLibrary.Dimension;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary.Dimensions;

public class ViewportDimensionsService : IViewportDimensionsService
{
    private readonly IJSRuntime _jsRuntime;

    public ViewportDimensionsService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<ViewportDimensionsModel> GetViewportDimensionsAsync()
    {
        return await _jsRuntime.InvokeAsync<ViewportDimensionsModel>("hunterFreemanDevRazorClassLibrary.getViewportDimensions");
    }
}


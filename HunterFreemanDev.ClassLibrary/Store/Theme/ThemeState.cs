using Fluxor;
using HunterFreemanDev.ClassLibrary.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Store.Theme;

[FeatureState]
public record ThemeState(ThemeRecord ThemeRecord)
{
    public ThemeState() : this(ThemeFacts.HfdLightThemeDefaultContrast)
    {

    }
}

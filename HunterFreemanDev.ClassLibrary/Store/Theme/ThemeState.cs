using Fluxor;
using HunterFreemanDev.ClassLibrary.Theme;

namespace HunterFreemanDev.ClassLibrary.Store.Theme;

[FeatureState]
public record ThemeState(ThemeRecord ThemeRecord)
{
    public ThemeState() : this(ThemeFacts.HfdLightThemeDefaultContrast)
    {

    }
}

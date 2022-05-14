using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.Theme;

public class ThemeReducers
{
    [ReducerMethod]
    public static ThemeState ReduceSetThemeStateAction(ThemeState previousThemeState,
        SetThemeStateAction setThemeStateAction)
    {
        return new ThemeState(setThemeStateAction.ThemeRecord);
    }
}
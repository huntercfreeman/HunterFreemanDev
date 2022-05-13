using Fluxor;
using HunterFreemanDev.ClassLibrary.Theme;

namespace HunterFreemanDev.ClassLibrary.Store.Theme;

[FeatureState]
public record ThemesProvider(List<ThemeRecord> ThemeRecords)
{
    public ThemesProvider() : this(new List<ThemeRecord>())
    {
        foreach(var themeRecord in ThemeFacts.AllDefaultHfdThemes)
        {
            ThemeRecords.Add(themeRecord);
        }
    }
}

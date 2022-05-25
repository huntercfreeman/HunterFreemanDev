using System.Collections.Immutable;

namespace HunterFreemanDev.ClassLibrary.Theme;

public static class ThemeFacts
{
    public static readonly ThemeRecord HfdLightThemeDefaultContrast = new ThemeRecord(Guid.NewGuid(),
        "Hfd Light Theme Default Contrast",
        "hfd_hfd-light-theme-default-contrast",
        "The default light theme for the IDE with default contrast.",
        ThemeContrastKind.DefaultContrast,
        ThemeColorPaletteKind.Light);

    public static readonly ThemeRecord HfdLightThemeHighContrast = new ThemeRecord(Guid.NewGuid(),
        "Hfd Light Theme High Contrast",
        "hfd_hfd-light-theme-high-contrast",
        "The default light theme for the IDE with high contrast.",
        ThemeContrastKind.HighContrast,
        ThemeColorPaletteKind.Light);

    public static readonly ThemeRecord HfdDarkThemeDefaultContrast = new ThemeRecord(Guid.NewGuid(),
        "Hfd Dark Theme Default Contrast",
        "hfd_hfd-dark-theme-default-contrast",
        "The default light theme for the IDE with default contrast.",
        ThemeContrastKind.DefaultContrast,
        ThemeColorPaletteKind.Light);

    public static readonly ThemeRecord HfdDarkThemeHighContrast = new ThemeRecord(Guid.NewGuid(),
        "Hfd Dark Theme High Contrast",
        "hfd_hfd-dark-theme-high-contrast",
        "The default dark theme for the IDE with high contrast.",
        ThemeContrastKind.DefaultContrast,
        ThemeColorPaletteKind.Light);

    public static readonly ImmutableArray<ThemeRecord> AllDefaultHfdThemes = new ThemeRecord[]
    {
        HfdLightThemeDefaultContrast,
        HfdLightThemeHighContrast,
        HfdDarkThemeDefaultContrast,
        HfdDarkThemeHighContrast,
    }.ToImmutableArray();
}

namespace HunterFreemanDev.ClassLibrary.Theme;

public record ThemeRecord(Guid ThemeId,
    string ThemeDisplayName,
    string ThemeCssClassName,
    string ThemeDescription,
    ThemeContrastKind ThemeContrastKind,
    ThemeColorPaletteKind ThemeColorPaletteKind);

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Theme;

public record ThemeRecord(Guid ThemeId,
    string ThemeCssClassName,
    string ThemeDescription,
    ThemeContrastKind ThemeContrastKind,
    ThemeColorPaletteKind ThemeColorPaletteKind);

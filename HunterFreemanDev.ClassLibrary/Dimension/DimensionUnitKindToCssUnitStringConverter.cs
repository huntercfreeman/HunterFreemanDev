namespace HunterFreemanDev.ClassLibrary.Dimension;

public static class DimensionUnitKindToCssUnitStringConverter
{
    public static string ConvertToCssUnitString(this DimensionUnitKind dimensionUnitKind) => dimensionUnitKind switch
    {
        DimensionUnitKind.Pixels => "px",
        DimensionUnitKind.PercentageOfParent => "%",
        DimensionUnitKind.CharacterWidth or DimensionUnitKind.Ch  => "ch",
        DimensionUnitKind.RootCharacterHeight or DimensionUnitKind.Rem => "rem",
        DimensionUnitKind.CharacterHeight or DimensionUnitKind.Em => "em",
        _ => throw new ApplicationException($"The {nameof(DimensionUnitKind)} is not currently supported.")
    };
}

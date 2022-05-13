namespace HunterFreemanDev.ClassLibrary.Dimension;

public static class DimensionUnitKindToCssUnitStringConverter
{
    public static string ConvertToCssUnitString(this DimensionUnitKind dimensionUnitKind) => dimensionUnitKind switch
    {
        DimensionUnitKind.Pixels => "px",
        DimensionUnitKind.PercentageOfParentAsDecimal => "%",
        _ => throw new ApplicationException($"The {nameof(DimensionUnitKind)} is not currently supported.")
    };
}

namespace HunterFreemanDev.ClassLibrary.Dimension;

public record DimensionValuedUnit(double Value, DimensionUnitKind DimensionUnitKind)
{
    public string BuildCssStyleString() => $"{Value}{DimensionUnitKind.ConvertToCssUnitString()}";
}

using System.Text;

namespace HunterFreemanDev.ClassLibrary.Dimension;

public record DimensionsRecord(DimensionValuedUnit Width, DimensionValuedUnit Height, DimensionValuedUnit Left, DimensionValuedUnit Top)
{
    public string BuildCssStyleString()
    {
        var cssStyleBuilder = new StringBuilder();

        cssStyleBuilder.Append($"width: {Width.BuildCssStyleString()}; ");
        cssStyleBuilder.Append($"height: {Height.BuildCssStyleString()}; ");

        cssStyleBuilder.Append($"left: {Left.BuildCssStyleString()}; ");
        cssStyleBuilder.Append($"top: {Top.BuildCssStyleString()}; ");

        return cssStyleBuilder.ToString();
    }

    public static DimensionsRecord FromPixelUnits(double width, double height, double left, double top)
    {
        return new DimensionsRecord(new DimensionValuedUnit(width, DimensionUnitKind.Pixels),
            new DimensionValuedUnit(height, DimensionUnitKind.Pixels),
            new DimensionValuedUnit(left, DimensionUnitKind.Pixels),
            new DimensionValuedUnit(top, DimensionUnitKind.Pixels));
    }
    
    public static DimensionsRecord FromPercentageOfParentUnits(double width, double height, double left, double top)
    {
        return new DimensionsRecord(new DimensionValuedUnit(width, DimensionUnitKind.PercentageOfParent),
            new DimensionValuedUnit(height, DimensionUnitKind.PercentageOfParent),
            new DimensionValuedUnit(left, DimensionUnitKind.PercentageOfParent),
            new DimensionValuedUnit(top, DimensionUnitKind.PercentageOfParent));
    }
}

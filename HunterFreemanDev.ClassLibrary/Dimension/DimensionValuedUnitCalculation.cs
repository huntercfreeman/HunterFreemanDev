using System.Text;

namespace HunterFreemanDev.ClassLibrary.Dimension;

public record DimensionValuedUnitCalculation(DimensionValuedUnit LeftOperand,
    DimensionValuedUnitCalculationOperatorKind DimensionValuedUnitCalculationOperatorKind,
    DimensionValuedUnit RightOperand)
{
    public string BuildCssStyleString()
    {
        var cssStyleBuilder = new StringBuilder();

        cssStyleBuilder.Append("calc(");

        cssStyleBuilder.Append(LeftOperand.BuildCssStyleString());
        
        cssStyleBuilder.Append($" {DimensionValuedUnitCalculationOperatorKind.ConvertToCssUnitString()} ");

        cssStyleBuilder.Append(RightOperand.BuildCssStyleString());

        cssStyleBuilder.Append(')');
        
        return cssStyleBuilder.ToString();
    }
}

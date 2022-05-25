namespace HunterFreemanDev.ClassLibrary.Dimension;

public static class DimensionValuedUnitCalculationOperatorKindConverter
{
    public static string ConvertToCssUnitString(this DimensionValuedUnitCalculationOperatorKind dimensionValuedUnitCalculationOperatorKind) => dimensionValuedUnitCalculationOperatorKind switch
    {
        DimensionValuedUnitCalculationOperatorKind.Addition => "+",
        DimensionValuedUnitCalculationOperatorKind.Subtraction => "-",
        DimensionValuedUnitCalculationOperatorKind.Multiplication => "*",
        DimensionValuedUnitCalculationOperatorKind.Division => "/",
        _ => throw new ApplicationException($"The {nameof(DimensionValuedUnitCalculationOperatorKind)} is not currently supported.")
    };
}

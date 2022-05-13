using HunterFreemanDev.ClassLibrary.Dimension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.ClassLibrary.Dialog;

public record DialogRecord(Guid DialogRecordId, Type RenderedContentType, DimensionsRecord DimensionsRecord)
{
    public const double DEFAULT_WIDTH_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.7;
    public const double DEFAULT_HEIGHT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.7;
    
    public const double DEFAULT_LEFT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.15;
    public const double DEFAULT_TOP_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL = 0.15;

    public static async Task<DimensionsRecord> ConstructDefaultDimensionsRecord(IViewportDimensionsService viewportDimensionsService)
    {
        var viewportDimensions = await viewportDimensionsService.GetViewportDimensionsAsync();

        DimensionValuedUnit widthInPixels = 
            new DimensionValuedUnit(DEFAULT_WIDTH_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.WidthInPixels,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit heightInPixels = 
            new DimensionValuedUnit(DEFAULT_HEIGHT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.HeightInPixels,
                DimensionUnitKind.Pixels);
        
        DimensionValuedUnit leftInPixels = 
            new DimensionValuedUnit(DEFAULT_LEFT_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.WidthInPixels,
                DimensionUnitKind.Pixels);

        DimensionValuedUnit topInPixels = 
            new DimensionValuedUnit(DEFAULT_TOP_PERCENTAGE_OF_VIEWPORT_DIMENSIONS_MULTIPLIER_AS_DECIMAL * viewportDimensions.HeightInPixels,
                DimensionUnitKind.Pixels);

        return new DimensionsRecord(widthInPixels, heightInPixels, leftInPixels, topInPixels);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

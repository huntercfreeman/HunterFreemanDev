using System.Collections.Immutable;

namespace HunterFreemanDev.ClassLibrary.ImmutableArrayExtensions;

public static class HunterFreemanDevImmutableArrayExtensions
{
    public static ImmutableArray<ImmutableArray<T>> ConvertToImmutable<T>(IEnumerable<IEnumerable<T>> items)
    {
        List<ImmutableArray<T>> temporaryRows = new();

        foreach (var row in items)
        {
            List<T> temporaryRow = new();

            foreach (var gridItemRecord in row)
            {
                temporaryRow.Add(gridItemRecord);
            }

            temporaryRows.Add(temporaryRow.ToImmutableArray());
        }

        return temporaryRows.ToImmutableArray();
    }
}

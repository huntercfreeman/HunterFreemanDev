namespace HunterFreemanDev.ClassLibrary.Theme;

public static class ColorKindConverter
{
    public static string ToCssClassString(this ColorKind colorKind) => colorKind switch
    {
        ColorKind.Primary => "hfd_primary",
        ColorKind.Secondary => "hfd_secondary",
        ColorKind.Success => "hfd_success",
        ColorKind.Danger => "hfd_danger",
        ColorKind.Warning => "hfd_warning",
        ColorKind.Info => "hfd_info",
        _ => ""
    };
}
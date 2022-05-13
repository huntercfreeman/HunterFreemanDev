namespace HunterFreemanDev.ClassLibrary.Element;

public record ElementRecord(Type RenderedContentType)
{
    public static ElementRecord Uninitialized => new ElementRecord(typeof(UninitializedElementRecord));
}
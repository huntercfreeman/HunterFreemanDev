namespace HunterFreemanDev.ClassLibrary.Element;

public record ElementRecord(Type RenderedType)
{
    public static ElementRecord Uninitialized => new ElementRecord(typeof(UninitializedElementRecord));
}
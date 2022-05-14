namespace HunterFreemanDev.ClassLibrary.Element;

public record ElementRecord(Guid ElementRecordId, Type RenderedContentType)
{
    public static ElementRecord Uninitialized => new ElementRecord(Guid.NewGuid(), typeof(UninitializedElementRecord));
}
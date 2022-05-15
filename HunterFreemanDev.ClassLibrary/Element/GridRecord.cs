namespace HunterFreemanDev.ClassLibrary.Element;

public record GridRecord(Guid GridRecordId, Type RenderedContentType, string? GridRecordChildComponentStateParameterName)
{
    public static GridRecord Uninitialized => new GridRecord(Guid.NewGuid(), typeof(UninitializedElementRecord), null);
}
namespace HunterFreemanDev.ClassLibrary.Toolbar;

public interface IToolbarRecordTyped<T> : IToolbarRecordUntyped
{
    public T? TypedItemParameter { get; }
    public Type TypeOfTypedItemParameter => typeof(T);
}

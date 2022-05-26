using System.Collections.Immutable;

namespace HunterFreemanDev.ClassLibrary.TreeView;

public abstract record TreeViewRecordBase<T>
{
    protected TreeViewRecordBase(T data)
    {
        Data = data;
    }

    public T Data { get; }

    public abstract ImmutableArray<TreeViewRecordBase<T>> GetChildTreeViewRecords { get; }

    public abstract Task LoadChildTreeViewRecords();
}
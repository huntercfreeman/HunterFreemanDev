using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.Errors;

namespace HunterFreemanDev.ClassLibrary.TreeView;

public abstract record TreeViewRecordBase<T>
{
    protected TreeViewRecordBase(T data)
    {
        Data = data;
    }

    public T Data { get; }
    // TODO: Strictly enforce immutability within the TreeViews
    public bool IsExpanded { get; set; }
    public RichErrorModel? RichErrorModel { get; set; }

    public abstract ImmutableArray<TreeViewRecordBase<T>> GetChildTreeViewRecords { get; }

    public abstract Task LoadChildTreeViewRecords();
}
using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.TreeView;

public record DefaultFileTreeViewRecord : FileTreeViewRecordBase
{
    public DefaultFileTreeViewRecord(IAbsoluteFilePath data) 
        : base(data)
    {
    }

    public override bool CanHaveChildren => false;

    public override ImmutableArray<TreeViewRecordBase<IAbsoluteFilePath>> GetChildTreeViewRecords =>
        Array.Empty<TreeViewRecordBase<IAbsoluteFilePath>>()
            .ToImmutableArray();

    public override Task LoadChildTreeViewRecords()
    {
        return Task.CompletedTask;
    }
}

using System.Collections.Immutable;
using HunterFreemanDev.ClassLibrary.Errors;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.TreeView;

public record DirectoryFileTreeViewRecord : FileTreeViewRecordBase
{
    private DirectoryFileTreeViewRecord[] _childDirectoryFileTreeViewRecords = 
        Array.Empty<DirectoryFileTreeViewRecord>();

    private DefaultFileTreeViewRecord[] _childDefaultFileTreeViewRecords = 
        Array.Empty<DefaultFileTreeViewRecord>();

    public DirectoryFileTreeViewRecord(IAbsoluteFilePath data) 
        : base(data)
    {
    }

    public override bool CanHaveChildren => true;

    public override ImmutableArray<TreeViewRecordBase<IAbsoluteFilePath>> GetChildTreeViewRecords =>
        _childDirectoryFileTreeViewRecords
            .Select(x => (TreeViewRecordBase<IAbsoluteFilePath>)x)
            .Union(_childDefaultFileTreeViewRecords.Select(x => (TreeViewRecordBase<IAbsoluteFilePath>)x))
            .ToImmutableArray();

    public override Task LoadChildTreeViewRecords()
    {
        try
        {
            var childDirectoryStringFilePaths = System.IO.Directory.GetDirectories(Data.GetAbsoluteFilePathString());
            var childFileStringFilePaths = System.IO.Directory.GetFiles(Data.GetAbsoluteFilePathString());

            _childDirectoryFileTreeViewRecords = childDirectoryStringFilePaths
                .Select(x => new DirectoryFileTreeViewRecord(new AbsoluteFilePath(x, true)))
                .ToArray();

            _childDefaultFileTreeViewRecords = childFileStringFilePaths
                .Select(x => new DefaultFileTreeViewRecord(new AbsoluteFilePath(x, false)))
                .ToArray();
        }
        catch (Exception e)
        {
            RichErrorModel = new(e.Message, $"{nameof(LoadChildTreeViewRecords)} threw an exception.");
        }

        return Task.CompletedTask;
    }
}
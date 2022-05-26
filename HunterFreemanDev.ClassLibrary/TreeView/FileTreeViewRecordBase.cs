using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.TreeView;

public abstract record FileTreeViewRecordBase : TreeViewRecordBase<IAbsoluteFilePath>
{
    protected FileTreeViewRecordBase(IAbsoluteFilePath data) 
        : base(data)
    {
    }
}
using Fluxor;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.Store.FileBuffer;

[FeatureState]
public record FileBufferStates
{
    private Dictionary<FileDescriptorRecordKey, FileDescriptorRecord> _fileDescriptorRecordMap;

    public FileBufferStates()
    {
        _fileDescriptorRecordMap = new();
    }

    protected FileBufferStates(FileBufferStates otherFileBufferStates)
    {
        _fileDescriptorRecordMap = new(otherFileBufferStates._fileDescriptorRecordMap);
    }

    public FileBufferStates WithAdd(FileDescriptorRecordKey fileDescriptorRecordKey,
        IAbsoluteFilePath absoluteFilePath)
    {
        var nextFileBufferStates = new FileBufferStates(this);

        nextFileBufferStates._fileDescriptorRecordMap.Add(fileDescriptorRecordKey, 
            new FileDescriptorRecord(absoluteFilePath, Guid.NewGuid()));

        return nextFileBufferStates;
    }

    public FileDescriptorRecord LookupFileDescriptor(FileDescriptorRecordKey fileDescriptorRecordKey)
    {
        return _fileDescriptorRecordMap[fileDescriptorRecordKey];
    }
}

public record RegisterFileDescriptorRecordAction(FileDescriptorRecordKey FileDescriptorRecordKey, IAbsoluteFilePath AbsoluteFilePath);

public class FileBufferReducer
{
    [ReducerMethod]
    public static FileBufferStates ReduceRegisterFileDescriptorAction(FileBufferStates previousFileBufferStates,
        RegisterFileDescriptorRecordAction registerFileDescriptorRecordAction)
    {
        return previousFileBufferStates.WithAdd(registerFileDescriptorRecordAction.FileDescriptorRecordKey,
            registerFileDescriptorRecordAction.AbsoluteFilePath);
    }
}
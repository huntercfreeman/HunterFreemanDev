using Fluxor;

namespace HunterFreemanDev.ClassLibrary.Store.FolderExplorer;

public class FolderExplorerReducer
{
    [ReducerMethod]
    public static FolderExplorerState ReduceSetFolderExplorerWorkspaceAction(FolderExplorerState previousFolderExplorerState,
        SetFolderExplorerWorkspaceAction setFolderExplorerWorkspaceAction)
    {
        return previousFolderExplorerState with
        {
            WorkspaceAbsoluteFilePath = setFolderExplorerWorkspaceAction.WorkspaceAbsoluteFilePath,
        };
    }
}
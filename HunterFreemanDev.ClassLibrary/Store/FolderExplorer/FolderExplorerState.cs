﻿using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.Store.FolderExplorer;

public record FolderExplorerState(IAbsoluteFilePath WorkspaceAbsoluteFilePath);
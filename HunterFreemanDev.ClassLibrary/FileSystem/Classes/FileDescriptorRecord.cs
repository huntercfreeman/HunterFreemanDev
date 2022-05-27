﻿using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;

namespace HunterFreemanDev.ClassLibrary.FileSystem.Classes;

public record FileDescriptorRecord(IAbsoluteFilePath AbsoluteFilePath, Guid FileDescriptorRecordSequenceId);
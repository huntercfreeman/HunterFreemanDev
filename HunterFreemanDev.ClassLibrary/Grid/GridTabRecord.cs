﻿namespace HunterFreemanDev.ClassLibrary.Grid;

public record GridTabRecord(GridTabRecordKey GridTabRecordKey,
    bool IsCloseable,
    Type RenderedContentType,
    string RenderedContentTitleText,
    Dictionary<string, object>? Parameters = null);

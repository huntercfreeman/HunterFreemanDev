﻿using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.Html;

namespace HunterFreemanDev.ClassLibrary.Store.Html;

public record ReplaceHtmlElementDimensionsRecordAction(HtmlElementRecordKey HtmlElementRecordKey, DimensionsRecord DimensionsRecord);
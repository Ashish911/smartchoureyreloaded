using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class LanguageInformation
{
    public string Code { get; set; } = null!;

    public string LanguageName { get; set; } = null!;

    public string? LanguageNameEnglish { get; set; }
}

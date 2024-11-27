using System;

namespace AspireSample.ApiService.Infrastructure;

[AttributeUsage(AttributeTargets.Class)]
public class OptionsSectionAttribute : Attribute
{
    public OptionsSectionAttribute(string sectionName)
    {
        SectionName = sectionName;
    }

    public string SectionName { get; }
}

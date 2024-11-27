using AspireSample.ApiService.Infrastructure;

namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration, string key) where T : new()
        {
            var section = new T();
            configuration.GetSection(key).Bind(section);
            return section;
        }

        public static T GetSection<T>(this IConfiguration configuration) where T : new()
        {
            var key = GetKeySection<T>();
            return GetSection<T>(configuration, key);
        }

        public static IConfigurationSection GetConfigurationSection<T>(this IConfiguration configuration) where T : new()
        {
            var key = GetKeySection<T>();
            return configuration.GetSection(key);
        }

        private static string GetKeySection<T>() where T : new()
        {
            var key = typeof(T).Name;
            var sectionNameAttribute = typeof(T).GetCustomAttributes(typeof(OptionsSectionAttribute), false)?.FirstOrDefault();
            if (sectionNameAttribute is OptionsSectionAttribute attribute)
            {
                key = attribute.SectionName;
            }

            return key;
        }
    }
}

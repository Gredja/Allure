using Allure.Core.Configuration.Config;
using Newtonsoft.Json;

namespace Allure.Core.Configuration;

public static class ConfigurationProvider<T> where T: TestConfiguration
{
    public static T Configuration => InitializeConfiguration();

    private static T InitializeConfiguration()
    {
#if QA
            var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.qa.json");
#elif STAGE
            var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.stage.json");
#else
        var json = File.ReadAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}config.json");
#endif
        return JsonConvert.DeserializeObject<T>(json); ;
    }
}
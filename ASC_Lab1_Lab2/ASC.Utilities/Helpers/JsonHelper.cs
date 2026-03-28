using Newtonsoft.Json;

namespace ASC.Utilities.Helpers;

public static class JsonHelper
{
    public static string ToPrettyJson(object value) =>
        JsonConvert.SerializeObject(value, Formatting.Indented);

    public static T? FromJson<T>(string json) =>
        JsonConvert.DeserializeObject<T>(json);
}

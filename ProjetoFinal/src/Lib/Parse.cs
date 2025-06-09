public static class Parser
{
    public static bool TryParse<T>(string input, out T? result)
    {
        result = default;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        input = input.Trim();
        Type type = typeof(T);

        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Boolean:
                return TryParseBool(input, out result);

            default:
                return TryParseByReflection(input, out result);
        }
    }

    private static bool TryParseBool<T>(string input, out T? result)
    {
        result = default;
        switch (input.ToLowerInvariant())
        {
            case "y":
            case "yes":
            case "true":
            case "1":
                result = (T)(object)true;
                return true;

            case "n":
            case "no":
            case "false":
            case "0":
                result = (T)(object)false;
                return true;

            default:
                return false;
        }
    }

    private static bool TryParseByReflection<T>(string input, out T? result)
    {
        result = default;
        var type = typeof(T);
        var method = type.GetMethod("TryParse", new[] { typeof(string), type.MakeByRefType() });

        if (method != null)
        {
            object[] args = new object[] { input, null! };
            bool success = (bool)method.Invoke(null, args)!;
            if (success)
            {
                result = (T)args[1]!;
                return true;
            }
        }

        return false;
    }
}

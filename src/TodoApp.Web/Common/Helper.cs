namespace TodoApp.Web.Common;

public static class Helper
{
    public static bool IsNew<T>(this T value) =>
        value?.Equals(default(T)) ?? false;

    public static string FormaterResultResponse<TId>(this TId key, string post_msg, string put_msg, params object?[] args) =>
        (key.IsNew() ? post_msg : put_msg).FormaterString(args);

    public static string FormaterString(this string value, params object?[] args) =>
        string.Format(value, args);
}
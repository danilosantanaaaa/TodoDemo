namespace TodoApp.TestCommon.TestUteils.Constaints;

public partial class Constaint
{
    public static class Todo
    {
        public static Guid TodoId1 = Guid.Parse("0f7a15ea-992d-41d1-86f3-edb00ab5c863");
        public static Guid TodoId2 = Guid.Parse("95ab4c29-a0fb-423d-93fd-8f6e7e171dbc");
        public const string Description = "Task";
        public static Guid UserId = Guid.Parse("a38ce0dd-b4b8-4080-9e61-cbae8c502c2b");
        public static TimeOnly TimeRemember = new TimeOnly(13, 0);
    }
}
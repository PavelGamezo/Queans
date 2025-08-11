namespace Queans.Application.UnitTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class Tag
        {
            public static readonly Guid TagId = Guid.NewGuid();

            public const string Name = "Tag Name";

            public static List<string> CreateUtilTagList() =>
                new List<string>()
                {
                    Tag.Name
                };
        }
    }


}

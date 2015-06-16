using Xunit;

namespace WrightsAtHome.Tests.Integration.Utility
{
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}

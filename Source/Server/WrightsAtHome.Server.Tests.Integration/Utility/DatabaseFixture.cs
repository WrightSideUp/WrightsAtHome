using System;
using WrightsAtHome.Server.Tests.Integration.Utility;

namespace WrightsAtHome.Tests.Integration.Utility
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            // Try to delete the snapshot in case it was left over from aborted test run.
            try { DatabaseSnapshot.DeleteSnapShot(); } catch { /* Ignore any exception */ }

            DatabaseSnapshot.SetupStoredProcedures();
            DatabaseSnapshot.CreateSnapShot();
        }

        public void Dispose()
        {
            DatabaseSnapshot.RestoreSnapShot();
            DatabaseSnapshot.DeleteSnapShot();
        }
    }
}

using OwaspPlaywrightTests.Fixtures;

namespace OwaspPlaywrightTests.Collections;

[CollectionDefinition("Logged in collection")]
public class LoggedInCollection : ICollectionFixture<PrepareRandomUserFixture> { }

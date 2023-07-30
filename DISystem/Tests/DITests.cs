using NUnit.Framework;

namespace DISystem.Tests;

internal class DITests
{
	[Test]
	public void RootContext_IsNotNull()
	{
		Assert.NotNull(DI.RootContext);
	}
}
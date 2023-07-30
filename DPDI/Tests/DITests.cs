using NUnit.Framework;

namespace DPDI.Tests;

internal class DITests
{
	[Test]
	public void RootContext_IsNotNull()
	{
		Assert.NotNull(DI.RootContext);
	}
}
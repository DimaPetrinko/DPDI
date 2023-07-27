using NUnit.Framework;

namespace DI.Container.Tests;

internal class MiscTests
{
	[Test]
	public void ContainerException_PropertiesAreCorrect()
	{
		var container = new Implementation.Container();
		var someObject = new TestClass();
		var id = new IdStruct();

		container.Add(someObject, id);

		var exception = Assert.Throws<ContainerException>(() => container.Add(someObject, id));

		Assert.AreEqual(typeof(TestClass), exception.ObjectType);
		Assert.AreEqual(id, exception.Id);
	}
}
using NUnit.Framework;

namespace DISystem.Container.Tests.Heredity;

internal class HeredityAddTests
{
	[Test]
	public void Add_Throws_IfSameTypeAddedAsSingleInParent()
	{
		var parentContainer = new Implementation.Container();
		var container = new Implementation.Container(parentContainer);
		var someObject = new TestClass();
		var id = new IdStruct(0);

		parentContainer.Add(someObject);

		Assert.Throws<ContainerException>(() => container.Add(someObject, id));
	}
}
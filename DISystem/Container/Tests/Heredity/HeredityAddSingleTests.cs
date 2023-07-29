using NUnit.Framework;

namespace DISystem.Container.Tests.Heredity;

internal class HeredityAddSingleTests
{
	[Test]
	public void AddSingle_Throws_IfSameTypeAddedByIdInParent()
	{
		var parentContainer = new Implementation.Container();
		var container = new Implementation.Container(parentContainer);
		var someObject = new TestClass();
		var id = new IdStruct(0);

		parentContainer.Add(someObject, id);

		Assert.Throws<ContainerException>(() => container.Add(someObject));
	}
}
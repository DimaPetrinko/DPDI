using NUnit.Framework;

namespace DPDI.Container.Tests;

internal class AddTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void Add_Adds_IfEmpty()
	{
		var someObject = new TestClass();

		Assert.DoesNotThrow(() => mContainer.Add(someObject, new IdStruct(0)));
	}

	[Test]
	public void Add_Adds_IfAddedDifferentType()
	{
		var someObject1 = new TestClass();
		var id1 = new IdStruct(0);
		var someObject2 = new TestClass2();
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id2));
	}

	[Test]
	public void Add_Adds_IfAddedDifferentTypeButSameId()
	{
		var someObject1 = new TestClass();
		var id = new IdStruct(0);
		var someObject2 = new TestClass2();

		mContainer.Add(someObject1, id);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id));
	}

	[Test]
	public void Add_Adds_IfAlreadyAddedDifferentId()
	{
		var someObject1 = new TestClass();
		var id1 = new IdStruct(0);
		var someObject2 = new TestClass();
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id2));
	}

	[Test]
	public void Add_Throws_IfAlreadyAddedSameId()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject1, id);

		var e = Assert.Throws<ContainerException>(() => mContainer.Add(someObject2, id));
	}

	[Test]
	public void Add_Throws_IfSameTypeAddedAsSingle()
	{
		var container = new Implementation.Container();
		var someObject = new TestClass();
		var id = new IdStruct(0);

		container.Add(someObject);

		Assert.Throws<ContainerException>(() => container.Add(someObject, id));
	}
}
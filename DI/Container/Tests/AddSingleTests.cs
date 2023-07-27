using NUnit.Framework;

namespace DI.Container.Tests;

internal class AddSingleTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void AddSingle_Adds_IfEmpty()
	{
		var someObject = new TestClass();

		Assert.DoesNotThrow(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Adds_IfAlreadyAddedDifferentType()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass2();

		mContainer.Add(someObject1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2));
	}

	[Test]
	public void AddSingle_Adds_IfAlreadyAddedDifferentTypeButSameInstance()
	{
		var someObject = new TestClass();

		mContainer.Add((ITestClass)someObject);

		Assert.DoesNotThrow(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Throws_IfAlreadyAddedSameInstance()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Throws_IfAlreadyAddedSameType()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();

		mContainer.Add(someObject1);

		Assert.Throws<ContainerException>(() => mContainer.Add(someObject2));
	}

	[Test]
	public void AddSingle_Throws_IfSameTypeAddedById()
	{
		var container = new Implementation.Container();
		var someObject = new TestClass();
		var id = new IdStruct(0);

		container.Add(someObject, id);

		Assert.Throws<ContainerException>(() => container.Add(someObject));
	}
}
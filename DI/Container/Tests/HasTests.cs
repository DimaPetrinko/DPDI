using NUnit.Framework;

namespace DI.Container.Tests;

internal class HasTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void Has_ReturnsTrue_IfAdded()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		var result = mContainer.Has<TestClass>(id);

		Assert.IsTrue(result);
	}

	[Test]
	public void Has_ReturnsTrue_IfAddedAsSingle()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject);

		var result = mContainer.Has<TestClass>(id);

		Assert.IsTrue(result);
	}

	[Test]
	public void Has_ReturnsFalse_IfNoneAdded()
	{
		var id = new IdStruct(0);

		var result = mContainer.Has<TestClass>(id);

		Assert.IsFalse(result);
	}

	[Test]
	public void Has_ReturnsFalse_IfAddedDifferentType()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		var result = mContainer.Has<TestClass2>(id);

		Assert.IsFalse(result);
	}

	[Test]
	public void Has_ReturnsFalse_IfAddedDifferentId()
	{
		var someObject = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject, id1);

		var result = mContainer.Has<TestClass>(id2);

		Assert.IsFalse(result);
	}
}
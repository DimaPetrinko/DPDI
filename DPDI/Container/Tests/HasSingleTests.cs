using NUnit.Framework;

namespace DPDI.Container.Tests;

internal class HasSingleTests
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

		mContainer.Add(someObject);

		var result = mContainer.Has<TestClass>();

		Assert.IsTrue(result);
	}

	[Test]
	public void Has_ReturnsTrue_IfAddedAsRegular()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject, new IdStruct(0));

		var result = mContainer.Has<TestClass>();

		Assert.IsTrue(result);
	}

	[Test]
	public void Has_ReturnsFalse_IfNoneAdded()
	{
		var result = mContainer.Has<TestClass>();

		Assert.IsFalse(result);
	}

	[Test]
	public void Has_ReturnsFalse_IfAddedDifferentType()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);

		var result = mContainer.Has<TestClass2>();

		Assert.IsFalse(result);
	}
}
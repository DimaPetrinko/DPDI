using NUnit.Framework;

namespace DISystem.Container.Tests;

internal class GetTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void Get_ReturnsInstance_IfAlreadyAdded()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		var result = mContainer.Get<TestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedSameInstanceForDifferentTypes()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);
		mContainer.Add((ITestClass)someObject, id);

		var result = mContainer.Get<ITestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedSameInstanceForSameTypesButDifferentIds()
	{
		var someObject = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject, id1);
		mContainer.Add(someObject, id2);

		var result1 = mContainer.Get<TestClass>(id1);
		var result2 = mContainer.Get<TestClass>(id2);

		Assert.IsNotNull(result1);
		Assert.IsNotNull(result2);
		Assert.AreEqual(someObject, result1);
		Assert.AreEqual(someObject, result2);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedDifferentInstancesForSameTypesButDifferentIds()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);
		mContainer.Add(someObject2, id2);

		var result1 = mContainer.Get<TestClass>(id1);
		var result2 = mContainer.Get<TestClass>(id2);

		Assert.IsNotNull(result1);
		Assert.IsNotNull(result2);
		Assert.AreEqual(someObject1, result1);
		Assert.AreEqual(someObject2, result2);
		Assert.AreNotEqual(result1, result2);
	}

	[Test]
	public void Get_Throws_IfNotAdded()
	{
		var id = new IdStruct(0);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id));
	}

	[Test]
	public void Get_Throws_IfAddedDifferentId()
	{
		var someObject = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject, id1);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id2));
	}

	[Test]
	public void Get_Throws_IfAddedDifferentType()
	{
		var someObject = new TestClass2();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id));
	}
}
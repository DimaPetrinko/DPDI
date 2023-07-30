using NUnit.Framework;

namespace DPDI.Container.Tests;

internal class GetSingleTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void GetSingle_ReturnsInstance_IfAlreadyAdded()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);

		var result = mContainer.Get<TestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void GetSingle_ReturnsInstance_IfAddedSameInstanceForDifferentTypes()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);
		mContainer.Add((ITestClass)someObject);

		var result = mContainer.Get<ITestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void GetSingle_Throws_IfNotAdded()
	{
		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>());
	}
}
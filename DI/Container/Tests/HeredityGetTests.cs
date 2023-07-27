using NUnit.Framework;

namespace DI.Container.Tests;

internal class HeredityGetTests
{
	private IContainer mParentContainer;
	private IContainer mParentContainer2;
	private IContainer mGrandParentContainer;
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mGrandParentContainer = new Implementation.Container();
		mParentContainer = new Implementation.Container((IContainerHeredity)mGrandParentContainer);
		mParentContainer2 = new Implementation.Container();
		mContainer = new Implementation.Container((IContainerHeredity)mParentContainer, (IContainerHeredity)mParentContainer2);
	}

	[Test]
	public void Get_ReturnsInstance_IfAlreadyAddedToParent()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mParentContainer.Add(someObject, id);

		var result = mContainer.Get<TestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
	}

	[Test]
	public void Get_ReturnsFirstInstance_IfAddedToBothParentsWithSameId()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();
		var id = new IdStruct(0);

		mParentContainer.Add(someObject1, id);
		mParentContainer2.Add(someObject2, id);

		var result = mContainer.Get<TestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject1, result);
	}

	[Test]
	public void Get_Throws_IfAddedToParentOfParent()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mGrandParentContainer.Add(someObject, id);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id));
	}
}
﻿using NUnit.Framework;

namespace DPDI.Container.Tests.Heredity;

internal class HeredityGetSingleTests
{
	private IContainer mParentContainer;
	private IContainer mParentContainer2;
	private IContainer mGrandParentContainer;
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mGrandParentContainer = new Implementation.Container();
		mParentContainer = new Implementation.Container(mGrandParentContainer.AsHeredity());
		mParentContainer2 = new Implementation.Container();
		mContainer = new Implementation.Container(mParentContainer.AsHeredity(), mParentContainer2.AsHeredity());
	}

	[Test]
	public void GetSingle_ReturnsInstance_IfAlreadyAddedToParent()
	{
		var someObject = new TestClass();

		mParentContainer.Add(someObject);

		var result = mContainer.Get<TestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
	}

	[Test]
	public void GetSingle_ReturnsFirstInstance_IfAddedToBothParents()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();

		mParentContainer.Add(someObject1);
		mParentContainer2.Add(someObject2);

		var result = mContainer.Get<TestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject1, result);
	}

	[Test]
	public void GetSingle_Throws_IfAddedToParentOfParent()
	{
		var someObject = new TestClass();

		mGrandParentContainer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>());
	}
}
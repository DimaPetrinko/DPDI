using DI.Container;
using NUnit.Framework;

namespace DI.Context.Tests;

public class StructureTests
{
	private IContext mContext;

	[SetUp]
	public void SetUp()
	{
		mContext = new Implementation.Context();
	}

	[Test]
	public void Base_IsParentTo_All()
	{
		var someObject = new TestClass();
		mContext.BaseLayer.Add(someObject);

		Assert.DoesNotThrow(() => mContext.StaticDataLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.DynamicDataLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.ModelLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.ViewLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.PresenterLayer.Get<TestClass>());
	}

	[Test]
	public void StaticData_IsParentTo_ModelViewPresenter()
	{
		var someObject = new TestClass();
		mContext.StaticDataLayer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContext.BaseLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.DynamicDataLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.ModelLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.ViewLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.PresenterLayer.Get<TestClass>());
	}

	[Test]
	public void DynamicData_IsParentTo_ModelPresenter()
	{
		var someObject = new TestClass();
		mContext.DynamicDataLayer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContext.BaseLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.StaticDataLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.ModelLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.ViewLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.PresenterLayer.Get<TestClass>());
	}

	[Test]
	public void Model_IsParentTo_Presenter()
	{
		var someObject = new TestClass();
		mContext.ModelLayer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContext.BaseLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.StaticDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.DynamicDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.ViewLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.PresenterLayer.Get<TestClass>());
	}

	[Test]
	public void View_IsParentTo_Presenter()
	{
		var someObject = new TestClass();
		mContext.ViewLayer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContext.BaseLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.StaticDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.DynamicDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.ModelLayer.Get<TestClass>());
		Assert.DoesNotThrow(() => mContext.PresenterLayer.Get<TestClass>());
	}

	[Test]
	public void Presenter_IsParentTo_None()
	{
		var someObject = new TestClass();
		mContext.PresenterLayer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContext.BaseLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.StaticDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.DynamicDataLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.ModelLayer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContext.ViewLayer.Get<TestClass>());
	}
}
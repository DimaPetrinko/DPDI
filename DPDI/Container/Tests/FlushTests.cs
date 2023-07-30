using NUnit.Framework;

namespace DPDI.Container.Tests;

internal class FlushTests
{
	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	[Test]
	public void Flush_RemovesAllObjects()
	{
		mContainer.Add(new TestClass());
		mContainer.Add((ITestClass)new TestClass());
		mContainer.Add(new TestClass2(), new IdStruct(0));
		mContainer.Add(new TestClass2(), new IdStruct(1));
		mContainer.Add(new TestClass2(), new IdStruct(2));
		mContainer.Add(new TestClass2(), new IdStruct(3));

		mContainer.Flush();
		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>());
		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(new IdStruct(0)));
	}

	[Test]
	public void Flush_DisposesAllDisposablesInSingle()
	{
		var disposableClass = new DisposableClass();

		mContainer.Add(disposableClass);

		mContainer.Flush();

		Assert.IsTrue(disposableClass.IsDisposed);
	}

	[Test]
	public void Flush_DisposesAllDisposablesInRegular()
	{
		var disposableClass = new DisposableClass();
		var id = new IdStruct(0);

		mContainer.Add(disposableClass, id);

		mContainer.Flush();

		Assert.IsTrue(disposableClass.IsDisposed);
	}
}
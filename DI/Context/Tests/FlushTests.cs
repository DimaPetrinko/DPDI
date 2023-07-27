using DI.Container;
using NUnit.Framework;

namespace DI.Context.Tests;

internal class FlushTests
{
	private IContext mContext;

	[SetUp]
	public void SetUp()
	{
		mContext = new Implementation.Context();
	}

	[Test]
	public void Flush_RemovesAllObjectsFromAllLayers()
	{
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);
		var id3 = new IdStruct(2);
		var id4 = new IdStruct(3);
		var id5 = new IdStruct(4);
		var id6 = new IdStruct(5);

		mContext.BaseLayer.Add(new TestClass(), id1);
		mContext.StaticDataLayer.Add(new TestClass(), id2);
		mContext.DynamicDataLayer.Add(new TestClass(), id3);
		mContext.ModelLayer.Add(new TestClass(), id4);
		mContext.ViewLayer.Add(new TestClass(), id5);
		mContext.PresenterLayer.Add(new TestClass(), id6);

		mContext.Flush();

		Assert.IsFalse(mContext.BaseLayer.Has<TestClass>(id1));
		Assert.IsFalse(mContext.StaticDataLayer.Has<TestClass>(id2));
		Assert.IsFalse(mContext.DynamicDataLayer.Has<TestClass>(id3));
		Assert.IsFalse(mContext.ModelLayer.Has<TestClass>(id4));
		Assert.IsFalse(mContext.ViewLayer.Has<TestClass>(id5));
		Assert.IsFalse(mContext.PresenterLayer.Has<TestClass>(id6));
	}
}
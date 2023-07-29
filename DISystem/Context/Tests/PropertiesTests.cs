using NUnit.Framework;

namespace DISystem.Context.Tests;

internal class PropertiesTests
{
	private IContext mContext;

	[SetUp]
	public void SetUp()
	{
		mContext = new Implementation.Context();
	}

	[Test]
	public void Layers_AreNotNull()
	{
		Assert.NotNull(mContext.BaseLayer);
		Assert.NotNull(mContext.StaticDataLayer);
		Assert.NotNull(mContext.DynamicDataLayer);
		Assert.NotNull(mContext.ModelLayer);
		Assert.NotNull(mContext.ViewLayer);
		Assert.NotNull(mContext.PresenterLayer);
	}

	[Test]
	public void Layers_AreUnique()
	{
		Assert.AreNotEqual(mContext.BaseLayer, mContext.StaticDataLayer);
		Assert.AreNotEqual(mContext.BaseLayer, mContext.DynamicDataLayer);
		Assert.AreNotEqual(mContext.BaseLayer, mContext.ModelLayer);
		Assert.AreNotEqual(mContext.BaseLayer, mContext.ViewLayer);
		Assert.AreNotEqual(mContext.BaseLayer, mContext.PresenterLayer);

		Assert.AreNotEqual(mContext.StaticDataLayer, mContext.DynamicDataLayer);
		Assert.AreNotEqual(mContext.StaticDataLayer, mContext.ModelLayer);
		Assert.AreNotEqual(mContext.StaticDataLayer, mContext.ViewLayer);
		Assert.AreNotEqual(mContext.StaticDataLayer, mContext.PresenterLayer);

		Assert.AreNotEqual(mContext.DynamicDataLayer, mContext.ModelLayer);
		Assert.AreNotEqual(mContext.DynamicDataLayer, mContext.ViewLayer);
		Assert.AreNotEqual(mContext.DynamicDataLayer, mContext.PresenterLayer);

		Assert.AreNotEqual(mContext.ModelLayer, mContext.ViewLayer);
		Assert.AreNotEqual(mContext.ModelLayer, mContext.PresenterLayer);

		Assert.AreNotEqual(mContext.ViewLayer, mContext.PresenterLayer);
	}
}
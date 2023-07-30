using NUnit.Framework;

namespace DPDI.Context.Tests;

internal class ContextHeredityTests
{
	private IContextHeredity mContext;

	[SetUp]
	public void SetUp()
	{
		mContext = new Implementation.Context();
	}

	[Test]
	public void AddChild_AddsNewChild()
	{
		mContext.AddNewChildContext();

		Assert.AreEqual(1, mContext.Children.Count());
	}

	[Test]
	public void AddChild_SetsParentToNewChild()
	{
		var child = mContext.AddNewChildContext();

		Assert.AreEqual(mContext.Children.First(), child);
		Assert.AreEqual(mContext, child.ParentContext);
	}

	[Test]
	public void Parent_IsSet_InConstructor()
	{
		var parentContext = new Implementation.Context();
		mContext = new Implementation.Context(parentContext);

		Assert.NotNull(mContext.ParentContext);
		Assert.AreEqual(parentContext, mContext.ParentContext);
	}
}
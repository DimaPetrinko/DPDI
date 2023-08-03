using System.Collections.Generic;
using DPDI.Container;
using ContainerImpl = DPDI.Container.Implementation.Container;

namespace DPDI.Context.Implementation;

internal class Context : IContext, IContextHeredity
{
	private readonly List<IContextHeredity> mChildren;

	public IContainer BaseLayer { get; }
	public IContainer StaticDataLayer { get; }
	public IContainer DynamicDataLayer { get; }
	public IContainer ModelLayer { get; }
	public IContainer PresenterLayer { get; }
	public IContainer ViewLayer { get; }

	public IContextHeredity? ParentContext { get; }
	public IEnumerable<IContextHeredity> Children => mChildren;

	public Context(IContextHeredity? parentContext = null)
	{
		ParentContext = parentContext;
		mChildren = new List<IContextHeredity>();

		BaseLayer = new Container.Implementation.Container();

		StaticDataLayer = new Container.Implementation.Container(
			BaseLayer.AsHeredity());
		DynamicDataLayer = new Container.Implementation.Container(
			BaseLayer.AsHeredity());

		ModelLayer = new Container.Implementation.Container(
			DynamicDataLayer.AsHeredity(),
			StaticDataLayer.AsHeredity(),
			BaseLayer.AsHeredity());
		ViewLayer = new Container.Implementation.Container(
			StaticDataLayer.AsHeredity(),
			BaseLayer.AsHeredity());

		PresenterLayer = new Container.Implementation.Container(
			ModelLayer.AsHeredity(),
			ViewLayer.AsHeredity(),
			DynamicDataLayer.AsHeredity(),
			StaticDataLayer.AsHeredity(),
			BaseLayer.AsHeredity());
	}

	public IContextHeredity AddNewChildContext()
	{
		var newContext = new Context(this);
		mChildren.Add(newContext);
		return newContext;
	}

	public void Flush()
	{
		Flush(true);
	}

	public void Flush(bool recursive)
	{
		FlushAllLayers();

		if (!recursive) return;

		foreach (var child in mChildren)
		{
			child.Flush(true);
		}
		mChildren.Clear();
	}

	private void FlushAllLayers()
	{
		BaseLayer.Flush();
		StaticDataLayer.Flush();
		DynamicDataLayer.Flush();
		ModelLayer.Flush();
		PresenterLayer.Flush();
		ViewLayer.Flush();
	}
}
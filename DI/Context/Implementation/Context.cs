using DI.Container;
using ContainerImpl = DI.Container.Implementation.Container;

namespace DI.Context.Implementation;

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

		BaseLayer = new ContainerImpl();

		StaticDataLayer = new ContainerImpl(
			BaseLayer.AsHeredity());
		DynamicDataLayer = new ContainerImpl(
			BaseLayer.AsHeredity());

		ModelLayer = new ContainerImpl(
			DynamicDataLayer.AsHeredity(),
			StaticDataLayer.AsHeredity(),
			BaseLayer.AsHeredity());
		ViewLayer = new ContainerImpl(
			StaticDataLayer.AsHeredity(),
			BaseLayer.AsHeredity());

		PresenterLayer = new ContainerImpl(
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
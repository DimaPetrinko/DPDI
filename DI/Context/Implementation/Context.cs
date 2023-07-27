using DI.Container;
using ContainerImpl = DI.Container.Implementation.Container;

namespace DI.Context.Implementation;

internal class Context : IContext
{
	public IContainer BaseLayer { get; }
	public IContainer StaticDataLayer { get; }
	public IContainer DynamicDataLayer { get; }
	public IContainer ModelLayer { get; }
	public IContainer PresenterLayer { get; }
	public IContainer ViewLayer { get; }

	public Context()
	{
		BaseLayer = new ContainerImpl();

		StaticDataLayer = new ContainerImpl((IContainerHeredity)BaseLayer);
		DynamicDataLayer = new ContainerImpl((IContainerHeredity)BaseLayer);

		ModelLayer = new ContainerImpl(
			(IContainerHeredity)DynamicDataLayer,
			(IContainerHeredity)StaticDataLayer,
			(IContainerHeredity)BaseLayer);
		ViewLayer = new ContainerImpl(
			(IContainerHeredity)StaticDataLayer,
			(IContainerHeredity)BaseLayer);

		PresenterLayer = new ContainerImpl(
			(IContainerHeredity)ModelLayer,
			(IContainerHeredity)ViewLayer,
			(IContainerHeredity)DynamicDataLayer,
			(IContainerHeredity)StaticDataLayer,
			(IContainerHeredity)BaseLayer);
	}

	public void Flush()
	{
		BaseLayer.Flush();
		StaticDataLayer.Flush();
		DynamicDataLayer.Flush();
		ModelLayer.Flush();
		PresenterLayer.Flush();
		ViewLayer.Flush();
	}
}
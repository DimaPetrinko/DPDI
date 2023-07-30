namespace DISystem.Container;

internal static class ContainerExtensions
{
	public static IContainerHeredity AsHeredity(this IContainer container)
	{
		return (IContainerHeredity)container;
	}

	public static IContainer AsBase(this IContainerHeredity container)
	{
		return (IContainer)container;
	}
}
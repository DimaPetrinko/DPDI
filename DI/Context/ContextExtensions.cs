namespace DI.Context;

public static class ContextExtensions
{
	public static IContextHeredity AsHeredity(this IContext context)
	{
		return (IContextHeredity)context;
	}

	public static IContext AsBase(this IContextHeredity context)
	{
		return (IContext)context;
	}
}
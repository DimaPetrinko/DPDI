using DPDI.Context;

namespace DPDI;

public static class DI
{
	public static IContext RootContext { get; } = new Context.Implementation.Context();
}
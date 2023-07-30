using DISystem.Context;

namespace DISystem;

public static class DI
{
	public static IContext RootContext { get; } = new Context.Implementation.Context();
}
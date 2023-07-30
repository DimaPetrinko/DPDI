namespace DISystem.Container;

internal interface IContainerHeredity
{
	T Get<T>(bool includeParents = true) where T : class;
	T Get<T>(object id, bool includeParents = true) where T : class;
}
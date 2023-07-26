namespace DI.Container;

public interface IContainer
{
	void Add<T>(T obj, object id) where T : class;
	void Add<T>(T obj) where T : class;
	T Get<T>() where T : class;
	T Get<T>(object id) where T : class;
}
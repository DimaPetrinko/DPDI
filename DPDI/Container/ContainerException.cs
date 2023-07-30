namespace DPDI.Container;

public class ContainerException : Exception
{
	public Type ObjectType { get; }
	public object? Id { get; }

	public ContainerException(Type objectType, string message) : base(message)
	{
		ObjectType = objectType;
		Id = null;
	}

	public ContainerException(Type objectType, object id, string message) : base(message)
	{
		ObjectType = objectType;
		Id = id;
	}
}
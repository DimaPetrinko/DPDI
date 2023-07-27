namespace DI.Container.Tests;

internal interface ITestClass
{
	int InstanceId { get; }
}

internal class TestClass : ITestClass
{
	public int InstanceId { get; }

	public TestClass()
	{
		var random = new Random();
		InstanceId = random.Next();
	}
}

internal class TestClass2 : ITestClass
{
	public int InstanceId { get; }

	public TestClass2()
	{
		var random = new Random();
		InstanceId = random.Next();
	}
}

internal class DisposableClass : IDisposable
{
	public bool IsDisposed { get; private set; }

	public void Dispose()
	{
		IsDisposed = true;
	}
}

internal readonly struct IdStruct
{
	public readonly int Id;

	public IdStruct(int id)
	{
		Id = id;
	}

	public override string ToString()
	{
		return Id.ToString();
	}
}
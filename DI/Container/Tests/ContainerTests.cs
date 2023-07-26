using NUnit.Framework;

namespace DI.Container.Tests;

internal class ContainerTests
{
	#region Mocks

	private interface ITestClass
	{
		int InstanceId { get; }
	}

	private class TestClass : ITestClass
	{
		public int InstanceId { get; }

		public TestClass()
		{
			var random = new Random();
			InstanceId = random.Next();
		}
	}

	private class TestClass2 : ITestClass
	{
		public int InstanceId { get; }

		public TestClass2()
		{
			var random = new Random();
			InstanceId = random.Next();
		}
	}

	private readonly struct IdStruct
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

	#endregion

	private IContainer mContainer;

	[SetUp]
	public void SetUp()
	{
		mContainer = new Implementation.Container();
	}

	#region AddSingle

	[Test]
	public void AddSingle_Adds_IfEmpty()
	{
		var someObject = new TestClass();

		Assert.DoesNotThrow(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Adds_IfAlreadyAddedDifferentType()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass2();

		mContainer.Add(someObject1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2));
	}

	[Test]
	public void AddSingle_Adds_IfAlreadyAddedDifferentTypeButSameInstance()
	{
		var someObject = new TestClass();

		mContainer.Add((ITestClass)someObject);

		Assert.DoesNotThrow(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Throws_IfAlreadyAddedSameInstance()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);

		Assert.Throws<ContainerException>(() => mContainer.Add(someObject));
	}

	[Test]
	public void AddSingle_Throws_IfAlreadyAddedSameType()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();

		mContainer.Add(someObject1);

		Assert.Throws<ContainerException>(() => mContainer.Add(someObject2));
	}

	#endregion

	#region Add

	[Test]
	public void Add_Adds_IfEmpty()
	{
		var someObject = new TestClass();

		Assert.DoesNotThrow(() => mContainer.Add(someObject, new IdStruct(0)));
	}

	[Test]
	public void Add_Adds_IfAddedDifferentType()
	{
		var someObject1 = new TestClass();
		var id1 = new IdStruct(0);
		var someObject2 = new TestClass2();
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id2));
	}

	[Test]
	public void Add_Adds_IfAddedDifferentTypeButSameId()
	{
		var someObject1 = new TestClass();
		var id = new IdStruct(0);
		var someObject2 = new TestClass2();

		mContainer.Add(someObject1, id);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id));
	}

	[Test]
	public void Add_Adds_IfAlreadyAddedDifferentId()
	{
		var someObject1 = new TestClass();
		var id1 = new IdStruct(0);
		var someObject2 = new TestClass();
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);

		Assert.DoesNotThrow(() => mContainer.Add(someObject2, id2));
	}

	[Test]
	public void Add_Throws_IfAlreadyAddedSameId()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject1, id);

		var e = Assert.Throws<ContainerException>(() => mContainer.Add(someObject2, id));
	}

	#endregion

	#region GetSingle

	[Test]
	public void GetSingle_ReturnsInstance_IfAlreadyAdded()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);

		var result = mContainer.Get<TestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void GetSingle_ReturnsInstance_IfAddedSameInstanceForDifferentTypes()
	{
		var someObject = new TestClass();

		mContainer.Add(someObject);
		mContainer.Add((ITestClass)someObject);

		var result = mContainer.Get<ITestClass>();

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void GetSingle_Throws_IfNotAdded()
	{
		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>());
	}

	#endregion

	#region Get

	[Test]
	public void Get_ReturnsInstance_IfAlreadyAdded()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		var result = mContainer.Get<TestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedSameInstanceForDifferentTypes()
	{
		var someObject = new TestClass();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);
		mContainer.Add((ITestClass)someObject, id);

		var result = mContainer.Get<ITestClass>(id);

		Assert.IsNotNull(result);
		Assert.AreEqual(someObject, result);
		Assert.AreEqual(someObject.InstanceId, result.InstanceId);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedSameInstanceForSameTypesButDifferentIds()
	{
		var someObject = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject, id1);
		mContainer.Add(someObject, id2);

		var result1 = mContainer.Get<TestClass>(id1);
		var result2 = mContainer.Get<TestClass>(id2);

		Assert.IsNotNull(result1);
		Assert.IsNotNull(result2);
		Assert.AreEqual(someObject, result1);
		Assert.AreEqual(someObject, result2);
	}

	[Test]
	public void Get_ReturnsInstance_IfAddedDifferentInstancesForSameTypesButDifferentIds()
	{
		var someObject1 = new TestClass();
		var someObject2 = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject1, id1);
		mContainer.Add(someObject2, id2);

		var result1 = mContainer.Get<TestClass>(id1);
		var result2 = mContainer.Get<TestClass>(id2);

		Assert.IsNotNull(result1);
		Assert.IsNotNull(result2);
		Assert.AreEqual(someObject1, result1);
		Assert.AreEqual(someObject2, result2);
		Assert.AreNotEqual(result1, result2);
	}

	[Test]
	public void Get_Throws_IfNotAdded()
	{
		var id = new IdStruct(0);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id));
	}

	[Test]
	public void Get_Throws_IfAddedDifferentId()
	{
		var someObject = new TestClass();
		var id1 = new IdStruct(0);
		var id2 = new IdStruct(1);

		mContainer.Add(someObject, id1);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id2));
	}

	[Test]
	public void Get_Throws_IfAddedDifferentType()
	{
		var someObject = new TestClass2();
		var id = new IdStruct(0);

		mContainer.Add(someObject, id);

		Assert.Throws<ContainerException>(() => mContainer.Get<TestClass>(id));
	}

	#endregion
}
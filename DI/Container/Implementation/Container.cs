namespace DI.Container.Implementation;

internal class Container : IContainer, IContainerHeredity
{
	private readonly Dictionary<Type, object> mSingleBucket;
	private readonly Dictionary<Type, Dictionary<object, object>> mRegularBucket;
	private readonly IContainerHeredity[] mParentContainers;

	public Container(params IContainerHeredity[] parentContainers)
	{
		mParentContainers = parentContainers;
		mSingleBucket = new Dictionary<Type, object>();
		mRegularBucket = new Dictionary<Type, Dictionary<object, object>>();
	}

	public void Add<T>(T obj, object id) where T : class
	{
		var type = typeof(T);
		if (!mRegularBucket.TryGetValue(type, out var innerBucket))
		{
			innerBucket = new Dictionary<object, object>();
			mRegularBucket.Add(type, innerBucket);
		}

		var alreadyContains = innerBucket.ContainsKey(id);
		if (alreadyContains)
		{
			throw new ContainerException(type, id, $"Instance with id {id} for type {type} already exists in this container");
		}

		innerBucket.Add(id, obj);
	}

	public void Add<T>(T obj) where T : class
	{
		var type = typeof(T);
		var alreadyContains = mSingleBucket.ContainsKey(type);
		if (alreadyContains)
		{
			throw new ContainerException(type, $"Type {type} already exists in this container");
		}

		mSingleBucket.Add(type, obj);
	}

	public T Get<T>() where T : class
	{
		return ((IContainerHeredity)this).Get<T>();
	}

	T IContainerHeredity.Get<T>(bool includeParents)
	{
		var type = typeof(T);
		if (mSingleBucket.TryGetValue(type, out var result))
		{
			return (T)result;
		}

		if (!includeParents)
		{
			throw GetException();
		}
		foreach (var parent in mParentContainers)
		{
			try
			{
				return parent.Get<T>(false);
			}
			catch
			{
				// ignored
			}
		}

		throw GetException();

		ContainerException GetException()
		{
			return new ContainerException(type, $"Could not find type {type} in this container");
		}
	}

	public T Get<T>(object id) where T : class
	{
		return ((IContainerHeredity)this).Get<T>(id);
	}

	T IContainerHeredity.Get<T>(object id, bool includeParents)
	{
		var type = typeof(T);

		if (mRegularBucket.TryGetValue(type, out var innerBucket)
		    && innerBucket.TryGetValue(id, out var result))
		{
			return (T)result;
		}

		if (!includeParents)
		{
			throw GetException();
		}
		foreach (var parent in mParentContainers)
		{
			try
			{
				return parent.Get<T>(id, false);
			}
			catch
			{
				// ignored
			}
		}

		throw GetException();

		ContainerException GetException()
		{
			return new ContainerException(type, id, $"Could not find id {id} for type {type} in this container");
		}
	}
}
﻿namespace DPDI.Container;

public interface IContainer
{
	void Add<T>(T obj, object id) where T : class;
	void Add<T>(T obj) where T : class;

	T Get<T>() where T : class;
	T Get<T>(object id) where T : class;

	bool Has<T>() where T : class;
	bool Has<T>(object id) where T : class;

	void Flush();
}
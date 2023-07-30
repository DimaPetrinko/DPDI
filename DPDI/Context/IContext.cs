using DPDI.Container;

namespace DPDI.Context;

public interface IContext
{
	IContainer BaseLayer { get; }
	IContainer StaticDataLayer { get; }
	IContainer DynamicDataLayer { get; }
	IContainer ModelLayer { get; }
	IContainer PresenterLayer { get; }
	IContainer ViewLayer { get; }
	void Flush();
}
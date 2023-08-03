using System.Collections.Generic;

namespace DPDI.Context;

public interface IContextHeredity
{
	IContextHeredity? ParentContext { get; }
	IEnumerable<IContextHeredity> Children { get; }

	IContextHeredity AddNewChildContext();
	void Flush(bool recursive);
}
using System;
using System.Linq.Expressions;

namespace Services.Projections
{
	public interface IProjection<T, U>
	{
		Expression<Func<T, U>> Predicate { get; }
	}
}

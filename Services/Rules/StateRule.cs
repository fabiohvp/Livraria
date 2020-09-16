using LinqKit;
using Services.Projections;
using System;
using System.Linq.Expressions;

namespace Services.Rules
{
	/// <summary>Interface for Enflow state rules</summary>
	/// <typeparam name="T"></typeparam>
	public interface IStateRule<T> : IProjection<T, bool>
	{
		string Description { get; set; }
		bool IsSatisfied(T candidate);
	}

	public abstract class StateRule<T> : IStateRule<T>
	{
		public abstract Expression<Func<T, bool>> Predicate { get; }
		public string Description { get; set; }
		public bool IsSatisfied(T candidate) { return Predicate.Compile().Invoke(candidate); }
	}

	/// <summary>Composite state rule where both input rules must be satisfied.</summary>
	/// <typeparam name="T"></typeparam>
	public class AndStateRule<T> : StateRule<T>
	{
		private readonly IStateRule<T> _ruleA;
		private readonly IStateRule<T> _ruleB;

		internal AndStateRule(IStateRule<T> ruleA, IStateRule<T> ruleB)
		{
			_ruleA = ruleA;
			_ruleB = ruleB;
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return _ruleA.Predicate.And(_ruleB.Predicate); }
		}
	}

	/// <summary>Composite state rule where at least one of the input rules must be satisfied.</summary>
	/// <typeparam name="T"></typeparam>
	public class OrStateRule<T> : StateRule<T>
	{
		private readonly IStateRule<T> _ruleA;
		private readonly IStateRule<T> _ruleB;

		internal OrStateRule(IStateRule<T> ruleA, IStateRule<T> ruleB)
		{
			_ruleA = ruleA;
			_ruleB = ruleB;
		}

		public override Expression<Func<T, bool>> Predicate
		{
			get { return _ruleA.Predicate.Or(_ruleB.Predicate); }
		}
	}

	/// <summary>State rule that enforces a logical NOT of the input rule.</summary>
	/// <typeparam name="T"></typeparam>
	public class NotStateRule<T> : StateRule<T>
	{
		private readonly IStateRule<T> _rule;
		internal NotStateRule(IStateRule<T> rule) { _rule = rule; }

		public override Expression<Func<T, bool>> Predicate
		{
			get
			{
				var negated = Expression.Not(_rule.Predicate.Body);
				return Expression.Lambda<Func<T, bool>>(negated, _rule.Predicate.Parameters);
			}
		}
	}

	/// <summary>Facilitates the fluent API for composing state rules from atomic constituents.</summary>
	public static class StateRuleFluentExtensions
	{
		public static IStateRule<T> And<T>(this IStateRule<T> ruleA, IStateRule<T> ruleB)
		{
			return new AndStateRule<T>(ruleA, ruleB);
		}

		public static IStateRule<T> Or<T>(this IStateRule<T> ruleA, IStateRule<T> ruleB)
		{
			return new OrStateRule<T>(ruleA, ruleB);
		}

		public static IStateRule<T> Not<T>(this IStateRule<T> rule)
		{
			return new NotStateRule<T>(rule);
		}

		public static IStateRule<T> Describe<T>(this IStateRule<T> rule, string description)
		{
			rule.Description = description;
			return rule;
		}
	}
}

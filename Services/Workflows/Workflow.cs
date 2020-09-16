using Repository;
using Services.Exceptions;
using Services.Rules;

namespace Services.Workflows
{
    /// <summary>Interface for transitioning types through workflows.</summary>
    public interface IWorkflow<T, U>
    {
        U Execute(T candidate);
    }

    public interface IWorkflow<T> : IWorkflow<T, T> { }

    public abstract class Workflow<T, U> : IWorkflow<T, U>
    {
        protected readonly IStateRule<T> PreStateRule;
        protected readonly IRepository Repository;

        protected Workflow(IRepository repository)
        {
            Repository = repository;
        }
        protected Workflow(IRepository repository, IStateRule<T> preStateRule)
            : this(repository)
        {
            PreStateRule = preStateRule;
        }

        /// <summary>Validates the pre-condition state rule and executes the workflow logic.</summary>
        /// <param name="candidate"></param>
        public virtual U Execute(T candidate)
        {
            ValidateStateRule(candidate, PreStateRule);
            return ExecuteWorkflow(candidate);
        }

        /// <summary>Workflow logic not including pre-condition rule validation.</summary>
        /// <param name="candidate"></param>
        protected abstract U ExecuteWorkflow(T candidate);

        private static void ValidateStateRule(T candidate, IStateRule<T> rule)
        {
            if (rule == null) return;
            if (!rule.IsSatisfied(candidate)) throw new StateRuleException(rule.Description);
        }
    }

    public abstract class Workflow<T> : Workflow<T, T>, IWorkflow<T>
    {
        protected Workflow(IRepository repository)
            : base(repository)
        {
        }
        protected Workflow(IRepository repository, IStateRule<T> preStateRule) : base(repository, preStateRule) { }
    }
}

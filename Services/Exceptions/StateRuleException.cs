using System;

namespace Services.Exceptions
{
	public class StateRuleException : Exception
	{
		public StateRuleException(string message) : base(message) { }
	}
}

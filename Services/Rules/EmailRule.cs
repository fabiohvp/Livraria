using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Services.Rules
{
    public class EmailRule : StateRule<string>
    {
        public  readonly Regex Rule = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public override Expression<Func<string, bool>> Predicate => email => Rule.Match(email).Success;
    }
}

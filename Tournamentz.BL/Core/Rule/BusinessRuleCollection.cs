namespace Tournamentz.BL.Core.Rule
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class BusinessRuleCollection : IEnumerable<BusinessRule>
    {
        private readonly List<BusinessRule> _rules;

        public BusinessRuleCollection()
        {
            this._rules = new List<BusinessRule>();
        }

        public void Add(BusinessRule rule)
        {
            this._rules.Add(rule);
        }

        public void Add(BusinessRuleCollection rules)
        {
            foreach (BusinessRule businessRule in rules)
            {
                this.Add(businessRule);
            }
        }

        public bool AllRulesAreObeyed { get { return this._rules.TrueForAll(b => !b.IsBroken); } }

        public IEnumerator<BusinessRule> GetEnumerator()
        {
            return this._rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
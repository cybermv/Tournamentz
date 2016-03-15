namespace Tournamentz.BL.Core.Rule
{
    public sealed class BusinessRule
    {
        public BusinessRule(bool isBroken, string message)
            : this(isBroken, message, string.Empty)
        {
        }

        public BusinessRule(bool isBroken, string message, string affectedProperty)
        {
            IsBroken = isBroken;
            Message = message;
            AffectedProperty = affectedProperty;
        }

        public bool IsBroken { get; set; }

        public string Message { get; set; }

        public string AffectedProperty { get; set; }

        public static implicit operator BusinessRuleCollection(BusinessRule rule)
        {
            BusinessRuleCollection collection = new BusinessRuleCollection();
            collection.Add(rule);
            return collection;
        }
    }
}
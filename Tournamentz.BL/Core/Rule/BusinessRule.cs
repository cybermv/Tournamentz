namespace Tournamentz.BL.Core.Rule
{
    using System.Net.Configuration;

    public sealed class BusinessRule
    {
        public BusinessRule(bool isObeyed, string message)
            : this(isObeyed, message, string.Empty)
        {
        }

        public BusinessRule(bool isObeyed, string message, string affectedProperty)
        {
            this.IsObeyed = isObeyed;
            this.Message = message;
            this.AffectedProperty = affectedProperty;
        }

        public bool IsObeyed { get; private set; }

        public bool IsBroken { get { return !this.IsObeyed; } }

        public string Message { get; private set; }

        public string AffectedProperty { get; private set; }

        public static implicit operator BusinessRuleCollection(BusinessRule rule)
        {
            BusinessRuleCollection collection = new BusinessRuleCollection();
            collection.Add(rule);
            return collection;
        }
    }
}
namespace Tournamentz.BL.Core.Attribute
{
    using DAL.Entity.Core;
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ExistsInTableAttribute : Attribute
    {
        public ExistsInTableAttribute(Type entityType)
        {
            if (!entityType.GetInterfaces().Contains(typeof(IEntity)))
            {
                throw new NotSupportedException(
                    string.Format("The type '{0}' is not an IEntity",
                        entityType.Name));
            }

            this.EntityType = entityType;
        }

        public Type EntityType { get; set; }
    }
}
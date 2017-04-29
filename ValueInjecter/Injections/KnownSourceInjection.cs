using System.Reflection;

namespace Omu.ValueInjecter.Injections
{
    public abstract class KnownSourceInjection<TSource> : IValueInjection
    {
        public object Map(object source, object target)
        {
            Inject((TSource) source, target);
            return target;
        }

        protected abstract void Inject(TSource source, object target);

        protected virtual void SetValue(object target, PropertyInfo tp, object value)
        {
            // override in delta implementation
        }
    }
}
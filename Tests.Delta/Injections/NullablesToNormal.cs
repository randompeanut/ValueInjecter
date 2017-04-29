using System;
using System.Reflection;

namespace Tests.Delta.Injections
{
    public class NullablesToNormal : Omu.ValueInjecter.Delta.Injections.LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return Nullable.GetUnderlyingType(source) == target;
        }

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var val = sp.GetValue(source);
            if (val != null)
            {
                tp.SetValue(target, val);
            }
        }
    }
}
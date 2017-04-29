using System;

namespace Tests.Delta.Injections
{
    public class NormalToNullables : Omu.ValueInjecter.Delta.Injections.LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return source == Nullable.GetUnderlyingType(target);
        }
    }
}
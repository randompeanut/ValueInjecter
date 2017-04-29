using System;

namespace Tests.Delta.Injections
{
    public class IntToEnum : Omu.ValueInjecter.Delta.Injections.LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return source == typeof(int) && target.IsSubclassOf(typeof(Enum));
        }
    }
}
using System;

namespace Tests.Delta.Injections
{
    public class EnumToInt : Omu.ValueInjecter.Delta.Injections.LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return source.IsSubclassOf(typeof(Enum)) && target == typeof(int);
        }
    }
}
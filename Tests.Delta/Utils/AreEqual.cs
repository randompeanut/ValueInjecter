using System.Reflection;
using NUnit.Framework;

namespace Tests.Delta.Utils
{
    public class AreEqual : Omu.ValueInjecter.Delta.Injections.LoopInjection
    {
        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            Assert.AreEqual(sp.GetValue(source), tp.GetValue(target));
        }
    }
}
using System.Collections.Generic;
using System.Reflection;
using Omu.ValueInjecter.Injections;
using Omu.ValueInjecter.Utils;

namespace Omu.ValueInjecter.Delta.Injections
{
    internal class SameNameType : ValueInjection, IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void Inject(object source, object target)
        {
            var sourceProps = source.GetProps();

            var targetType = target.GetType();

            foreach (var sp in sourceProps)
                if (sp.CanRead && sp.GetGetMethod() != null)
                {
                    var tp = targetType.GetProperty(sp.Name);

                    if (tp != null && tp.CanWrite && sp.PropertyType == tp.PropertyType && tp.GetSetMethod() != null)
                        SetValue(source, target, sp, tp);
                }
        }

        protected void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            DeltaResults.Add(new DeltaResult(source, target, sp, tp));
        }
    }
}
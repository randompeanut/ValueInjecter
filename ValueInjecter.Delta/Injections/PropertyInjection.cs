using System.Collections.Generic;
using System.Reflection;

namespace Omu.ValueInjecter.Delta.Injections
{
    public class PropertyInjection : ValueInjecter.Injections.PropertyInjection, IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public PropertyInjection()
        {
        }

        public PropertyInjection(string[] ignoredProps)
        {
            this.ignoredProps = ignoredProps;
        }

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            DeltaResults.Add(new DeltaResult(source, target, sp, tp));
        }
    }
}
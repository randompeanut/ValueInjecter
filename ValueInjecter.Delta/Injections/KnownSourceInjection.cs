using System.Collections.Generic;
using System.Reflection;

namespace Omu.ValueInjecter.Delta.Injections
{
    public abstract class KnownSourceInjection<TSource> : ValueInjecter.Injections.KnownSourceInjection<TSource>,
        IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void SetValue(object target, PropertyInfo tp, object value)
        {
            DeltaResults.Add(new DeltaResult(target, tp, value));
        }
    }
}
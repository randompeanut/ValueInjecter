using System.Collections.Generic;
using System.Reflection;

namespace Omu.ValueInjecter.Delta.Injections
{
    /// <summary>
    ///     FlatLoopInjection, matches unflat properties to flat ( a.b.c => abc )
    ///     override SetValue to control the how the value is set ( do type casting, ignore setting in certain scenarios etc. )
    ///     override Match to control unflat target checking
    /// </summary>
    public class FlatLoopInjection : ValueInjecter.Injections.FlatLoopInjection, IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            DeltaResults.Add(new DeltaResult(source, target, sp, tp));
        }
    }
}
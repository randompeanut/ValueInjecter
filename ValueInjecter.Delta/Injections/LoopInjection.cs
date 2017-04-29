using System.Collections.Generic;
using System.Reflection;

namespace Omu.ValueInjecter.Delta.Injections
{
    /// <summary>
    ///     LoopInjection, by default will match properties with the same name and type;
    ///     override MatchTypes to change type matching;
    ///     override GetTargetProp to change how the target property is determined based on the source property;
    ///     override SetValue to control the how the value is set ( do type casting, ignore setting in certain scenarios etc. )
    /// </summary>
    public class LoopInjection : ValueInjecter.Injections.LoopInjection, IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public LoopInjection()
        {
        }

        public LoopInjection(string[] ignoredProps)
            : base(ignoredProps)
        {
        }

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            DeltaResults.Add(new DeltaResult(source, target, sp, tp));
        }
    }
}
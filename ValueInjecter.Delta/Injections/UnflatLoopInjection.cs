using System;
using System.Collections.Generic;
using System.Reflection;

namespace Omu.ValueInjecter.Delta.Injections
{
    /// <summary>
    ///     UnflatLoopInjection, matches flat properties to unflat ( abc => a.b.c );
    ///     override SetValue to control the how the value is set ( do type casting, ignore setting in certain scenarios etc.
    ///     );
    ///     override Match to control unflat target checking;
    /// </summary>
    public class UnflatLoopInjection : ValueInjecter.Injections.UnflatLoopInjection, IDeltaInjection
    {
        private List<DeltaResult> deltaResults;

        public UnflatLoopInjection()
        {
        }

        /// <summary>
        ///     Create injection and set the creator func
        /// </summary>
        /// <param name="activator">
        ///     creator func, used to create objects along the way if null is encountered, by default
        ///     Activator.CreateIntance is used
        /// </param>
        public UnflatLoopInjection(Func<PropertyInfo, object, object> activator)
            : base(activator)
        {
        }

        public List<DeltaResult> DeltaResults => deltaResults ?? (deltaResults = new List<DeltaResult>());

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            DeltaResults.Add(new DeltaResult(source, target, sp, tp));
        }
    }
}
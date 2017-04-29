using System.Collections.Generic;
using Omu.ValueInjecter.Delta.Injections;
using Omu.ValueInjecter.Injections;

namespace Omu.ValueInjecter.Delta
{
    public static class StaticValueInjecter
    {
        public static IValueInjection DefaultInjection = new SameNameType();
        private static IDeltaInjection currentInjection;

        public static List<DeltaResult> DeltaResults(this object target)
        {
            if (currentInjection == null) return null;
            return currentInjection.DeltaResults;
        }

        /// <summary>
        ///     Injects values from source to target
        /// </summary>
        /// <typeparam name="T">ValueInjection used</typeparam>
        /// <param name="target">target where the value is going to be injected</param>
        /// <param name="source">source from where the value is taken</param>
        /// <returns>the modified target</returns>
        public static object InjectFrom<T>(this object target, object source)
            where T : IValueInjection, new()
        {
            var injection = new T();

            currentInjection = (IDeltaInjection)injection;

            target = injection.Map(source, target);
            return target;
        }

        /// <summary>
        ///     Injects values from source to target
        /// </summary>
        /// <param name="target">target where the value is going to be injected</param>
        /// <param name="injection">ValueInjection used</param>
        /// <param name="source">source from where the value is taken</param>
        /// <returns>the modified target</returns>
        public static object InjectFrom(this object target, IValueInjection injection, object source)
        {
            currentInjection = (IDeltaInjection) injection;

            target = injection.Map(source, target);
            return target;
        }

        /// <summary>
        ///     Injects values into the target
        /// </summary>
        /// <typeparam name="T">ValueInjection(INoSourceValueInjection) used for that</typeparam>
        /// <param name="target">target where the value is going to be injected</param>
        /// <returns>the modified target</returns>
        public static object InjectFrom<T>(this object target) where T : INoSourceInjection, new()
        {
            var injection = new T();

            currentInjection = (IDeltaInjection)injection;

            return new T().Map(target);
        }

        /// <summary>
        ///     Injects value into target without source
        /// </summary>
        /// <param name="target">the target where the value is going to be injected</param>
        /// <param name="injection"> the injection(INoSourceValueInjection) used to inject value</param>
        /// <returns>the modified target</returns>
        public static object InjectFrom(this object target, INoSourceInjection injection)
        {
            currentInjection = (IDeltaInjection)injection;

            return injection.Map(target);
        }

        /// <summary>
        ///     Inject properties with exact same name and type
        /// </summary>
        public static object InjectFrom(this object target, object source)
        {
            currentInjection = (IDeltaInjection)DefaultInjection;

            return DefaultInjection.Map(source, target);
        }
    }
}
using System.Reflection;
using Omu.ValueInjecter.Delta.Enums;

namespace Omu.ValueInjecter.Delta
{
    public class DeltaResult
    {
        private object checkedSourceValue;
        private object checkedTargetValueNew;
        private object checkedTargetValueOld;
        private bool hasCheckedSourceValue;
        private bool hasCheckedTargetValueNew;
        private bool hasCheckedTargetValueOld;

        public DeltaResult(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var deltaResultTypeSet = false;

            Source = source;
            SourceProperty = sp;
            Target = target;
            TargetProperty = tp;

            if (TargetValueOld == null && SourceValue != null)
            {
                DeltaResultType = DeltaResultType.New;
                deltaResultTypeSet = true;
            }
            if (TargetValueOld != null && SourceValue == null)
            {
                DeltaResultType = DeltaResultType.Deleted;
                deltaResultTypeSet = true;
            }
            if (TargetValueOld == SourceValue)
            {
                DeltaResultType = DeltaResultType.Unchanged;
                deltaResultTypeSet = true;
            }

            if (DeltaResultType == DeltaResultType.Unchanged) return;

            TargetProperty.SetValue(Target, SourceValue);

            if (deltaResultTypeSet) return;

            DeltaResultType = DeltaResultType.New;
        }

        public DeltaResult(object target, PropertyInfo tp, object value)
        {
            var deltaResultTypeSet = false;

            Target = target;
            TargetProperty = tp;

            if (TargetValueOld == null && value != null)
            {
                DeltaResultType = DeltaResultType.New;
                deltaResultTypeSet = true;
            }
            if (TargetValueOld != null && value == null)
            {
                DeltaResultType = DeltaResultType.Deleted;
                deltaResultTypeSet = true;
            }
            if (TargetValueOld == value)
            {
                DeltaResultType = DeltaResultType.Unchanged;
                deltaResultTypeSet = true;
            }

            if (DeltaResultType == DeltaResultType.Unchanged) return;

            TargetProperty.SetValue(Target, value);

            if (deltaResultTypeSet) return;

            DeltaResultType = DeltaResultType.New;
        }

        public DeltaResultType DeltaResultType { get; }

        public PropertyInfo ParentProperty { get; }

        public object Source { get; }

        public object SourceValue
        {
            get
            {
                var returnValue = hasCheckedSourceValue
                    ? checkedSourceValue
                    : (checkedSourceValue = SourceProperty.GetValue(Source, null));

                if (!hasCheckedSourceValue) hasCheckedSourceValue = true;

                return returnValue;
            }
        }

        public PropertyInfo SourceProperty { get; }

        public object Target { get; }

        public object TargetValueOld
        {
            get
            {
                var returnValue = hasCheckedTargetValueOld
                    ? checkedTargetValueOld
                    : (checkedTargetValueOld = TargetProperty.GetValue(Target, null));

                if (!hasCheckedTargetValueOld) hasCheckedTargetValueOld = true;

                return returnValue;
            }
        }

        public object TargetValueNew
        {
            get
            {
                var returnValue = hasCheckedTargetValueNew
                    ? checkedTargetValueNew
                    : (checkedTargetValueNew = TargetProperty.GetValue(Target, null));

                if (!hasCheckedTargetValueNew) hasCheckedTargetValueNew = true;

                return returnValue;
            }
        }

        public PropertyInfo TargetProperty { get; }
    }
}
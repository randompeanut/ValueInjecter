using System.Collections.Generic;

namespace Omu.ValueInjecter.Delta.Injections
{
    public interface IDeltaInjection
    {
        List<DeltaResult> DeltaResults { get; }
    }
}
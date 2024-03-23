using System;
using System.Collections.Generic;

namespace BuberBreakfast.Contracts.BuberBreakfast
{
    public record CreateBreakfastResponse
    (
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime,
        List<string> Savory,
        List<string> Sweet
    );
}

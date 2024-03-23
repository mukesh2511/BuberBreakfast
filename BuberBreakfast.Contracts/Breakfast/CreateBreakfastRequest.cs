using System;
using System.Collections.Generic; // Add this namespace

namespace BuberBreakfast.Contracts.BuberBreakfast
{
    public record CreateBreakfastRequest(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> Savory,
        List<string> Sweet
    );
}

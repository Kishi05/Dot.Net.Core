using Microsoft.AspNetCore.Mvc.Filters;

namespace Section21.Filters.OverrideDummyFilter
{
    // Skip‑Filter pattern: mark an action or controller with a dummy [SkipFilter] attribute; each filter checks context.Filters for that attribute and, if present, omits its own processing.

    public class SkipFilter : Attribute , IFilterMetadata
    {
    }
}

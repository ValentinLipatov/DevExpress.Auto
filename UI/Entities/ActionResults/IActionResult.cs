using System;

namespace XML
{
    public interface IActionResult
    {
        IField Field { get; }

        string Description { get; }

        ActionResultType Type { get; }
    }
}
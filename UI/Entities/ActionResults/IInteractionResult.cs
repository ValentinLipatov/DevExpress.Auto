using System.Collections.Generic;

namespace XML
{
    public interface IInteractionResult
    {
        IList<IActionResult> ActionResults { get; }

        IList<IActionResult> Warnings { get; }

        IList<IActionResult> Errors { get; }

        public bool Success { get; }

        public bool SuccessOrWarning { get; }

        public bool Error { get; }

        void Add(IActionResult actionResult);
    }
}
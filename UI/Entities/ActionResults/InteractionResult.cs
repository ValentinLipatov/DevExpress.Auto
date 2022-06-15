using System.Collections.Generic;
using System.Linq;

namespace XML
{
    public class InteractionResult : IInteractionResult
    {
        public IList<IActionResult> ActionResults { get; protected set; } = new List<IActionResult>();
        
        public IList<IActionResult> Warnings => ActionResults.Where(e => e.Type == ActionResultType.Warning).ToList();

        public IList<IActionResult> Errors => ActionResults.Where(e => e.Type == ActionResultType.Error).ToList();
        
        public bool Success => !ActionResults.Any();
        
        public bool SuccessOrWarning => ActionResults.All(e => e.Type == ActionResultType.Warning);
        
        public bool Error => ActionResults.Any(e => e.Type == ActionResultType.Error);

        public void Add(IActionResult actionResult)
        {
            ActionResults.Add(actionResult);
        }
    }
}
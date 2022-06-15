using System;

namespace XML
{
    public static class InteractionResultHelper
    {
        public static void AddWarning(this IInteractionResult interactionResult, IField field, string description)
        {
            interactionResult.Add(new ActionResult(field, description, ActionResultType.Warning));
        }

        public static void AddError(this IInteractionResult interactionResult, IField field, string description)
        {
            interactionResult.Add(new ActionResult(field, description, ActionResultType.Error));
        }

        public static void Add(this IInteractionResult interactionResult, IField field, string description, ActionResultType type)
        {
            interactionResult.Add(new ActionResult(field, description, type));
        }
    }
}
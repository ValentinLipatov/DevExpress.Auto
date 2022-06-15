using System;

namespace XML
{
    public class PredicateAction : Action
    {
        public PredicateAction(IEntity entity, string name, string caption, System.Action predicate) : base (entity, name, caption)
        {
            Predicate = predicate;
        }

        public System.Action Predicate { get; set; }

        public override void Execute()
        {
            Predicate?.Invoke();
        }
    }
}
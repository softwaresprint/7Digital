namespace Digital7.Shopping
{
    using System.Collections.Generic;

    public class RuleFactory : IRuleCreator
    {
        private readonly IRuleCreator ruleCreator;

        public RuleFactory()
        {
            this.ruleCreator = new RuleXmlCreator();
        }

        public RuleFactory(IRuleCreator creator)
        {
            this.ruleCreator = creator;
        }

        public List<Rule> Load()
        {
            return this.ruleCreator.Load();
        }
    }
}
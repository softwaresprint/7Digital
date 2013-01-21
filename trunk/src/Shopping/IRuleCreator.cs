namespace Digital7.Shopping
{
    using System.Collections.Generic;

    public interface IRuleCreator
    {
        List<Rule> Load();
    }
}

using System;
using System.Linq.Expressions;

namespace PSMore.Formatting
{
    public interface ICondition
    {
        bool Applies(object o);
    }
}

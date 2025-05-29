using System.Linq.Expressions;

namespace KKBookstore.Services;

internal class ReplaceParameterVisitor : ExpressionVisitor
{
    private readonly Expression _oldParameter;
    private readonly Expression _newParameter;

    private ReplaceParameterVisitor(Expression oldParameter, Expression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    public static Expression Replace(Expression expression, Expression oldParameter, Expression newParameter)
    {
        return new ReplaceParameterVisitor(oldParameter, newParameter).Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _oldParameter ? _newParameter : base.VisitParameter(node);
    }
}
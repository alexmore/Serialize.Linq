#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Serialize.Linq.Interfaces;

namespace Serialize.Linq.Nodes
{
    public class ParameterExpressionNode : ExpressionNode<ParameterExpression>
    {
        public ParameterExpressionNode() { }

        public ParameterExpressionNode(INodeFactory factory, ParameterExpression expression)
            : base(factory, expression) { }

        public bool IsByRef { get; set; }
        public string Name { get; set; }

        protected override void Initialize(ParameterExpression expression)
        {
            this.IsByRef = expression.IsByRef;
            this.Name = expression.Name;
        }

        public override Expression ToExpression(ExpressionContext context)
        {
            return context.GetParameterExpression(this);
        }
    }
}

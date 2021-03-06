﻿#region Copyright
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
    public class BinaryExpressionNode : ExpressionNode<BinaryExpression>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpressionNode"/> class.
        /// </summary>
        public BinaryExpressionNode() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpressionNode"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="expression">The expression.</param>
        public BinaryExpressionNode(INodeFactory factory, BinaryExpression expression)
            : base(factory, expression) { }


        /// <summary>
        /// Gets or sets the conversion.
        /// </summary>
        /// <value>
        /// The conversion.
        /// </value>        
        public ExpressionNode Conversion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is lifted to null.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is lifted to null; otherwise, <c>false</c>.
        /// </value>        
        public bool IsLiftedToNull { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public ExpressionNode Left { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public MethodInfoNode Method { get; set; }



        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public ExpressionNode Right { get; set; }

        /// <summary>
        /// Initializes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        protected override void Initialize(BinaryExpression expression)
        {
            this.Left = this.Factory.Create(expression.Left);
            this.Right = this.Factory.Create(expression.Right);
            this.Conversion = this.Factory.Create(expression.Conversion);
            this.Method = new MethodInfoNode(this.Factory, expression.Method);
            this.IsLiftedToNull = expression.IsLiftedToNull;
        }

        /// <summary>
        /// Converts this instance to an expression.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Expression ToExpression(ExpressionContext context)
        {
            var conversion = this.Conversion != null ? this.Conversion.ToExpression() as LambdaExpression : null;
            if (this.Method != null && conversion != null)
                return Expression.MakeBinary(
                    this.NodeType,
                    this.Left.ToExpression(context), this.Right.ToExpression(context),
                    this.IsLiftedToNull,
                    this.Method.ToMemberInfo(context),
                    conversion);
            if (this.Method != null)
                return Expression.MakeBinary(
                    this.NodeType,
                    this.Left.ToExpression(context), this.Right.ToExpression(context),
                    this.IsLiftedToNull,
                    this.Method.ToMemberInfo(context));
            return Expression.MakeBinary(this.NodeType,
                    this.Left.ToExpression(context), this.Right.ToExpression(context));
        }
    }
}

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
    public class MemberAssignmentNode : MemberBindingNode
    {
        public MemberAssignmentNode() { }

        public MemberAssignmentNode(INodeFactory factory, MemberAssignment memberAssignment)
            : base(factory, memberAssignment.BindingType, memberAssignment.Member)
        {
            this.Expression = this.Factory.Create(memberAssignment.Expression);
        }

        public ExpressionNode Expression { get; set; }

        internal override MemberBinding ToMemberBinding(ExpressionContext context)
        {
            return System.Linq.Expressions.Expression.Bind(this.Member.ToMemberInfo(context), this.Expression.ToExpression(context));
        }
    }
}
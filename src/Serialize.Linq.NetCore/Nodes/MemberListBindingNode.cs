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
    public class MemberListBindingNode : MemberBindingNode
    {
        public MemberListBindingNode() { }

        public MemberListBindingNode(INodeFactory factory, MemberListBinding memberListBinding)
            : base(factory, memberListBinding.BindingType, memberListBinding.Member)
        {
            this.Initializers = new ElementInitNodeList(this.Factory, memberListBinding.Initializers);
        }

        public ElementInitNodeList Initializers { get; set; }

        internal override MemberBinding ToMemberBinding(ExpressionContext context)
        {
            return Expression.ListBind(this.Member.ToMemberInfo(context), this.Initializers.GetElementInits(context));
        }
    }
}

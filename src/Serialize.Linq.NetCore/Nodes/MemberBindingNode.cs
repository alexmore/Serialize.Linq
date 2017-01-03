﻿#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Serialize.Linq.Interfaces;

namespace Serialize.Linq.Nodes
{
    public abstract class MemberBindingNode : Node
    {
        protected MemberBindingNode() { }

        protected MemberBindingNode(INodeFactory factory)
            : base(factory) { }

        protected MemberBindingNode(INodeFactory factory, MemberBindingType bindingType, MemberInfo memberInfo)
            : base(factory)
        {
            this.BindingType = bindingType;
            this.Member = new MemberInfoNode(this.Factory, memberInfo);
        }

        public MemberBindingType BindingType { get; set; }
        public MemberInfoNode Member { get; set; }

        internal abstract MemberBinding ToMemberBinding(ExpressionContext context);

        internal static MemberBindingNode Create(INodeFactory factory, MemberBinding memberBinding)
        {
            MemberBindingNode memberBindingNode = null;

            if (memberBinding is MemberAssignment)
                memberBindingNode = new MemberAssignmentNode(factory, (MemberAssignment)memberBinding);
            else if (memberBinding is MemberListBinding)
                memberBindingNode = new MemberListBindingNode(factory, (MemberListBinding)memberBinding);
            else if (memberBinding is MemberMemberBinding)
                memberBindingNode = new MemberMemberBindingNode(factory, (MemberMemberBinding)memberBinding);
            else if (memberBinding != null)
                throw new ArgumentException("Unknown member binding of type " + memberBinding.GetType(), "memberBinding");

            return memberBindingNode;
        }
    }
}
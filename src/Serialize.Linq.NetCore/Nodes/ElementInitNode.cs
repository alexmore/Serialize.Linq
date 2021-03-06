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
    public class ElementInitNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementInitNode"/> class.
        /// </summary>
        public ElementInitNode() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementInitNode"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="elementInit">The element init.</param>
        public ElementInitNode(INodeFactory factory, ElementInit elementInit)
            : base(factory)
        {
            this.Initialize(elementInit);
        }

        /// <summary>
        /// Initializes the specified element init.
        /// </summary>
        /// <param name="elementInit">The element init.</param>
        /// <exception cref="System.ArgumentNullException">elementInit</exception>
        private void Initialize(ElementInit elementInit)
        {
            if (elementInit == null)
                throw new ArgumentNullException("elementInit");

            this.AddMethod = new MethodInfoNode(this.Factory, elementInit.AddMethod);
            this.Arguments = new ExpressionNodeList(this.Factory, elementInit.Arguments);
        }

        
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>       
        public ExpressionNodeList Arguments { get; set; }
        
        /// <summary>
        /// Gets or sets the add method.
        /// </summary>
        /// <value>
        /// The add method.
        /// </value>
        public MethodInfoNode AddMethod { get; set; }

        internal ElementInit ToElementInit(ExpressionContext context)
        {
            return Expression.ElementInit(this.AddMethod.ToMemberInfo(context), this.Arguments.GetExpressions(context));
        }
    }
}

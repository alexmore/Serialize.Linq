#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Serialize.Linq.Factories;
using Serialize.Linq.Interfaces;
using Serialize.Linq.Nodes;
using Serialize.Linq.Serializers;

namespace Serialize.Linq
{
    public class ExpressionSerializer : ExpressionConverter
    {
        private readonly ISerializer _serializer;
        private readonly FactorySettings _factorySettings;
        private readonly ExpressionContext context;

        public ExpressionSerializer(ISerializer serializer, ExpressionContext context, FactorySettings factorySettings = null)
        {
            this._serializer = serializer != null ? serializer : new JsonSerializer();
            this.context = context != null ? context : new ExpressionContext();
            _factorySettings = factorySettings;
        }

        public ExpressionSerializer(ISerializer serializer, FactorySettings factorySettings = null) : this(serializer, null, factorySettings) { }
        public ExpressionSerializer(ExpressionContext context, FactorySettings factorySettings = null) : this(null, context, factorySettings) { }
        public ExpressionSerializer(FactorySettings factorySettings = null) : this(null, null, factorySettings) { }

        public void Serialize(Stream stream, Expression expression, FactorySettings factorySettings = null)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            _serializer.Serialize(stream, this.Convert(expression, factorySettings ?? _factorySettings));
        }

        public string Serialize(Expression expression, FactorySettings factorySettings = null)
        {
            return _serializer.Serialize(this.Convert(expression, factorySettings ?? _factorySettings));
        }

        public Expression Deserialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var node = _serializer.Deserialize<ExpressionNode>(stream);
            return node != null ? node.ToExpression(context) : null;
        }

        public Expression Deserialize(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            var node = _serializer.Deserialize<ExpressionNode>(text);
            return node != null ? node.ToExpression(context) : null;
        }
    }
}
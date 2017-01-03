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
using Serialize.Linq.Nodes;

namespace Serialize.Linq.Interfaces
{
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the specified object to the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="obj">The obj.</param>
        void Serialize<T>(Stream stream, T obj) where T : Node;
        /// <summary>
        /// Serializes the specified object to the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize<T>(T obj) where T : Node;

        /// <summary>
        /// Deserializes an object of type T from the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        T Deserialize<T>(Stream stream) where T : Node;
        /// <summary>
        ///  Deserializes an object of type T from the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        T Deserialize<T>(string text) where T : Node;
    }
}

/*
* DISCLAIMER
*
* Copyright 2016 ArangoDB GmbH, Cologne, Germany
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* Copyright holder is ArangoDB GmbH, Cologne, Germany
*/

namespace VelocyPack
{
    using System;
    using VelocyPack.Exceptions;

    public interface IVPackSerializer
    {
        void Serialize(VPackBuilder builder, string attribute, object value, IVPackSerializationContext context);
    }

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class VPackSerializer<T> : IVPackSerializer
    {
        /// <exception cref="VPackException"/>
        public abstract void Serialize(VPackBuilder builder, string attribute, T value, IVPackSerializationContext context);

        void IVPackSerializer.Serialize(VPackBuilder builder, string attribute, object value, IVPackSerializationContext context)
        {
            Serialize(builder, attribute, (T)value, context);
        }
    }
}
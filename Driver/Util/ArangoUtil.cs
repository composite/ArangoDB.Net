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

namespace ArangoDB.Util
{
    using System.Reflection;

    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
    public class ArangoUtil
    {
        private readonly VPack vpacker;

        private readonly VPack vpackerNull;

        private readonly VPackParser vpackParser;

        public ArangoUtil(VPack vpacker, VPack vpackerNull, VPackParser vpackParser)
            : base()
        {
            this.vpacker = vpacker;
            this.vpackerNull = vpackerNull;
            this.vpackParser = vpackParser;
        }

        /// <summary>Deserialze a given VelocPack to an instance of a given type</summary>
        /// <param name="vpack">The VelocyPack to deserialize</param>
        /// <param name="type">The target type to deserialize to. Use String for raw Json.</param>
        /// <returns>The deserialized VelocyPack</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual T Deserialize<T>(VPackSlice vpack)
        {
            try
            {
                T doc;
                if (typeof(T) == typeof(string) && !vpack.isString())
                {
                    doc = (T)(object)this.vpackParser.toJson(vpack);
                }
                else
                {
                    doc = this.vpacker.deserialize<T>(vpack);
                }
                return doc;
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <summary>Serialize a given Object to VelocyPack</summary>
        /// <param name="entity">The Object to serialize. If it is from type String, it will be handled as a Json.
        /// 	</param>
        /// <returns>The serialized VelocyPack</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual VPackSlice Serialize(object entity)
        {
            try
            {
                VPackSlice vpack;
                if (entity is string || typeof(string).IsCastableTo(entity.GetType()) || typeof(string).IsCastableTo(entity.GetType(), true))
                {
                    vpack = this.vpackParser.fromJson((string)entity);
                }
                else
                {
                    vpack = this.vpacker.serialize(entity);
                }
                return vpack;
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <summary>Serialize a given Object to VelocyPack</summary>
        /// <param name="entity">The Object to serialize. If it is from type String, it will be handled as a Json.
        /// 	</param>
        /// <param name="serializeNullValues">Whether or not null values should be excluded from serialization.
        /// 	</param>
        /// <returns>the serialized VelocyPack</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual VPackSlice Serialize(object entity, bool serializeNullValues)
        {
            try
            {
                VPackSlice vpack;
                if (entity is string || typeof(string).IsCastableTo(entity.GetType()) || typeof(string).IsCastableTo(entity.GetType(), true))
                {
                    vpack = this.vpackParser.fromJson((string)entity, serializeNullValues);
                }
                else
                {
                    VPack vp = serializeNullValues ? this.vpackerNull : this.vpacker;
                    vpack = vp.serialize(entity);
                }
                return vpack;
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <summary>Serialize a given Object to VelocyPack.</summary>
        /// <remarks>
        /// Serialize a given Object to VelocyPack. This method is for serialization of types with generic parameter like
        /// Collection, List, Map.
        /// </remarks>
        /// <param name="entity">The Object to serialize</param>
        /// <param name="type">The source type of the Object.</param>
        /// <returns>the serialized VelocyPack</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual VPackSlice Serialize(object entity, global::System.Type type)
        {
            try
            {
                return this.vpacker.serialize(entity, type);
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <summary>Serialize a given Object to VelocyPack.</summary>
        /// <remarks>
        /// Serialize a given Object to VelocyPack. This method is for serialization of types with generic parameter like
        /// Collection, List, Map.
        /// </remarks>
        /// <param name="entity">The Object to serialize</param>
        /// <param name="type">The source type of the Object.</param>
        /// <param name="serializeNullValues">Whether or not null values should be excluded from serialization.
        /// 	</param>
        /// <returns>the serialized VelocyPack</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual VPackSlice Serialize(object entity, global::System.Type type, bool serializeNullValues)
        {
            try
            {
                VPack vp = serializeNullValues ? this.vpackerNull : this.vpacker;
                return vp.serialize(entity, type);
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <summary>Serialize a given Object to VelocyPack.</summary>
        /// <remarks>
        /// Serialize a given Object to VelocyPack. This method is for serialization of types with generic parameter like
        /// Collection, List, Map.
        /// </remarks>
        /// <param name="entity">The Object to serialize</param>
        /// <param name="type">The source type of the Object.</param>
        /// <param name="serializeNullValues">Whether or not null values should be excluded from serialization.
        /// 	</param>
        /// <param name="additionalFields">Additional Key/Value pairs to include in the created VelocyPack
        /// 	</param>
        /// <returns>the serialized VelocyPack</returns>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual VPackSlice Serialize(object entity, global::System.Type type, System.Collections.Generic.IDictionary<string, object> additionalFields)
        {
            try
            {
                return this.vpacker.serialize(entity, type, additionalFields);
            }
            catch (VPackParserException e)
            {
                throw new ArangoDBException(e);
            }
        }
    }
}
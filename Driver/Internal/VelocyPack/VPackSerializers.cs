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

namespace ArangoDB.Internal.VelocyPack
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackSerializers
    {
        private sealed class _VPackSerializer_48 : VPackSerializer
            <Request>
        {
            public _VPackSerializer_48()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , Request value, VPackSerializationContext
                 context)
            {
                builder.Add(attribute, ValueType.ARRAY);
                builder.Add(value.getVersion());
                builder.Add(value.getType());
                builder.Add(value.getDatabase());
                builder.Add(value.getRequestType().getType());
                builder.Add(value.getRequest());
                builder.Add(ValueType.OBJECT);
                foreach (System.Collections.Generic.KeyValuePair<string, string> entry in value.getQueryParam
                    ())
                {
                    builder.Add(entry.Key, entry.Value);
                }
                builder.Close();
                builder.Add(ValueType.OBJECT);
                foreach (System.Collections.Generic.KeyValuePair<string, string> entry_1 in value
                    .getHeaderParam())
                {
                    builder.Add(entry_1.Key, entry_1.Value);
                }
                builder.Close();
                builder.Close();
            }
        }

        public static readonly VPackSerializer<Request
            > REQUEST = new _VPackSerializer_48();

        private sealed class _VPackSerializer_75 : VPackSerializer
            <AuthenticationRequest>
        {
            public _VPackSerializer_75()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , AuthenticationRequest value, VPackSerializationContext
                 context)
            {
                builder.Add(attribute, ValueType.ARRAY);
                builder.Add(value.getVersion());
                builder.Add(value.getType());
                builder.Add(value.getEncryption());
                builder.Add(value.getUser());
                builder.Add(value.getPassword());
                builder.Close();
            }
        }

        public static readonly VPackSerializer<AuthenticationRequest
            > AUTH_REQUEST = new _VPackSerializer_75();

        private sealed class _VPackSerializer_92 : VPackSerializer
            <CollectionType>
        {
            public _VPackSerializer_92()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , CollectionType value, VPackSerializationContext
                 context)
            {
                builder.Add(attribute, value.getType());
            }
        }

        public static readonly VPackSerializer<CollectionType
            > COLLECTION_TYPE = new _VPackSerializer_92();

        private sealed class _VPackSerializer_103 : VPackSerializer
            <BaseDocument>
        {
            public _VPackSerializer_103()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , BaseDocument value, VPackSerializationContext
                 context)
            {
                System.Collections.Generic.IDictionary<string, object> doc = new System.Collections.Generic.Dictionary
                    <string, object>();
                doc.putAll(value.getProperties());
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.ID.getSerializeName()] = value.getId();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.KEY.getSerializeName()] = value.getKey
                    ();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.REV.getSerializeName()] = value.getRevision
                    ();
                context.serialize(builder, attribute, doc);
            }
        }

        public static readonly VPackSerializer<BaseDocument
            > BASE_DOCUMENT = new _VPackSerializer_103();

        private sealed class _VPackSerializer_119 : VPackSerializer
            <BaseEdgeDocument>
        {
            public _VPackSerializer_119()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , BaseEdgeDocument value, VPackSerializationContext
                 context)
            {
                System.Collections.Generic.IDictionary<string, object> doc = new System.Collections.Generic.Dictionary
                    <string, object>();
                doc.putAll(value.getProperties());
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.ID.getSerializeName()] = value.getId();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.KEY.getSerializeName()] = value.getKey
                    ();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.REV.getSerializeName()] = value.getRevision
                    ();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.FROM.getSerializeName()] = value.getFrom
                    ();
                doc[com.arangodb.entity.DocumentFieldAttribute.Type.TO.getSerializeName()] = value.getTo();
                context.serialize(builder, attribute, doc);
            }
        }

        public static readonly VPackSerializer<BaseEdgeDocument
            > BASE_EDGE_DOCUMENT = new _VPackSerializer_119();

        private sealed class _VPackSerializer_137 : VPackSerializer
            <TraversalOptions.Order>
        {
            public _VPackSerializer_137()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , TraversalOptions.Order value, VPackSerializationContext
                 context)
            {
                if (TraversalOptions.Order.preorder_expander == value)
                {
                    builder.Add(attribute, "preorder-expander");
                }
                else
                {
                    builder.Add(attribute, value.ToString());
                }
            }
        }

        public static readonly VPackSerializer<TraversalOptions.Order
            > TRAVERSAL_ORDER = new _VPackSerializer_137();

        private sealed class _VPackSerializer_152 : VPackSerializer
            <LogLevel>
        {
            public _VPackSerializer_152()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , LogLevel value, VPackSerializationContext
                 context)
            {
                builder.Add(attribute, value.getLevel());
            }
        }

        public static readonly VPackSerializer<LogLevel
            > LOG_LEVEL = new _VPackSerializer_152();
    }
}
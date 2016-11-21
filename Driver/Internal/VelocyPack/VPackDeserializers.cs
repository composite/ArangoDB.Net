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
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackDeserializers
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(VPackDeserializers
        ));

        private const string DATE_TIME_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.SSSZ";

        private sealed class _VPackDeserializer_51 : VPackDeserializer
            <Response>
        {
            public _VPackDeserializer_51()
            {
            }

            /// <exception cref="VPackException"/>
            public Response deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                Response response = new Response
                    ();
                response.setVersion(vpack.get(0).getAsInt());
                response.setType(vpack.get(1).getAsInt());
                response.setResponseCode(vpack.get(2).getAsInt());
                return response;
            }
        }

        public static readonly VPackDeserializer<Response
            > RESPONSE = new _VPackDeserializer_51();

        private sealed class _VPackDeserializer_65 : VPackDeserializer
            <CollectionType>
        {
            public _VPackDeserializer_65()
            {
            }

            /// <exception cref="VPackException"/>
            public CollectionType deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return CollectionType.fromType(vpack.getAsInt());
            }
        }

        public static readonly VPackDeserializer<CollectionType
            > COLLECTION_TYPE = new _VPackDeserializer_65();

        private sealed class _VPackDeserializer_75 : VPackDeserializer
            <CollectionStatus>
        {
            public _VPackDeserializer_75()
            {
            }

            /// <exception cref="VPackException"/>
            public CollectionStatus deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return CollectionStatus.fromStatus(vpack.getAsInt());
            }
        }

        public static readonly VPackDeserializer<CollectionStatus
            > COLLECTION_STATUS = new _VPackDeserializer_75();

        private sealed class _VPackDeserializer_86 : VPackDeserializer
            <BaseDocument>
        {
            public _VPackDeserializer_86()
            {
            }

            /// <exception cref="VPackException"/>
            public BaseDocument deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return new BaseDocument(context.deserialize<System.Collections.IDictionary
                    >(vpack));
            }
        }

        public static readonly VPackDeserializer<BaseDocument
            > BASE_DOCUMENT = new _VPackDeserializer_86();

        private sealed class _VPackDeserializer_97 : VPackDeserializer
            <BaseEdgeDocument>
        {
            public _VPackDeserializer_97()
            {
            }

            /// <exception cref="VPackException"/>
            public BaseEdgeDocument deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return new BaseEdgeDocument(context.deserialize<System.Collections.IDictionary
                    >(vpack));
            }
        }

        public static readonly VPackDeserializer<BaseEdgeDocument
            > BASE_EDGE_DOCUMENT = new _VPackDeserializer_97();

        private sealed class _VPackDeserializer_107 : VPackDeserializer
            <System.DateTime>
        {
            public _VPackDeserializer_107()
            {
            }

            /// <exception cref="VPackException"/>
            public System.DateTime deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                try
                {
                    return new java.text.SimpleDateFormat(VPackDeserializers
                        .DATE_TIME_FORMAT).parse(vpack.getAsString());
                }
                catch (java.text.ParseException)
                {
                    if (VPackDeserializers.LOGGER.isDebugEnabled())
                    {
                        VPackDeserializers.LOGGER.debug("got ParseException for date string: "
                             + vpack.getAsString());
                    }
                }
                return null;
            }
        }

        public static readonly VPackDeserializer<System.DateTime>
             DATE_STRING = new _VPackDeserializer_107();

        private sealed class _VPackDeserializer_124 : VPackDeserializer
            <LogLevel>
        {
            public _VPackDeserializer_124()
            {
            }

            /// <exception cref="VPackException"/>
            public LogLevel deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return LogLevel.fromLevel(vpack.getAsInt());
            }
        }

        public static readonly VPackDeserializer<LogLevel
            > LOG_LEVEL = new _VPackDeserializer_124();
    }
}
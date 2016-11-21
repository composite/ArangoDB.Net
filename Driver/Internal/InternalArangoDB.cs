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

namespace ArangoDB.Internal
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocystream;
    using System.Collections.Generic;

    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	/// <?/>
	/// <?/>
	public class InternalArangoDB<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        public InternalArangoDB(E executor)
            : base(executor)
        {
        }

        protected internal virtual Request createDatabaseRequest
            (string name)
        {
            Request request = new Request
                (ArangoDBConstants.SYSTEM, RequestType
                .POST, ArangoDBConstants.PATH_API_DATABASE);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(new DBCreateOptions
                (), name)));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <bool> createDatabaseResponseDeserializer()
        {
            return new _ResponseDeserializer_65();
        }

        private sealed class _ResponseDeserializer_65 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <bool>
        {
            public _ResponseDeserializer_65()
            {
            }

            /// <exception cref="VPackException"/>
            public bool deserialize(Response response)
            {
                return response.getBody().get(ArangoDBConstants.RESULT).getAsBoolean
                    ();
            }
        }

        protected internal virtual Request getDatabasesRequest(
            string database)
        {
            return new Request(database, RequestType
                .GET, ArangoDBConstants.PATH_API_DATABASE);
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>> getDatabaseResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_78(this);
        }

        private sealed class _ResponseDeserializer_78 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>>
        {
            public _ResponseDeserializer_78(InternalArangoDB<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<string> deserialize(Response
                 response)
            {
                VPackSlice result = response.getBody().get(ArangoDBConstants
                    .RESULT);
                return this._enclosing.executor.Deserialize(result, new _Type_82().getType());
            }

            private sealed class _Type_82 : Type<System.Collections.Generic.ICollection
                <string>>
            {
                public _Type_82()
                {
                }
            }

            private readonly InternalArangoDB<E, R, C> _enclosing;
        }

        protected internal virtual Request getAccessibleDatabasesRequest
            (string database)
        {
            return new Request(database, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_DATABASE
                , ArangoDBConstants.USER));
        }

        protected internal virtual Request getAccessibleDatabasesForRequest
            (string database, string user)
        {
            return new Request(database, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_USER
                , user, ArangoDBConstants.DATABASE));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>> getAccessibleDatabasesForResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_99();
        }

        private sealed class _ResponseDeserializer_99 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>>
        {
            public _ResponseDeserializer_99()
            {
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<string> deserialize(Response
                 response)
            {
                VPackSlice result = response.getBody().get(ArangoDBConstants
                    .RESULT);
                System.Collections.Generic.ICollection<string> dbs = new System.Collections.Generic.List
                    <string>();
                for (System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> iterator = result.objectIterator()
                    ; iterator.MoveNext();)
                {
                    dbs.add(iterator.Current.Key);
                }
                return dbs;
            }
        }

        protected internal virtual Request getVersionRequest()
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.GET, ArangoDBConstants
                .PATH_API_VERSION);
        }

        protected internal virtual Request createUserRequest(string
             database, string user, string passwd, UserCreateOptions options
            )
        {
            Request request;
            request = new Request(database, RequestType
                .POST, ArangoDBConstants.PATH_API_USER);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new UserCreateOptions(), user, passwd)));
            return request;
        }

        protected internal virtual Request deleteUserRequest(string
             database, string user)
        {
            return new Request(database, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_USER
                , user));
        }

        protected internal virtual Request getUsersRequest(string
             database)
        {
            return new Request(database, RequestType
                .GET, ArangoDBConstants.PATH_API_USER);
        }

        protected internal virtual Request getUserRequest(string
             database, string user)
        {
            return new Request(database, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_USER
                , user));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<UserEntity>> getUsersResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_142(this);
        }

        private sealed class _ResponseDeserializer_142 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<UserEntity>>
        {
            public _ResponseDeserializer_142(InternalArangoDB<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<UserEntity> deserialize
                (Response response)
            {
                VPackSlice result = response.getBody().get(ArangoDBConstants
                    .RESULT);
                return this._enclosing.executor.Deserialize(result, new _Type_146().getType());
            }

            private sealed class _Type_146 : Type<ICollection<UserEntity>>
            {
                public _Type_146()
                {
                }
            }

            private readonly InternalArangoDB<E, R, C> _enclosing;
        }

        protected internal virtual Request updateUserRequest(string
             database, string user, UserUpdateOptions options)
        {
            Request request;
            request = new Request(database, RequestType
                .PATCH, this.executor.createPath(ArangoDBConstants.PATH_API_USER
                , user));
            request.setBody(this.executor.Serialize(options != null ? options : new UserUpdateOptions
                ()));
            return request;
        }

        protected internal virtual Request replaceUserRequest(string
             database, string user, UserUpdateOptions options)
        {
            Request request;
            request = new Request(database, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_USER
                , user));
            request.setBody(this.executor.Serialize(options != null ? options : new UserUpdateOptions
                ()));
            return request;
        }

        protected internal virtual Request getLogsRequest(LogOptions
             options)
        {
            LogOptions @params = options != null ? options : new LogOptions
                ();
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.GET, ArangoDBConstants
                .PATH_API_ADMIN_LOG).putQueryParam(LogOptions.PROPERTY_UPTO,
                @params.getUpto()).putQueryParam(LogOptions.PROPERTY_LEVEL, @params
                .getLevel()).putQueryParam(LogOptions.PROPERTY_START, @params
                .getStart()).putQueryParam(LogOptions.PROPERTY_SIZE, @params.
                getSize()).putQueryParam(LogOptions.PROPERTY_OFFSET, @params.
                getOffset()).putQueryParam(LogOptions.PROPERTY_SEARCH, @params
                .getSearch()).putQueryParam(LogOptions.PROPERTY_SORT, @params
                .getSort());
        }

        protected internal virtual Request getLogLevelRequest()
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.GET, ArangoDBConstants
                .PATH_API_ADMIN_LOG_LEVEL);
        }

        protected internal virtual Request setLogLevelRequest(LogLevelEntity
             entity)
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.PUT, ArangoDBConstants
                .PATH_API_ADMIN_LOG_LEVEL).setBody(this.executor.Serialize(entity));
        }
    }
}
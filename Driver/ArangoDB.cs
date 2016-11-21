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

namespace ArangoDB
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal;
    using global::ArangoDB.Internal.VelocyPack;
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoDB : InternalArangoDB<ArangoExecutorSync
        , Response, ConnectionSync
        >
    {
        public class Builder
        {
            private const string PROPERTY_KEY_HOST = "arangodb.host";

            private const string PROPERTY_KEY_PORT = "arangodb.port";

            private const string PROPERTY_KEY_TIMEOUT = "arangodb.timeout";

            private const string PROPERTY_KEY_USER = "arangodb.user";

            private const string PROPERTY_KEY_PASSWORD = "arangodb.password";

            private const string PROPERTY_KEY_USE_SSL = "arangodb.usessl";

            private const string PROPERTY_KEY_V_STREAM_CHUNK_CONTENT_SIZE = "arangodb.chunksize";

            private const string DEFAULT_PROPERTY_FILE = "/arangodb.properties";

            private string host;

            private int port;

            private int timeout;

            private string user;

            private string password;

            private bool useSsl;

            private javax.net.ssl.SSLContext sslContext;

            private int chunksize;

            private readonly VPack.Builder vpackBuilder;

            private readonly CollectionCache collectionCache;

            private readonly VPackParser vpackParser;

            public Builder()
                : base()
            {
                this.vpackBuilder = new VPack.Builder();
                this.collectionCache = new CollectionCache();
                this.vpackParser = new VPackParser();
                VPackConfigure.configure(this.vpackBuilder, this.vpackParser
                    , this.collectionCache);
                this.loadProperties(typeof(ArangoDB).getResourceAsStream
                    (DEFAULT_PROPERTY_FILE));
            }

            public virtual ArangoDB.Builder loadProperties(java.io.InputStream @in
                )
            {
                if (@in != null)
                {
                    java.util.Properties properties = new java.util.Properties();
                    try
                    {
                        properties.load(@in);
                        host = getProperty(properties, PROPERTY_KEY_HOST, host, ArangoDBConstants
                            .DEFAULT_HOST);
                        port = System.Convert.ToInt32(getProperty(properties, PROPERTY_KEY_PORT, port, ArangoDBConstants
                            .DEFAULT_PORT));
                        timeout = System.Convert.ToInt32(getProperty(properties, PROPERTY_KEY_TIMEOUT, timeout
                            , ArangoDBConstants.DEFAULT_TIMEOUT));
                        user = getProperty(properties, PROPERTY_KEY_USER, user, null);
                        password = getProperty(properties, PROPERTY_KEY_PASSWORD, password, null);
                        useSsl = bool.parseBoolean(getProperty(properties, PROPERTY_KEY_USE_SSL, useSsl,
                            ArangoDBConstants.DEFAULT_USE_SSL));
                        chunksize = System.Convert.ToInt32(getProperty(properties, PROPERTY_KEY_V_STREAM_CHUNK_CONTENT_SIZE
                            , chunksize, ArangoDBConstants.CHUNK_DEFAULT_CONTENT_SIZE
                            ));
                    }
                    catch (System.IO.IOException e)
                    {
                        throw new ArangoDBException(e);
                    }
                }
                return this;
            }

            private string getProperty<T>(java.util.Properties properties, string key, T currentValue
                , T defaultValue)
            {
                return properties.getProperty(key, currentValue != null ? currentValue.ToString()
                     : defaultValue != null ? defaultValue.ToString() : null);
            }

            public virtual ArangoDB.Builder host(string host)
            {
                this.host = host;
                return this;
            }

            public virtual ArangoDB.Builder port(int port)
            {
                this.port = port;
                return this;
            }

            public virtual ArangoDB.Builder timeout(int timeout)
            {
                this.timeout = timeout;
                return this;
            }

            public virtual ArangoDB.Builder user(string user)
            {
                this.user = user;
                return this;
            }

            public virtual ArangoDB.Builder password(string password)
            {
                this.password = password;
                return this;
            }

            public virtual ArangoDB.Builder useSsl(bool useSsl)
            {
                this.useSsl = useSsl;
                return this;
            }

            public virtual ArangoDB.Builder sslContext(javax.net.ssl.SSLContext
                sslContext)
            {
                this.sslContext = sslContext;
                return this;
            }

            public virtual ArangoDB.Builder chunksize(int chunksize)
            {
                this.chunksize = chunksize;
                return this;
            }

            public virtual ArangoDB.Builder registerSerializer<T>(VPackSerializer
                <T> serializer)
            {
                System.Type clazz = typeof(T);
                this.vpackBuilder.registerSerializer(clazz, serializer);
                return this;
            }

            public virtual ArangoDB.Builder registerDeserializer<T>(VPackDeserializer
                <T> deserializer)
            {
                System.Type clazz = typeof(T);
                this.vpackBuilder.registerDeserializer(clazz, deserializer);
                return this;
            }

            public virtual ArangoDB.Builder registerInstanceCreator<T>(VPackInstanceCreator
                <T> creator)
            {
                System.Type clazz = typeof(T);
                this.vpackBuilder.registerInstanceCreator(clazz, creator);
                return this;
            }

            public virtual ArangoDB build()
            {
                return new ArangoDB(new CommunicationSync.Builder
                    ().host(host).port(port).timeout(timeout).user(user).password(password).useSsl(useSsl
                    ).sslContext(sslContext).chunksize(chunksize), this.vpackBuilder.build(), this.vpackBuilder
                    .serializeNullValues(true).build(), this.vpackParser, this.collectionCache);
            }
        }

        public ArangoDB(CommunicationSync.Builder commBuilder
            , VPack vpack, VPack vpackNull,
            VPackParser vpackParser, CollectionCache
             collectionCache)
            : base(new ArangoExecutorSync(commBuilder.build(vpack, collectionCache
                ), vpack, vpackNull, vpackParser, new DocumentCache(), collectionCache
                ))
        {
            Communication<Response
                , ConnectionSync> cacheCom = commBuilder.build
                (vpack, collectionCache);
            collectionCache.init(new _DBAccess_196(this, cacheCom, vpackNull, vpack, vpackParser
                ));
        }

        private sealed class _DBAccess_196 : CollectionCache.DBAccess
        {
            public _DBAccess_196(ArangoDB _enclosing, Communication
                <Response, ConnectionSync
                > cacheCom, VPack vpackNull, VPack
                 vpack, VPackParser vpackParser)
            {
                this._enclosing = _enclosing;
                this.cacheCom = cacheCom;
                this.vpackNull = vpackNull;
                this.vpack = vpack;
                this.vpackParser = vpackParser;
            }

            public ArangoDatabase db(string name)
            {
                return new ArangoDatabase(this.cacheCom, this.vpackNull, this.vpack, this.vpackParser, this
                    ._enclosing._enclosing.executor.documentCache(), null, name);
            }

            private readonly ArangoDB _enclosing;

            private readonly Communication<Response
                , ConnectionSync> cacheCom;

            private readonly VPack vpackNull;

            private readonly VPack vpack;

            private readonly VPackParser vpackParser;
        }

        protected internal virtual ArangoExecutorSync executor()
        {
            return executor;
        }

        public virtual void shutdown()
        {
            executor.communication().disconnect();
        }

        /// <summary>Returns a handler of the system database</summary>
        /// <returns>database handler</returns>
        public virtual ArangoDatabase db()
        {
            return this.db(ArangoDBConstants.SYSTEM);
        }

        /// <summary>Returns a handler of the database by the given name</summary>
        /// <param name="name">Name of the database</param>
        /// <returns>database handler</returns>
        public virtual ArangoDatabase db(string name)
        {
            return new ArangoDatabase(this, name);
        }

        /// <summary>Creates a new database</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#create-database">API
        /// *      Documentation</a></seealso>
        /// <param name="name">Has to contain a valid database name</param>
        /// <returns>true if the database was created successfully.</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual bool createDatabase(string name)
        {
            return executor.execute(this.createDatabaseRequest(name), this.createDatabaseResponseDeserializer
                ());
        }

        /// <summary>Retrieves a list of all existing databases</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#list-of-databases">API
        /// *      Documentation</a></seealso>
        /// <returns>a list of all existing databases</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<string> getDatabases()
        {
            return executor.execute(this.getDatabasesRequest(this.db().name()), this.getDatabaseResponseDeserializer
                ());
        }

        /// <summary>Retrieves a list of all databases the current user can access</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#list-of-accessible-databases">API
        /// *      Documentation</a></seealso>
        /// <returns>a list of all databases the current user can access</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<string> getAccessibleDatabases
            ()
        {
            return executor.execute(this.getAccessibleDatabasesRequest(this.db().name()), this.getDatabaseResponseDeserializer
                ());
        }

        /// <summary>List available database to the specified user</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/UserManagement/index.html#list-the-databases-available-to-a-user">API
        /// *      Documentation</a></seealso>
        /// <param name="user">The name of the user for which you want to query the databases
        /// 	</param>
        /// <returns/>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<string> getAccessibleDatabasesFor
            (string user)
        {
            return executor.execute(this.getAccessibleDatabasesForRequest(this.db().name(), user), this.getAccessibleDatabasesForResponseDeserializer
                ());
        }

        /// <summary>Returns the server name and version number.</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/MiscellaneousFunctions/index.html#return-server-version">API
        /// *      Documentation</a></seealso>
        /// <returns>the server version, number</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual ArangoDBVersion getVersion()
        {
            return executor.execute(this.getVersionRequest(), typeof(
                ArangoDBVersion));
        }

        /// <summary>Create a new user.</summary>
        /// <remarks>
        /// Create a new user. This user will not have access to any database. You need permission to the _system database in
        /// order to execute this call.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#create-user">API Documentation</a>
        /// 	</seealso>
        /// <param name="user">The name of the user</param>
        /// <param name="passwd">The user password</param>
        /// <returns>information about the user</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual UserEntity createUser(string user, string passwd
            )
        {
            return executor.execute(this.createUserRequest(this.db().name(), user, passwd, new UserCreateOptions
                ()), typeof(UserEntity));
        }

        /// <summary>Create a new user.</summary>
        /// <remarks>
        /// Create a new user. This user will not have access to any database. You need permission to the _system database in
        /// order to execute this call.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#create-user">API Documentation</a>
        /// 	</seealso>
        /// <param name="user">The name of the user</param>
        /// <param name="passwd">The user password</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the user</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual UserEntity createUser(string user, string passwd
            , UserCreateOptions options)
        {
            return executor.execute(this.createUserRequest(this.db().name(), user, passwd, options), 
                typeof(UserEntity));
        }

        /// <summary>Removes an existing user, identified by user.</summary>
        /// <remarks>Removes an existing user, identified by user. You need access to the _system database.
        /// 	</remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#remove-user">API Documentation</a>
        /// 	</seealso>
        /// <param name="user">The name of the user</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteUser(string user)
        {
            executor.execute(this.deleteUserRequest(this.db().name(), user), 
                typeof(java.lang.Void));
        }

        /// <summary>Fetches data about the specified user.</summary>
        /// <remarks>
        /// Fetches data about the specified user. You can fetch information about yourself or you need permission to the
        /// _system database in order to execute this call.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#fetch-user">API Documentation</a>
        /// 	</seealso>
        /// <param name="user">The name of the user</param>
        /// <returns>information about the user</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual UserEntity getUser(string user)
        {
            return executor.execute(this.getUserRequest(this.db().name(), user), 
                typeof(UserEntity));
        }

        /// <summary>Fetches data about all users.</summary>
        /// <remarks>Fetches data about all users. You can only execute this call if you have access to the _system database.
        /// 	</remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#list-available-users">API
        /// *      Documentation</a></seealso>
        /// <returns>informations about all users</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<UserEntity
            > getUsers()
        {
            return executor.execute(this.getUsersRequest(this.db().name()), this.getUsersResponseDeserializer
                ());
        }

        /// <summary>Partially updates the data of an existing user.</summary>
        /// <remarks>
        /// Partially updates the data of an existing user. The name of an existing user must be specified in user. You can
        /// only change the password of your self. You need access to the _system database to change the active flag.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#update-user">API Documentation</a>
        /// 	</seealso>
        /// <param name="user">The name of the user</param>
        /// <param name="options">Properties of the user to be changed</param>
        /// <returns>information about the user</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual UserEntity updateUser(string user, UserUpdateOptions
             options)
        {
            return executor.execute(this.updateUserRequest(this.db().name(), user, options), 
                typeof(UserEntity));
        }

        /// <summary>Replaces the data of an existing user.</summary>
        /// <remarks>
        /// Replaces the data of an existing user. The name of an existing user must be specified in user. You can only
        /// change the password of your self. You need access to the _system database to change the active flag.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#replace-user">API
        /// *      Documentation</a></seealso>
        /// <param name="user">The name of the user</param>
        /// <param name="options">Additional properties of the user, can be null</param>
        /// <returns>information about the user</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual UserEntity replaceUser(string user, UserUpdateOptions
             options)
        {
            return executor.execute(this.replaceUserRequest(this.db().name(), user, options), 
                typeof(UserEntity));
        }

        /// <summary>Generic Execute.</summary>
        /// <remarks>Generic Execute. Use this method to execute custom FOXX services.</remarks>
        /// <param name="request">VelocyStream request</param>
        /// <returns>VelocyStream response</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual Response execute(Request
             request)
        {
            return executor.execute(request, new _ResponseDeserializer_416());
        }

        private sealed class _ResponseDeserializer_416 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <Response>
        {
            public _ResponseDeserializer_416()
            {
            }

            /// <exception cref="VPackException"/>
            public Response deserialize(Response
                 response)
            {
                return response;
            }
        }

        /// <summary>Returns fatal, error, warning or info log messages from the server's global log.
        /// 	</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AdministrationAndMonitoring/index.html#read-global-logs-from-the-server">API
        /// *      Documentation</a></seealso>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the log messages</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual LogEntity getLogs(LogOptions
             options)
        {
            return executor.execute(this.getLogsRequest(options), typeof(LogEntity));
        }

        /// <summary>Returns the server's current loglevel settings.</summary>
        /// <returns>the server's current loglevel settings</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual LogLevelEntity getLogLevel()
        {
            return executor.execute(this.getLogLevelRequest(), typeof(
                LogLevelEntity));
        }

        /// <summary>Modifies and returns the server's current loglevel settings.</summary>
        /// <param name="entity">loglevel settings</param>
        /// <returns>the server's current loglevel settings</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual LogLevelEntity setLogLevel(LogLevelEntity
             entity)
        {
            return executor.execute(this.setLogLevelRequest(entity), 
                typeof(LogLevelEntity));
        }
    }
}
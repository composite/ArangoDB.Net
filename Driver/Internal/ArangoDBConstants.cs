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
    /// <author>Mark - mark at arangodb.com</author>
    public class ArangoDBConstants
    {
        public const string DEFAULT_HOST = "127.0.0.1";

        public static readonly int DEFAULT_PORT = 8529;

        public static readonly int DEFAULT_TIMEOUT = 0;

        public static readonly bool DEFAULT_USE_SSL = false;

        public const int INTEGER_BYTES = int.SIZE / byte.SIZE;

        public const int LONG_BYTES = long.SIZE / byte.SIZE;

        public const int CHUNK_MIN_HEADER_SIZE = INTEGER_BYTES + INTEGER_BYTES + LONG_BYTES;

        public const int CHUNK_MAX_HEADER_SIZE = CHUNK_MIN_HEADER_SIZE + LONG_BYTES;

        public const int CHUNK_DEFAULT_CONTENT_SIZE = 30000;

        public const string PATH_API_DOCUMENT = "/_api/document";

        public const string PATH_API_COLLECTION = "/_api/collection";

        public const string PATH_API_DATABASE = "/_api/database";

        public const string PATH_API_VERSION = "/_api/version";

        public const string PATH_API_INDEX = "/_api/index";

        public const string PATH_API_USER = "/_api/user";

        public const string PATH_API_CURSOR = "/_api/cursor";

        public const string PATH_API_GHARIAL = "/_api/gharial";

        public const string PATH_API_TRANSACTION = "/_api/transaction";

        public const string PATH_API_AQLFUNCTION = "/_api/aqlfunction";

        public const string PATH_API_EXPLAIN = "/_api/explain";

        public const string PATH_API_QUERY = "/_api/query";

        public const string PATH_API_QUERY_CACHE = "/_api/query-cache";

        public const string PATH_API_QUERY_CACHE_PROPERTIES = "/_api/query-cache/properties";

        public const string PATH_API_QUERY_PROPERTIES = "/_api/query/properties";

        public const string PATH_API_QUERY_CURRENT = "/_api/query/current";

        public const string PATH_API_QUERY_SLOW = "/_api/query/slow";

        public const string PATH_API_TRAVERSAL = "/_api/traversal";

        public const string PATH_API_ADMIN_LOG = "/_admin/log";

        public const string PATH_API_ADMIN_LOG_LEVEL = "/_admin/log/level";

        public const string PATH_API_ADMIN_ROUTING_RELOAD = "/_admin/routing/reload";

        public const string ENCRYPTION_PLAIN = "plain";

        public const string SYSTEM = "_system";

        public const string ID = "id";

        public const string RESULT = "result";

        public const string VISITED = "visited";

        public const string VERTICES = "vertices";

        public const string EDGES = "edges";

        public const string WAIT_FOR_SYNC = "waitForSync";

        public const string IF_NONE_MATCH = "If-None-Match";

        public const string IF_MATCH = "If-Match";

        public const string KEEP_NULL = "keepNull";

        public const string MERGE_OBJECTS = "mergeObjects";

        public const string IGNORE_REVS = "ignoreRevs";

        public const string RETURN_NEW = "returnNew";

        public const string NEW = "new";

        public const string RETURN_OLD = "returnOld";

        public const string OLD = "old";

        public const string COLLECTION = "collection";

        public const string COLLECTIONS = "collections";

        public const string EXCLUDE_SYSTEM = "excludeSystem";

        public const string USER = "user";

        public const string RW = "rw";

        public const string NONE = "none";

        public const string DATABASE = "database";

        public const string CURRENT = "current";

        public const string INDEXES = "indexes";

        public const string TRUNCATE = "truncate";

        public const string COUNT = "count";

        public const string LOAD = "load";

        public const string UNLOAD = "unload";

        public const string PROPERTIES = "properties";

        public const string RENAME = "rename";

        public const string REVISION = "revision";

        public const string FULLCOUNT = "fullCount";

        public const string GROUP = "group";

        public const string NAMESPACE = "namespace";

        public const string GRAPH = "graph";

        public const string GRAPHS = "graphs";

        public const string VERTEX = "vertex";

        public const string EDGE = "edge";

        public const string ERROR = "error";
    }
}
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

namespace ArangoDB.Model
{
    using global::ArangoDB.Entity;

    /// <author>Mark - mark at arangodb.com</author>
	public class OptionsBuilder
    {
        private OptionsBuilder()
            : base()
        {
        }

        public static UserCreateOptions build(UserCreateOptions
             options, string user, string passwd)
        {
            return options.user(user).passwd(passwd);
        }

        public static HashIndexOptions build(HashIndexOptions
             options, System.Collections.Generic.ICollection<string> fields)
        {
            return options.fields(fields);
        }

        public static SkiplistIndexOptions build(SkiplistIndexOptions
             options, System.Collections.Generic.ICollection<string> fields)
        {
            return options.fields(fields);
        }

        public static PersistentIndexOptions build(PersistentIndexOptions
             options, System.Collections.Generic.ICollection<string> fields)
        {
            return options.fields(fields);
        }

        public static GeoIndexOptions build(GeoIndexOptions
             options, System.Collections.Generic.ICollection<string> fields)
        {
            return options.fields(fields);
        }

        public static FulltextIndexOptions build(FulltextIndexOptions
             options, System.Collections.Generic.ICollection<string> fields)
        {
            return options.fields(fields);
        }

        public static CollectionCreateOptions build(CollectionCreateOptions
             options, string name)
        {
            return options.name(name);
        }

        public static AqlQueryOptions build(AqlQueryOptions
             options, string query, System.Collections.Generic.IDictionary<string, object> bindVars
            )
        {
            return options.query(query).bindVars(bindVars);
        }

        public static AqlQueryExplainOptions build(AqlQueryExplainOptions
             options, string query, System.Collections.Generic.IDictionary<string, object> bindVars
            )
        {
            return options.query(query).bindVars(bindVars);
        }

        public static AqlQueryParseOptions build(AqlQueryParseOptions
             options, string query)
        {
            return options.query(query);
        }

        public static GraphCreateOptions build(GraphCreateOptions
             options, string name, System.Collections.Generic.ICollection<EdgeDefinition
            > edgeDefinitions)
        {
            return options.name(name).edgeDefinitions(edgeDefinitions);
        }

        public static TransactionOptions build(TransactionOptions
             options, string action)
        {
            return options.action(action);
        }

        public static CollectionRenameOptions build(CollectionRenameOptions
             options, string name)
        {
            return options.name(name);
        }

        public static DBCreateOptions build(DBCreateOptions
             options, string name)
        {
            return options.name(name);
        }

        public static UserAccessOptions build(UserAccessOptions
             options, string grant)
        {
            return options.grant(grant);
        }

        public static AqlFunctionCreateOptions build(AqlFunctionCreateOptions
             options, string name, string code)
        {
            return options.name(name).code(code);
        }

        public static VertexCollectionCreateOptions build(VertexCollectionCreateOptions
             options, string collection)
        {
            return options.collection(collection);
        }
    }
}
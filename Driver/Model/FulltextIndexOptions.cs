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
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Fulltext.html#create-fulltext-index">API
	/// *      Documentation</a></seealso>
	public class FulltextIndexOptions
    {
        private System.Collections.Generic.ICollection<string> fields;

        private readonly IndexType type = IndexType
            .fulltext;

        private int minLength;

        public FulltextIndexOptions()
            : base()
        {
        }

        protected internal virtual System.Collections.Generic.ICollection<string> getFields
            ()
        {
            return fields;
        }

        /// <param name="fields">A list of attribute paths</param>
        /// <returns>options</returns>
        protected internal virtual FulltextIndexOptions fields(System.Collections.Generic.ICollection
            <string> fields)
        {
            this.fields = fields;
            return this;
        }

        protected internal virtual IndexType getType()
        {
            return this.type;
        }

        public virtual int getMinLength()
        {
            return minLength;
        }

        /// <param name="minLength">
        /// Minimum character length of words to index. Will default to a server-defined value if unspecified. It
        /// is thus recommended to set this value explicitly when creating the index.
        /// </param>
        /// <returns>options</returns>
        public virtual FulltextIndexOptions minLength(int minLength)
        {
            this.minLength = minLength;
            return this;
        }
    }
}
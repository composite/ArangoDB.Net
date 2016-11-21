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
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Skiplist.html#create-skip-list">API Documentation</a>
	/// 	</seealso>
	public class SkiplistIndexOptions
    {
        private System.Collections.Generic.ICollection<string> fields;

        private readonly IndexType type = IndexType
            .skiplist;

        private bool unique;

        private bool sparse;

        public SkiplistIndexOptions()
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
        protected internal virtual SkiplistIndexOptions fields(System.Collections.Generic.ICollection
            <string> fields)
        {
            this.fields = fields;
            return this;
        }

        protected internal virtual IndexType getType()
        {
            return this.type;
        }

        public virtual bool getUnique()
        {
            return unique;
        }

        /// <param name="unique">if true, then create a unique index</param>
        /// <returns>options</returns>
        public virtual SkiplistIndexOptions unique(bool unique)
        {
            this.unique = unique;
            return this;
        }

        public virtual bool getSparse()
        {
            return sparse;
        }

        /// <param name="sparse">if true, then create a sparse index</param>
        /// <returns>options</returns>
        public virtual SkiplistIndexOptions sparse(bool sparse)
        {
            this.sparse = sparse;
            return this;
        }
    }
}
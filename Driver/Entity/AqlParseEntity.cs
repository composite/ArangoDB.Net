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

namespace ArangoDB.Entity
{
    /// <author>Mark - mark at arangodb.com</author>
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#parse-an-aql-query">API Documentation</a>
    /// 	</seealso>
    public class AqlParseEntity
    {
        public class AstNode
        {
            private string type;

            private System.Collections.Generic.ICollection<AstNode
                > subNodes;

            private string name;

            private long id;

            private object value;

            public virtual string getType()
            {
                return this.type;
            }

            public virtual System.Collections.Generic.ICollection<AstNode
                > getSubNodes()
            {
                return this.subNodes;
            }

            public virtual string getName()
            {
                return this.name;
            }

            public virtual long getId()
            {
                return this.id;
            }

            public virtual object getValue()
            {
                return this.value;
            }
        }

        private System.Collections.Generic.ICollection<string> collections;

        private System.Collections.Generic.ICollection<string> bindVars;

        private System.Collections.Generic.ICollection<AstNode
            > ast;

        public virtual System.Collections.Generic.ICollection<string> getCollections()
        {
            return this.collections;
        }

        public virtual System.Collections.Generic.ICollection<string> getBindVars()
        {
            return this.bindVars;
        }

        public virtual System.Collections.Generic.ICollection<AstNode
            > getAst()
        {
            return this.ast;
        }
    }
}
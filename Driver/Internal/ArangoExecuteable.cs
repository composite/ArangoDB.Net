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
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Util;

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class ArangoExecuteable<E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        protected internal readonly E executor;

        public ArangoExecuteable(E executor)
            : base()
        {
            this.executor = executor;
        }

        public virtual ArangoUtil util()
        {
            return this.executor.util();
        }
    }
}
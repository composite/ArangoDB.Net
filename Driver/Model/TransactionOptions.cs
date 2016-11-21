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
    /// <author>Mark - mark at arangodb.com</author>
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Transaction/index.html#execute-transaction">API
    /// *      Documentation</a></seealso>
    public class TransactionOptions
    {
        private string action;

        private object @params;

        private readonly TransactionOptions.TransactionCollectionOptions
             collections;

        private int lockTimeout;

        private bool waitForSync;

        public TransactionOptions()
            : base()
        {
            this.collections = new TransactionOptions.TransactionCollectionOptions
                ();
        }

        protected internal virtual string getAction()
        {
            return action;
        }

        /// <param name="action">the actual transaction operations to be executed, in the form of stringified JavaScript code
        /// 	</param>
        /// <returns>options</returns>
        protected internal virtual TransactionOptions action(string action
            )
        {
            this.action = action;
            return this;
        }

        public virtual object getParams()
        {
            return @params;
        }

        /// <param name="params">optional arguments passed to action</param>
        /// <returns>options</returns>
        public virtual TransactionOptions @params(object @params)
        {
            this.@params = @params;
            return this;
        }

        public virtual int getLockTimeout()
        {
            return lockTimeout;
        }

        /// <param name="lockTimeout">
        /// an optional numeric value that can be used to set a timeout for waiting on collection locks. If not
        /// specified, a default value will be used. Setting lockTimeout to 0 will make ArangoDB not time out
        /// waiting for a lock.
        /// </param>
        /// <returns>options</returns>
        public virtual TransactionOptions lockTimeout(int lockTimeout)
        {
            this.lockTimeout = lockTimeout;
            return this;
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">
        /// an optional boolean flag that, if set, will force the transaction to write all data to disk before
        /// returning
        /// </param>
        /// <returns>options</returns>
        public virtual TransactionOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        /// <param name="read">contains the array of collection-names to be used in the transaction (mandatory) for read
        /// 	</param>
        /// <returns>options</returns>
        public virtual TransactionOptions readCollections(params string
            [] read)
        {
            this.collections.read(read);
            return this;
        }

        /// <param name="write">contains the array of collection-names to be used in the transaction (mandatory) for write
        /// 	</param>
        /// <returns>options</returns>
        public virtual TransactionOptions writeCollections(params string
            [] write)
        {
            this.collections.write(write);
            return this;
        }

        public virtual TransactionOptions allowImplicit(bool allowImplicit
            )
        {
            this.collections.allowImplicit(allowImplicit);
            return this;
        }

        public class TransactionCollectionOptions
        {
            private System.Collections.Generic.ICollection<string> read;

            private System.Collections.Generic.ICollection<string> write;

            private bool allowImplicit;

            public virtual System.Collections.Generic.ICollection<string> getRead()
            {
                return read;
            }

            public virtual TransactionOptions.TransactionCollectionOptions
                 read(params string[] read)
            {
                this.read = java.util.Arrays.asList(read);
                return this;
            }

            public virtual System.Collections.Generic.ICollection<string> getWrite()
            {
                return write;
            }

            public virtual TransactionOptions.TransactionCollectionOptions
                 write(params string[] write)
            {
                this.write = java.util.Arrays.asList(write);
                return this;
            }

            public virtual bool getAllowImplicit()
            {
                return allowImplicit;
            }

            public virtual TransactionOptions.TransactionCollectionOptions
                 allowImplicit(bool allowImplicit)
            {
                this.allowImplicit = allowImplicit;
                return this;
            }
        }
    }
}
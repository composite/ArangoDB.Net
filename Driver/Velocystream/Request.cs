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

namespace ArangoDB.Velocystream
{
    using global::ArangoDB.Velocypack;

    /// <author>Mark - mark at arangodb.com</author>
	public class Request
    {
        private int version = 1;

        private int type = 1;

        private readonly string database;

        private readonly RequestType requestType;

        private readonly string request;

        private System.Collections.Generic.IDictionary<string, string> queryParam;

        private System.Collections.Generic.IDictionary<string, string> headerParam;

        private VPackSlice body;

        public Request(string database, RequestType requestType
            , string path)
            : base()
        {
            this.database = database;
            this.requestType = requestType;
            this.request = path;
            this.body = null;
            this.queryParam = new System.Collections.Generic.Dictionary<string, string>();
            this.headerParam = new System.Collections.Generic.Dictionary<string, string>();
        }

        public virtual int getVersion()
        {
            return this.version;
        }

        public virtual Request setVersion(int version)
        {
            this.version = version;
            return this;
        }

        public virtual int getType()
        {
            return this.type;
        }

        public virtual Request setType(int type)
        {
            this.type = type;
            return this;
        }

        public virtual string getDatabase()
        {
            return this.database;
        }

        public virtual RequestType getRequestType()
        {
            return this.requestType;
        }

        public virtual string getRequest()
        {
            return this.request;
        }

        public virtual System.Collections.Generic.IDictionary<string, string> getQueryParam
            ()
        {
            if (this.queryParam == null)
            {
                this.queryParam = new System.Collections.Generic.Dictionary<string, string>();
            }
            return this.queryParam;
        }

        public virtual Request putQueryParam(string key, object
             value)
        {
            if (value != null)
            {
                this.getQueryParam()[key] = value.ToString();
            }
            return this;
        }

        public virtual System.Collections.Generic.IDictionary<string, string> getHeaderParam
            ()
        {
            if (this.headerParam == null)
            {
                this.headerParam = new System.Collections.Generic.Dictionary<string, string>();
            }
            return this.headerParam;
        }

        public virtual Request putHeaderParam(string key, string
             value)
        {
            if (value != null)
            {
                this.getHeaderParam()[key] = value;
            }
            return this;
        }

        public virtual VPackSlice getBody()
        {
            return this.body;
        }

        public virtual Request setBody(VPackSlice
             body)
        {
            this.body = body;
            return this;
        }
    }
}
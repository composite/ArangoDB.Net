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

namespace VelocyPack.Velocystream
{
    using System.Collections.Generic;

    /// <author>Mark - mark at arangodb.com</author>
    public class Request
    {
        private readonly string database;

        private readonly RequestType requestType;

        private IDictionary<string, string> queryParam;

        private IDictionary<string, string> headerParam;

        public Request(string database, RequestType requestType, string path)
        {
            this.database = database;
            this.requestType = requestType;
            this.Path = path;
            this.Body = null;
            this.queryParam = new Dictionary<string, string>();
            this.headerParam = new Dictionary<string, string>();

            this.Version = 1;
            this.Type = 1;
        }

        public virtual int Version { get; set; }

        public virtual int Type { get; set; }

        public virtual string Database
        {
            get
            {
                return this.database;
            }
        }

        public virtual RequestType RequestType
        {
            get
            {
                return this.requestType;
            }
        }

        public virtual string Path { get; }

        public virtual IDictionary<string, string> QueryParam
        {
            get
            {
                if (this.queryParam == null)
                {
                    this.queryParam = new Dictionary<string, string>();
                }

                return this.queryParam;
            }
        }

        public virtual Request putQueryParam(string key, object value)
        {
            if (value != null)
            {
                this.QueryParam[key] = value.ToString();
            }

            return this;
        }

        public virtual IDictionary<string, string> HeaderParam
        {
            get
            {
                if (this.headerParam == null)
                {
                    this.headerParam = new Dictionary<string, string>();
                }

                return this.headerParam;
            }
        }

        public virtual Request putHeaderParam(string key, string value)
        {
            if (value != null)
            {
                this.HeaderParam[key] = value;
            }

            return this;
        }

        public virtual VPackSlice Body { get; set; }
    }
}
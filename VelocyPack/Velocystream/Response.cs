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
    /// <author>Mark - mark at arangodb.com</author>
	public class Response
    {
        private int version = 1;

        private int type = 2;

        private int responseCode;

        private VPackSlice body = null;

        public Response()
            : base()
        {
        }

        public virtual int getVersion()
        {
            return this.version;
        }

        public virtual void setVersion(int version)
        {
            this.version = version;
        }

        public virtual int getType()
        {
            return this.type;
        }

        public virtual void setType(int type)
        {
            this.type = type;
        }

        public virtual int getResponseCode()
        {
            return this.responseCode;
        }

        public virtual void setResponseCode(int responseCode)
        {
            this.responseCode = responseCode;
        }

        public virtual VPackSlice getBody()
        {
            return this.body;
        }

        public virtual void setBody(VPackSlice body)
        {
            this.body = body;
        }
    }
}
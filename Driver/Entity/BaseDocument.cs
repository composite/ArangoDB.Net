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
    using System.Collections.Generic;
    using System.Text;
    using Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class BaseDocument
    {
        private IDictionary<string, object> properties;

        public BaseDocument()
            : base()
        {
            this.properties = new Dictionary<string, object>();
        }

        public BaseDocument(string key)
            : this()
        {
            this.Key = key;
        }

        public BaseDocument(IDictionary<string, object> properties
            )
            : this()
        {
            var tmpId = properties.RemoveAndGet(DocumentFieldAttribute.Type.ID.GetSerializeName());
            if (tmpId != null)
            {
                this.Id = tmpId.ToString();
            }

            var tmpKey = properties.RemoveAndGet(DocumentFieldAttribute.Type.KEY.GetSerializeName());
            if (tmpKey != null)
            {
                this.Key = tmpKey.ToString();
            }

            var tmpRev = properties.RemoveAndGet(DocumentFieldAttribute.Type.REV.GetSerializeName());
            if (tmpRev != null)
            {
                this.Revision = tmpRev.ToString();
            }

            this.properties = properties;
        }

        public virtual string Id { get; set; }

        public virtual string Key { get; set; }

        public virtual string Revision { get; set; }

        public virtual IDictionary<string, object> getProperties()
        {
            return this.properties;
        }

        public virtual void setProperties(IDictionary<string, object> properties)
        {
            this.properties = properties;
        }

        public virtual void addAttribute(string key, object value)
        {
            this.properties[key] = value;
        }

        public virtual void updateAttribute(string key, object value)
        {
            if (this.properties.ContainsKey(key))
            {
                this.properties[key] = value;
            }
        }

        public virtual object getAttribute(string key)
        {
            return this.properties[key];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BaseDocument [documentRevision=");
            sb.Append(this.Revision);
            sb.Append(", documentHandle=");
            sb.Append(this.Id);
            sb.Append(", documentKey=");
            sb.Append(this.Key);
            sb.Append(", properties=");
            sb.Append(this.properties);
            sb.Append("]");
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + (this.Id == null ? 0 : this.Id.GetHashCode());
            result = prime * result + (this.Key == null ? 0 : this.Key.GetHashCode());
            result = prime * result + (this.properties == null ? 0 : this.properties.GetHashCode());
            result = prime * result + (this.Revision == null ? 0 : this.Revision.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            BaseDocument other = (BaseDocument)obj;
            if (this.Id == null)
            {
                if (other.Id != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.Id.Equals(other.Id))
                {
                    return false;
                }
            }

            if (this.Key == null)
            {
                if (other.Key != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.Key.Equals(other.Key))
                {
                    return false;
                }
            }

            if (this.properties == null)
            {
                if (other.properties != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.properties.Equals(other.properties))
                {
                    return false;
                }
            }

            if (this.Revision == null)
            {
                if (other.Revision != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.Revision.Equals(other.Revision))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
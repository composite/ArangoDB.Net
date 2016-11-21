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
    public class BaseEdgeDocument : BaseDocument
    {
        private const long serialVersionUID = 6904923804449368783L;

        private string from;

        private string to;

        public BaseEdgeDocument()
            : base()
        {
        }

        public BaseEdgeDocument(string from, string to)
            : base()
        {
            this.from = from;
            this.to = to;
        }

        public BaseEdgeDocument(System.Collections.Generic.IDictionary<string, object> properties
            )
            : base(properties)
        {
            object tmpFrom = Sharpen.Collections.Remove(properties, com.arangodb.entity.DocumentFieldAttribute.Type
                .FROM.getSerializeName());
            if (tmpFrom != null)
            {
                this.@from = tmpFrom.ToString();
            }
            object tmpTo = Sharpen.Collections.Remove(properties, com.arangodb.entity.DocumentFieldAttribute.Type
                .TO.getSerializeName());
            if (tmpTo != null)
            {
                this.to = tmpTo.ToString();
            }
        }

        public virtual string getFrom()
        {
            return this.@from;
        }

        public virtual void setFrom(string from)
        {
            this.from = from;
        }

        public virtual string getTo()
        {
            return this.to;
        }

        public virtual void setTo(string to)
        {
            this.to = to;
        }

        public override string ToString()
        {
            java.lang.StringBuilder sb = new java.lang.StringBuilder();
            sb.Append("BaseDocument [documentRevision=");
            sb.Append(this.revision);
            sb.Append(", documentHandle=");
            sb.Append(this.id);
            sb.Append(", documentKey=");
            sb.Append(this.key);
            sb.Append(", from=");
            sb.Append(this.@from);
            sb.Append(", to=");
            sb.Append(this.to);
            sb.Append(", properties=");
            sb.Append(this.properties);
            sb.Append("]");
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = base.GetHashCode();
            result = prime * result + (this.@from == null ? 0 : this.@from.GetHashCode());
            result = prime * result + (this.to == null ? 0 : this.to.GetHashCode());
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (!base.Equals(obj))
            {
                return false;
            }
            if (Sharpen.Runtime.getClassForObject(this) != Sharpen.Runtime.getClassForObject(
                obj))
            {
                return false;
            }
            BaseEdgeDocument other = (BaseEdgeDocument
                )obj;
            if (this.@from == null)
            {
                if (other.from != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.@from.Equals(other.from))
                {
                    return false;
                }
            }
            if (this.to == null)
            {
                if (other.to != null)
                {
                    return false;
                }
            }
            else
            {
                if (!this.to.Equals(other.to))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
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
    public class IndexEntity
    {
        private string id;

        private IndexType type;

        private System.Collections.Generic.ICollection<string> fields;

        private int selectivityEstimate;

        private bool unique;

        private bool sparse;

        private int minLength;

        private bool isNewlyCreated;

        private bool geoJson;

        private bool constraint;

        public IndexEntity()
            : base()
        {
        }

        public virtual string getId()
        {
            return this.id;
        }

        public virtual IndexType getType()
        {
            return this.type;
        }

        public virtual System.Collections.Generic.ICollection<string> getFields()
        {
            return this.fields;
        }

        public virtual int getSelectivityEstimate()
        {
            return this.selectivityEstimate;
        }

        public virtual bool getUnique()
        {
            return this.unique;
        }

        public virtual bool getSparse()
        {
            return this.sparse;
        }

        public virtual int getMinLength()
        {
            return this.minLength;
        }

        public virtual bool getIsNewlyCreated()
        {
            return this.isNewlyCreated;
        }

        public virtual bool getGeoJson()
        {
            return this.geoJson;
        }

        public virtual bool getConstraint()
        {
            return this.constraint;
        }
    }
}
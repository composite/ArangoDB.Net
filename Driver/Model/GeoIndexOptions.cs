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
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Geo.html#create-geospatial-index">API Documentation</a>
	/// 	</seealso>
	public class GeoIndexOptions
    {
        private System.Collections.Generic.ICollection<string> fields;

        private readonly IndexType type = IndexType
            .geo;

        private bool geoJson;

        public GeoIndexOptions()
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
        protected internal virtual GeoIndexOptions fields(System.Collections.Generic.ICollection
            <string> fields)
        {
            this.fields = fields;
            return this;
        }

        protected internal virtual IndexType getType()
        {
            return this.type;
        }

        public virtual bool getGeoJson()
        {
            return geoJson;
        }

        /// <param name="geoJson">
        /// If a geo-spatial index on a location is constructed and geoJson is true, then the order within the
        /// array is longitude followed by latitude. This corresponds to the format described in
        /// </param>
        /// <returns>options</returns>
        public virtual GeoIndexOptions geoJson(bool geoJson)
        {
            this.geoJson = geoJson;
            return this;
        }
    }
}
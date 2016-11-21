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

namespace ArangoDB
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal;
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoCollection : InternalArangoCollection<ArangoExecutorSync
        , Response, ConnectionSync
        >
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(ArangoCollection));

        protected internal ArangoCollection(ArangoDatabase db, string name)
            : base(db.executor(), db.name(), name)
        {
        }

        /// <summary>Creates a new document from the given document, unless there is already a document with the _key given.
        /// 	</summary>
        /// <remarks>
        /// Creates a new document from the given document, unless there is already a document with the _key given. If no
        /// _key is given, a new unique _key is generated automatically.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#create-document">API
        /// *      Documentation</a></seealso>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentCreateEntity<T> insertDocument<T>(T value
            )
        {
            return this.executor.execute(this.insertDocumentRequest(value, new DocumentCreateOptions
                ()), this.insertDocumentResponseDeserializer(value));
        }

        /// <summary>Creates a new document from the given document, unless there is already a document with the _key given.
        /// 	</summary>
        /// <remarks>
        /// Creates a new document from the given document, unless there is already a document with the _key given. If no
        /// _key is given, a new unique _key is generated automatically.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#create-document">API
        /// *      Documentation</a></seealso>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentCreateEntity<T> insertDocument<T>(T value
            , DocumentCreateOptions options)
        {
            return this.executor.execute(this.insertDocumentRequest(value, options), this.insertDocumentResponseDeserializer
                (value));
        }

        /// <summary>Creates new documents from the given documents, unless there is already a document with the _key given.
        /// 	</summary>
        /// <remarks>
        /// Creates new documents from the given documents, unless there is already a document with the _key given. If no
        /// _key is given, a new unique _key is generated automatically.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#create-document">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A List of documents (POJO, VPackSlice or String for Json)</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentCreateEntity<T>> insertDocuments<T>(System.Collections.Generic.ICollection<T> values)
        {
            DocumentCreateOptions @params = new DocumentCreateOptions
                ();
            return this.executor.execute(this.insertDocumentsRequest(values, @params), this.insertDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>Creates new documents from the given documents, unless there is already a document with the _key given.
        /// 	</summary>
        /// <remarks>
        /// Creates new documents from the given documents, unless there is already a document with the _key given. If no
        /// _key is given, a new unique _key is generated automatically.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#create-document">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A List of documents (POJO, VPackSlice or String for Json)</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentCreateEntity<T>> insertDocuments<T>(System.Collections.Generic.ICollection<T> values, DocumentCreateOptions
             options)
        {
            DocumentCreateOptions @params = options != null ? options : new
                                                                            DocumentCreateOptions();
            return this.executor.execute(this.insertDocumentsRequest(values, @params), this.insertDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>Reads a single document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="type">The type of the document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>the document identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        public virtual T getDocument<T>(string key)
        {
            System.Type type = typeof(T);
            this.executor.validateDocumentKey(key);
            try
            {
                return this.executor.execute(this.getDocumentRequest(key, new DocumentReadOptions
                    ()), type);
            }
            catch (ArangoDBException e)
            {
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(e.Message, e);
                }
                return null;
            }
        }

        /// <summary>Reads a single document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="type">The type of the document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the document identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getDocument<T>(string key, DocumentReadOptions
             options)
        {
            System.Type type = typeof(T);
            this.executor.validateDocumentKey(key);
            try
            {
                return this.executor.execute(this.getDocumentRequest(key, options), type);
            }
            catch (ArangoDBException e)
            {
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(e.Message, e);
                }
                return null;
            }
        }

        /// <summary>
        /// Replaces the document with key with the one in the body, provided there is such a document and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#replace-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentUpdateEntity<T> replaceDocument<T>(string
             key, T value)
        {
            return this.executor.execute(this.replaceDocumentRequest(key, value, new DocumentReplaceOptions
                ()), this.replaceDocumentResponseDeserializer(value));
        }

        /// <summary>
        /// Replaces the document with key with the one in the body, provided there is such a document and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#replace-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentUpdateEntity<T> replaceDocument<T>(string
             key, T value, DocumentReplaceOptions options)
        {
            return this.executor.execute(this.replaceDocumentRequest(key, value, options), this.replaceDocumentResponseDeserializer
                (value));
        }

        /// <summary>
        /// Replaces multiple documents in the specified collection with the ones in the values, the replaced documents are
        /// specified by the _key attributes in the documents in values.
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#replace-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A List of documents (POJO, VPackSlice or String for Json)</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentUpdateEntity<T>> replaceDocuments<T>(System.Collections.Generic.ICollection<T> values)
        {
            DocumentReplaceOptions @params = new DocumentReplaceOptions
                ();
            return this.executor.execute(this.replaceDocumentsRequest(values, @params), this.replaceDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>
        /// Replaces multiple documents in the specified collection with the ones in the values, the replaced documents are
        /// specified by the _key attributes in the documents in values.
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#replace-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A List of documents (POJO, VPackSlice or String for Json)</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentUpdateEntity<T>> replaceDocuments<T>(System.Collections.Generic.ICollection<T> values, DocumentReplaceOptions
             options)
        {
            DocumentReplaceOptions @params = options != null ? options :
                                                 new DocumentReplaceOptions();
            return this.executor.execute(this.replaceDocumentsRequest(values, @params), this.replaceDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>Partially updates the document identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the document identified by document-key. The value must contain a document with the attributes
        /// to patch (the patch document). All attributes from the patch document will be added to the existing document if
        /// they do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentUpdateEntity<T> updateDocument<T>(string
             key, T value)
        {
            return this.executor.execute(this.updateDocumentRequest(key, value, new DocumentUpdateOptions
                ()), this.updateDocumentResponseDeserializer(value));
        }

        /// <summary>Partially updates the document identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the document identified by document-key. The value must contain a document with the attributes
        /// to patch (the patch document). All attributes from the patch document will be added to the existing document if
        /// they do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="value">A representation of a single document (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DocumentUpdateEntity<T> updateDocument<T>(string
             key, T value, DocumentUpdateOptions options)
        {
            return this.executor.execute(this.updateDocumentRequest(key, value, options), this.updateDocumentResponseDeserializer
                (value));
        }

        /// <summary>
        /// Partially updates documents, the documents to update are specified by the _key attributes in the objects on
        /// values.
        /// </summary>
        /// <remarks>
        /// Partially updates documents, the documents to update are specified by the _key attributes in the objects on
        /// values. Vales must contain a list of document updates with the attributes to patch (the patch documents). All
        /// attributes from the patch documents will be added to the existing documents if they do not yet exist, and
        /// overwritten in the existing documents if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A list of documents (POJO, VPackSlice or String for Json)</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentUpdateEntity<T>> updateDocuments<T>(System.Collections.Generic.ICollection<T> values)
        {
            DocumentUpdateOptions @params = new DocumentUpdateOptions
                ();
            return this.executor.execute(this.updateDocumentsRequest(values, @params), this.updateDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>
        /// Partially updates documents, the documents to update are specified by the _key attributes in the objects on
        /// values.
        /// </summary>
        /// <remarks>
        /// Partially updates documents, the documents to update are specified by the _key attributes in the objects on
        /// values. Vales must contain a list of document updates with the attributes to patch (the patch documents). All
        /// attributes from the patch documents will be added to the existing documents if they do not yet exist, and
        /// overwritten in the existing documents if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="values">A list of documents (POJO, VPackSlice or String for Json)</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentUpdateEntity<T>> updateDocuments<T>(System.Collections.Generic.ICollection<T> values, DocumentUpdateOptions
             options)
        {
            DocumentUpdateOptions @params = options != null ? options : new
                                                                            DocumentUpdateOptions();
            return this.executor.execute(this.updateDocumentsRequest(values, @params), this.updateDocumentsResponseDeserializer
                (values, @params));
        }

        /// <summary>Removes a document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#removes-a-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="type">
        /// The type of the document (POJO class, VPackSlice or String for Json). Only necessary if
        /// options.returnOld is set to true, otherwise can be null.
        /// </param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual DocumentDeleteEntity<java.lang.Void> deleteDocument
            (string key)
        {
            return this.executor.execute(this.deleteDocumentRequest(key, new DocumentDeleteOptions
                ()), this.deleteDocumentResponseDeserializer<java.lang.Void>());
        }

        /// <summary>Removes a document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#removes-a-document">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="type">
        /// The type of the document (POJO class, VPackSlice or String for Json). Only necessary if
        /// options.returnOld is set to true, otherwise can be null.
        /// </param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the document</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual DocumentDeleteEntity<T> deleteDocument<T>(string
             key, DocumentDeleteOptions options)
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.deleteDocumentRequest(key, options), deleteDocumentResponseDeserializer
                (type));
        }

        /// <summary>Removes multiple document</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#removes-multiple-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="keys">The keys of the documents</param>
        /// <param name="type">
        /// The type of the documents (POJO class, VPackSlice or String for Json). Only necessary if
        /// options.returnOld is set to true, otherwise can be null.
        /// </param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentDeleteEntity
            <java.lang.Void>> deleteDocuments(System.Collections.Generic.ICollection<string>
             keys)
        {
            return this.executor.execute(this.deleteDocumentsRequest(keys, new DocumentDeleteOptions
                ()), this.deleteDocumentsResponseDeserializer<java.lang.Void>());
        }

        /// <summary>Removes multiple document</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#removes-multiple-documents">API
        /// *      Documentation</a></seealso>
        /// <param name="keys">The keys of the documents</param>
        /// <param name="type">
        /// The type of the documents (POJO class, VPackSlice or String for Json). Only necessary if
        /// options.returnOld is set to true, otherwise can be null.
        /// </param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual MultiDocumentEntity<DocumentDeleteEntity<T>> deleteDocuments<T>(System.Collections.Generic.ICollection<string> keys, DocumentDeleteOptions
             options)
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.deleteDocumentsRequest(keys, options), deleteDocumentsResponseDeserializer
                (type));
        }

        /// <summary>Checks if the document exists by reading a single document head</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document-header">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <returns>true if the document was found, otherwise false</returns>
        public virtual bool documentExists(string key)
        {
            return this.documentExists(key, new DocumentExistsOptions());
        }

        /// <summary>Checks if the document exists by reading a single document head</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document-header">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the document</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>true if the document was found, otherwise false</returns>
        public virtual bool documentExists(string key, DocumentExistsOptions
             options)
        {
            try
            {
                this.executor.communication().execute(this.documentExistsRequest(key, options));
                return true;
            }
            catch (ArangoDBException)
            {
                return false;
            }
        }

        /// <summary>Creates a hash index for the collection if it does not already exist.</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Hash.html#create-hash-index">API Documentation</a>
        /// 	</seealso>
        /// <param name="fields">A list of attribute paths</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity createHashIndex(System.Collections.Generic.ICollection
            <string> fields, HashIndexOptions options)
        {
            return this.executor.execute(this.createHashIndexRequest(fields, options), 
                typeof(IndexEntity));
        }

        /// <summary>Creates a skip-list index for the collection, if it does not already exist.
        /// 	</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Skiplist.html#create-skip-list">API
        /// *      Documentation</a></seealso>
        /// <param name="fields">A list of attribute paths</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity createSkiplistIndex(System.Collections.Generic.ICollection
            <string> fields, SkiplistIndexOptions options)
        {
            return this.executor.execute(this.createSkiplistIndexRequest(fields, options), 
                typeof(IndexEntity));
        }

        /// <summary>Creates a persistent index for the collection, if it does not already exist.
        /// 	</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Persistent.html#create-a-persistent-index">API
        /// *      Documentation</a></seealso>
        /// <param name="fields">A list of attribute paths</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity createPersistentIndex(System.Collections.Generic.ICollection
            <string> fields, PersistentIndexOptions options)
        {
            return this.executor.execute(this.createPersistentIndexRequest(fields, options), 
                typeof(IndexEntity));
        }

        /// <summary>Creates a geo-spatial index for the collection, if it does not already exist.
        /// 	</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Geo.html#create-geospatial-index">API
        /// *      Documentation</a></seealso>
        /// <param name="fields">A list of attribute paths</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity createGeoIndex(System.Collections.Generic.ICollection
            <string> fields, GeoIndexOptions options)
        {
            return this.executor.execute(this.createGeoIndexRequest(fields, options), 
                typeof(IndexEntity));
        }

        /// <summary>Creates a fulltext index for the collection, if it does not already exist.
        /// 	</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/Fulltext.html#create-fulltext-index">API
        /// *      Documentation</a></seealso>
        /// <param name="fields">A list of attribute paths</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity createFulltextIndex(System.Collections.Generic.ICollection
            <string> fields, FulltextIndexOptions options)
        {
            return this.executor.execute(this.createFulltextIndexRequest(fields, options), 
                typeof(IndexEntity));
        }

        /// <summary>Returns all indexes of the collection</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Indexes/WorkingWith.html#read-all-indexes-of-a-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the indexes</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<IndexEntity
            > getIndexes()
        {
            return this.executor.execute(this.getIndexesRequest(), this.getIndexesResponseDeserializer());
        }

        /// <summary>Removes all documents from the collection, but leaves the indexes intact
        /// 	</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html#truncate-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity truncate()
        {
            return this.executor.execute(this.truncateRequest(), typeof(
                CollectionEntity));
        }

        /// <summary>Counts the documents in a collection</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Collection/Getting.html#return-number-of-documents-in-a-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection, including the number of documents</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionPropertiesEntity count()
        {
            return this.executor.execute(this.countRequest(), typeof(CollectionPropertiesEntity
            ));
        }

        /// <summary>Drops the collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html#drops-collection">API
        /// *      Documentation</a></seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void drop()
        {
            this.executor.execute(this.dropRequest(), typeof(java.lang.Void
            ));
        }

        /// <summary>Loads a collection into memory.</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Modifying.html#load-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity load()
        {
            return this.executor.execute(this.loadRequest(), typeof(CollectionEntity
            ));
        }

        /// <summary>Removes a collection from memory.</summary>
        /// <remarks>
        /// Removes a collection from memory. This call does not delete any documents. You can use the collection afterwards;
        /// in which case it will be loaded into memory, again.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Modifying.html#unload-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity unload()
        {
            return this.executor.execute(this.unloadRequest(), typeof(CollectionEntity
            ));
        }

        /// <summary>Returns information about the collection</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Collection/Getting.html#return-information-about-a-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity getInfo()
        {
            return this.executor.execute(this.getInfoRequest(), typeof(
                CollectionEntity));
        }

        /// <summary>Reads the properties of the specified collection</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Collection/Getting.html#read-properties-of-a-collection">API
        /// *      Documentation</a></seealso>
        /// <returns>properties of the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionPropertiesEntity getProperties()
        {
            return this.executor.execute(this.getPropertiesRequest(), typeof(
                CollectionPropertiesEntity));
        }

        /// <summary>Changes the properties of a collection</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Collection/Modifying.html#change-properties-of-a-collection">API
        /// *      Documentation</a></seealso>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>properties of the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionPropertiesEntity changeProperties(CollectionPropertiesOptions
             options)
        {
            return this.executor.execute(this.changePropertiesRequest(options), 
                typeof(CollectionPropertiesEntity));
        }

        /// <summary>Renames a collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Modifying.html#rename-collection">API
        /// *      Documentation</a></seealso>
        /// <param name="newName">The new name</param>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity rename(string newName)
        {
            return this.executor.execute(this.renameRequest(newName), typeof(
                CollectionEntity));
        }

        /// <summary>Retrieve the collections revision</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Getting.html#return-collection-revision-id">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the collection, including the collections revision</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionRevisionEntity getRevision()
        {
            return this.executor.execute(this.getRevisionRequest(), typeof(
                CollectionRevisionEntity));
        }
    }
}
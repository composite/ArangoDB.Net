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
    using global::ArangoDB.Entity;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocystream;
    using System.Collections.Generic;

    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public class InternalArangoCollection<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        private readonly string name;

        private readonly string db;

        public InternalArangoCollection(E executor, string db, string name)
            : base(executor)
        {
            this.db = db;
            this.name = name;
        }

        public virtual string name()
        {
            return name;
        }

        public virtual string createDocumentHandle(string key)
        {
            this.executor.validateDocumentKey(key);
            return this.executor.createPath(name, key);
        }

        public virtual Request insertDocumentRequest<T>(T value
            , DocumentCreateOptions options)
        {
            Request request = new Request
                (this.db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_DOCUMENT, name));
            DocumentCreateOptions @params = options != null ? options : new
                                                                            DocumentCreateOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<DocumentCreateEntity
            <T>> insertDocumentResponseDeserializer<T>(T value)
        {
            return new _ResponseDeserializer_95(this, value);
        }

        private sealed class _ResponseDeserializer_95 : ArangoExecutor<,>.IResponseDeserializer
            <DocumentCreateEntity<T>>
        {
            public _ResponseDeserializer_95(InternalArangoCollection<E, R, C> _enclosing, T value
                )
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public DocumentCreateEntity<T> Deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody();
                DocumentCreateEntity<T> doc = this._enclosing.executor.Deserialize
                    (body, typeof(com.arangodb.entity.DocumentCreateEntity
                ));
                VPackSlice newDoc = body.get(ArangoDBConstants
                    .NEW);
                if (newDoc.isObject())
                {
                    doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, Sharpen.Runtime.getClassForObject
                        (this.value)));
                }
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.ID] = doc.getId();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.KEY] = doc.getKey();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        public virtual Request insertDocumentsRequest<T>(System.Collections.Generic.ICollection
            <T> values, DocumentCreateOptions @params)
        {
            Request request = new Request
                (this.db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_DOCUMENT, name));
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.setBody(this.executor.Serialize(values));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<MultiDocumentEntity
            <DocumentCreateEntity<T>>> insertDocumentsResponseDeserializer
            <T>(System.Collections.Generic.ICollection<T> values, DocumentCreateOptions
             @params)
        {
            return new _ResponseDeserializer_128(this, @params, values);
        }

        private sealed class _ResponseDeserializer_128 : ArangoExecutor<,>.IResponseDeserializer
            <MultiDocumentEntity<DocumentCreateEntity
            <T>>>
        {
            public _ResponseDeserializer_128(InternalArangoCollection<E, R, C> _enclosing, DocumentCreateOptions
                 @params, System.Collections.Generic.ICollection<T> values)
            {
                this._enclosing = _enclosing;
                this.@params = @params;
                this.values = values;
            }

            /// <exception cref="VPackException"/>
            public MultiDocumentEntity<DocumentCreateEntity
                <T>> Deserialize(Response response)
            {
                java.lang.Class type = null;
                if (this.@params.getReturnNew() != null && this.@params.getReturnNew())
                {
                    if (!this.values.isEmpty())
                    {
                        type = (java.lang.Class)Sharpen.Runtime.getClassForObject(this.values.GetEnumerator().
                            Current);
                    }
                }
                MultiDocumentEntity<DocumentCreateEntity<
                    T>> multiDocument = new MultiDocumentEntity<DocumentCreateEntity
                    <T>>();
                System.Collections.Generic.ICollection<DocumentCreateEntity<T
                    >> docs = new System.Collections.Generic.List<DocumentCreateEntity
                    <T>>();
                System.Collections.Generic.ICollection<ErrorEntity> errors =
                    new System.Collections.Generic.List<ErrorEntity>();
                VPackSlice body = response.getBody();
                for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                     = body.arrayIterator(); iterator.MoveNext();)
                {
                    VPackSlice next = iterator.Current;
                    if (next.get(ArangoDBConstants.ERROR).isTrue())
                    {
                        errors.add((ErrorEntity)this._enclosing.executor.Deserialize(
                            next, typeof(ErrorEntity)));
                    }
                    else
                    {
                        DocumentCreateEntity<T> doc = this._enclosing.executor.Deserialize
                            (next, typeof(com.arangodb.entity.DocumentCreateEntity
                        ));
                        VPackSlice newDoc = next.get(ArangoDBConstants
                            .NEW);
                        if (newDoc.isObject())
                        {
                            doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, type));
                        }
                        docs.add(doc);
                    }
                }
                multiDocument.setDocuments(docs);
                multiDocument.setErrors(errors);
                return multiDocument;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly DocumentCreateOptions @params;

            private readonly System.Collections.Generic.ICollection<T> values;
        }

        public virtual Request getDocumentRequest(string key, DocumentReadOptions
             options)
        {
            Request request = new Request
                (this.db, RequestType.GET, this.executor.createPath(ArangoDBConstants
                .PATH_API_DOCUMENT, this.createDocumentHandle(key)));
            DocumentReadOptions @params = options != null ? options : new
                                                                          DocumentReadOptions();
            request.putHeaderParam(ArangoDBConstants.IF_NONE_MATCH, @params
                .getIfNoneMatch());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            return request;
        }

        public virtual Request replaceDocumentRequest<T>(string
             key, T value, DocumentReplaceOptions options)
        {
            Request request = new Request
                (this.db, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_DOCUMENT, this.createDocumentHandle(key)));
            DocumentReplaceOptions @params = options != null ? options :
                                                 new DocumentReplaceOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.IGNORE_REVS, @params
                .getIgnoreRevs());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<DocumentUpdateEntity
            <T>> replaceDocumentResponseDeserializer<T>(T value)
        {
            return new _ResponseDeserializer_185(this, value);
        }

        private sealed class _ResponseDeserializer_185 : ArangoExecutor<,>.IResponseDeserializer
            <DocumentUpdateEntity<T>>
        {
            public _ResponseDeserializer_185(InternalArangoCollection<E, R, C> _enclosing, T
                value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public DocumentUpdateEntity<T> Deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody();
                DocumentUpdateEntity<T> doc = this._enclosing.executor.Deserialize
                    (body, typeof(com.arangodb.entity.DocumentUpdateEntity
                ));
                VPackSlice newDoc = body.get(ArangoDBConstants
                    .NEW);
                if (newDoc.isObject())
                {
                    doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, Sharpen.Runtime.getClassForObject
                        (this.value)));
                }
                VPackSlice oldDoc = body.get(ArangoDBConstants
                    .OLD);
                if (oldDoc.isObject())
                {
                    doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, Sharpen.Runtime.getClassForObject
                        (this.value)));
                }
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        public virtual Request replaceDocumentsRequest<T>(System.Collections.Generic.ICollection
            <T> values, DocumentReplaceOptions @params)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , name));
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.IGNORE_REVS, @params
                .getIgnoreRevs());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(values));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<MultiDocumentEntity
            <DocumentUpdateEntity<T>>> replaceDocumentsResponseDeserializer
            <T>(System.Collections.Generic.ICollection<T> values, DocumentReplaceOptions
             @params)
        {
            return new _ResponseDeserializer_223(this, @params, values);
        }

        private sealed class _ResponseDeserializer_223 : ArangoExecutor<,>.IResponseDeserializer
            <MultiDocumentEntity<DocumentUpdateEntity
            <T>>>
        {
            public _ResponseDeserializer_223(InternalArangoCollection<E, R, C> _enclosing, DocumentReplaceOptions
                 @params, System.Collections.Generic.ICollection<T> values)
            {
                this._enclosing = _enclosing;
                this.@params = @params;
                this.values = values;
            }

            /// <exception cref="VPackException"/>
            public MultiDocumentEntity<DocumentUpdateEntity
                <T>> Deserialize(Response response)
            {
                java.lang.Class type = null;
                if ((this.@params.getReturnNew() != null && this.@params.getReturnNew()) || (this.@params.getReturnOld
                    () != null && this.@params.getReturnOld()))
                {
                    if (!this.values.isEmpty())
                    {
                        type = (java.lang.Class)Sharpen.Runtime.getClassForObject(this.values.GetEnumerator().
                            Current);
                    }
                }
                MultiDocumentEntity<DocumentUpdateEntity<
                    T>> multiDocument = new MultiDocumentEntity<DocumentUpdateEntity
                    <T>>();
                System.Collections.Generic.ICollection<DocumentUpdateEntity<T
                    >> docs = new System.Collections.Generic.List<DocumentUpdateEntity
                    <T>>();
                System.Collections.Generic.ICollection<ErrorEntity> errors =
                    new System.Collections.Generic.List<ErrorEntity>();
                VPackSlice body = response.getBody();
                for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                     = body.arrayIterator(); iterator.MoveNext();)
                {
                    VPackSlice next = iterator.Current;
                    if (next.get(ArangoDBConstants.ERROR).isTrue())
                    {
                        errors.add((ErrorEntity)this._enclosing.executor.Deserialize(
                            next, typeof(ErrorEntity)));
                    }
                    else
                    {
                        DocumentUpdateEntity<T> doc = this._enclosing.executor.Deserialize
                            (next, typeof(com.arangodb.entity.DocumentUpdateEntity
                        ));
                        VPackSlice newDoc = next.get(ArangoDBConstants
                            .NEW);
                        if (newDoc.isObject())
                        {
                            doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, type));
                        }
                        VPackSlice oldDoc = next.get(ArangoDBConstants
                            .OLD);
                        if (oldDoc.isObject())
                        {
                            doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, type));
                        }
                        docs.add(doc);
                    }
                }
                multiDocument.setDocuments(docs);
                multiDocument.setErrors(errors);
                return multiDocument;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly DocumentReplaceOptions @params;

            private readonly System.Collections.Generic.ICollection<T> values;
        }

        public virtual Request updateDocumentRequest<T>(string
            key, T value, DocumentUpdateOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PATCH, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , this.createDocumentHandle(key)));
            DocumentUpdateOptions @params = options != null ? options : new
                                                                            DocumentUpdateOptions();
            request.putQueryParam(ArangoDBConstants.KEEP_NULL, @params
                .getKeepNull());
            request.putQueryParam(ArangoDBConstants.MERGE_OBJECTS, @params
                .getMergeObjects());
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.IGNORE_REVS, @params
                .getIgnoreRevs());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(value, @params.getSerializeNull() == null || @params
                .getSerializeNull()));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<DocumentUpdateEntity
            <T>> updateDocumentResponseDeserializer<T>(T value)
        {
            return new _ResponseDeserializer_279(this, value);
        }

        private sealed class _ResponseDeserializer_279 : ArangoExecutor<,>.IResponseDeserializer
            <DocumentUpdateEntity<T>>
        {
            public _ResponseDeserializer_279(InternalArangoCollection<E, R, C> _enclosing, T
                value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public DocumentUpdateEntity<T> Deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody();
                DocumentUpdateEntity<T> doc = this._enclosing.executor.Deserialize
                    (body, typeof(com.arangodb.entity.DocumentUpdateEntity
                ));
                VPackSlice newDoc = body.get(ArangoDBConstants
                    .NEW);
                if (newDoc.isObject())
                {
                    doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, Sharpen.Runtime.getClassForObject
                        (this.value)));
                }
                VPackSlice oldDoc = body.get(ArangoDBConstants
                    .OLD);
                if (oldDoc.isObject())
                {
                    doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, Sharpen.Runtime.getClassForObject
                        (this.value)));
                }
                return doc;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        public virtual Request updateDocumentsRequest<T>(System.Collections.Generic.ICollection
            <T> values, DocumentUpdateOptions @params)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PATCH, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , name));
            bool keepNull = @params.getKeepNull();
            request.putQueryParam(ArangoDBConstants.KEEP_NULL, keepNull
                );
            request.putQueryParam(ArangoDBConstants.MERGE_OBJECTS, @params
                .getMergeObjects());
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.IGNORE_REVS, @params
                .getIgnoreRevs());
            request.putQueryParam(ArangoDBConstants.RETURN_NEW, @params
                .getReturnNew());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(values, true));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<MultiDocumentEntity
            <DocumentUpdateEntity<T>>> updateDocumentsResponseDeserializer
            <T>(System.Collections.Generic.ICollection<T> values, DocumentUpdateOptions
             @params)
        {
            return new _ResponseDeserializer_317(this, @params, values);
        }

        private sealed class _ResponseDeserializer_317 : ArangoExecutor<,>.IResponseDeserializer
            <MultiDocumentEntity<DocumentUpdateEntity
            <T>>>
        {
            public _ResponseDeserializer_317(InternalArangoCollection<E, R, C> _enclosing, DocumentUpdateOptions
                 @params, System.Collections.Generic.ICollection<T> values)
            {
                this._enclosing = _enclosing;
                this.@params = @params;
                this.values = values;
            }

            /// <exception cref="VPackException"/>
            public MultiDocumentEntity<DocumentUpdateEntity
                <T>> Deserialize(Response response)
            {
                java.lang.Class type = null;
                if ((this.@params.getReturnNew() != null && this.@params.getReturnNew()) || (this.@params.getReturnOld
                    () != null && this.@params.getReturnOld()))
                {
                    if (!this.values.isEmpty())
                    {
                        type = (java.lang.Class)Sharpen.Runtime.getClassForObject(this.values.GetEnumerator().
                            Current);
                    }
                }
                MultiDocumentEntity<DocumentUpdateEntity<
                    T>> multiDocument = new MultiDocumentEntity<DocumentUpdateEntity
                    <T>>();
                System.Collections.Generic.ICollection<DocumentUpdateEntity<T
                    >> docs = new System.Collections.Generic.List<DocumentUpdateEntity
                    <T>>();
                System.Collections.Generic.ICollection<ErrorEntity> errors =
                    new System.Collections.Generic.List<ErrorEntity>();
                VPackSlice body = response.getBody();
                for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                     = body.arrayIterator(); iterator.MoveNext();)
                {
                    VPackSlice next = iterator.Current;
                    if (next.get(ArangoDBConstants.ERROR).isTrue())
                    {
                        errors.add((ErrorEntity)this._enclosing.executor.Deserialize(
                            next, typeof(ErrorEntity)));
                    }
                    else
                    {
                        DocumentUpdateEntity<T> doc = this._enclosing.executor.Deserialize
                            (next, typeof(com.arangodb.entity.DocumentUpdateEntity
                        ));
                        VPackSlice newDoc = next.get(ArangoDBConstants
                            .NEW);
                        if (newDoc.isObject())
                        {
                            doc.setNew((T)this._enclosing.executor.Deserialize(newDoc, type));
                        }
                        VPackSlice oldDoc = next.get(ArangoDBConstants
                            .OLD);
                        if (oldDoc.isObject())
                        {
                            doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, type));
                        }
                        docs.add(doc);
                    }
                }
                multiDocument.setDocuments(docs);
                multiDocument.setErrors(errors);
                return multiDocument;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly DocumentUpdateOptions @params;

            private readonly System.Collections.Generic.ICollection<T> values;
        }

        public virtual Request deleteDocumentRequest(string key
            , DocumentDeleteOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , this.createDocumentHandle(key)));
            DocumentDeleteOptions @params = options != null ? options : new
                                                                            DocumentDeleteOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<DocumentDeleteEntity
            <T>> deleteDocumentResponseDeserializer<T>()
        {
            System.Type type = typeof(T);
            return new _ResponseDeserializer_368(this, type);
        }

        private sealed class _ResponseDeserializer_368 : ArangoExecutor<,>.IResponseDeserializer
            <DocumentDeleteEntity<T>>
        {
            public _ResponseDeserializer_368(InternalArangoCollection<E, R, C> _enclosing, java.lang.Class
                 type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public DocumentDeleteEntity<T> Deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody();
                DocumentDeleteEntity<T> doc = this._enclosing.executor.Deserialize
                    (body, typeof(com.arangodb.entity.DocumentDeleteEntity
                ));
                VPackSlice oldDoc = body.get(ArangoDBConstants
                    .OLD);
                if (oldDoc.isObject())
                {
                    doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, this.type));
                }
                return doc;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly java.lang.Class type;
        }

        public virtual Request deleteDocumentsRequest(System.Collections.Generic.ICollection
            <string> keys, DocumentDeleteOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , name));
            DocumentDeleteOptions @params = options != null ? options : new
                                                                            DocumentDeleteOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putQueryParam(ArangoDBConstants.RETURN_OLD, @params
                .getReturnOld());
            request.setBody(this.executor.Serialize(keys));
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<MultiDocumentEntity
            <DocumentDeleteEntity<T>>> deleteDocumentsResponseDeserializer
            <T>()
        {
            System.Type type = typeof(T);
            return new _ResponseDeserializer_395(this, type);
        }

        private sealed class _ResponseDeserializer_395 : ArangoExecutor<,>.IResponseDeserializer
            <MultiDocumentEntity<DocumentDeleteEntity
            <T>>>
        {
            public _ResponseDeserializer_395(InternalArangoCollection<E, R, C> _enclosing, java.lang.Class
                 type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public MultiDocumentEntity<DocumentDeleteEntity
                <T>> Deserialize(Response response)
            {
                MultiDocumentEntity<DocumentDeleteEntity<
                    T>> multiDocument = new MultiDocumentEntity<DocumentDeleteEntity
                    <T>>();
                System.Collections.Generic.ICollection<DocumentDeleteEntity<T
                    >> docs = new System.Collections.Generic.List<DocumentDeleteEntity
                    <T>>();
                System.Collections.Generic.ICollection<ErrorEntity> errors =
                    new System.Collections.Generic.List<ErrorEntity>();
                VPackSlice body = response.getBody();
                for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                     = body.arrayIterator(); iterator.MoveNext();)
                {
                    VPackSlice next = iterator.Current;
                    if (next.get(ArangoDBConstants.ERROR).isTrue())
                    {
                        errors.add((ErrorEntity)this._enclosing.executor.Deserialize(
                            next, typeof(ErrorEntity)));
                    }
                    else
                    {
                        DocumentDeleteEntity<T> doc = this._enclosing.executor.Deserialize
                            (next, typeof(com.arangodb.entity.DocumentDeleteEntity
                        ));
                        VPackSlice oldDoc = next.get(ArangoDBConstants
                            .OLD);
                        if (oldDoc.isObject())
                        {
                            doc.setOld((T)this._enclosing.executor.Deserialize(oldDoc, this.type));
                        }
                        docs.add(doc);
                    }
                }
                multiDocument.setDocuments(docs);
                multiDocument.setErrors(errors);
                return multiDocument;
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;

            private readonly java.lang.Class type;
        }

        public virtual Request documentExistsRequest(string key
            , DocumentExistsOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .HEAD, this.executor.createPath(ArangoDBConstants.PATH_API_DOCUMENT
                , this.createDocumentHandle(key)));
            DocumentExistsOptions @params = options != null ? options : new
                                                                            DocumentExistsOptions();
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.putHeaderParam(ArangoDBConstants.IF_NONE_MATCH, @params
                .getIfNoneMatch());
            return request;
        }

        public virtual Request createHashIndexRequest(System.Collections.Generic.ICollection
            <string> fields, HashIndexOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .POST, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new HashIndexOptions(), fields)));
            return request;
        }

        public virtual Request createSkiplistIndexRequest(System.Collections.Generic.ICollection
            <string> fields, SkiplistIndexOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .POST, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new SkiplistIndexOptions(), fields)));
            return request;
        }

        public virtual Request createPersistentIndexRequest(System.Collections.Generic.ICollection
            <string> fields, PersistentIndexOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .POST, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new PersistentIndexOptions(), fields)));
            return request;
        }

        public virtual Request createGeoIndexRequest(System.Collections.Generic.ICollection
            <string> fields, GeoIndexOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .POST, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new GeoIndexOptions(), fields)));
            return request;
        }

        public virtual Request getIndexesRequest()
        {
            Request request;
            request = new Request(this.db, RequestType
                .GET, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            return request;
        }

        public virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer<System.Collections.Generic.ICollection
            <IndexEntity>> getIndexesResponseDeserializer()
        {
            return new _ResponseDeserializer_478(this);
        }

        private sealed class _ResponseDeserializer_478 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<IndexEntity>>
        {
            public _ResponseDeserializer_478(InternalArangoCollection<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<IndexEntity> deserialize
                (Response response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .INDEXES), new _Type_482().getType());
            }

            private sealed class _Type_482 : Type<ICollection<IndexEntity>>
            {
                public _Type_482()
                {
                }
            }

            private readonly InternalArangoCollection<E, R, C> _enclosing;
        }

        public virtual Request truncateRequest()
        {
            return new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.TRUNCATE));
        }

        public virtual Request countRequest()
        {
            return new Request(this.db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.COUNT));
        }

        public virtual Request createFulltextIndexRequest(System.Collections.Generic.ICollection
            <string> fields, FulltextIndexOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .POST, ArangoDBConstants.PATH_API_INDEX);
            request.putQueryParam(ArangoDBConstants.COLLECTION, name);
            request.setBody(this.executor.Serialize(OptionsBuilder.build(options
                 != null ? options : new FulltextIndexOptions(), fields)));
            return request;
        }

        public virtual Request dropRequest()
        {
            return new Request(this.db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name));
        }

        public virtual Request loadRequest()
        {
            return new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.LOAD));
        }

        public virtual Request unloadRequest()
        {
            return new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.UNLOAD));
        }

        public virtual Request getInfoRequest()
        {
            return new Request(this.db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name));
        }

        public virtual Request getPropertiesRequest()
        {
            return new Request(this.db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.PROPERTIES));
        }

        public virtual Request changePropertiesRequest(CollectionPropertiesOptions
             options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.PROPERTIES));
            request.setBody(this.executor.Serialize(options != null ? options : new CollectionPropertiesOptions
                ()));
            return request;
        }

        public virtual Request renameRequest(string newName)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.RENAME));
            request.setBody(this.executor.Serialize(OptionsBuilder.build(new CollectionRenameOptions
                (), newName)));
            return request;
        }

        public virtual Request getRevisionRequest()
        {
            return new Request(this.db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_COLLECTION
                , name, ArangoDBConstants.REVISION));
        }
    }
}
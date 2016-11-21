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

namespace ArangoDB.Internal.VelocyStream
{
    using global::ArangoDB.Velocypack;

    /// <author>Mark - mark at arangodb.com</author>
	public class Message
    {
        private readonly long id;

        private readonly VPackSlice head;

        private readonly VPackSlice body;

        /// <exception cref="java.nio.BufferUnderflowException"/>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public Message(long id, byte[] chunkBuffer)
            : base()
        {
            this.id = id;
            this.head = new VPackSlice(chunkBuffer);
            int headSize = this.head.getByteSize();
            if (chunkBuffer.Length > headSize)
            {
                this.body = new VPackSlice(chunkBuffer, headSize);
            }
            else
            {
                this.body = null;
            }
        }

        public Message(long id, VPackSlice head, VPackSlice
             body)
            : base()
        {
            this.id = id;
            this.head = head;
            this.body = body;
        }

        public virtual long getId()
        {
            return this.id;
        }

        public virtual VPackSlice getHead()
        {
            return this.head;
        }

        public virtual VPackSlice getBody()
        {
            return this.body;
        }
    }
}
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
    /// <author>Mark - mark at arangodb.com</author>
    public class Chunk
    {
        private readonly long messageId;

        private readonly long messageLength;

        private readonly int chunkX;

        private readonly int contentOffset;

        private readonly int contentLength;

        public Chunk(long messageId, int chunkX, long messageLength, int contentOffset, int
             contentLength)
        {
            this.messageId = messageId;
            this.chunkX = chunkX;
            this.messageLength = messageLength;
            this.contentOffset = contentOffset;
            this.contentLength = contentLength;
        }

        public Chunk(long messageId, int chunkIndex, int numberOfChunks, long messageLength
            , int contentOffset, int contentLength)
            : this(messageId, chunkX(chunkIndex, numberOfChunks), messageLength, contentOffset
                , contentLength)
        {
        }

        private static int chunkX(int chunkIndex, int numberOfChunks)
        {
            int chunkX;
            if (numberOfChunks == 1)
            {
                chunkX = 3;
            }
            else
            {
                // last byte: 0000 0011
                if (chunkIndex == 0)
                {
                    chunkX = (numberOfChunks << 1) + 1;
                }
                else
                {
                    chunkX = chunkIndex << 1;
                }
            }
            return chunkX;
        }

        public virtual long getMessageId()
        {
            return this.messageId;
        }

        public virtual long getMessageLength()
        {
            return this.messageLength;
        }

        public virtual bool isFirstChunk()
        {
            return 1 == (chunkX & unchecked((int)0x1));
        }

        public virtual int getChunk()
        {
            return chunkX >> 1;
        }

        public virtual int getChunkX()
        {
            return chunkX;
        }

        public virtual int getContentOffset()
        {
            return this.contentOffset;
        }

        public virtual int getContentLength()
        {
            return this.contentLength;
        }
    }
}
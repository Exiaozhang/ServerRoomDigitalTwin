using System;
using System.Collections.Generic;
using System.IO;

namespace ETModel
{
	//一个循环的Buffer
    public class CircularBuffer: Stream
    {
	    /// <summary>
	    /// 队列中每个BufferArray的长度
	    /// </summary>
        public int ChunkSize = 8192;
		
        /// <summary>
        /// 已用队列
        /// </summary>
        private readonly Queue<byte[]> bufferQueue = new Queue<byte[]>();

        /// <summary>
        /// 缓存队列
        /// </summary>
        private readonly Queue<byte[]> bufferCache = new Queue<byte[]>();

        /// <summary>
        /// 已用Buffer队列,末尾Buffer的索引
        /// </summary>
        public int LastIndex { get; set; }
        
        /// <summary>
        /// 已用Buffer队列,首位Buffer的索引
        /// </summary>
        public int FirstIndex { get; set; }
        
		/// <summary>
		/// 可用队列的末尾
		/// </summary>
        private byte[] lastBuffer;

	    public CircularBuffer()
	    {
		    this.AddLast();
	    }

	    /// <summary>
	    /// 返回可用队列申请的长度之和
	    /// </summary>
        public override long Length
        {
            get
            {
                int c = 0;
                if (this.bufferQueue.Count == 0)
                {
                    c = 0;
                }
                else
                {
                    c = (this.bufferQueue.Count - 1) * ChunkSize + this.LastIndex - this.FirstIndex;
                }
                if (c < 0)
                {
                    Log.Error("CircularBuffer count < 0: {0}, {1}, {2}".Fmt(this.bufferQueue.Count, this.LastIndex, this.FirstIndex));
                }
                return c;
            }
        }
        
        /// <summary>
        /// 添加一个Buffer到BufferQueue的末尾
        /// 如果缓存对别有，就从缓存中取
        /// </summary>
        public void AddLast()
        {
	        
            byte[] buffer;
            if (this.bufferCache.Count > 0)
            {
                buffer = this.bufferCache.Dequeue();
            }
            else
            {
                buffer = new byte[ChunkSize];
            }
            this.bufferQueue.Enqueue(buffer);
            this.lastBuffer = buffer;
        }
		
        /// <summary>
        /// 将可用队列的首位放到缓存队列中
        /// </summary>
        public void RemoveFirst()
        {
            this.bufferCache.Enqueue(bufferQueue.Dequeue());
        }
		
        /// <summary>
        /// 取出可用队列的首位
        /// 如果可用队列的数量为0,调用AddLast函数
        /// </summary>
        public byte[] First
        {
            get
            {
                if (this.bufferQueue.Count == 0)
                {
                    this.AddLast();
                }
                return this.bufferQueue.Peek();
            }
        }
		
        /// <summary>
        /// 取出可用队列的末位
        /// 如果可用队列的数量为0,调用AddLast函数
        /// </summary>
        public byte[] Last
        {
            get
            {
                if (this.bufferQueue.Count == 0)
                {
                    this.AddLast();
                }
                return this.lastBuffer;
            }
        }

		/// <summary>
		/// 从CircularBuffer读到stream中
		/// 将FirstBuffer写入到stream
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public async ETTask ReadAsync(Stream stream)
	    {
		    //判断FirstBuffer的长度
		    long buffLength = this.Length;
			int sendSize = this.ChunkSize - this.FirstIndex;
		    if (sendSize > buffLength)
		    {
			    sendSize = (int)buffLength;
		    }
			//将FirstBuffer写入到流中
		    await stream.WriteAsync(this.First, this.FirstIndex, sendSize);
		    
		    //移动索引到写入后的位置
		    this.FirstIndex += sendSize;
		    
		    //判断First索引到的位置，是否写入完成
		    if (this.FirstIndex == this.ChunkSize)
		    {
			    this.FirstIndex = 0;
			    this.RemoveFirst();
		    }
		}

	    /// <summary>
	    /// 从CircularBuffer读到stream
	    /// </summary>
	    /// <param name="stream"></param>
	    /// <param name="count">需要读取的字节数</param>
	    /// <exception cref="Exception"></exception>
	    public void Read(Stream stream, int count)
	    {
		    //判断写入的字节数是否大于可用的bufferList
		    if (count > this.Length)
		    {
			    throw new Exception($"bufferList length < count, {Length} {count}");
		    }

		    //一个一个Buffer的写入
		    int alreadyCopyCount = 0;
		    while (alreadyCopyCount < count)
		    {
			    int n = count - alreadyCopyCount;
			    if (ChunkSize - this.FirstIndex > n)
			    {
				    stream.Write(this.First, this.FirstIndex, n);
				    this.FirstIndex += n;
				    alreadyCopyCount += n;
			    }
			    else
			    {
				    stream.Write(this.First, this.FirstIndex, ChunkSize - this.FirstIndex);
				    alreadyCopyCount += ChunkSize - this.FirstIndex;
				    this.FirstIndex = 0;
				    this.RemoveFirst();
			    }
		    }
	    }
	    
	    // 从stream写入CircularBuffer
	    public void Write(Stream stream)
		{
			//写入的字节数
			int count = (int)(stream.Length - stream.Position);
			
			int alreadyCopyCount = 0;
			while (alreadyCopyCount < count)
			{
				//空间不足添加新的Buffer
				if (this.LastIndex == ChunkSize)
				{
					this.AddLast();
					this.LastIndex = 0;
				}

				int n = count - alreadyCopyCount;
				//判断剩余的字节数是否大于一个buffer的长度
				if (ChunkSize - this.LastIndex > n)
				{
					stream.Read(this.lastBuffer, this.LastIndex, n);
					this.LastIndex += count - alreadyCopyCount;
					alreadyCopyCount += n;
				}
				else
				{
					//读取stream写入到已用Buffer的末尾
					stream.Read(this.lastBuffer, this.LastIndex, ChunkSize - this.LastIndex);
					alreadyCopyCount += ChunkSize - this.LastIndex;
					this.LastIndex = ChunkSize;
				}
			}
		}
	    

	    /// <summary>
		///  从stream写入CircularBuffer
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public async ETTask<int> WriteAsync(Stream stream)
	    {
		    int size = this.ChunkSize - this.LastIndex;
		    
		    int n = await stream.ReadAsync(this.Last, this.LastIndex, size);

		    if (n == 0)
		    {
			    return 0;
		    }

		    this.LastIndex += n;

		    if (this.LastIndex == this.ChunkSize)
		    {
			    this.AddLast();
			    this.LastIndex = 0;
		    }

		    return n;
	    }

	    // 把CircularBuffer中数据写入buffer
        public override int Read(byte[] buffer, int offset, int count)
        {
	        if (buffer.Length < offset + count)
	        {
		        throw new Exception($"bufferList length < coutn, buffer length: {buffer.Length} {offset} {count}");
	        }

	        long length = this.Length;
			if (length < count)
            {
	            count = (int)length;
            }

            int alreadyCopyCount = 0;
            while (alreadyCopyCount < count)
            {
                int n = count - alreadyCopyCount;
				if (ChunkSize - this.FirstIndex > n)
                {
                    Array.Copy(this.First, this.FirstIndex, buffer, alreadyCopyCount + offset, n);
                    this.FirstIndex += n;
                    alreadyCopyCount += n;
                }
                else
                {
                    Array.Copy(this.First, this.FirstIndex, buffer, alreadyCopyCount + offset, ChunkSize - this.FirstIndex);
                    alreadyCopyCount += ChunkSize - this.FirstIndex;
                    this.FirstIndex = 0;
                    this.RemoveFirst();
                }
            }

	        return count;
        }

	    // 把buffer写入CircularBuffer中
        public override void Write(byte[] buffer, int offset, int count)
        {
	        int alreadyCopyCount = 0;
            while (alreadyCopyCount < count)
            {
                if (this.LastIndex == ChunkSize)
                {
                    this.AddLast();
                    this.LastIndex = 0;
                }

                int n = count - alreadyCopyCount;
                if (ChunkSize - this.LastIndex > n)
                {
                    Array.Copy(buffer, alreadyCopyCount + offset, this.lastBuffer, this.LastIndex, n);
                    this.LastIndex += count - alreadyCopyCount;
                    alreadyCopyCount += n;
                }
                else
                {
                    Array.Copy(buffer, alreadyCopyCount + offset, this.lastBuffer, this.LastIndex, ChunkSize - this.LastIndex);
                    alreadyCopyCount += ChunkSize - this.LastIndex;
                    this.LastIndex = ChunkSize;
                }
            }
        }

	    public override void Flush()
	    {
		    throw new NotImplementedException();
		}

	    public override long Seek(long offset, SeekOrigin origin)
	    {
			throw new NotImplementedException();
	    }

	    public override void SetLength(long value)
	    {
		    throw new NotImplementedException();
		}

	    public override bool CanRead
	    {
		    get
		    {
			    return true;
		    }
	    }

	    public override bool CanSeek
	    {
		    get
		    {
			    return false;
		    }
	    }

	    public override bool CanWrite
	    {
		    get
		    {
			    return true;
		    }
	    }

	    public override long Position { get; set; }
    }
}
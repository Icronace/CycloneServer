using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace De.Cyclone.Network
{
    public class BufferedStream : Stream
    {
        public Stream Stream { get; set; }
        public MemoryStream PendingStream { get; set; }
        internal bool WriteImmediately { get; set; }

        public BufferedStream(Stream stream)
        {
            Stream = stream;
            PendingStream = new MemoryStream(1024);
            WriteImmediately = false;
        }

        public override bool CanRead
        {
            get { return Stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return Stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return Stream.CanWrite; }
        }

        public override long Length
        {
            get { return Stream.Length; }
        }

        public override long Position
        {
            get { return Stream.Length; }
            set { Stream.Position = value; }
        }

        public override void Flush()
        {
            Stream.Write(PendingStream.GetBuffer(), 0, (int) PendingStream.Position);
            PendingStream.Position = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (WriteImmediately) {
                Stream.Write(buffer, offset, count);
            }
            else {
                PendingStream.Write(buffer, offset, count);
            }
        }
    }
}

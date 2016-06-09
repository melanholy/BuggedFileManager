using System;

namespace FileManager.Domain.Models.Files
{
    public class FileSize
    {
        public long Value { get; }

        public const long DirSize = -1;

        public FileSize(long value)
        {
            if (Value < -1)
                throw new ArgumentException();

            Value = value;
        }
    }
}
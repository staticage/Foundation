using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Foundation.Core
{
    public interface IIdGenerator
    {
        long CreateId();
        IEnumerable<long> IdStream();
    }

    public class IdGenerator : IEnumerable<long>,IEnumerable, IIdGenerator
    {
            private readonly int _timeUnit;

            public static readonly DateTime DefaultEpoch = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            private long _lastgen = -1;
            private readonly object genlock = new object();
            private int _sequence;
            private readonly long _generatorId;
            private readonly long MASK_SEQUENCE;
            private readonly long MASK_TIME;
            private readonly long MASK_GENERATOR;
            private readonly int SHIFT_TIME;
            private readonly int SHIFT_GENERATOR;

            public int Id => (int) _generatorId;


            public IdGenerator(int generatorId,
                int timeBits = 32,
                int timeUnit = 10000000,
                int sequenceBits = 10,
                int generatorIdBits = 10)
            {
               // _mutex = new Mutex(false, "IdGenerator" + generatorId);

                _timeUnit = timeUnit;

//                if (maskConfig == null)
//                    throw new ArgumentNullException(nameof(maskConfig));
//                if (timeSource == null)
//                    throw new ArgumentNullException(nameof(timeSource));
//                if (maskConfig.TotalBits != 63)
//                    throw new InvalidOperationException("Number of bits used to generate Id's is not equal to 63");
                if (generatorIdBits > 31)
                    throw new ArgumentOutOfRangeException("GeneratorId cannot have more than 31 bits");
                if (sequenceBits > 31)
                    throw new ArgumentOutOfRangeException("Sequence cannot have more than 31 bits");
                this.MASK_TIME = GetMask(timeBits);
                this.MASK_GENERATOR = GetMask(generatorIdBits);
                this.MASK_SEQUENCE = GetMask(sequenceBits);
                if (generatorId < 0 || (long) generatorId > this.MASK_GENERATOR)
                    throw new ArgumentOutOfRangeException(string.Format("GeneratorId must be between 0 and {0} (inclusive).", (object) this.MASK_GENERATOR));
                this.SHIFT_TIME = (int) generatorIdBits + (int) sequenceBits;
                this.SHIFT_GENERATOR = (int) sequenceBits;

                this._generatorId = (long) generatorId;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long CreateId()
            {
                lock (genlock)
                {
                    long ticks = ((DateTime.UtcNow - DefaultEpoch).Ticks) / _timeUnit;
                    long num = ticks & this.MASK_TIME;
                    if (num < this._lastgen || ticks < 0L)
                        throw new InvalidSystemClockException(string.Format("Clock moved backwards or wrapped around. Refusing to generate id for {0} ticks",
                            (object) (this._lastgen - num)));
                    if (num == this._lastgen)
                    {
                        if ((long) this._sequence >= this.MASK_SEQUENCE)
                            throw new SequenceOverflowException("Sequence overflow. Refusing to generate id for rest of tick");
                        ++this._sequence;
                    }
                    else
                    {
                        this._sequence = 0;
                        this._lastgen = num;
                    }

                    var result = (num << this.SHIFT_TIME) + (this._generatorId << this.SHIFT_GENERATOR) + (long) this._sequence;

                    return result;
                }
            }


            private static long GetMask(int bits)
            {
                return (1L << bits) - 1L;
            }

        public IEnumerable<long> IdStream()
            {
                while (true)
                    yield return this.CreateId();
            }

            public IEnumerator<long> GetEnumerator()
            {
                return this.IdStream().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator) this.GetEnumerator();
            }
        }

    public class InvalidSystemClockException : Exception
    {
        public InvalidSystemClockException(string message) : base(message)
        {
        }
    }

    public class SequenceOverflowException : Exception
    {
        public SequenceOverflowException(string message) : base(message)
        {   
        }
    }
}
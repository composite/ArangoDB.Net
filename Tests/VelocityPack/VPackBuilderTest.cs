using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArangoDB.Net.Tests.VelocityPack
{
    using System.Numerics;

    using VelocyPack;
    using VelocyPack.Exceptions;

    using Xunit;

    public class VPackBuilderTest
    {
        [Fact]
        public void Empty()
        {
            VPackSlice slice = new VPackBuilder().Slice();
            Assert.True(slice.IsNone);
        }

        [Fact]
        public void AddNull()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(ValueType.NULL);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsNull);
        }

        [Fact]
        public void AddBooleanTrue()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(true);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsBoolean);
            Assert.True(slice.AsBoolean);
        }

        [Fact]
        public void AddBooleanFalse()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(false);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsBoolean);
            Assert.False(slice.AsBoolean);
        }

        //TODO AddBooleanNull (for Nullable<T>)

        [Fact]
        public void AddDouble()
        {
            VPackBuilder builder = new VPackBuilder();
            const double VALUE = Double.MaxValue;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsDouble);
            Assert.Equal(VALUE, slice.AsDouble);
        }

        [Fact]
        public void AddIntegerAsSmallIntMin()
        {
            VPackBuilder builder = new VPackBuilder();
            const int VALUE = -6;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(VALUE, slice.AsInt);
        }

        [Fact]
        public void AddIntegerAsSmallIntMax()
        {
            VPackBuilder builder = new VPackBuilder();
            const int VALUE = 9;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(VALUE, slice.AsInt);
        }

        [Fact]
        public void AddLongAsSmallIntMin()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = -6;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(VALUE, slice.AsLong);
        }

        [Fact]
        public void AddLongAsSmallIntMax()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = 9;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(VALUE, slice.AsLong);
        }

        [Fact]
        public void AddLongAsSmallIntOutofRange()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = long.MaxValue;
            Assert.Throws<VPackBuilderNumberOutOfRangeException>(() => builder.Add(VALUE, ValueType.SMALLINT));
        }

        [Fact]
        public void AddBigIntegerAsSmallIntMin()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(-6);
            builder.Add(value, ValueType.SMALLINT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(value, slice.AsBigInteger);
        }

        [Fact]
        public void AddBigIntegerAsSmallIntMax()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(9);
            builder.Add(value, ValueType.SMALLINT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsSmallInt);
            Assert.Equal(value, slice.AsBigInteger);
        }

        [Fact]
        public void AddBigIntegerAsSmallIntOutofRange()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(long.MaxValue);
            Assert.Throws<VPackBuilderNumberOutOfRangeException>(() => builder.Add(value, ValueType.SMALLINT));
        }

        [Fact]
        public void AddIntegerAsInt()
        {
            VPackBuilder builder = new VPackBuilder();
            const int VALUE = int.MaxValue;
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsInt);
            Assert.Equal(VALUE, slice.AsInt);
        }

        [Fact]
        public void AddLongAsInt()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = long.MaxValue;
            builder.Add(VALUE, ValueType.INT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsInt);
            Assert.Equal(VALUE, slice.AsLong);
        }

        [Fact]
        public void AddBigIntegerAsInt()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(long.MaxValue);
            builder.Add(value, ValueType.INT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsInt);
            Assert.Equal(value, slice.AsBigInteger);
        }

        [Fact]
        public void AddLongAsUInt()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = long.MaxValue;
            builder.Add(VALUE, ValueType.UINT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsUInt);
            Assert.Equal(VALUE, slice.AsLong);
        }

        [Fact]
        public void AddBigIntegerAsUInt()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(long.MaxValue);
            builder.Add(value, ValueType.UINT);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsUInt);
            Assert.Equal(value, slice.AsBigInteger);
        }

        [Fact]
        public void AddLongAsUIntNegative()
        {
            VPackBuilder builder = new VPackBuilder();
            const long VALUE = -10;
            Assert.Throws<VPackBuilderUnexpectedValueException>(() => builder.Add(VALUE, ValueType.UINT));
        }

        [Fact]
        public void AddBigIntegerAsUIntNegative()
        {
            VPackBuilder builder = new VPackBuilder();
            BigInteger value = new BigInteger(-10);
            Assert.Throws<VPackBuilderUnexpectedValueException>(() => builder.Add(value, ValueType.UINT));
        }

        [Fact]
        public void AddDate()
        {
            VPackBuilder builder = new VPackBuilder();
            DateTime value = DateTime.Now;
            builder.Add(value);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsDate);
            Assert.Equal(value, slice.AsDate);
        }

        [Fact]
        public void AddStringShort()
        {
            VPackBuilder builder = new VPackBuilder();
            const string VALUE = "Hallo Welt!";
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsString);
            Assert.Equal(VALUE, slice.AsString);
        }

        [Fact]
        public void AddStringLong()
        {
            VPackBuilder builder = new VPackBuilder();
            const string VALUE = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.";
            builder.Add(VALUE);

            VPackSlice slice = builder.Slice();
            Assert.True(slice.IsString);
            Assert.Equal(VALUE, slice.AsString);
        }


    }
}

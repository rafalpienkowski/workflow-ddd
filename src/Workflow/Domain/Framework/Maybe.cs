using System;

namespace Workflow.Domain.Framework
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly T _value;
        public T Value 
        {
            get
            {
                if (HasNoValue)
                {
                    throw new ArgumentNullException("Cannot access value which is not set");
                }

                return _value;
            }
        }

        public static Maybe<T> None => new Maybe<T>();

        public bool HasNoValue => _value == null;
        public bool HasValue => !HasNoValue;

        private Maybe(T value)
        {
            _value = value;
        }

        public static Maybe<T> From(T obj) => new Maybe<T>(obj);

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Maybe<T>))
            {
                if (obj is T)
                {
                    obj = new Maybe<T>((T)obj);
                }

                if (!(obj is Maybe<T>))
                    return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            if (HasNoValue)
                return 0;

            return _value.GetHashCode();
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
                return true;

            if (HasNoValue || other.HasNoValue)
                return false;

            return _value.Equals(other._value);
        }

        public static implicit operator Result<T>(Maybe<T> maybe)
        {
            if(maybe == Maybe<T>.None)
            {
                return Result.Failure<T>("No value returned");
            }

            return Result.Success<T>(maybe.Value);
        }
    }
}
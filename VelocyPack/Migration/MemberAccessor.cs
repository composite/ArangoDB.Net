using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection.Emit
{
    using System.Linq.Expressions;

    internal class MemberAccessor
    {
        public MemberInfo MemberInfo { get; }

        private Func<object, object> get;

        private Action<object, object> set;

        public MemberAccessor(PropertyInfo property)
        {

            this.MemberInfo = property;
            get = InitializeGet(property);
            set = InitializeSet(property);

        }

        public MemberAccessor(FieldInfo field)
        {

            this.MemberInfo = field;
            get = InitializeGet(field);
            set = InitializeSet(field);

        }

        private Action<object, object> InitializeSet(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var isValue = property.DeclaringType.GetTypeInfo().IsValueType;

            // value as T is slightly faster than (T)value, so if it's not a value type, use that
            UnaryExpression instanceCast = (!isValue) ? Expression.TypeAs(instance, property.DeclaringType) : Expression.Convert(instance, property.DeclaringType);
            UnaryExpression valueCast = (!isValue) ? Expression.TypeAs(value, property.PropertyType) : Expression.Convert(value, property.PropertyType);
            return Expression.Lambda<Action<object, object>>(Expression.Call(instanceCast, property.SetMethod, valueCast), instance, value).Compile();
        }

        private Func<object, object> InitializeGet(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var isValue = property.DeclaringType.GetTypeInfo().IsValueType;

            UnaryExpression instanceCast = (!isValue) ? Expression.TypeAs(instance, property.DeclaringType) : Expression.Convert(instance, property.DeclaringType);
            return Expression.Lambda<Func<object, object>>(Expression.TypeAs(Expression.Call(instanceCast, property.GetMethod), typeof(object)), instance).Compile();
        }

        private Action<object, object> InitializeSet(FieldInfo field)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var isValue = field.DeclaringType.GetTypeInfo().IsValueType;

            // value as T is slightly faster than (T)value, so if it's not a value type, use that
            UnaryExpression instanceCast = (!isValue) ? Expression.TypeAs(instance, field.DeclaringType) : Expression.Convert(instance, field.DeclaringType);
            UnaryExpression valueCast = (!isValue) ? Expression.TypeAs(value, field.FieldType) : Expression.Convert(value, field.FieldType);
            return Expression.Lambda<Action<object, object>>(Expression.Assign(Expression.Field(instanceCast, field), valueCast), instance, value).Compile();
        }

        private Func<object, object> InitializeGet(FieldInfo field)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var isValue = field.DeclaringType.GetTypeInfo().IsValueType;

            UnaryExpression instanceCast = (!isValue) ? Expression.TypeAs(instance, field.DeclaringType) : Expression.Convert(instance, field.DeclaringType);
            return Expression.Lambda<Func<object, object>>(Expression.TypeAs(Expression.Field(instanceCast, field), typeof(object)), instance).Compile();
        }

        public object Get(object instance)
        {
            return this.get(instance);
        }

        public void Set(object instance, object value)
        {
            this.set(instance, value);
        }

    }
}

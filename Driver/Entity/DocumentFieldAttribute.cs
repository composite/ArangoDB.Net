using System;

namespace ArangoDB.Entity
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DocumentFieldAttribute : Attribute
    {
        private Type type;

        public DocumentFieldAttribute(Type type)
        {
            this.type = type;
            this.SerializeName = "_" + type.ToString().ToLower();
        }

        public enum Type
        {
            ID,
            KEY,
            REV,
            FROM,
            TO
        }

        public string SerializeName { get; }

        public override string ToString()
        {
            return this.SerializeName;
        }
    }

    public static class DocumentFieldUtility
    {
        public static string GetSerializeName(this DocumentFieldAttribute.Type type)
        {
            return "_" + type.ToString().ToLower();
        }
    }
}
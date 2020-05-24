#region Header
#pragma warning disable 1587 // XML comment is not placed on a valid language element
/**
 * IJsonWrapper.cs
 *   Interface that represents a type capable of handling all kinds of JSON
 *   data. This is mainly used when mapping objects through JsonMapper, and
 *   it's implemented by JsonData.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#pragma warning restore 1587 // XML comment is not placed on a valid language element
#endregion


#pragma warning disable 1591 // Missing XML comment for publicly visible type or member
using System.Collections;
using System.Collections.Specialized;


namespace Htc.Vita.Core.Json.LitJson
{
    public enum JsonType
    {
        None,

        Object,
        Array,
        String,
        Int,
        Long,
        Double,
        Boolean
    }

    public interface IJsonWrapper : IList, IOrderedDictionary
    {
        bool IsArray   { get; }
        bool IsBoolean { get; }
        bool IsDouble  { get; }
        bool IsInt     { get; }
        bool IsLong    { get; }
        bool IsObject  { get; }
        bool IsString  { get; }

        bool     GetBoolean ();
        double   GetDouble ();
        int      GetInt ();
        JsonType GetJsonType ();
        long     GetLong ();
        string   GetString ();

        void SetBoolean  (bool val);
        void SetDouble   (double val);
        void SetInt      (int val);
        void SetJsonType (JsonType type);
        void SetLong     (long val);
        void SetString   (string val);

        string ToJson ();
        void   ToJson (JsonWriter writer);
    }
}
#pragma warning restore 1591 // Missing XML comment for publicly visible type or member

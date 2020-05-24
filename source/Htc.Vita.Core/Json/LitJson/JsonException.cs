#region Header
#pragma warning disable 1587 // XML comment is not placed on a valid language element
/**
 * JsonException.cs
 *   Base class throwed by LitJSON when a parsing error occurs.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#pragma warning restore 1587 // XML comment is not placed on a valid language element
#endregion


#pragma warning disable 1591 // Missing XML comment for publicly visible type or member
using System;


namespace Htc.Vita.Core.Json.LitJson
{
    public class JsonException :
#if NETSTANDARD1_5
        Exception
#else
        ApplicationException
#endif
    {
        public JsonException () : base ()
        {
        }

        internal JsonException (ParserToken token) :
            base (String.Format (
                    "Invalid token '{0}' in input string", token))
        {
        }

        internal JsonException (ParserToken token,
                                Exception inner_exception) :
            base (String.Format (
                    "Invalid token '{0}' in input string", token),
                inner_exception)
        {
        }

        internal JsonException (int c) :
            base (String.Format (
                    "Invalid character '{0}' in input string", (char) c))
        {
        }

        internal JsonException (int c, Exception inner_exception) :
            base (String.Format (
                    "Invalid character '{0}' in input string", (char) c),
                inner_exception)
        {
        }


        public JsonException (string message) : base (message)
        {
        }

        public JsonException (string message, Exception inner_exception) :
            base (message, inner_exception)
        {
        }
    }
}
#pragma warning restore 1591 // Missing XML comment for publicly visible type or member

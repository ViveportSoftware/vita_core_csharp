using System;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class ExternalReferenceAttribute.
    /// Implements the <see cref="Attribute" />
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.All,
            AllowMultiple = true)]
    public class ExternalReferenceAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        /// <value>The link.</value>
        public virtual string Link { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalReferenceAttribute" /> class.
        /// </summary>
        /// <param name="link">The link.</param>
        public ExternalReferenceAttribute(string link)
        {
            Link = link;
        }
    }
}

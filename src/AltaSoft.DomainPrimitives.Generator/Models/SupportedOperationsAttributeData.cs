namespace AltaSoft.DomainPrimitives.Generator.Models
{
    internal sealed class SupportedOperationsAttributeData
    {
        /// <summary>
        /// Indicates whether addition operators should be generated.
        /// </summary>
        public bool Addition { get; set; }

        /// <summary>
        /// Indicates whether subtraction operators should be generated.
        /// </summary>
        public bool Subtraction { get; set; }

        /// <summary>
        /// Indicates whether multiplication operators should be generated.
        /// </summary>
        public bool Multiplication { get; set; }

        /// <summary>
        /// Indicates whether division operators should be generated.
        /// </summary>
        public bool Division { get; set; }

        /// <summary>
        /// Indicates whether modulus operators should be generated.
        /// </summary>
        public bool Modulus { get; set; }
    }
}
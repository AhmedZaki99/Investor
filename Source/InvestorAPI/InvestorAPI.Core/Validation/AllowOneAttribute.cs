using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace InvestorAPI.Core
{

    /// <summary>
    /// Validation attribute to indicate that only one of two properties can be set to a value, while the other should be null.
    /// </summary>
    /// <remarks>
    /// This validation attribute should be applied to nullable properties, otherwise it will have no benefit.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AllowOneAttribute : ValidationAttribute
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name of the other property to check its value.
        /// </summary>
        /// <remarks>
        /// This property should be of a nullable type, otherwise the attribute will have no benefit.
        /// </remarks>
        public string OtherProperty { get; set; }

        /// <summary>
        /// Gets the other property's display name set by <see cref="DisplayAttribute"/> attribute.
        /// </summary>
        public string? OtherPropertyDisplayName { get; private set; }

        #endregion

        #region Base Properties

        /// <inheritdoc/>
        public override bool RequiresValidationContext => true;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor that accepts the name of the other property.
        /// </summary>
        /// <param name="otherProperty">The other property's name, which will be checked together with the underlying property if both have values.</param>
        /// <remarks>
        /// Both the underlying property and the other property should be of nullable type, otherwise the attribute will have no benefit.
        /// </remarks>
        public AllowOneAttribute(string otherProperty) : base("Only one of the two properties '{0}', and '{1}' is allowed to be set to a value.")
        {
            OtherProperty = otherProperty ?? throw new ArgumentNullException(nameof(otherProperty));
        }

        #endregion


        #region Error Message

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OtherProperty);
        }

        #endregion

        #region Validation

        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"There's no property found with the name '{OtherProperty}'.");
            }

            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);
            if (value is not null && otherPropertyValue is not null)
            {
                OtherPropertyDisplayName ??= GetDisplayNameForProperty(otherPropertyInfo);

                string[]? memberNames = validationContext.MemberName != null
                   ? new[] { validationContext.MemberName }
                   : null;
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
            }

            return null;
        }

        #endregion

        #region Helper Methods

        private string? GetDisplayNameForProperty(PropertyInfo property)
        {
            var attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
            foreach (var attribute in attributes)
            {
                if (attribute is DisplayAttribute display)
                {
                    return display.GetName();
                }
            }
            return OtherProperty;
        }

        #endregion

    }
}

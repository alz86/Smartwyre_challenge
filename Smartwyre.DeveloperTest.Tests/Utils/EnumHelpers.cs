using System;

namespace Smartwyre.DeveloperTest.Tests.Utils
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Gets a value from the same enum as the parameter, 
        /// but different from the it.
        /// </summary>
        /// <returns>A value of the enum that is different to the one provided by parameter.</returns>
        /// <exception cref="InvalidOperationException">Throw if there is no other value than the
        /// one in provided as parameter.</exception>
        public static T GetDifferentEnumValue<T>(T currentValue) where T : Enum
        {
            // Gets all values of the enum
            T[] values = (T[])Enum.GetValues(typeof(T));

            // Iterates through all enum values
            foreach (var value in values)
            {
                if (!Equals(value, currentValue))
                {
                    return value;  // Return the first different value found
                }
            }

            // If no different value is found (unlikely, unless the enum has only one value), throws an exception
            throw new InvalidOperationException("No different enum value found.");
        }
    }
}

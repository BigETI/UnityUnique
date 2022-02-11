using System;

/// <summary>
/// Unity Unique namespace
/// </summary>
namespace UnityUnique
{
    /// <summary>
    /// A class that contains functions for GUIDs
    /// </summary>
    public static class GUIDs
    {
        /// <summary>
        /// Converts a string to a GUID
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>GUID</returns>
        public static Guid StringToGUID(string input) => ((input != null) && Guid.TryParse(input, out Guid ret)) ? ret : Guid.Empty;
    }
}

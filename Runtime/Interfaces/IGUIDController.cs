using System;
using UnityPatterns;

/// <summary>
/// Unity Unique namespace
/// </summary>
namespace UnityUnique
{
    /// <summary>
    /// An interface that represents a GUID controller script
    /// </summary>
    public interface IGUIDController : IController
    {
        /// <summary>
        /// GUID
        /// </summary>
        Guid GUID { get; set; }

        /// <summary>
        /// Is unique
        /// </summary>
        bool IsUnique
        {
            get;
#if UNITY_EDITOR
            set;
#endif
        }
    }
}

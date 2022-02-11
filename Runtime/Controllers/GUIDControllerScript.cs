using System;
#if UNITY_EDITOR
using System.Collections.Generic;
#endif
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif
using UnityPatterns.Controllers;

/// <summary>
/// Unity Unique controllers namespace
/// </summary>
namespace UnityUnique.Controllers
{
    /// <summary>
    /// A class that describes a GUID controller script
    /// </summary>
    public class GUIDControllerScript : AControllerScript, IGUIDController
    {
#if UNITY_EDITOR
        /// <summary>
        /// Root game objects
        /// </summary>
        private readonly List<GameObject> rootGameObjects = new List<GameObject>();

        /// <summary>
        /// GUID controllers
        /// </summary>
        private readonly List<GUIDControllerScript> guidControllers = new List<GUIDControllerScript>();
#endif
        /// <summary>
        /// GUID
        /// </summary>
        [SerializeField]
        private string guid;

        /// <summary>
        /// Is unique
        /// </summary>
        [SerializeField]
        private bool isUnique = true;

        /// <summary>
        /// GUID
        /// </summary>
        public Guid GUID
        {
            get => GUIDs.StringToGUID(guid);
            set => guid = value.ToString();
        }

        /// <summary>
        /// Is unique
        /// </summary>
        public bool IsUnique
        {
            get => isUnique;
#if UNITY_EDITOR
            set => isUnique = value;
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Validates GUIDs
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <returns>"true" if validation should happen again</returns>
        private bool ValidateGUIDs(Transform transform)
        {
            bool ret = false;
            transform.GetComponents(guidControllers);
            foreach (GUIDControllerScript guid_controller in guidControllers)
            {
                if ((guid_controller != this) && (guid_controller.GUID == GUID))
                {
                    GUID = Guid.NewGuid();
                    ret = true;
                    break;
                }
            }
            guidControllers.Clear();
            if (!ret)
            {
                for (int transform_index = 0; transform_index < transform.childCount; transform_index++)
                {
                    ret = ValidateGUIDs(transform.GetChild(transform_index));
                    if (ret)
                    {
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets invoked when script needs to be validated
        /// </summary>
        protected virtual void OnValidate()
        {
            if (!Guid.TryParse(this.guid ?? string.Empty, out Guid guid) || (guid == Guid.Empty))
            {
                GUID = Guid.NewGuid();
            }
            if (isUnique)
            {
                bool is_checking = true;
                while (is_checking)
                {
                    is_checking = false;
                    for (int scene_index = 0; (scene_index < SceneManager.sceneCount) && !is_checking; scene_index++)
                    {
                        SceneManager.GetSceneAt(scene_index).GetRootGameObjects(rootGameObjects);
                        foreach (GameObject root_game_object in rootGameObjects)
                        {
                            is_checking = ValidateGUIDs(root_game_object.transform);
                            if (is_checking)
                            {
                                break;
                            }
                        }
                        rootGameObjects.Clear();
                    }
                }
            }
        }
#endif
    }
}

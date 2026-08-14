using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEditor.EditorTools;

namespace Uni.Settings
{
    [CreateAssetMenu(menuName = "SettingsUI/Prefab Data")]
    public class UIPrefabDataSheet : ScriptableObject
    {
        public GameObject GroupTextHeaderPrefab;

        public SerializedDictionary<string, UIPrefabData> MainPrefabDatas = new();

        [Tooltip("Just for any other prefabs you might need.")]
        public SerializedDictionary<string, GameObject> OtherPrefabs;

#if UNITY_EDITOR
        [ContextMenu("Add default UI keys")]
        public void AddDefaultUIKeys()
        {
            TryAddKeys("Dropdown", "Button", "Toggle", "Slider");

            void TryAddKeys(params string[] keys)
            {
                foreach (var key in keys)
                    MainPrefabDatas.TryAdd(key, default);
            }
        }
#endif
    }
}
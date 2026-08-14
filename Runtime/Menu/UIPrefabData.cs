using System;
using UnityEngine;

namespace Uni.Settings
{
    [Serializable]
    public struct UIPrefabData
    {
        public GameObject Prefab;

        [Tooltip("The name of the transform of the label text")]
        public string LabelTransformName;
    }
}
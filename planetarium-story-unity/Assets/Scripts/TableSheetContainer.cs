using System.Collections.Generic;
using UnityEngine;

namespace Script.ScriptableObjects
{
    [CreateAssetMenu]
    public class TableSheetContainer : ScriptableObject
    {
        public List<TextAsset> tableCsvAssets;
    }
}
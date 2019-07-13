using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Osric {

    public static class FilePathUtils
    {
        public static string GetPrefabPath(string name) {
            string eggshellPath = Osric.GetConfiguration().GetConfig("Prefabs");
            return eggshellPath + @"\" + name + ".prefab";
        }
        
    }

}

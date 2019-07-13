using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

using Osric;

public class PrefabSpawner : Eggshell
{

    private GameObject prefab;

    public PrefabSpawner(string name) {
        OsricConfiguration config = Osric.Osric.GetConfiguration();
        string path = config.GetConfig("Prefabs") + @"\" + name + ".prefab";
        /*
        if (File.Exists(path)) {
            this.prefab = (GameObject) AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        }
        */
        PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
    }

    public void ParseCommand(string command) {
        
    }

}

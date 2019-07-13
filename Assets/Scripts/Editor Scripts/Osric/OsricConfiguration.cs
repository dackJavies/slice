using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class OsricConfiguration
{

    private const string CONFIG_FILE_LOCATION = @"\config.osric";

    private Dictionary<string, string> configs;

    public OsricConfiguration() {
        this.configs = new Dictionary<string, string>();
    }

    public void ReadConfig() {
        if (!File.Exists(Directory.GetCurrentDirectory() + CONFIG_FILE_LOCATION)) {
            Debug.Log("Cannot read Osric config file; file not found.");
        } else {
            using (StreamReader sr = File.OpenText(Directory.GetCurrentDirectory() + CONFIG_FILE_LOCATION)) {
                string nextLine;
                while((nextLine = sr.ReadLine()) != null) {
                    if (nextLine == "") {
                        continue;
                    }
                    if (nextLine.Substring(0, 2) == "//") {
                        continue;
                    }
                    string[] split = nextLine.Split(new string[] {" : "}, StringSplitOptions.RemoveEmptyEntries);
                    this.configs.Add(split[0], split[1]);
                }
            }
        }
    }

    public string GetConfig(string key) {
        return this.configs[key];
    }

}

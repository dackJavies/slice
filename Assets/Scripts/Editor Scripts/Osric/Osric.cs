using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Osric {

    [InitializeOnLoad]
    public class Osric
    {

        private static OsricConfiguration config;

        static Osric() {
            config = new OsricConfiguration();
            config.ReadConfig();
        }

        public static OsricConfiguration GetConfiguration() {
            return config;
        }

    }

}

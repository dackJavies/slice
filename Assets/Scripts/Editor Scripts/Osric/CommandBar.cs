using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

namespace Osric {

    public class CommandBar : EditorWindow
    {

        private string text;

        public static void ShowWindow() {
            EditorWindow.GetWindowWithRect<CommandBar>(new Rect(0, 0, 400, 40), true, "Cmd");
        }

        void OnGUI() {

            GUIStyle style = new GUIStyle();
            style.fixedHeight = 30f;
            style.fontSize = 20;

            if (Event.current.keyCode == KeyCode.Return) {
                string[] split = text.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if (split != null && split.Length >= 2 && split[0] == "spawn") {
                    PrefabSpawner ps = new PrefabSpawner(split[1]);
                }
                this.Close();
            }

            GUI.SetNextControlName("TheText");

            this.text = EditorGUILayout.TextField("", this.text, style);

            GUI.FocusControl("TheText");
        }
    }

}

using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

using Osric;

[EditorTool("DragFill Tool")]
public class DragFillTool : EditorTool
{
    private GameObject selectedTile;

    [MenuItem("My Commands/Make Window #;")]
    static void MakeWindow() {
        CommandBar.ShowWindow();
    }

    // This is called for each window that your tool is active in. Put the functionality of your tool here.
    public override void OnToolGUI(EditorWindow window)
    {

        Debug.Log("Foobar?");

        if (Input.GetKeyDown(KeyCode.Colon)) {
            Debug.Log("foobar");
        }

    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(LaneManager)), CanEditMultipleObjects]
//public class LaneManagerEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        LaneManager laneManager = (LaneManager)target;
//        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg", typeof(GameObject));
//        base.OnInspectorGUI();

//        if(GUILayout.Button("Add Note"))
//        {
//            Debug.Log("So far so good");
//            laneManager.AddNote();
//        }
//    }
//}

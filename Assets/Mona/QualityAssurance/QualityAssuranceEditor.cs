#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Mona
{
    public class QualityAssuranceEditor : EditorWindow
    {
        Vector2 Scroll;

        [MenuItem("Mona/Quality Assurance")]
        public static void Init()
        {
            QualityAssuranceEditor window = (QualityAssuranceEditor)EditorWindow.GetWindow(typeof(QualityAssuranceEditor));

            window.titleContent = new GUIContent("Quality Assurance");
            window.Show();

            QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
        }

        void OnGUI()
        {
            Scroll = GUILayout.BeginScrollView(Scroll, false, false);

            GUIStyle buttonStyle = GUI.skin.button;
            buttonStyle.margin = new RectOffset(30, 30, 10, 10);

            GUILayout.BeginHorizontal();
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/qa.png", typeof(Texture));
            GUI.DrawTexture(new Rect(0, 0, 498, 66), banner, ScaleMode.ScaleToFit, false);
            GUILayout.EndHorizontal();
            GUILayout.Space(64);

            if (QualityAssurance.SpaceErrors == null || QualityAssurance.SpaceErrors.Count == 0)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label("√ No errors found.");
                GUILayout.EndHorizontal();
            }
            else
            {
                foreach (string error in QualityAssurance.SpaceErrors)
                {
                    GUILayout.Space(1);

                    string title = error.Replace(".", " ");
                    title = title.Replace("-", " ");
                    title = title.Substring(0, 1).ToUpper() + title.Substring(1);

                    GUILayout.BeginHorizontal("box");
                    GUILayout.Box(title, GUILayout.MinWidth(100));
                    GUILayout.Label(QualityAssurance.GetErrorDescription(error));
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(1);
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Run Quality Test", buttonStyle))
            {
                QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }
    }

}
#endif
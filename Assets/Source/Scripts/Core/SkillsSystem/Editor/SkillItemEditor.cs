using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Clicker.Core.SkillSystem
{
    [CustomEditor(typeof(SkillItem))]
    public class SkillItemEditor : Editor
    {
        private SkillItem script;
        private SerializedProperty buyedIconProp;
        private SerializedProperty selectedIconProp;
        private SerializedProperty defaultIconProp;
        private SerializedProperty skillScriptProp;

        private SkillEditorWindow skillEditorWindow;

        private bool isDebugInfoOpen;

        public void OnEnable()
        {
            FieldInfo buyedIconFieldInfo = target.GetType().GetField("buyedIcon", BindingFlags.NonPublic | BindingFlags.Instance);
            buyedIconProp = serializedObject.FindProperty(buyedIconFieldInfo.Name);

            FieldInfo selectedIconFieldInfo = target.GetType().GetField("selectedIcon", BindingFlags.NonPublic | BindingFlags.Instance);
            selectedIconProp = serializedObject.FindProperty(selectedIconFieldInfo.Name);

            FieldInfo defaultIconFieldInfo = target.GetType().GetField("defaultIcon", BindingFlags.Instance | BindingFlags.NonPublic);
            defaultIconProp = serializedObject.FindProperty(defaultIconFieldInfo.Name);

            FieldInfo skillScriptFieldInfo = target.GetType().GetField("_skill", BindingFlags.Instance | BindingFlags.NonPublic);
            skillScriptProp = serializedObject.FindProperty(skillScriptFieldInfo.Name);
        }

        public override void OnInspectorGUI()
        {
            script = (SkillItem)target;

            EditorGUILayout.PropertyField(buyedIconProp, new GUIContent("Buyed Icon"));
            EditorGUILayout.PropertyField(selectedIconProp, new GUIContent("Selected Icon"));
            EditorGUILayout.PropertyField(defaultIconProp, new GUIContent("Default Icon"));

            GUILayout.Space(25);

            EditorGUILayout.PropertyField(skillScriptProp, new GUIContent("Skill"));
            if (script.Skill != null && script.GetComponent<SkillBase>() == script.Skill)
            {
                script.SkillCostTypeEditor = (SkillCostType)EditorGUILayout.EnumPopup("Skill Cost Type", script.SkillCostTypeEditor);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                switch (script.SkillCostTypeEditor)
                {
                    case SkillCostType.BOTH:
                        script.SkillPointsCostEditor = EditorGUILayout.IntField("Skill Points Cost", script.SkillPointsCostEditor);
                        script.MoneyCostEditor = EditorGUILayout.IntField("Money Cost", script.MoneyCostEditor);
                        break;
                    case SkillCostType.MONEY:
                        script.MoneyCostEditor = EditorGUILayout.IntField("Money Cost", script.MoneyCostEditor);
                        break;
                    case SkillCostType.SKILL_POINTS:
                        script.SkillPointsCostEditor = EditorGUILayout.IntField("Skill Points Cost", script.SkillPointsCostEditor);
                        break;
                }

                if (GUILayout.Button("Open Skill Editor") && skillEditorWindow == null)
                {
                    skillEditorWindow = SkillEditorWindow.ShowWindow(script.Skill);
                }
                EditorGUILayout.EndVertical();
            }
            else if (script.Skill != null)
            {
                target.GetType().GetField("_skill", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(target, null);
                Debug.LogError("Skill script must be on same object");
            }

            GUILayout.Space(35);

            isDebugInfoOpen = EditorGUILayout.BeginFoldoutHeaderGroup(isDebugInfoOpen, "Debug Info", EditorStyles.helpBox);

            // Debug Info
            if (isDebugInfoOpen)
            {
                GUILayout.Label("Is Buyed: " + script.IsBuyed);
                GUILayout.Label("Is Effect Applyed: " + script.IsEffectsApplyed);
            }

            // Save Changes and other logic
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed)
            {
                if (skillEditorWindow != null && script.Skill == null)
                    skillEditorWindow.Close();

                EditorUtility.SetDirty(script);
                EditorSceneManager.MarkSceneDirty(script.gameObject.scene);
            }
        }
    }
}
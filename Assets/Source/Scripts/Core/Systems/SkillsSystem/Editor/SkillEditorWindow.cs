using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEditor.SceneManagement;

namespace Clicker.Core.SkillSystem
{
	public class SkillEditorWindow : EditorWindow
	{
		private SkillBase skill;
		private SerializedObject serializedObject;
		private SerializedProperty skillNameProp;
		private SerializedProperty skillDescriptionProp;
		private SerializedProperty skillLevelProp;

		private Vector2 descriptionFieldScrollPos;

		public static SkillEditorWindow ShowWindow(SkillBase skill)
		{
			SkillEditorWindow window = GetWindow<SkillEditorWindow>();
			window.Init(skill);
			window.titleContent = new GUIContent("Skill editor");
			window.Show();
			return window;
		}
		void OnEnable()
		{
			minSize = new Vector2(250, 300);
			maxSize = new Vector2(250, 300);
			EditorApplication.hierarchyChanged += CheckComponentExisting;
		}

		public void Init(SkillBase skill)
		{
			this.skill = skill;

			serializedObject = new SerializedObject(skill);

			FieldInfo skillNameField = skill.GetType().GetField("skillName", BindingFlags.NonPublic | BindingFlags.Instance);
			skillNameProp = serializedObject.FindProperty(skillNameField.Name);

			FieldInfo skillDescriptionField = skill.GetType().GetField("description", BindingFlags.NonPublic | BindingFlags.Instance);
			skillDescriptionProp = serializedObject.FindProperty(skillDescriptionField.Name);

			FieldInfo skillLevelField = skill.GetType().GetField("level", BindingFlags.NonPublic | BindingFlags.Instance);
			skillLevelProp = serializedObject.FindProperty(skillLevelField.Name);
		}

		private void CheckComponentExisting()
		{
			if (skill == null || skill.gameObject == null || skill.gameObject.GetComponent<SkillBase>() == null)
				Close();
			else //Save properties changes
				serializedObject.ApplyModifiedProperties();
		}

		public void OnGUI()
		{
			GUIStyle centeredLabelStyle = new(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };


            GUILayout.Space(15);

			GUILayout.BeginHorizontal();
			//---
			GUILayout.Label("Skill Name");
			EditorGUILayout.PropertyField(skillNameProp, GUIContent.none);
			//---
			GUILayout.EndHorizontal();

			GUILayout.Label("---------------------------------------", centeredLabelStyle);

			GUILayout.Label("Skill Description", centeredLabelStyle);

			descriptionFieldScrollPos = EditorGUILayout.BeginScrollView(descriptionFieldScrollPos, GUILayout.Height(100));
			skillDescriptionProp.stringValue = EditorGUILayout.TextArea(skillDescriptionProp.stringValue, GUILayout.ExpandHeight(true));
			EditorGUILayout.EndScrollView();

			GUILayout.Label("---------------------------------------", centeredLabelStyle);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Level", GUILayout.Width(35));
            EditorGUILayout.PropertyField(skillLevelProp, GUIContent.none, GUILayout.Width(30));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            CheckComponentExisting();

			//Save changes
			if (GUI.changed)
			{
				CheckLevelFieldClamp();
				EditorUtility.SetDirty(skill);
				EditorSceneManager.MarkSceneDirty(skill.gameObject.scene);
			}
		}

		private void CheckLevelFieldClamp()
		{
			if(skillLevelProp.intValue < 1)
				skillLevelProp.intValue = 1;
			else if(skillLevelProp.intValue > skill.MaxLevels)
				skillLevelProp.intValue = skill.MaxLevels;
		}
	}
}
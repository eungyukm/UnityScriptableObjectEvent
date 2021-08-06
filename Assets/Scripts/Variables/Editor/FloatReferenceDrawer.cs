using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


namespace Comman.ScriptableObject.Variables
{
    /// <summary>
    /// PropertyDrawers 두 가지 용도
    /// 1. Serializble 클래스의 모든 인스턴스에 대한 GUI를 사용자 정의 함
    /// 2. PropertyAttribute를 사용하여 스크립트 구성원의 GUI를 사용자 정의 함
    /// </summary>

    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : PropertyDrawer
    {
        private readonly string[] popupOptions =
        {
            "Use Constant",
            "Use Variable"
        };

        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // 
            SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("constantValue");
            SerializedProperty variable = property.FindPropertyRelative("variable");


            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

            useConstant.boolValue = result == 0;

            EditorGUI.PropertyField(position,
                useConstant.boolValue ? constantValue : variable,
                GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
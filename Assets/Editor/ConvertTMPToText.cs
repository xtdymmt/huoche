using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Collections.Generic;

public class ConvertTMPToText : EditorWindow
{
    [MenuItem("Tools/Convert TMP to Text")]
    public static void ShowWindow()
    {
        GetWindow<ConvertTMPToText>("TMP to Text Converter");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Convert All TextMeshProUGUI to Text"))
        {
            ConvertAllTMPToText();
        }
    }

    private static void ConvertAllTMPToText()
    {
        // 获取场景中所有 GameObject
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);

        int convertedCount = 0;
        List<GameObject> modifiedObjects = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // 检查是否有 TextMeshProUGUI 组件
            TextMeshProUGUI tmpText = obj.GetComponent<TextMeshProUGUI>();
            if (tmpText != null)
            {
                // 获取 TMP 的属性
                string textContent = tmpText.text;
                Color textColor = tmpText.color;
                FontStyles fontStyle = tmpText.fontStyle;
                float fontSize = tmpText.fontSize;
                TextAnchor alignment = ConvertTMPAlignmentToTextAnchor(tmpText.alignment);

                // 移除 TextMeshProUGUI
                DestroyImmediate(tmpText, true);

                // 添加 Unity UI Text 组件
                Text unityText = obj.AddComponent<Text>();
                unityText.text = textContent;
                unityText.color = textColor;
                unityText.fontSize = Mathf.RoundToInt(fontSize);
                unityText.alignment = alignment;
                obj.AddComponent<Outline>();
                // 设置默认字体（可以改成你的项目默认字体）
                unityText.font = Resources.GetBuiltinResource<Font>("font.ttf");

                modifiedObjects.Add(obj);
                convertedCount++;
            }
        }

        Debug.Log($"转换完成！共转换 {convertedCount} 个 TextMeshProUGUI 到 Text");
        if (modifiedObjects.Count > 0)
        {
            Selection.objects = modifiedObjects.ToArray();
        }
    }

    // 转换 TMP 对齐方式到 TextAnchor
    private static TextAnchor ConvertTMPAlignmentToTextAnchor(TextAlignmentOptions alignment)
    {
        switch (alignment)
        {
            case TextAlignmentOptions.TopLeft:
                return TextAnchor.UpperLeft;
            case TextAlignmentOptions.Top:
                return TextAnchor.UpperCenter;
            case TextAlignmentOptions.TopRight:
                return TextAnchor.UpperRight;
            case TextAlignmentOptions.Left:
                return TextAnchor.MiddleLeft;
            case TextAlignmentOptions.Center:
                return TextAnchor.MiddleCenter;
            case TextAlignmentOptions.Right:
                return TextAnchor.MiddleRight;
            case TextAlignmentOptions.BottomLeft:
                return TextAnchor.LowerLeft;
            case TextAlignmentOptions.Bottom:
                return TextAnchor.LowerCenter;
            case TextAlignmentOptions.BottomRight:
                return TextAnchor.LowerRight;
            default:
                return TextAnchor.MiddleCenter;
        }
    }
}
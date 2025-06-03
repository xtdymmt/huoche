using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 如果是NGUI，需要添加NGUI的引用
#if USING_NGUI
using UnityEngine;
#endif

public class FontReplacer : EditorWindow
{
    private Font targetFontUGUI;
#if USING_NGUI
    private UIFont targetFontNGUI;
#endif
    private bool replaceUGUI = true;
    private bool replaceNGUI = true;

    [MenuItem("Tools/字体一键替换")]
    public static void ShowWindow()
    {
        GetWindow<FontReplacer>("字体替换工具");
    }

    void OnGUI()
    {
        GUILayout.Label("字体替换设置", EditorStyles.boldLabel);

        replaceUGUI = EditorGUILayout.Toggle("替换UGUI Text", replaceUGUI);
        if (replaceUGUI)
        {
            targetFontUGUI = (Font)EditorGUILayout.ObjectField("UGUI字体", targetFontUGUI, typeof(Font), false);
        }

#if USING_NGUI
        replaceNGUI = EditorGUILayout.Toggle("替换NGUI Label", replaceNGUI);
        if (replaceNGUI)
        {
            targetFontNGUI = (UIFont)EditorGUILayout.ObjectField("NGUI字体", targetFontNGUI, typeof(UIFont), false);
        }
#else
        EditorGUILayout.HelpBox("NGUI支持未启用，如需替换NGUI字体请先导入NGUI并定义USING_NGUI宏", MessageType.Info);
#endif

        if (GUILayout.Button("替换场景中所有字体"))
        {
            ReplaceAllFonts();
        }
    }

    void ReplaceAllFonts()
    {
        if (replaceUGUI && targetFontUGUI == null)
        {
            Debug.LogError("请选择UGUI字体");
            return;
        }

#if USING_NGUI
        if (replaceNGUI && targetFontNGUI == null)
        {
            //Debug.LogError("请选择NGUI字体");
            //return;
        }
#endif

        int uguiCount = 0;
        int nguiCount = 0;

        // 获取场景中所有游戏对象
        GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject go in allObjects)
        {
            // 处理UGUI Text
            if (replaceUGUI)
            {
                Text[] texts = go.GetComponentsInChildren<Text>(true);
                foreach (Text text in texts)
                {
                    text.font = targetFontUGUI;
                    EditorUtility.SetDirty(text);
                    uguiCount++;
                }
            }

#if USING_NGUI
            // 处理NGUI Label
            if (replaceNGUI)
            {
                UILabel[] labels = go.GetComponentsInChildren<UILabel>(true);
                foreach (UILabel label in labels)
                {
                    label.trueTypeFont = targetFontUGUI;
                    //label.font = targetFontNGUI;
                    EditorUtility.SetDirty(label);
                    nguiCount++;
                }
            }
#endif
        }

        Debug.Log($"字体替换完成 - UGUI Text: {uguiCount}个, NGUI Label: {nguiCount}个");
        EditorUtility.DisplayDialog("完成", $"字体替换完成\nUGUI Text: {uguiCount}个\nNGUI Label: {nguiCount}个", "确定");
    }
}
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

public class TransformCopyTool : EditorWindow
{
    private Transform[] sourceTransforms = new Transform[0];
    private Transform[] targetTransforms = new Transform[0];
    private Vector2 scrollPos;
    private bool copyPosition = true;
    private bool copyRotation = true;
    private bool copyScale = true;
    private bool showAdvanced = false;

    [MenuItem("Tools/Transform 数组复制工具")]
    public static void ShowWindow()
    {
        var window = GetWindow<TransformCopyTool>();
        window.titleContent = new GUIContent("Transform数组复制");
        window.minSize = new Vector2(400, 300);
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("按数组索引复制Transform", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("将按照数组中元素的顺序一对一复制Transform属性", MessageType.Info);

        // 基础设置
        copyPosition = EditorGUILayout.Toggle("复制位置", copyPosition);
        copyRotation = EditorGUILayout.Toggle("复制旋转", copyRotation);
        copyScale = EditorGUILayout.Toggle("复制缩放", copyScale);

        EditorGUILayout.Space();
        showAdvanced = EditorGUILayout.Foldout(showAdvanced, "高级选项", true);
        if (showAdvanced)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.HelpBox("以下选项会影响复制行为", MessageType.None);
            EditorGUI.indentLevel--;
        }

        // 数组编辑区域
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawArrayField("源物体数组", ref sourceTransforms);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawArrayField("目标物体数组", ref targetTransforms);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndScrollView();

        // 状态信息
        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"匹配情况: {GetValidPairCount()} 对有效配对", EditorStyles.centeredGreyMiniLabel);

        // 操作按钮
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("从选中物体初始化源数组"))
        {
            sourceTransforms = Selection.transforms;
            for (int i = 0; i < sourceTransforms.Length; i++)
            {
                sourceTransforms[i] = sourceTransforms[i].GetChild(0);
            }
        }

        if (GUILayout.Button("从选中物体初始化目标数组"))
        {
            targetTransforms = Selection.transforms;
            for (int i = 0; i < targetTransforms.Length; i++)
            {
                targetTransforms[i] = targetTransforms[i].GetChild(0);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUI.enabled = GetValidPairCount() > 0;
        if (GUILayout.Button("执行复制", GUILayout.Height(30)))
        {
            CopyTransformsByIndex();
        }
        GUI.enabled = true;

        if (GUILayout.Button("清空数组", GUILayout.Height(30)))
        {
            sourceTransforms = new Transform[0];
            targetTransforms = new Transform[0];
        }
        EditorGUILayout.EndHorizontal();
    }

    void DrawArrayField(string label, ref Transform[] array)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        int newSize = EditorGUILayout.IntField("数组大小", array.Length);

        if (newSize != array.Length)
        {
            System.Array.Resize(ref array, newSize);
        }

        EditorGUI.indentLevel++;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = EditorGUILayout.ObjectField($"元素 {i}", array[i], typeof(Transform), true) as Transform;
        }
        EditorGUI.indentLevel--;
    }

    int GetValidPairCount()
    {
        int count = Mathf.Min(sourceTransforms.Length, targetTransforms.Length);
        int validCount = 0;

        for (int i = 0; i < count; i++)
        {
            if (sourceTransforms[i] != null && targetTransforms[i] != null)
            {
                validCount++;
            }
        }

        return validCount;
    }

    void CopyTransformsByIndex()
    {
        int pairCount = Mathf.Min(sourceTransforms.Length, targetTransforms.Length);
        if (pairCount == 0)
        {
            EditorUtility.DisplayDialog("错误", "没有有效的物体配对", "确定");
            return;
        }

        // 收集所有需要撤销操作的目标物体
        var validTargets = targetTransforms.Take(pairCount)
            .Where(t => t != null && sourceTransforms[System.Array.IndexOf(targetTransforms, t)] != null)
            .ToArray();

        Undo.RecordObjects(validTargets, "Copy Transforms");

        int successCount = 0;
        for (int i = 0; i < pairCount; i++)
        {
            if (sourceTransforms[i] == null || targetTransforms[i] == null) continue;

            if (copyPosition) targetTransforms[i].position = sourceTransforms[i].position;
            if (copyRotation) targetTransforms[i].rotation = sourceTransforms[i].rotation;
            if (copyScale) targetTransforms[i].localScale = sourceTransforms[i].localScale;

            successCount++;
            EditorUtility.SetDirty(targetTransforms[i]);
        }

        Debug.Log($"成功复制 {successCount}/{pairCount} 个物体的Transform属性");
        EditorUtility.DisplayDialog("完成",
            $"复制操作完成\n成功: {successCount}个\n总数: {pairCount}个", "确定");
    }

    // 自动填充选中物体
    void OnSelectionChange()
    {
        Repaint();
    }
}
#endif
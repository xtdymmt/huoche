using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class TextureBatchProcessor : EditorWindow
{
    private TextureImporterPlatformSettings androidSettings;
    private TextureImporterPlatformSettings webglSettings;
    private bool applyToSelected = true;
    private bool resizeLargeTextures = true;
    private int maxTextureSize = 2048;

    [MenuItem("Tools/批量处理图片设置")]
    public static void ShowWindow()
    {
        GetWindow<TextureBatchProcessor>("批量图片处理");
    }

    private void OnEnable()
    {
        // 默认Android设置
        androidSettings = new TextureImporterPlatformSettings
        {
            name = "Android",
            overridden = true,
            format = TextureImporterFormat.ASTC_6x6,
            maxTextureSize = 2048,
            compressionQuality = 50
        };

        // 默认WebGL设置
        webglSettings = new TextureImporterPlatformSettings
        {
            name = "WebGL",
            overridden = true,
            format = TextureImporterFormat.ASTC_6x6,
            maxTextureSize = 2048,
            compressionQuality = 50
        };
    }

    void OnGUI()
    {
        GUILayout.Label("平台压缩设置", EditorStyles.boldLabel);

        androidSettings.format = (TextureImporterFormat)EditorGUILayout.EnumPopup("Android格式", androidSettings.format);
        androidSettings.maxTextureSize = EditorGUILayout.IntField("Android最大尺寸", androidSettings.maxTextureSize);

        webglSettings.format = (TextureImporterFormat)EditorGUILayout.EnumPopup("WebGL格式", webglSettings.format);
        webglSettings.maxTextureSize = EditorGUILayout.IntField("WebGL最大尺寸", webglSettings.maxTextureSize);

        EditorGUILayout.Space();
        resizeLargeTextures = EditorGUILayout.Toggle("自动调整大尺寸图片", resizeLargeTextures);
        maxTextureSize = EditorGUILayout.IntField("尺寸减半阈值", maxTextureSize);

        applyToSelected = EditorGUILayout.Toggle("仅处理选中对象", applyToSelected);

        if (GUILayout.Button("开始处理"))
        {
            ProcessTextures();
        }
    }

    void ProcessTextures()
    {
        string[] paths;
        if (applyToSelected)
        {
            // 获取选中的图片
            paths = GetSelectedTexturePaths();
        }
        else
        {
            // 获取项目中所有图片
            paths = Directory.GetFiles("Assets", "*.png", SearchOption.AllDirectories);
            //paths = paths.Concat(Directory.GetFiles("Assets", "*.jpg", SearchOption.AllDirectories)).ToArray();
        }

        int processedCount = 0;
        foreach (string path in paths)
        {
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null) continue;

            // 自动调整大尺寸图片
            if (resizeLargeTextures)
            {
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                if (texture != null && (texture.width > maxTextureSize || texture.height > maxTextureSize))
                {
                    importer.maxTextureSize = Mathf.NextPowerOfTwo(Mathf.Max(texture.width, texture.height) / 4);
                    androidSettings.maxTextureSize = Mathf.NextPowerOfTwo(Mathf.Max(texture.width, texture.height) / 4);
                    webglSettings.maxTextureSize = Mathf.NextPowerOfTwo(Mathf.Max(texture.width, texture.height) / 4);
                }
            }

            // 应用平台设置
            importer.SetPlatformTextureSettings(androidSettings);
            importer.SetPlatformTextureSettings(webglSettings);

            // 保存修改
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
            processedCount++;
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("完成", $"已处理 {processedCount} 张图片", "确定");
    }

    string[] GetSelectedTexturePaths()
    {
        List<string> paths = new List<string>();
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".jpeg"))
            {
                paths.Add(path);
            }
        }
        return paths.ToArray();
    }
}
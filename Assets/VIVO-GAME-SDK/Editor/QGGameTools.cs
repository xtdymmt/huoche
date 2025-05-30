﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Rendering;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;

namespace QGMiniGame
{
    public class QGGameTools
    {

        public static void OpenIssueGihub() 
        {
            string githubUrl = "https://github.com/vivominigame/issues/issues";

            Application.OpenURL(githubUrl);

        }

        public static void OpenVivoGame() 
        {
            string vivoGameUrl = "http://minigame.vivo.com.cn/documents/#/lesson/base/start";

            Application.OpenURL(vivoGameUrl);
        }

        public static void OpenUnityGame() 
        {
            string vivoGameUrl = "https://minigame.vivo.com.cn/documents/#/lesson/engine/unity/engine-unity-home";

            Application.OpenURL(vivoGameUrl);
        }

        public static void OpenQuestionGithub() 
        {
            string questionUrl = "https://github.com/vivominigame/issues/issues/246";

            Application.OpenURL(questionUrl);
        }


        // 检查更新
        public static void CheckUpdate()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://vg-palace.vivo.com.cn/api-v0/config/get?configId=17");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 20000;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            var updateResonse = JsonUtility.FromJson<UpdateResonse>(responseContent);
            if (updateResonse == null) {
                QGLog.LogWarning("更新接口Json数据异常,请尝试重新打开");
                return;
            }
            UpdateConfig mConfig = updateResonse.data;
            if (mConfig != null && QG.SDK_VERSION < mConfig.version && mConfig.pluginUrl != String.Empty)
            {
                if (EditorUtility.DisplayDialog("VIVO小游戏插件更新提示", "插件有更新\n是否立即更新", "是", "否"))
                {
                    Application.OpenURL(mConfig.pluginUrl);
                }
                else
                {
                    QGLog.LogWarning("有更新版本插件，请及时更新：<a href=\"" + mConfig.pluginUrl + "\">" + mConfig.pluginUrl + "</a>");
                }
            }
            else
            {
                QGLog.Log("VIVO小游戏插件无需更新");
            }
        }

        // 设置打包成 webgl的参数 
        public static void SetPlayer()
        {
#if UNITY_2021
            PlayerSettings.colorSpace = ColorSpace.Gamma;
#endif
            PlayerSettings.runInBackground = false;
            PlayerSettings.WebGL.threadsSupport = false;
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.WebGL, false);
            GraphicsDeviceType[] targets = { GraphicsDeviceType.OpenGLES2 };
            PlayerSettings.SetGraphicsAPIs(BuildTarget.WebGL, targets);
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
            PlayerSettings.WebGL.template = "APPLICATION:Minimal";
            EditorSettings.spritePackerMode = SpritePackerMode.AlwaysOnAtlas;
            PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm;
        }

        // 删除文件夹
        public static void DelectDir(string srcPath)
        {
            if (!Directory.Exists(srcPath))
            {
                return;
            }
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();

                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(i.FullName);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //构建配置文件
        public static void CreateEnvConfig(string loadingSrc, string addressableSrc, string webglSrc, ArrayList assetsList,bool useSubPkgLoading)
        {
            string preloadUrl = "";
            foreach (string i in assetsList)
            {
                preloadUrl += i;
                if (assetsList.IndexOf(i) != assetsList.Count - 1) {
                    preloadUrl += ";";
                }
            }
            QGLog.Log("[BuildConfig] Start: Please Waitting");
            EnvConfig config = new EnvConfig();
            config.wasmUrl = loadingSrc;
            config.streamingAssetsUrl = addressableSrc;
            config.preloadUrl = preloadUrl;
            config.subUnityPkg = useSubPkgLoading;
            System.IO.File.WriteAllText(webglSrc + "/build/env.conf", JsonUtility.ToJson(config));
            QGLog.Log("[BuildConfig] end");
        }

        //构建WebGL
        public static void BuildWebGL(string srcPath)
        {
            QGLog.Log("[BuildWebGL] Start: Please Waitting");
            BuildOptions option = BuildOptions.None;
            BuildPipeline.BuildPlayer(GetScenePaths(), srcPath, BuildTarget.WebGL, option);
            QGLog.Log("[BuildWebGL] Done: " + srcPath);
        }

        //构建小游戏
        public static void ConvetWebGl(string buildSrc, string webglSrc, bool useSelfLoading,bool useSubPkgLoading)
        {
            QGLog.Log("[BuildMiniGame] Start: Please Waitting");

            // 清空文件 
            string webglVivoPath = Path.Combine(buildSrc, "webgl_vivo");
            DelectDir(webglVivoPath);

            // 获取生成的framework原始代码

#if UNITY_2020_1_OR_NEWER
            string frameworkContent = File.ReadAllText(Path.Combine(webglSrc, "Build", "webgl.framework.js"), Encoding.UTF8);
#else
            string frameworkContent = File.ReadAllText(Path.Combine(webglSrc, "Build", "webgl.wasm.framework.unityweb"), Encoding.UTF8);
#endif

            // 替换规则
            int i;
            for (i = 0; i < QGReplaceRules.frameworkRules.Length; i++)
            {
                var rule = QGReplaceRules.frameworkRules[i];
                frameworkContent = Regex.Replace(frameworkContent, rule.oldStr, rule.newStr);
            }


            // 拷贝文件
            CopyDirectory(Path.Combine(Application.dataPath, "VIVO-GAME-SDK", "Default"), webglVivoPath, true);
            CopyDirectory(Path.Combine(webglSrc, "Build"), Path.Combine(webglVivoPath, "buildUnity"), true);


            // 生成替换后的framework
            string frameworkFileName = "webgl.wasm.framework.unityweb";
            File.WriteAllText(Path.Combine(webglVivoPath, "buildUnity", frameworkFileName), frameworkContent, new UTF8Encoding(false));

            // 生成配置文件
#if UNITY_2020_1_OR_NEWER
            WebGlConfig config = new WebGlConfig();
            File.WriteAllText(Path.Combine(webglVivoPath, "buildUnity", "webgl.json"), JsonUtility.ToJson(config));
#endif
            //ResetMainfest(webglVivoPath, package);

            // 自定义loading
            if (useSelfLoading) {
                HandleGzip(webglVivoPath);
            }

            if (useSubPkgLoading){
                HandleSubPkgLoading(webglVivoPath);
            }



            ShowInExplorer(buildSrc);
        }
        //GameName = "";
        //versionName = "";
        //minPlatformVersion = ""
        //deviceOrientation = "";
        //private static void ResetMainfest(string buildSrc,string package,string name,string versionName,string minPlatformVersion,string deviceOrientation)
        public static void ResetMainfest(string buildSrc, string package, string GameName, string versionName, string minPlatformVersion, string deviceOrientation)
        {
            string webglVivoPath = Path.Combine(buildSrc, "webgl_vivo");

            string oldPackage = "\"package\": \"com.application.demo.vivominigame\"";
            string newPackage = "\"package\": \"" + package + "\"";

            string oldGameName = "\"name\": \"webgl_vivo\"";
            string newGameName = "\"name\": \"" + GameName + "\"";

            string oldVersionName = "\"versionName\": \"1.0.0\"";
            string newVersionName = "\"versionName\": \"" + versionName + "\"";

            string oldMinPlatformVersion = "\"minPlatformVersion\": \"1104\"";
            string newMinPlatformVersion = "\"minPlatformVersion\": \"" + minPlatformVersion + "\"";

            string oldDeviceOrientation = "\"deviceOrientation\": \"portrait\"";
            string newDeviceOrientation = "\"deviceOrientation\": \"" + deviceOrientation + "\"";

            string oldHomePage = "\"icon\": \"/image/logo.png\"";
            string newHomePage = "\"icon\": \"/image/logo.png\",\n  \"homePage\": \"/image/start.png\"";

            //更改清单文件中的配置
            String manifestStr = File.ReadAllText(Path.Combine(webglVivoPath, "src", "manifest.json"), Encoding.UTF8);

            manifestStr = Regex.Replace(manifestStr, oldPackage, newPackage);
            manifestStr = Regex.Replace(manifestStr, oldGameName, newGameName);
            manifestStr = Regex.Replace(manifestStr, oldVersionName, newVersionName);
            manifestStr = Regex.Replace(manifestStr, oldMinPlatformVersion, newMinPlatformVersion);
            manifestStr = Regex.Replace(manifestStr, oldDeviceOrientation, newDeviceOrientation);
            manifestStr = Regex.Replace(manifestStr, oldHomePage, newHomePage);
            File.WriteAllText(Path.Combine(webglVivoPath, "src", "manifest.json"), manifestStr, new UTF8Encoding(false));
        }


            private static void HandleSubPkgLoading(string webglVivoPath)
        {
            //生成分包的目录结构
            string dataPath = Path.Combine(webglVivoPath, "buildUnity", "webgl.data.unityweb");
            string codePath = Path.Combine(webglVivoPath, "buildUnity", "webgl.wasm.code.unityweb");
            //yuan


            if (!File.Exists(dataPath) || !File.Exists(codePath))
            {
                QGLog.LogError("[BuildMiniGame] Gzip error: file not exist");
                return;
            }

            string subPkgPath = Path.Combine(webglVivoPath, "src","unitySubPkg");

            if (!Directory.Exists(subPkgPath))
            {
                Directory.CreateDirectory(subPkgPath);
            }

            string subPkgRootPath = Path.Combine(subPkgPath, "game.js");
            if (!File.Exists(subPkgRootPath)) {
                File.Create(subPkgRootPath).Dispose();
            }

            File.Copy(dataPath, Path.Combine(subPkgPath, "webgl.data.unityweb"));
            File.Copy(codePath, Path.Combine(subPkgPath, "webgl.wasm.code.unityweb"));

            File.Delete(dataPath);
            File.Delete(codePath);    

            //更改清单文件中的配置
            String manifestStr = File.ReadAllText(Path.Combine(webglVivoPath, "src", "manifest.json"), Encoding.UTF8);

            manifestStr = Regex.Replace(manifestStr, QGReplaceRules.manifestRule.oldStr, QGReplaceRules.manifestRule.newStr);

            File.WriteAllText(Path.Combine(webglVivoPath, "src", "manifest.json"), manifestStr, new UTF8Encoding(false));
        }


        private static void HandleGzip(string webglVivoPath) {

            string dataPath = Path.Combine(webglVivoPath, "buildUnity", "webgl.data.unityweb");
            string codePath = Path.Combine(webglVivoPath, "buildUnity", "webgl.wasm.code.unityweb");
            if (!File.Exists(dataPath) || !File.Exists(codePath)) {
                QGLog.LogError("[BuildMiniGame] Gzip error: file not exist");
                return;
            }

            string gzipTempPath = Path.Combine(webglVivoPath, "tempGzip");
            string gzipPath = Path.Combine(webglVivoPath, "gzip");

            if (!Directory.Exists(gzipTempPath)) {
                Directory.CreateDirectory(gzipTempPath);
            }
            if (!Directory.Exists(gzipPath))
            {
                Directory.CreateDirectory(gzipPath);
            }

            FileInfo dataInfo = new FileInfo(dataPath);
            FileInfo codeInfo = new FileInfo(codePath);
            

            ZipFile zipFile = ZipFile.Create(Path.Combine(gzipPath, "wasm.zip"));
            zipFile.BeginUpdate();
            zipFile.Add(dataInfo.FullName, "online_mini.data.unityweb");
            zipFile.Add(codeInfo.FullName, "online_mini.wasm.code.unityweb");
            zipFile.CommitUpdate();
            zipFile.Close();
            dataInfo.Delete();
            codeInfo.Delete();

            // 替换配置文件
            string webglJson = File.ReadAllText(Path.Combine(webglVivoPath, "buildUnity", "webgl.json"), Encoding.UTF8);
            int i;
            for (i = 0; i < QGReplaceRules.jsonConfigRules.Length; i++)
            {
                var rule = QGReplaceRules.jsonConfigRules[i];
                webglJson = Regex.Replace(webglJson, rule.oldStr, rule.newStr);
            }
            File.WriteAllText(Path.Combine(webglVivoPath, "buildUnity", "webgl.json"), webglJson);

            DelectDir(gzipTempPath);
            Directory.Delete(gzipTempPath);
        }


        private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            var separator = Path.DirectorySeparatorChar;
            var ignoreFiles = new List<string>() { "webgl.loader.js", "webgl.wasm.framework.unityweb" , "webgl.framework.js" , "UnityLoader.js" };

            RenameFile[] renameFiles = {
                new RenameFile()
                {
                    oldName = "webgl.data",
                    newName = "webgl.data.unityweb"
                },
                 new RenameFile()
                {
                    oldName = "webgl.wasm",
                    newName = "webgl.wasm.code.unityweb"
                }
            };
            var ignoreDirs = new List<string>() {};
            try
            {

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }
                    else
                    {
                        // 已经存在，删掉目录下无用的文件
                        foreach (string filename in ignoreFiles)
                        {
                            var filepath = Path.Combine(DestinationPath, filename);
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);
                            }
                        }
                        foreach (string dir in ignoreDirs)
                        {
                            var dirpath = Path.Combine(DestinationPath, dir);
                            if (Directory.Exists(dirpath))
                            {
                                Directory.Delete(dirpath);
                            }
                        }
                    }

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {

                        FileInfo flinfo = new FileInfo(fls);
                        if (flinfo.Extension == ".meta" || ignoreFiles.Contains(flinfo.Name))
                        {
                            continue;
                        }

                        string targetFileName = flinfo.Name;
                        foreach (RenameFile renameFile in renameFiles)
                        {
                            if (renameFile.oldName.Equals(flinfo.Name)) {
                                targetFileName = renameFile.newName;
                                break;
                            }
                        }

                        flinfo.CopyTo(Path.Combine(DestinationPath, targetFileName), overwriteexisting);

                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (ignoreDirs.Contains(drinfo.Name))
                        {
                            continue;
                        }
                        if (CopyDirectory(drs, Path.Combine(DestinationPath, drinfo.Name), overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                UnityEngine.Debug.LogError(ex);
            }
            return ret;
        }


        //获取游戏中的场景
        public static string[] GetScenePaths()
        {
            List<string> scenes = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var scene = EditorBuildSettings.scenes[i];

                if (scene.enabled)
                {
                    scenes.Add(scene.path);
                }
            }

            return scenes.ToArray();
        }

        //打开指定的文件 
        public static void ShowInExplorer(string path)
        {
            if (IsInWinOS)
            {
                OpenInWin(path);
            }
            else if (IsInMacOS)
            {
                OpenInMac(path);
            }
            else
            {
                QGLog.LogError("ShowInExplorer error not mac and win");
            }
        }

        //配置文件获取
        public static QGGameConfig GetEditorConfig()
        {
            var config = AssetDatabase.LoadAssetAtPath("Assets/VIVO-GAME-SDK/Editor/QGGameConfig.asset", typeof(QGGameConfig)) as QGGameConfig;
            if (config == null)
            {
                AssetDatabase.CreateAsset(EditorWindow.CreateInstance<QGGameConfig>(), "Assets/VIVO-GAME-SDK/Editor/QGGameConfig.asset");
                config = AssetDatabase.LoadAssetAtPath("Assets/VIVO-GAME-SDK/Editor/QGGameConfig.asset", typeof(QGGameConfig)) as QGGameConfig;
            }
            return config;
        }

        //配置文件设置
        public static void setEditorConfig(string buildSrc,string PackageName, string GameName, string versionName, string minPlatformVersion, string deviceOrientation,
            string wasmUrl, string streamingAssetsUrl, string assetsUrl1, string assetsUrl2, string assetsUrl3, string assetsUrl4, string assetsUrl5,bool useSelfLoading,bool useAddressable,bool usePreAsset)
        {
            var config = GetEditorConfig();
            if (buildSrc != String.Empty)
            {
                config.buildSrc = buildSrc;
            }
            if (PackageName != String.Empty)
            {
                config.PackageName = PackageName;
            }
            if (GameName != String.Empty)
            {
                config.GameName = GameName;
            }
            if (versionName != String.Empty)
            {
                config.versionName = versionName;
            }
            if (minPlatformVersion != String.Empty)
            {
                config.minPlatformVersion = minPlatformVersion;
            }
            if (deviceOrientation != String.Empty)
            {
                config.deviceOrientation = deviceOrientation;
            }
            if (assetsUrl1 != String.Empty)
            {
                config.assetsUrl1 = assetsUrl1;
            }
            if (assetsUrl2 != String.Empty)
            {
                config.assetsUrl2 = assetsUrl2;
            }
            if (assetsUrl3 != String.Empty)
            {
                config.assetsUrl3 = assetsUrl3;
            }
            if (assetsUrl4 != String.Empty)
            {
                config.assetsUrl4 = assetsUrl4;
            }
            if (assetsUrl5 != String.Empty)
            {
                config.assetsUrl5 = assetsUrl5;
            }
            if (wasmUrl != String.Empty)
            {
                config.envConfig.wasmUrl = wasmUrl;
            }
            if (streamingAssetsUrl != String.Empty)
            {
                config.envConfig.streamingAssetsUrl = streamingAssetsUrl;
            }
            config.useAddressable = useAddressable;
            config.usePreAsset = usePreAsset;
            config.useSelfLoading = useSelfLoading;
            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssets();
        }

        private static bool IsInMacOS
        {
            get
            {
                return SystemInfo.operatingSystem.IndexOf("Mac OS") != -1;
            }
        }

        private static bool IsInWinOS
        {
            get
            {
                return SystemInfo.operatingSystem.IndexOf("Windows") != -1;
            }
        }

        private static void OpenInMac(string path)
        {
            bool openInsidesOfFolder = false;

            // try mac
            string macPath = path.Replace("\\", "/");

            if (Directory.Exists(macPath))
            {
                openInsidesOfFolder = true;
            }

            if (!macPath.StartsWith("\""))
            {
                macPath = "\"" + macPath;
            }

            if (!macPath.EndsWith("\""))
            {
                macPath = macPath + "\"";
            }

            string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;

            try
            {
                System.Diagnostics.Process.Start("open", arguments);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                e.HelpLink = "";
            }
        }

        private static void OpenInWin(string path)
        {
            bool openInsidesOfFolder = false;

            string winPath = path.Replace("/", "\\");

            if (Directory.Exists(winPath))
            {
                openInsidesOfFolder = true;
            }

            try
            {
                System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                e.HelpLink = "";
            }
        }

    }
}

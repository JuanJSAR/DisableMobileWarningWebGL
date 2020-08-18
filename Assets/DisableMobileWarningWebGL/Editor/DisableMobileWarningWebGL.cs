using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;

public class DisableMobileWarningWebGL
{
#if UNITY_WEBGL
#if UNITY_2017 || UNITY_2018
    [PostProcessBuild]
#elif UNITY_2019_1_OR_NEWER
    [PostProcessBuildAttribute(1)]
#endif
    public static void OnPostProcessBuild(BuildTarget target, string targetPath)
    {
        var path = Path.Combine(targetPath, "Build/UnityLoader.js");
        var text = File.ReadAllText(path);
#if UNITY_2017
        text = Regex.Replace(text, @"compatibilityCheck:function\(e,t,r\)\{.+,Blobs:\{\},loadCode", "compatibilityCheck:function(e,t,r){t()},Blobs:{},loadCode");
#elif UNITY_2018_1_OR_NEWER
        text = text.Replace("UnityLoader.SystemInfo.mobile", "false");
#endif
        File.WriteAllText(path, text);
    }
#endif
}
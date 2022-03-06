using GOIModelReplacement;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: AssemblyVersion(PluginInfo.VERSION)]
[assembly: AssemblyTitle(PluginInfo.NAME + " (" + PluginInfo.GUID + ")")]
[assembly: AssemblyProduct(PluginInfo.NAME)]

namespace GOIModelReplacement
{
    /// <summary>
    /// Plugin infomation
    /// Based on https://github.com/BepInEx/BepInEx.PluginTemplate/blob/main/src/PluginInfo.cs
    /// </summary>
    internal static class PluginInfo
    {
        public const string GUID = "com.jor02.GOIModelReplacement";
        public const string NAME = "GOI Model Replacement";
        public const string VERSION = "0.1.0";
    }
}

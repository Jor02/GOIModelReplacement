using BepInEx;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx.Configuration;
using GOIModelReplacement.Core;
using System.IO;

namespace GOIModelReplacement
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Base : BaseUnityPlugin
    {
        void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene target, LoadSceneMode mode)
        {
            if (target.name == "Loader") // If we loaded the main menu
            {
                StartCoroutine(SetupMenu());
            } else if (target.name == "Mian" && mode != LoadSceneMode.Additive) // If we loaded into game
            {
                LoadCosmetics();
            }
        }

        private void LoadCosmetics()
        {
            AssetBundle.UnloadAllAssetBundles(true);

            string path = Application.dataPath + "\\..\\";
            path = Path.Combine(path, "Mods\\Cosmetics\\");
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    AssetBundle Cosmetic = null;
                    try
                    {
                        Cosmetic = AssetBundle.LoadFromFile(file);
                    }
                    catch (System.Exception) {}

                    if (Cosmetic == null) continue;

                    GameObject[] prefabs = Cosmetic.LoadAllAssets<GameObject>();

                    if (prefabs.Length < 1) Debug.LogWarning("No prefabs found in bundle");

                    foreach (GameObject prefab in prefabs)
                    {
                        Instantiate(prefab);
                    }

                    Cosmetic.Unload(false);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }

        private IEnumerator SetupMenu()
        {
            yield return null;

            Transform UI = GameObject.Find("/Canvas").transform;
            //Transform Column = UI.Find("Column");
            //GameObject templateButton = UI.Find("Column/Quit").gameObject;

            //Include mod version in VERSION
            TextMeshProUGUI GOIVersion = UI.Find("Version").GetComponent<TextMeshProUGUI>();
            GOIVersion.overflowMode = TextOverflowModes.Overflow;
            GOIVersion.alignment = TextAlignmentOptions.TopRight;
            GOIVersion.enableAutoSizing = true;
            GOIVersion.text += "<br> Cosmetics!";

            /*
            #region Setup Custom Button
            //Create button generator
            MenuButtons menuButtonGenerator = new MenuButtons();
            menuButtonGenerator.Init(templateButton.transform, Column);
            menuButtonGenerator.AddButton("Cosmetics", null);
            #endregion
            */
        }
    }
}

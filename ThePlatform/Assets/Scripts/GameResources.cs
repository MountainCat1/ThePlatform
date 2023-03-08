using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.Networking;

public static class GameResources
{
    private static IResourceManager ResourceManager { get; }

    static GameResources()
    {
 #if UNITY_EDITOR
        // TODO: start using EditorAssetsResourceManager 
        ResourceManager = new AssetBundleResourceManager();
#else
        ResourceManager = new AssetBundleResourceManager();
#endif
    }

    public static IEnumerable<T> LoadAll<T>(string bundleName) where T : Object
    {
        return ResourceManager.LoadAllAssets<T>(bundleName);
    }

    public static T Load<T>(string bundleName, string assetName) where T : Object
    {
        return ResourceManager.LoadAsset<T>(bundleName, assetName);
    }

    public static T[] LoadSubAssets<T>(string bundleName, string assetName) where T : Object
    {
        return ResourceManager.LoadSubAssets<T>(bundleName, assetName);
    }

    public static string[] GetAssetNames(string bundleName)
    {
        return ResourceManager.GetAssetsNamesInBundle(bundleName);
    }
}
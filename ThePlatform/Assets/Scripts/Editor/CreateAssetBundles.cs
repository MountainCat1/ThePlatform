using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";

        // CopyFolderStructure( Path.Combine("Assets", EditorAssetsResourceManager.AssetResourcesDirectory), assetBundleDirectory);
        
        if(!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
            BuildAssetBundleOptions.None, 
            BuildTarget.StandaloneWindows);

        
        var targetAssetBundleDirectory 
            = Path.Combine(new DirectoryInfo(Application.dataPath).Parent!.ToString(), "Build/SpaceGame_Data/StreamingAssets/AssetBundles");

        Directory.CreateDirectory(targetAssetBundleDirectory);
        
        CopyAll(assetBundleDirectory, targetAssetBundleDirectory);
    }
    
    
    private static void CopyAll(string source, string target)
    {
        Directory.CreateDirectory(target);

        foreach (var file in Directory.GetFiles(source))
        {
            string targetFile = Path.Combine(target, Path.GetFileName(file));
            File.Copy(file, targetFile, true);
        }

        foreach (var directory in Directory.GetDirectories(source))
        {
            string targetDir = Path.Combine(target, Path.GetFileName(directory));
            CopyAll(directory, targetDir);
        }
    }
    
    private static void CopyFolderStructure(string sourcePath, string destinationPath)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourcePath);
        DirectoryInfo[] subdirs = dir.GetDirectories();

        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        // Copy the directory structure recursively.
        foreach (DirectoryInfo subdir in subdirs)
        {
            string destPath = Path.Combine(destinationPath, subdir.Name);
            CopyFolderStructure(subdir.FullName, destPath);
        }
    }
}

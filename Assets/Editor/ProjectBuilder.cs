using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;


class ProjectBuilder {
	static string[] SCENES = FindEnabledEditorScenes();
	static string APP_NAME = "Unity3D_Project";
	
	[MenuItem ("Custom/CI/Build PC")]
	static void PerformAndroidBuild ()
	{
		string target_filename = APP_NAME + ".exe";
		GenericBuild(SCENES, target_filename, BuildTarget.StandaloneWindows ,BuildOptions.None);
	}
	
	private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_filename, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes, target_filename, build_target, build_options);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
}

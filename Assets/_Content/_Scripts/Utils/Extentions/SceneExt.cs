using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneExt
{
	public static IEnumerator LoadSceneAsync(int sceneIndex, LoadSceneMode loadMode, Action action)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, loadMode);
		yield return new WaitUntil(() => asyncLoad.isDone);
		action?.Invoke();
	}

	public static IEnumerator UnloadSceneAsync(int sceneIndex, Action action)
	{
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex);
		yield return new WaitUntil(() => asyncLoad.isDone);
		action?.Invoke();
	}
}
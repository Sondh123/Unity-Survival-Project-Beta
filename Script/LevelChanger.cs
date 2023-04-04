using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	public Animator animator;

	// Update is called once per frame
	public void FadeToMenu(string stageToPlay)
	{
		animator.SetTrigger("FadeOut");
		SceneManager.LoadScene(stageToPlay, LoadSceneMode.Single);
		animator.SetTrigger("FadeOut");
	}

	public void FadeToGamePlay(string stageToPlay)
	{		
		animator.SetTrigger("FadeOut");
		SceneManager.LoadScene("Essential", LoadSceneMode.Single);
		SceneManager.LoadScene(stageToPlay, LoadSceneMode.Additive);
		
	}
}

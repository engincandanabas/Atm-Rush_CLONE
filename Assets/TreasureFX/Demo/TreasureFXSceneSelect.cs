using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace TreasureFX
{

public class TreasureFXSceneSelect : MonoBehaviour
{
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	
	public void LoadSceneDemo1()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void LoadSceneDemo2()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void LoadSceneDemo3()
    {
        SceneManager.LoadScene("Scene3");
    }
    public void LoadSceneDemo4()
    {
        SceneManager.LoadScene("Scene4");
    }
    public void LoadSceneDemo5()
    {
        SceneManager.LoadScene("Scene5");
    }
    public void LoadSceneDemo6()
    {
        SceneManager.LoadScene("Scene6");
    }
    public void LoadSceneDemo7()
    {
        SceneManager.LoadScene("Scene7");
    }
    public void LoadSceneDemo8()
    {
        SceneManager.LoadScene("Scene8");
    }
    public void Load3DSceneDemo1()
    {
        SceneManager.LoadScene("3DScene1");
    }
    public void Load3DSceneDemo2()
    {
        SceneManager.LoadScene("3DScene2");
    }
    public void Load3DSceneDemo3()
    {
        SceneManager.LoadScene("3DScene3");
    }
	
	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.J))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("SceneSelectCanvas3D").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("SceneSelectCanvas3D").GetComponent<Canvas> ().enabled = true;
         }
     }
	      if(Input.GetKeyDown(KeyCode.K))
	 {
         GUIHide2 = !GUIHide2;
     
         if (GUIHide2)
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
         }
     }
	 }
}
}
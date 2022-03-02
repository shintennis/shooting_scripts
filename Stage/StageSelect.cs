using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public void Stage_1_Button()
    {
        SceneManager.LoadScene("Stage");
    }
    public void testStageButton()
    {
        SceneManager.LoadScene("testStage");
    }
    
    public void Stage_2_Button()
    {
        SceneManager.LoadScene("Stage_2");
    }
    public void Stage_3_Button()
    {
        SceneManager.LoadScene("Stage_3");
    }

    
}

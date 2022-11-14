using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour {
    public InputActionReference actionToExit;
    public TMP_Text Titles;
    public TMP_Text Names;
    public TextAsset TextAssetData;

    public void Start() {
        actionToExit.action.Enable();
        actionToExit.action.performed += Exit;
        ReadTextData();
    }

    private void ReadTextData() {
        string[] data = TextAssetData.text.Split(new string[] { "," }, System.StringSplitOptions.None);

        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
        }

        /*for(int i = 0; i < data.Length; i++) {
            switch(data[i])
            {
                case "Design":
                    Titles.text = data[i];
                    break;
                default:

                    break;
            }
            if (data[i].Equals("Design"))
            {

            }
        }*/

    }

    public void Exit(InputAction.CallbackContext action) {
        Debug.Log("Trying to exit");
        action.action.performed -= Exit;
        SceneManager.LoadScene(0);
    }
}

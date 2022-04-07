using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public Text textItems;
    public Text textWinner;
    public Text textLoser;
    public int itemCount;
    public int[] totalItems;
    private bool isGameCompleted = false;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        totalItems = new int[] {0,0,0};
        itemCount = 0;
        textItems.text = "Items: " + itemCount;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayMusic();
    }

    void OnGUI() {
        if (isGameCompleted) {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 30;
            if ( GUI.Button(new Rect(Screen.width/2-Screen.width/16, Screen.height/2-Screen.height/16, Screen.width/8, Screen.height/8), "REJUGAR", myButtonStyle)) {
                restartGame();
                SceneManager.LoadScene(0);
            }
        }
    }

    void restartGame() {
        isGameCompleted = false;
        itemCount = 0;
        textItems.text = "Items: " + itemCount;
        textWinner.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void updateItemsCount() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayFX(0);
        itemCount++;
        textItems.text = "Items: " + itemCount;

        if (itemCount >= totalItems[SceneManager.GetActiveScene().buildIndex] ) {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayFX(1);
            textWinner.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Invoke("nextLevel", 2f);
        }
    }

    public void gameOver() {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayFX(2);
        textLoser.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Invoke("resetLevel", 2f);
    }

    void resetLevel(){
        textLoser.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            itemCount = 0;
        else
            itemCount = totalItems[SceneManager.GetActiveScene().buildIndex-1];
        
        textItems.text = "Items: " + itemCount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void nextLevel(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        textWinner.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex < 2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isGameCompleted = true;
        }
    }
}
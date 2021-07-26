using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 

public class GameController : MonoBehaviour {

  public Camera camera;
  public GameObject[] balls;
  public float timeLeft;
  public Text timerText;
  private Score score;
  public GameObject gameOverText;
  public GameObject restartButton;
  private Rigidbody2D rigidbody2D;
  public GameObject splashScreen;
  public GameObject startButton;
  private float maxWidth;
  private bool playing;
  public HatController hatController;

  // Use this for initialization
  void Start()
  {
    if (camera == null)
    {
      camera = Camera.main;
    }
    score = GameObject.Find("Hat").GetComponent<Score>();
    
    
    playing = false;
    Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
    Vector3 targetWidth = camera.ScreenToWorldPoint(upperCorner);
    float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
    maxWidth = targetWidth.x - ballWidth;
    UpdateText();
  }

  void FixedUpdate(){
    if (playing){
      timeLeft -= Time.deltaTime;
      if (timeLeft < 0){
        timeLeft = 0;
      }
      UpdateText();
    }

  }

  public void startGame(){
    splashScreen.SetActive(false);
    startButton.SetActive(false);
    hatController.ToggleControl(true);
    StartCoroutine(Spawn ());
  }

  private bool insertScore(string score){
      bool result = false;
      try{
        WWWForm form = new WWWForm();
        form.AddField("score",score);
        WWW www = new WWW("http://192.168.0.28:8000/users/add", form);
        result = true;
      }
      catch(ExitGUIException e){
        result = false;
      }

      return result;
      
  }

  IEnumerator Spawn()
  {	  
    yield return new WaitForSeconds (2.0f);
    playing = true;
    while(timeLeft > 0){
        GameObject ball = balls[Random.Range(0, balls.Length)];
        Vector3 spawnPosition = new Vector3(
        Random.Range(-maxWidth, maxWidth),
        transform.position.y,
        0.0f);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(ball, spawnPosition, spawnRotation);
        yield return new WaitForSeconds (Random.Range(1.0f, 2.0f));
    }
    yield return new WaitForSeconds(2.0f);
    //if (insertScore(score.score.ToString())){
       gameOverText.SetActive(true);
       yield return new WaitForSeconds(2.0f);
       restartButton.SetActive(true);
    //}
  }
    
  void UpdateText(){
       timerText.text = "Time left:\n" + Mathf.RoundToInt(timeLeft);
  }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _ScoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _Liveimg;
    [SerializeField] private Text _GameOverText;
    [SerializeField] private Text _RestartLevelText;
    [SerializeField] private Text _CountDown;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreText.text = "Score :" + 0;

        _GameOverText.gameObject.SetActive(false);
        _RestartLevelText.gameObject.SetActive(false);
        _CountDown.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _ScoreText.text = "Score :" + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _Liveimg.sprite = _liveSprites[currentLives];

        if (currentLives <= 0)
        {

            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _RestartLevelText.gameObject.SetActive(true);
        _GameOverText.gameObject.SetActive(true);
        _CountDown.gameObject.SetActive(true);

        StartCoroutine(CountDown());
        
        StartCoroutine(TextFlickker());
        _gameManager.GameOver();
    }

    IEnumerator TextFlickker()
    {
        
        while (true)
        {
            

            _GameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);

            _GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator CountDown()
    {
        _CountDown.text = "5";
        yield return new WaitForSeconds(1f);

        _CountDown.text = "4";
        yield return new WaitForSeconds(1f);

        _CountDown.text = "3";
        yield return new WaitForSeconds(1f);

        _CountDown.text = "2";
        yield return new WaitForSeconds(1f);

        _CountDown.text = "1";
        yield return new WaitForSeconds(1f);

        _CountDown.text = "0";
        yield return new WaitForSeconds(1f);

        Application.Quit();
    }

    

   


}

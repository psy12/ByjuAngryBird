using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private int _remainingChances;
    private int _score;

    public GameObject birdRef;
    public Rigidbody2D hookRef;

    public GameObject _currentBird;

    public Text remainingChanesText;
    public Text scoreText;

    private void Start()
    {
        _remainingChances = 5;
        _score = 0;

        UpdateRemainingText();
    }

    public void CreateBird()
    {
        if (_remainingChances > 0 && (_currentBird == null || _currentBird.GetComponent<Bird>().IsThrown))
        {
            GameObject newBird = Instantiate(birdRef, hookRef.transform.position, Quaternion.identity);
            newBird.GetComponent<SpringJoint2D>().connectedBody = hookRef;
            _currentBird = newBird;
        }
    }

    public void DecreaseBirdCount()
    {
        if (_remainingChances > 0)
        {
            _remainingChances -= 1;
        }

        UpdateRemainingText();
    }

    public void UpdateScore()
    {
        _score += 1;

        UpdateScoreText();
    }

    private void UpdateRemainingText()
    {
        remainingChanesText.text = _remainingChances.ToString() + " Chances remaining!";
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + _score.ToString();
    }

    public void ReloadGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

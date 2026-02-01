using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextLevelName;
    Transform _player;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Enter()
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        float t = 0f;
        Vector3 startPos = _player.position;
        Vector3 startScale = _player.localScale;

        while (t < 1f)
        {
            t += Time.deltaTime;
            _player.position = Vector3.Lerp(startPos, transform.position, Easing.EaseInOut(t));
            _player.localScale = Vector3.Lerp(startScale, Vector3.zero, Easing.EaseOut(t));
            yield return null;
        }

        SceneManager.LoadScene(nextLevelName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;
    [SerializeField] private LayerMask _playerLayer;

    private void Start()
    {
        StartCoroutine(E_CheckOldSceneBelongsToHere());
    }

    IEnumerator E_CheckOldSceneBelongsToHere()
    {
        yield return new WaitUntil(() => SceneInformation.IsSceneLoaded);
        if (SceneInformation.OldSceneName == _sceneNameToLoad)
        {
            Player.Instance.transform.position = transform.position;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null &&
            (1 << collision.gameObject.layer & _playerLayer) > 0 &&
            Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene(_sceneNameToLoad);
        }
    }
}

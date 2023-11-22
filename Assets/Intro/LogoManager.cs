using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{

    [SerializeField] float delay;
    [SerializeField] int menuScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(menuScene);
    }


}

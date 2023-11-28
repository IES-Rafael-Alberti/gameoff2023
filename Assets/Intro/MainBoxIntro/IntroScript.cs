using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject kiwi;
    public Animator kiwiAnimations;

    public Camera cutsceneCamera;

    public BoxScript puzzleBox;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WalkIn());
    }

    IEnumerator WalkIn()
    {
        Vector3 starting_position = kiwi.transform.position;
        Quaternion final_direction = cutsceneCamera.transform.rotation;
        kiwi.transform.position = kiwi.transform.position - Vector3.forward * 2000f;
        cutsceneCamera.transform.LookAt(kiwi.transform);
        kiwiAnimations.SetBool("isRunning", true);

        float distance = Vector3.Distance(kiwi.transform.position, starting_position);
        while (distance > 5f)
        {
            cutsceneCamera.transform.rotation = Quaternion.Slerp(cutsceneCamera.transform.rotation, final_direction, 1000/distance * Time.deltaTime);
            kiwi.transform.position = Vector3.MoveTowards(kiwi.transform.position, starting_position, 1000f*Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(kiwi.transform.position, starting_position);
        }

        puzzleBox.RotateAndOpen();
    }
}

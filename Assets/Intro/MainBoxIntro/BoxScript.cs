using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoxScript : MonoBehaviour
{
    public GameObject mainBox;
    private Animator animator;

    public Animator kiwiAnimations;

    public GameObject greekCover;
    public GameObject egyptCover;
    public GameObject aztecCover;
    private string animName;
    private string revAnimName;
    private float neededRotation;

    public Level currentLevel = Level.Egyptian;

    public SBShuffleBoardScript levelController;

    public Camera cutsceneCamera;

    private Vector3 resetCameraPosition;
    private Quaternion resetCameraRotation;

    public GameObject victoryReference;

    public GameObject credits;

    private void Start()
    {
        animator = GetComponent<Animator>();
        LockScript.OnLevelComplete += NextLevel;
        resetCameraPosition = cutsceneCamera.transform.position;
        resetCameraRotation = cutsceneCamera.transform.rotation;
    }

    public void RotateAndOpen()
    {
        switch (currentLevel)
        {
            case Level.Egyptian:
                animName = "EgyptOpen";
                revAnimName = "EgyptClose";
                neededRotation = 0;
                break;
            case Level.Greek:
                animName = "GreekOpen";
                revAnimName = "GreekClose";
                neededRotation = -120;
                break;
            case Level.Aztec:
                animName = "AztecOpen";
                revAnimName = "AztecClose";
                neededRotation = -240;
                break;
        }
        StartCoroutine(RAndOHelper());
    }

    public void NextLevel()
    {
        switch (currentLevel)
        {
            case Level.Egyptian:
                currentLevel = Level.Greek;
                break;
            case Level.Greek:
                currentLevel = Level.Aztec;
                break;
            case Level.Aztec:
                currentLevel = Level.Egyptian;
                StartCoroutine(CloseBoard(true));
                return;
        }
        StartCoroutine(CloseBoard());
    }

    public void LoadLevel()
    {
        levelController.LoadLevel(currentLevel);
        cutsceneCamera.gameObject.GetComponent<AudioListener>().enabled = false;
    }

    public void DeleteBoard()
    {
        levelController.DestroyBoard();
        cutsceneCamera.gameObject.GetComponent<AudioListener>().enabled = true;
    }

    public void OpenBox()
    {

    }



    IEnumerator RAndOHelper()
    {
        kiwiAnimations.SetBool("isRunning", false);
        while (Quaternion.Angle(mainBox.transform.localRotation, Quaternion.Euler(new Vector3(0, 0, neededRotation))) > 0.1f)
        {
            mainBox.transform.localRotation = Quaternion.Lerp(mainBox.transform.localRotation, Quaternion.Euler(new Vector3(0, 0, neededRotation)), 5f*Time.deltaTime);
            yield return null;
        }

        kiwiAnimations.SetBool("isHolding", true);

        yield return new WaitForSeconds(1f);

        animator.Play(animName, 0, 0f);
        animator.SetBool("Loop", false);

        yield return new WaitForSeconds(1.5f);

        GameObject targetCamera = levelController.characterCamera.gameObject;

        float distance = Vector3.Distance(cutsceneCamera.transform.position, targetCamera.transform.position);
        while (distance > 1f)
        {
            cutsceneCamera.transform.position = Vector3.MoveTowards(cutsceneCamera.transform.position, targetCamera.transform.position, 400f * Time.deltaTime);
            cutsceneCamera.transform.rotation = Quaternion.Slerp(cutsceneCamera.transform.rotation, targetCamera.transform.rotation, 500 / distance * Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(cutsceneCamera.transform.position, targetCamera.transform.position);
        }

        cutsceneCamera.enabled = false;
    }

    IEnumerator CloseBoard(bool endGame = false)
    {
        cutsceneCamera.transform.position = levelController.characterCamera.gameObject.transform.position;
        cutsceneCamera.transform.rotation = levelController.characterCamera.gameObject.transform.rotation;
        cutsceneCamera.enabled = true;
        yield return null;

        float distance = Vector3.Distance(cutsceneCamera.transform.position, resetCameraPosition);
        while (distance > 1f)
        {
            cutsceneCamera.transform.position = Vector3.MoveTowards(cutsceneCamera.transform.position, resetCameraPosition, 400f * Time.deltaTime);
            cutsceneCamera.transform.rotation = Quaternion.Slerp(cutsceneCamera.transform.rotation, resetCameraRotation, 500 / distance * Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(cutsceneCamera.transform.position, resetCameraPosition);
        }
        animator.Play(revAnimName, 0, 0f);
        animator.SetBool("Loop", false);

        kiwiAnimations.SetBool("isFloating", false);
        kiwiAnimations.SetBool("isJumping", false);
        kiwiAnimations.SetBool("isRunning", false);
        kiwiAnimations.SetBool("isHolding", false);

        yield return new WaitForSeconds(1.5f);

        if (!endGame)
        {
            RotateAndOpen();
            yield break;
        }
        animator.Play("OpenBox", 0, 0f);
        animator.SetBool("Loop", false);

        Vector3 ccCameraPos = cutsceneCamera.transform.position;
        Vector3 goalPosition = new Vector3(ccCameraPos.x, victoryReference.transform.position.y + 200f, ccCameraPos.z);
        distance = Vector3.Distance(goalPosition, cutsceneCamera.transform.position);
        while(distance > 1f)
        {
            cutsceneCamera.transform.position = Vector3.MoveTowards(cutsceneCamera.transform.position, goalPosition, 400f * Time.deltaTime);

            Vector3 direction = victoryReference.transform.position - cutsceneCamera.transform.position;

            // Calculate the rotation to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Use Quaternion.RotateTowards to smoothly rotate towards the target
            cutsceneCamera.transform.rotation = Quaternion.RotateTowards(cutsceneCamera.transform.rotation, targetRotation, 25f * Time.deltaTime);

            yield return null;
            distance = Vector3.Distance(goalPosition, cutsceneCamera.transform.position);
        }

        credits.SetActive(true);
        cutsceneCamera.transform.parent = victoryReference.transform;
        while (true)
        {
            victoryReference.transform.Rotate(-Vector3.forward * 5f * Time.deltaTime);
            yield return null;
        }
    }
}

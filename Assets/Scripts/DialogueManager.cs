using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    public Image actorImage;

    public Canvas introCanvas;

    public Animator animator;

    private Message[] currentMessages;
    private Actor[] currentActors;
    private int activeMessage = 0;

    private float timer = 0;
    private bool enableScript = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        enableScript = true;

        DisplayMessage();
    }

    private void DisplayMessage()
    {
        animator.SetBool("IsOpen", true);
        Message messageToDisplay = currentMessages[activeMessage];

        StopAllCoroutines();
        StartCoroutine(TypeSentence(messageToDisplay.message));

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    IEnumerator TypeSentence(string sentence)
    {
        messageText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            messageText.text += letter;
            yield return null;
        }
    }

    private void NextMessage()
    {
        activeMessage++;
        if (enableScript)
        {
            if (activeMessage < currentMessages.Length)
            {
                animator.SetBool("IsOpen", true);
                DisplayMessage();
            }
            else
            {
                animator.SetBool("IsOpen", false);
                enableScript = false;
                FindFirstObjectByType<CameraController>().followPlayerFunction();
                FindFirstObjectByType<PlayerMovement>().enableMovement();
                introCanvas.GetComponent<Canvas>().enabled = false;
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        

        if (enableScript)
        { 
            foreach (Touch touch in Input.touches)
            {
                if (timer >= 0.2f)
                {
                    timer = 0;
                    NextMessage();
                }
            }
            if (Input.GetMouseButtonDown(0) && timer >= 0.2f)
            {
                timer = 0;
                NextMessage();
            }
            if (Input.anyKey && timer >= 0.2f)
            {
                timer = 0;
                NextMessage();
            }
        }
    }

    IEnumerator nextSceneRoutine()
    {
        yield return new WaitForSeconds(2);
        FindFirstObjectByType<FinishScript>().CompleteLevel();
    }

}

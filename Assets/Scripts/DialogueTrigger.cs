using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void startDialogue()
    {

        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }

}


[System.Serializable]
public class Message
{
    public int actorId;

    [TextArea(3, 10)]
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}


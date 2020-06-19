using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Queue<Sentences> sentences;

    public static DialogueManager instance;

    public Animator animator;

    public GameObject workingSupervisor;
    public GameObject dialogueBubble;

    public float typingSpeed = 0.5f;

    // Start is called before the first frame update

    void Awake(){

        if (instance == null)
        {
            instance = this.gameObject.GetComponent<DialogueManager>();
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(this.gameObject);

    }



    void Start()
    {
        sentences = new Queue<Sentences>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.promt[0].name;
        dialogueBubble.GetComponent<RawImage>().color = dialogue.promt[0].color;

        sentences.Clear();

        foreach (var sentence in dialogue.promt)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            Debug.Log("No More Sentences");
            Supervisor getAI = workingSupervisor.GetComponent<Supervisor>();
            getAI.currentBehavior = Supervisor.Behavior.GoOutside;
            EndDialogue();          
            return;
        }

        //Debug.Log("Next Sentence");
        Sentences sentence = sentences.Dequeue();
        nameText.text = sentence.name;
        dialogueBubble.GetComponent<RawImage>().color = sentence.color;
        dialogueText.text = sentence.sentence;
        //StartCoroutine(Type(sentence));
    }

    //IEnumerator Type (Sentences sentence)
    //{
     //   dialogueText.text = "";
        //foreach (char letter in sentence.sentence.ToCharArray())
        //{

        //    dialogueText.text += letter;
        //    yield return new WaitForSeconds(typingSpeed);
        //}
    //}


    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        //Debug.Log("End of conversation");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialougue UI")]
    [SerializeField] private GameObject dialougePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying {get; private set;}

    private static DialogueManager instance;

    private void Awake(){
        if(instance != null){
            Debug.LogWarning("Found more than one DM in scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance(){
        return instance;

    }

    private void Start(){

        dialogueIsPlaying = false;
        dialougePanel.SetActive(false);


        // get all of the choice text

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update(){
        //returns if dialogue isnt playing
        if(!dialogueIsPlaying){
            return;
        }

        //handles continuing the next line in the line when submit is pressed
         if(currentStory.currentChoices.Count == 0 
         && Input.GetKeyDown(KeyCode.Q))
         {
            ContineuStory();
         }
    }

    public void EnterDialogueMode(TextAsset inkJSON){
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialougePanel.SetActive(true);

        ContineuStory();
    }

       

    private IEnumerator ExitDialoguemode(){
            yield return  new WaitForSeconds(0.1f);

            dialogueIsPlaying = false;
            dialougePanel.SetActive(false);
            dialogueText.text = "";
        }

    

    private void ContineuStory(){


         if(currentStory.canContinue){

            //set text for current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices if any for this line
            DisplayChoices();

        } else {
        
        StartCoroutine(ExitDialoguemode());

        }
    }

    private void DisplayChoices(){

        List<Choice> currentChoices = currentStory.currentChoices;

        // def check to make sure ui can take all the choices given
        if(currentChoices.Count > choices.Length){

            Debug.LogError("More choices were given than the ui can support number of choices given;c"
             + currentChoices.Count);

        }

        int index = 0;
        //enable and initialize the choices up to te amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices){
        choices[index].gameObject.SetActive(true);
        choicesText[index].text = choice.text;
        index++;

        }

        //go through the remaining choices the ui supports and make then hidden 
        for(int i = index;i < choices.Length; i++){
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

    }

    private IEnumerator SelectFirstChoice(){

        //event system we clear then wait a frame before we set selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex){

        currentStory.ChooseChoiceIndex(choiceIndex);
        ContineuStory();
    }
}








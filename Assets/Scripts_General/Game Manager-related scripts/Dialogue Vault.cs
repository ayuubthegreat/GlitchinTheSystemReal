using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueVault : MonoBehaviour
{
    public static DialogueVault instance;
    public DialogueSet[][] dialogueSets;
    public DialogueSet[][] dialogueSetsYes;
    public DialogueSet[][] dialogueSetsNo;
    public DialogueSet[][] dialogueForBattler;
    public string enemyName;
    public bool isFallenMuslim;
    public bool isGirlorBoy; // True for girl, false for boy


    [Serializable]
    public struct DialogueSet
    {
        public string dialogueLine;
        public string characterName;
        public Action dialogueAction; // Optional action to perform after the dialogue line
    }

    void Awake()
    {
        instance = this;
        dialogueForBattler = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "An enemy appears!", characterName = "" },
                new DialogueSet { dialogueLine = "They're a fallen Muslim! You might want to save them!", characterName = "" },
                new DialogueSet { dialogueLine = "What shall you do?", characterName = "Narrator" },
                new DialogueSet { dialogueLine = "", characterName = " " }, // This will be set dynamically during the battle
                new DialogueSet {dialogueLine = "You faint........<Oh, well. At least you tried your best....."},
                new DialogueSet{dialogueLine = "You win the battle!", characterName= ""},
            },
        };
        dialogueSets = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "I managed to wake myself up so early. <Now I can begin my day. ", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I miss when there used to be other people besides me in this house...... <I wonder what they're all doing now.", characterName = "Abdurahman" },
            },
            
            // Yasir's Call
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Hello there, Yasir.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Hello, Abdurahman. Are you good?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I'm good, yes. Listen....I've been thinking....", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Thinking about what?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I've been thinking....those VR headsets that everyone's been talking about....<they weren't just built for amusement and entertainment.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "What else could they have been built for?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well....I think the P-Tech corporation had a more sinister reason for manufacturing them.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Are you just saying that because your other siblings seem to have prioritized them over their Islamic duties?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well....partially....but haven't you noticed your family acting strange?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Not really; I'm usually away from my house. I only go there occasionally now. But now that you mention it....", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You do notice?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "....My mother did buy the rest of my siblings the same VR headsets a week ago....<and I just received a call from her saying that they had gone missing. I'm on their trail now.", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You see now? This cannot be just a coincidence. My house has been deserted ever since my siblings received VR sets from the P-tech corporation, <and now yours has left you as well! There's a deeper agenda here, <and I think it's those VR goggles that are the main pawn in it.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "...That does make sense....but what are we supposed to do about it, other than ensure we never get those goggles?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I just called a police officer yesterday, <who explained to me that what P-Tech is doing to our society is far beyond illegal, <even more so than they usually are. I mean, you've seen the news. <Society has basically halted. No stores are running, no banks are printing checks....<it's like those VR headsets pulled everyone in to them <and left only husks walking around the streets.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Now that's what I would call either genius....or a conspiracy.", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Either way, Yasir....I think I'm on to something here. Since what P-Tech is doing is illegal, we have the right to stand up against them and get society functional again. Yasir, I was thinking of starting a revolution against the P-Tech company.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "That's....crazy. Just crazy....but if you're going to revolt against them, you're going to need some people to back you up. <(He pauses for a few seconds.) <I'm willing to join you in this cause. We'll reach our goal!", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well.....", characterName = "Abdurahman", dialogueAction = () => DialogueManager.instance.DisplayChoices("Will you invite Yasir?", "Yes", "No")},
            },
            // If Yasir is invited
            
            // If Yasir is not invited
            
            // Frantic Teenager encounter
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Oh, my god! \n OH, MY GOD!", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What's wrong, sir?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I just got back from the P-Tech internship lobby.....<they were doing some weird stuff, man.....really weird stuff.....", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What were they doing?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Well, first I saw them experimenting with a chicken.......", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What are you going to do to that chicken, Mr..?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Charles. Mr. Charles, \n and how lucky you are, young man, <to witness our biochemical department in action.", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "W-why would--?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "For meat, intern, for meat. \nDo you know how much meat we can get out of this one chicken?! \n <It's a real money-saver!", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "B-\nbut what about the chicken?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Do you think the chicken cares, intern? \n You're awfully soft, aren't you? \n <You're going to have to get a lot tougher if you want a job here, young man.", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "That poor chicken.....he can't even walk now......", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Wow......", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "What?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Oh, nothing......<so they mutated it?", characterName = "Abdurahman"},
                new DialogueSet { dialogueLine = "(He nods shakily.) ", characterName = " " },
                new DialogueSet { dialogueLine = "They must be stopped, man...<I can't even imagine what else they could be getting away with <if they got away with mutating farm animals……..", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Indeed, indeed. <Well, you know…..<I am going to start a revolt against them.", characterName = "Abdurahman"},
                new DialogueSet { dialogueLine = "A revolt? Against P-Tech? \n<WICKED! I want to help!", characterName = "Frantic Teenager"},
                new DialogueSet { dialogueLine = "Well……<Any information would be great right now. ", characterName = "Abdurahman" },
                new DialogueSet{dialogueLine = "(He looks around him wildly, then whispers:)", characterName = string.Empty},
                new DialogueSet{dialogueLine = "You know those P-Tech Phone Booths?", characterName = "Frantic Teenager", dialogueAction =() => {
                    Debug.Log("Phone Booths dialogue action triggered.");
                    GameManagerRPG.instance.MoveCamera(new Vector3(10, 10, 10), 10f);
                }},
                new DialogueSet{dialogueLine = "Yes, I do. <They are the ones that people use to call P-Tech's customer service, right?", characterName = "Abdurahman"},
                new DialogueSet{dialogueLine = "(The Frantic Teenager nods.)", characterName = string.Empty},
                new DialogueSet{dialogueLine = "Well, I heard from someone who was planning <to get an internship there that <they're hiding something in those booths.....<but she left before I could get anything else out of her.", characterName = "Frantic Teenager"},
                new DialogueSet{dialogueLine = "Really?", characterName = "Abdurahman"},
                new DialogueSet{dialogueLine = "(The Frantic Teenager nods.)", characterName = string.Empty},
                new DialogueSet{dialogueLine = "That's all I know.", characterName = "Frantic Teenager"},
                new DialogueSet{dialogueLine = "Thanks, man!", characterName = "Abdurahman"},
                new DialogueSet{dialogueLine = "No problem. <Take 'em down, will you?", characterName = "Frantic Teenager"},
                new DialogueSet{dialogueLine = "You got it. Bye!", characterName = "Abdurahman"},
                new DialogueSet{dialogueLine = "Bye!", characterName = "Frantic Teenager"},
            },
            // Homeless Man Dialogue
            
        };
        dialogueSetsYes = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "let's go, then! <You're on my team!", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Yeah!", characterName = "Yasir"},
                new DialogueSet { dialogueLine = DeclarePartyMember("Yasir"), characterName = " ", },
                new DialogueSet{dialogueLine = "Come over to my house, and we'll go over what we need to do next.", characterName = "Abdurahman" },
                new DialogueSet{dialogueLine = "All right! See you there. Bye.", characterName = "Yasir" },
                new DialogueSet{dialogueLine = "Goodbye, Yasir.", characterName = "Abdurahman"},
                new DialogueSet { dialogueLine = "And so the journey begins......<With Yasir on board for the rebellion, Abdurahman truly feels confident in his abilities<to lead the charge against P-Tech.<Inshallah, he will succeed in his goal.", characterName = "Narrator" },
            },
        };
        dialogueSetsNo = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "I don't know, Yasir....<I don't want you to get hurt alongside me.<It's best if you lay low.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "But what about you? Aren't you putting yourself at risk by fighting the largest corporation on Earth?", characterName = "Yasir"},
                new DialogueSet { dialogueLine = "I am, yes. But I have to do this. <You have more pressing matters to worry about--your family.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Alright......<then what can I do to help you out?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You can help train me, get me prepared for the fight ahead.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "All right, then. <That sounds good. Just call if you need some training, okay?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "All right.", characterName = "Abdurahman" },
            },
        };

        
    }
    public string DeclarePartyMember(string partyMemberName)
    {
        Debug.Log(partyMemberName + " has joined the revolution!");
        return partyMemberName + " has joined the revolution!";
    }
    public void MoveCameraToFirstPhoneBooth() => GameManagerRPG.instance.MoveCamera(new Vector3(10, 10, 10), 10f);
    public void AttackinBattle(string moveName = "Slash", int damageAmount = 10, bool isPhysical = true, int stageChange = 0) => StartCoroutine(attackinBattle(moveName, damageAmount, isPhysical, stageChange));
    public IEnumerator attackinBattle(string moveName = "Slash", int damageAmount = 10, bool isPhysical = true, int stageChange = 0)
    {
        yield return new WaitForSeconds(1f);
        UIManagerRPG.instance.battleShortMenu.SetActive(false);
        Debug.Log("Attacking enemy with " + moveName + " for " + damageAmount + " damage!");
        dialogueForBattler[0][3].dialogueLine = GameManagerRPG.instance.isPlayerTurn ? "You use " + moveName + "!" : enemyName + " uses " + moveName + "!";
        DialogueManager.instance.StartDialogueTexts(dialogueForBattler[0], 3, -1);
        // Implement attack logic here
        yield return new WaitForSeconds(1f);

        GameManagerRPG.instance.SpriteFlicker(GameManagerRPG.instance.isPlayerTurn ? GameManagerRPG.instance.enemiesInBattle[0].GetComponent<Image>() : GameManagerRPG.instance.battleAlliesPrefab[0].GetComponent<Image>(), 10);
        if (GameManagerRPG.instance.isPlayerTurn)
        {
            GameManagerRPG.instance.enemiesInBattle[0].GetComponent<battleStats>().health -= damageAmount;
            Debug.Log("Enemy health is now: " + GameManagerRPG.instance.enemiesInBattle[0].GetComponent<battleStats>().health);
            if (GameManagerRPG.instance.enemiesInBattle[0].GetComponent<battleStats>().health <= 0)
            {
                Debug.Log("Enemy defeated!");
                // Handle enemy defeat (e.g., remove from battle, give rewards, etc.)
                GameManagerRPG.instance.enemiesInBattle[GameManagerRPG.instance.currentEnemyIndex].SetActive(false);
                GameManagerRPG.instance.currentEnemyIndex++;
                if (GameManagerRPG.instance.currentEnemyIndex < GameManagerRPG.instance.enemiesInBattle.Length - 1)
                {
                    GameManagerRPG.instance.enemiesInBattle[GameManagerRPG.instance.currentEnemyIndex].SetActive(true);
                }
                else
                {
                   Debug.Log("All enemies defeated! You win the battle!");
                   dialogueForBattler[0][5].dialogueLine += "< You recieve " + UnityEngine.Random.Range(20, 100) + " coins as a reward!";
                   DialogueManager.instance.StartDialogueTexts(dialogueForBattler[0], 5, -1, 0, null, true, 1);
                    // Handle battle victory (e.g., exit battle mode, give rewards, etc.)
                    UIManagerRPG.instance.battleShortMenu.SetActive(false);
                    yield break; // Exit the coroutine early since the battle is over 
                }
                
                
                
                    
                
            }
        }
        else
        {
            GameManagerRPG.instance.battleAlliesPrefab[0].GetComponent<battleStats>().health -= damageAmount;
            Debug.Log("Player health is now: " + GameManagerRPG.instance.battleAlliesPrefab[0].GetComponent<battleStats>().health);
            if (GameManagerRPG.instance.battleAlliesPrefab[0].GetComponent<battleStats>().health <= 0)
            {
                Debug.Log("Player defeated! Game Over!");
                // Handle player defeat (e.g., game over sequence, reload last save, etc.)
                UIManagerRPG.instance.battleShortMenu.SetActive(false);
                yield break; // Exit the coroutine early since the game is over
            }
        }
        yield return new WaitForSeconds(1f);
        GameManagerRPG.instance.isPlayerTurn = !GameManagerRPG.instance.isPlayerTurn;
        if (GameManagerRPG.instance.isPlayerTurn)
        {
            UIManagerRPG.instance.battleShortMenu.SetActive(true);
            yield break;
        }
        else
        {
            GameManagerRPG.instance.CommenceBattle(UnityEngine.Random.Range(0, 4));
        }
        
    }

}

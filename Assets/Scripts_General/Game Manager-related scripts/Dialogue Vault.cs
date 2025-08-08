using System;
using UnityEngine;

public class DialogueVault : MonoBehaviour
{
    public static DialogueVault instance;
    public DialogueSet[][] dialogueSets;

    [Serializable]
    public struct DialogueSet
    {
        public string dialogueLine;
        public string characterName;
        public Action dialogueAction; // Optional action to perform after the dialogue line
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Opening dialogue
        dialogueSets = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Alhamdulillah, I managed to wake myself up so early. <Now I can begin my day. ", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I miss when there used to be other people besides me in this house...... <I wonder what they're all doing now.", characterName = "Abdurahman" },
            },
            
            // Yasir's Call
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Assalamu Alaykum, Yasir.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Wa alaykumas Salaam, Abdurahman. Are you good?", characterName = "Yasir" },
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
                new DialogueSet { dialogueLine = "You see now? This cannot be just a coincidence. My house has been deserted ever since my siblings received VR sets from the P-tech corporation, and now yours has left you as well! There's a deeper agenda here, and I think it's those VR goggles that are the main pawn in it.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "...That does make sense....but what are we supposed to do about it, other than ensure we never get those goggles?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I just called a police officer yesterday, who explained to me that what P-Tech is doing to our society is far beyond illegal, even more so than they usually are. I mean, you've seen the news. Society has basically halted. No stores are running, no banks are printing checks....it's like those VR headsets pulled everyone in to them and left only husks walking around the streets.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Now that's what I would call either genius....or a conspiracy.", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Either way, Yasir....I think I'm on to something here. Since what P-Tech is doing is illegal, we have the right to stand up against them and get society functional again. Yasir, I was thinking of starting a revolution against the P-Tech company.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "That's....crazy. Just crazy....but if you're going to revolt against them, you're going to need some people to back you up. (He pauses for a few seconds.) I'm willing to join you in this cause. Inshallah, we'll reach our goal!", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well.....", characterName = "Abdurahman" },
            },
            
            // Frantic Teenager encounter
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Oh, my god! \n OH, MY GOD!", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What's wrong, sir?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I just got back from the P-Tech internship lobby.....they were doing some weird stuff, man.....really weird stuff.....", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What were they doing?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Well, first I saw them experimenting with a chicken.......", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "What are you going to do to that chicken, Mr..?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Charles. Mr. Charles, \n and how lucky you are, young man, to witness our biochemical department in action.", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "W-why would--?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "For meat, intern, for meat. \n Do you know how much meat we can get out of this one chicken?! \n <It's a real money-saver!", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "B-\nbut what about the chicken?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Do you think the chicken cares, intern? \n You're awfully soft, aren't you? \n < You're going to have to get a lot tougher if you want a job here, young man.", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "That poor chicken.....he can't even walk now......", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Subhanallah......", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "What?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Oh, nothing......<so they mutated it?", characterName = "Abdurahman"},
                new DialogueSet { dialogueLine = "(He nods shakily.)", characterName = " " },
                new DialogueSet { dialogueLine = "They must be stopped, man...<I can't even imagine what else they could be getting away with if they got away with mutating farm animals……..", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Indeed, indeed. Well, you know…..I am going to start a revolt against them.", characterName = "Abdurahman"},
                new DialogueSet { dialogueLine = "A revolt? Against P-Tech? \n<wICKED! I want to help!", characterName = "Frantic Teenager"},
                new DialogueSet { dialogueLine = "Well……<Any information would be great right now. ", characterName = "Abdurahman" },
            }
        };
    }
}

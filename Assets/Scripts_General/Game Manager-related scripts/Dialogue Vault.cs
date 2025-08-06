using System;
using UnityEngine;

public class DialogueVault : MonoBehaviour
{
    public DialogueSet[][] dialogueSets;
    
    [Serializable]
    public struct DialogueSet
    {
        public string dialogueLine;
        public string characterName;
    }

    void Awake()
    {
        // Opening dialogue
        dialogueSets = new DialogueSet[][]
        {
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Alhamdulillah, I managed to wake myself up so early. Now I can begin my day.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I miss when there used to be other people besides me in this house......", characterName = "Abdurahman" },
            },
            
            // Yasir's Call
            new DialogueSet[]
            {
                new DialogueSet { dialogueLine = "Assalamu Alaykum, Yasir.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Wa alaykumas Salaam, Abdurahman. Are you good?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I'm good, yes. Listen....I've been thinking....", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Thinking about what?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I've been thinking....those VR headsets that everyone's been talking about....they weren't just built for amusement and entertainment.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "What else could they have been built for?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well....I think the P-Tech corporation had a more sinister reason for manufacturing them.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Are you just saying that because your other siblings seem to have prioritized them over their Islamic duties?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well....partially....but haven't you noticed your family acting strange?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Not really; I'm usually away from my house. I only go there occasionally now. But now that you mention it....", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You do notice?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "....My mother did buy the rest of my siblings the same VR headsets a week ago....and I just received a call from her saying that they had gone missing. I'm on their trail now.", characterName = "Yasir" },
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
                new DialogueSet { dialogueLine = "Charles. ", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "Did they have VR headsets?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Yeah! Yeah, they did! How did you know that?", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "Because the same thing happened to my family.", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Really? So you know what happened to them?", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "I have a theory. Listen, do you want to help me get them back?", characterName = "Frantic Teenager" },
                new DialogueSet { dialogueLine = "Yes! Anything! Please!", characterName = "Mr. Charles" },
                new DialogueSet { dialogueLine = "Good. Meet me at this address tomorrow morning.", characterName = "Mr. Charles" },
            }
        };
    }
}

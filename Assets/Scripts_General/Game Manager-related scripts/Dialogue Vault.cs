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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dialogueSets = new DialogueSet[][]
        {
            new DialogueSet[] {
                new DialogueSet { dialogueLine = "Alhamdulillah, I managed to go to Fajr and pray in the Jama'ah. Now I can start my day.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "I miss when there used to be other people besides me in this house.......", characterName = "Abdurahman" },
            },
            // When Abdurahman calls Yasir on his landline phone.
            new DialogueSet[] {
                new DialogueSet { dialogueLine = "Assalamu Alaykum, Yasir.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Wa alaykumas Salaam, Abdurahman. Are you good?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I’m good, yes. Listen…….I’ve been thinking…….", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Thinking about what?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I’ve been thinking….those VR headsets that everyone’s been talking about…..they weren’t just built for amusement and entertainment.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "What else could they have been built for?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well…...I think the P-Tech corporation had a more sinister reason for manufacturing them.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Are you just saying that because your other siblings 	seem to have prioritized them over their Islamic duties?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "Well…..partially…….but haven’t you noticed your family acting strange?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "Not really; I’m usually away from my house. I only go there occasionally now. But now that you mention it………", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You do notice?", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "….My mother did buy the rest of my siblings the same VR headsets a week ago…..and I just received a call from her saying that they had gone missing. I’m on their trail now.", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "You see now? This cannot be just a coincidence. My house has been deserted ever since my siblings received VR sets from the P-tech corporation, and now yours has left you as well! There’s a deeper agenda here, and I think it’s those VR goggles that are the main pawn in it.", characterName = "Abdurahman" },
                new DialogueSet { dialogueLine = "...That does make sense…….but what are we supposed to do about it, other than ensure we never get those goggles?", characterName = "Yasir" },
                new DialogueSet { dialogueLine = "I just called a police officer yesterday, who explained to me that what P-Tech is doing to our society is far beyond illegal, even more so than they usually are. I mean, you’ve seen the news. Society has basically halted. No stores are running, no banks are printing checks…...it’s like those VR headsets pulled everyone in to them and left only husks walking around the streets.", characterName = "Abdurahman" },
                },
            // Homeless man dialogue
            new DialogueSet[] {
                new DialogueSet{dialogueLine = "Please....help me out, brother. I have nothing to eat.....I've been starving for" + UnityEngine.Random.Range(3, 6).ToString() + "days! <page> Won't you please give me some money?", characterName = "Needy Man"},
                new DialogueSet{dialogueLine = "Well....", characterName = "Abdurahman" },
            }
        };
        

    }

    // Update is called once per frame
    void Update()
    {

    }
}

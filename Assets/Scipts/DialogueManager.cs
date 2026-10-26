
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public TMP_Text speakerName;
    public TMP_Text dialogueText;
    public TMP_Text continuePrompt;
    public Image backgroundImage;

    [Header("Screens")]
    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject dialoguePanel;

    [Header("Buttons")]
    public Button startButton;
    public Button playAgainButton;
    public Button closeGameButton;

    [Header("Backgrounds")]
    public Sprite[] backgrounds;

    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;

        [TextArea(2, 4)]
        public string text;

        public int backgroundIndex;
    }

    private DialogueLine[] dialogueLines;
    private int currentLine = 0;
    private bool storyFinished = false;

    void Start()
    {
        BuildStory();

        startButton.onClick.AddListener(StartStory);
        playAgainButton.onClick.AddListener(RestartStory);
        closeGameButton.onClick.AddListener(CloseGame);

        startScreen.SetActive(true);
        endScreen.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (storyFinished)
            return;

        if (startScreen.activeSelf || endScreen.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
        }
    }

    void BuildStory()
    {
        dialogueLines = new DialogueLine[]
        {
            MakeLine("YOU",
                "It's my first day at college. I hope I'll make some good friends here.", 0),

            MakeLine("YOU",
                "I wonder who I'll meet today.", 0),

            MakeLine("YOU",
                "The classroom is bigger than I expected. I should find a seat.", 1),

            MakeLine("MAYA",
                "Hey! Is this seat taken?", 1),

            MakeLine("YOU",
                "Oh, no! You can sit here.", 1),

            MakeLine("MAYA",
                "Thanks! I'm Maya, by the way. Is this your first day too?", 1),

            MakeLine("YOU",
                "Yeah. Honestly, I'm a little nervous.", 1),

            MakeLine("MAYA",
                "Me too! Maybe we can figure things out together.", 1),

            MakeLine("YOU",
                "At lunch, Maya waves at me from a table near the window.", 2),

            MakeLine("MAYA",
                "Come sit with me! We can get to know each other better.", 2),

            MakeLine("YOU",
                "Maybe making friends here won't be as difficult as I thought.", 2),

            MakeLine("YOU",
                "I always believed that if I treated everyone nicely, they would treat me the same way.", 2),

            MakeLine("YOU",
                "Over the next few weeks, I become part of Maya's group.", 1),

            MakeLine("FRIEND",
                "Hey, can you share your notes with me? I didn't pay attention in class.", 1),

            MakeLine("YOU",
                "Sure! I'll send them to you tonight.", 1),

            MakeLine("YOU",
                "I stay up late organizing my notes, even though I have an assignment of my own.", 1),

            MakeLine("FRIEND",
                "Can you finish the slides for our group presentation too? You're better at this stuff.", 1),

            MakeLine("YOU",
                "I have a lot to do, but... okay. I'll manage.", 1),

            MakeLine("YOU",
                "I keep saying yes because I don't want them to think I'm selfish.", 1),

            MakeLine("YOU",
                "One afternoon, I walk over to their table at lunch.", 2),

            MakeLine("YOU",
                "Hey! Is anyone sitting here?", 2),

            MakeLine("FRIEND",
                "Actually, we're saving these seats. You can sit somewhere else.", 2),

            MakeLine("YOU",
                "Oh... okay.", 2),

            MakeLine("YOU",
                "Later, I see their photos together online. Nobody even told me they were going out.", 2),

            MakeLine("YOU",
                "I help them whenever they need me. Why do I feel so alone around them?", 2),

            MakeLine("YOU",
                "That evening, I finally tell Mom what's been happening.", 3),

            MakeLine("MOTHER",
                "You sound hurt, sweetheart. Do you want to tell me about it?", 3),

            MakeLine("YOU",
                "I do so much for them. I help with their work, share everything, and never say no. But they don't seem to care about me.", 3),

            MakeLine("MOTHER",
                "Being kind is a beautiful thing. But tell me, are you being kind to yourself too?", 3),

            MakeLine("YOU",
                "I thought if I kept everyone happy, they'd want me around.", 3),

            MakeLine("MOTHER",
                "You don't have to earn friendship by exhausting yourself, dear.", 3),

            MakeLine("MOTHER",
                "A real friend can ask for your help, but they should also respect your time, your feelings, and your right to say no.", 3),

            MakeLine("YOU",
                "But what if they stop being my friends when I say no?", 3),

            MakeLine("MOTHER",
                "Then you'll learn something important about that friendship. You can be gentle without being a pushover, and firm without being cruel.", 3),

            MakeLine("MOTHER",
                "Never confuse keeping the peace with being at peace. You deserve relationships where you can be yourself, too.", 3),

            MakeLine("YOU",
                "For the first time, I wonder whether I've been so afraid of being alone that I've forgotten to stand up for myself.", 3),

            MakeLine("YOU",
                "A few days later, a familiar request comes my way.", 1),

            MakeLine("FRIEND",
                "Can you send me all your notes again? I haven't studied, and the test is tomorrow.", 1),

            MakeLine("YOU",
                "I can't send them tonight. I need to study for my own test. I hope you understand.", 1),

            MakeLine("FRIEND",
                "Wow. You've changed. You used to be helpful.", 1),

            MakeLine("YOU",
                "I still want to help when I can. But I can't keep putting my responsibilities aside every time.", 1),

            MakeLine("YOU",
                "My heart pounds as I say it. I expect to feel guilty, but instead, I feel a little lighter.", 1),

            MakeLine("YOU",
                "At lunch, I notice the group sitting together again.", 2),

            MakeLine("FRIEND",
                "We're going out after class. Can you do our part of the assignment first? We'll send you the details later.", 2),

            MakeLine("YOU",
                "No, I'll finish my own part. Everyone needs to contribute to the group assignment.", 2),

            MakeLine("FRIEND",
                "Seriously? You're making a big deal out of nothing.", 2),

            MakeLine("YOU",
                "I'm not trying to argue. I just want us to share the work fairly.", 2),

            MakeLine("YOU",
                "They walk away, annoyed. It hurts, but I don't chase after them to change their minds.", 2),

            MakeLine("YOU",
                "Later, another classmate asks if I'd like to join her for lunch.", 2),

            MakeLine("NEW FRIEND",
                "Hey, do you want to sit with us? There's room at our table.", 2),

            MakeLine("YOU",
                "I'd love to. Thank you for asking!", 2),

            MakeLine("YOU",
                "I used to think being liked meant always being available, always agreeing, and never disappointing anyone.", 2),

            MakeLine("YOU",
                "Now I'm learning that kindness should never require me to abandon myself.", 2),

            MakeLine("YOU",
                "I can care about others and still protect my time. I can forgive someone and still choose distance.", 2),

            MakeLine("YOU",
                "Not everyone will understand my boundaries. And that's okay.", 2),

            MakeLine("YOU",
                "I don't need a crowd that only likes what I can do for them. I want friends who care about who I am.", 2),

            MakeLine("END",
                "You don't have to become less kind to earn respect. You just have to remember that you deserve it, too.", 2)
        };

        currentLine = 0;
    }

    DialogueLine MakeLine(string speaker, string text, int backgroundIndex)
    {
        return new DialogueLine
        {
            speaker = speaker,
            text = text,
            backgroundIndex = backgroundIndex
        };
    }

    void StartStory()
    {
        currentLine = 0;
        storyFinished = false;

        startScreen.SetActive(false);
        endScreen.SetActive(false);
        dialoguePanel.SetActive(true);

        ShowLine();
    }

    void RestartStory()
    {
        BuildStory();
        StartStory();
    }

    void ShowLine()
    {
        DialogueLine line = dialogueLines[currentLine];

        speakerName.text = line.speaker;
        dialogueText.text = line.text;

        if (backgroundImage != null &&
            backgrounds != null &&
            line.backgroundIndex >= 0 &&
            line.backgroundIndex < backgrounds.Length &&
            backgrounds[line.backgroundIndex] != null)
        {
            backgroundImage.sprite = backgrounds[line.backgroundIndex];
        }

        continuePrompt.text = "Click to continue";
    }

    void ShowNextLine()
    {
        if (currentLine >= dialogueLines.Length - 1)
        {
            FinishStory();
            return;
        }

        currentLine++;
        ShowLine();
    }

    void FinishStory()
    {
        storyFinished = true;
        dialoguePanel.SetActive(false);
        endScreen.SetActive(true);
    }

    void CloseGame()
    {
        Application.Quit();
    }
}

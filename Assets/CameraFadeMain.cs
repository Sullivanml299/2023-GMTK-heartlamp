using UnityEngine;

public class CameraFadeMain : MonoBehaviour
{
    public KeyCode key = KeyCode.Space; // Which key should trigger the fade?
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    // Rather than Lerp or Slerp, we allow adaptability with a configurable curve
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;


    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    public float p1, p2, p3, p4;
    public float p5, p6, p7, p8;

    public Font myFont;

    public string QuestString;

    public int questNum;

    public static bool beenToDungeon = false;

    private void Start()
    {
        if (startFadedOut) alpha = 1f; else alpha = 0f;
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();

        p1 = (Screen.width / 2);
        p2 = (Screen.height / 2);
        p3 = 9999;
        p4 = 9999;


        p5 = (Screen.width* 0.03f);
        p6 = (Screen.height* 0.95f);
        p7 = 9999;
        p8 = 9999;


        //GUIStyle myStyle = new GUIStyle();
        GUI.skin.font = myFont;

        QuestString = "Go pick up The Hero.";

        questNum = 0;
    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Hero").Length != 0)
        {
            questNum = 0;
        }
        else if (beenToDungeon)
        {
            questNum = 2;
        }
        else
        {
            questNum = 1;
        }

        if (direction == 0 && Input.GetKeyDown(KeyCode.Q))
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;
            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
            }

        }
    }
    public void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if (alpha <= 0f || alpha >= 1f) direction = 0;
        }

        if (alpha >= 1f) // Fully faded out
        {
            if (questNum == 0) { GUI.Label(new Rect(p1, p2, p3, p4), "QUEST: Go pick up The Hero."); }
            if (questNum == 1) { GUI.Label(new Rect(p1, p2, p3, p4), "QUEST: Take The Hero to the dungeon in the desert."); }
            if (questNum == 2) { GUI.Label(new Rect(p1, p2, p3, p4), "QUEST: Kill The Beast atop the mountain and through the forest."); }
            GUI.Label(new Rect(p5, p6, p7, p8), "Press Q to exit quest view.");
        }
        else
        {
            GUI.Label(new Rect(p5, p6, p7, p8), "Press Q to see current quest. SPACE to Jump. WASD to move.");
        }
    }
}

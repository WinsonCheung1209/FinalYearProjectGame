using UnityEngine;
using System.Collections;

public class HiScoreApp : MonoBehaviour {

    public Rect m_uploadBut;
    public Rect m_downLoadBut;

    public Rect m_nameLablePos;
    public Rect m_scoreLablePos;
    public Rect m_nameTxtField;
    public Rect m_scoreTxtField;

    public Rect m_scrollViewPosition;
    public Vector2 m_scrollPos;
    public Rect m_scrollView;

    public Rect m_gridPos;


    public string[] m_hiscores;

    protected string m_name = "";

    protected string m_score = "";

    // Use this for initialization
    void Start() {

        m_hiscores = new string[20];
    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI() {

        m_name = GUI.TextField(m_nameTxtField, m_name);
        m_score = GUI.TextField(m_scoreTxtField, m_score);

        GUI.Label(m_nameLablePos, "Name");
        GUI.Label(m_scoreLablePos, "Score");

        if (GUI.Button(m_uploadBut, "Upload")) {
            StartCoroutine(UploadScore(m_name, m_score));
            m_name = "";
            m_score = "";
        }

        if (GUI.Button(m_downLoadBut, "Download")) {
            StartCoroutine(DownloadScores(m_name, m_score));
        }

        m_scrollPos = GUI.BeginScrollView(m_scrollViewPosition, m_scrollPos, m_scrollView);

        m_gridPos.height = m_hiscores.Length * 30;

        GUI.SelectionGrid(m_gridPos, 0, m_hiscores, 1);

        GUI.EndScrollView();
    }

    IEnumerator UploadScore(string name, string score) {
        PostStream poststream = new PostStream();

        poststream.BeginWrite(true);
        poststream.Write("username", name);
        poststream.Write("score", score);
        poststream.EndWrite();

        WWW www = new WWW("www.sussex.ac.uk/Users/wcwc20/Project/UploadScore.php", poststream.BYTES, poststream.headers);

        yield return www;

        if (www.error != null) {
            Debug.LogError(www.error);
        }

    }

    IEnumerator DownloadScores(string name, string score) {

        WWW www = new WWW("www.sussex.ac.uk/Users/wcwc20/Project/DownloadScores.php");

        yield return www;

        if (www.error != null) {
            Debug.LogError(www.error);
        } else {
            int count = 0;

            PostStream poststream = new PostStream();

            poststream.BeginRead(www, true);
            poststream.ReadInt(ref count);

            if (count > 0)
                m_hiscores = new string[count];
            for (int i = 0; i < count; i++) {
                string tname = "";
                string tscore = "";
                poststream.ReadString(ref tname);
                poststream.ReadString(ref tscore);

                m_hiscores[i] = tname + ":" + tscore;
            }
            bool ok = poststream.EndRead();
            if (!ok) {
                Debug.LogError("MD5 error");
            }
        }
    }
}

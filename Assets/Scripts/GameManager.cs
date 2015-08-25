using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public LoadManager loadManager;

    public Player player = new Player();

    public float toSecond = 60 * 60;

    public bool allowMusic = true;

    private int currentLevel = 1;

    private string tempId = "";

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        loadManager = GetComponent<LoadManager>();

        tempId = Guid.NewGuid().ToString("N");
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            GameManager.instance.loadManager.LoadSelectScene();
    }

    void OnLevelWasLoaded(int index)
    {

    }

    public bool allowLoad(int level)
    {
        if (level == currentLevel)
            return true;
        else
            return false;
    }

    public void GameClear()
    {
        currentLevel++;
    }

    public void SendEmail(string content)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("libraryescape@gmail.com");
        mail.To.Add("libraryescape@gmail.com");
        mail.Subject = tempId;
        DateTime nowTime = System.DateTime.Now;

        mail.Body = tempId + ", " + content + ", "+nowTime;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("libraryescape@gmail.com", "LE@ntu2015") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        //smtpServer.Send(mail);
        smtpServer.SendAsync(mail, null);
        Debug.Log("success");
    }

}

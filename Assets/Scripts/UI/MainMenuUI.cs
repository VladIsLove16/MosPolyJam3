using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    
    private UIDocument document;
    private VisualElement root;
    private VisualElement UpperPanel;
    private VisualElement CenterPanel;
    private VisualElement DownPanel;
    private VisualElement SettingsMenu;
    private UnityEngine.UIElements.TextField PromocodeField;
    private Button Submit;

    private Button MyVK;
    private Button NataliaVK;
    private Button MyTG;
    private Button Play;
    private Button EndlessMode;
    private Button SettingsBtn;
    private Button GoToShop;
    private Button Exit;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private SettingsUI settingsUI;

    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        //UpperPanel = root.Q("UpperPanel");
        //CenterPanel = root.Q("CenterPanel");
        //DownPanel = root.Q("DownPanel");

        Play = root.Q("A").Q("B").Q("Play") as Button;
        Play.clicked += OnPlayClicked  ;
        EndlessMode = root.Q("A").Q("B").Q("EndlessMode") as Button;
        if(PlayerPrefs.GetInt("gameWon") == 1)
        {
            EndlessMode.SetEnabled(true);
        }
        else
        {
            EndlessMode.SetEnabled(false);
        }    
        EndlessMode.clicked += OnEndlessModeClicked;

        VisualElement contacts = root.Q("Contacts");
        VisualElement References = root.Q("References");
        MyVK = References.Q("MyVK") as Button;
        MyVK.clicked += () =>
        {
            Application.OpenURL("https://vk.com/hochu_microbov");
        };
        NataliaVK = References.Q("NataliaVK") as Button;
        NataliaVK.clicked += () =>
        {
            Application.OpenURL("https://vk.com/id180773230");
        };
        MyTG = root.Q("A").Q("B").Q("MyTG") as Button;
        MyTG.clicked += () =>
        {
            Application.OpenURL("https://t.me/MeowLand_Vladislove");
        };

        Exit = root.Q("A").Q("B").Q("Exit") as Button;
        Exit.clicked += OnExitClicked;

        SettingsMenu = root.Q("SettingsMenu");
        SettingsMenu.style.display = DisplayStyle.None;
        settingsUI.Setup(SettingsMenu); 
        SettingsBtn = root.Q("A").Q("B").Q("Settings") as Button;
        SettingsBtn.clicked += () =>
        {
            ManageSettingsMenu();
        };
        //YandexGame.GameReadyAPI();

        PromocodeField = root.Q("Promocode") as TextField;
        Submit = PromocodeField.Q<Button>();

        //if (wallet.PromoEntered)
        //{
        //    Submit.text = "Добавлено!";
        //    Submit.SetEnabled(false);
        //}
        //Submit.clicked += () =>
        //{
        //    if (PromocodeField.text == "whatislove")
        //    {
        //        wallet.AddMoney(new Money(100));
        //        wallet.PromoEntered = true;
        //        Submit.text = "Добавлено!";
        //        Submit.SetEnabled(false);
        //    }
        //};
    }

    private void OnEndlessModeClicked()
    {
        SceneLoader.Load(SceneLoader.Scene.Level4);
    }

    private static void OnExitClicked()
    {
        Application.Quit();
    }

    private void ManageSettingsMenu()
    {
        if(SettingsMenu.style.display == DisplayStyle.Flex )
            SettingsMenu.style.display = DisplayStyle.None;
        else
            SettingsMenu.style.display = DisplayStyle.Flex;
    }
    private void OnPlayClicked()
    {
        Debug.Log("play clicke");
        ServiceLocator.Current.Get<EventBus>().Invoke(new GameStartedEvent());
        SceneLoader.Load(SceneLoader.Scene.Level4);
    }
}

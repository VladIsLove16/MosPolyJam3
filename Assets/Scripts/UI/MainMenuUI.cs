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
    private Button MyTG;
    private Button Play;
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
        Play.clicked += OnPlayClicked
            ;
        MyVK = root.Q("A").Q("B").Q("MyVK") as Button;
        MyVK.clicked += () =>
        {
            Application.OpenURL("https://vk.com/hochu_microbov");
        };
        MyTG = root.Q("A").Q("B").Q("MyTG") as Button;
        MyTG.clicked += () =>
        {
            Application.OpenURL("https://t.me/MeowLand_Vladislove");
        };
        GoToShop = root.Q("A").Q("B").Q("Shop") as Button;
        GoToShop.clicked += () =>
        {
            SceneLoader.Load(SceneLoader.Scene.Shop);
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
        SceneLoader.Load(SceneLoader.Scene.Game);
    }
}

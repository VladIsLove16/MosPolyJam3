using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioClip VolumeChangedSound;
    private bool isPlaing;
    [SerializeField]
    private AudioSource audioSource;
    private Slider MusicVolumeSlider;
    //private Slider MeowingVolumeSlider;
    private Slider ButtonsVolumeSlider;
    private Button Close;
    
    const string MusicVolume = "MusicVolume";
    //const string MeowingVolume = "MeowingVolume";
    const string ButtonsVolume = "ButtonsVolume";
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void Setup(VisualElement root)
    {
        MusicVolumeSlider = root.Q("Music") as Slider;
        MusicVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, MusicVolume));

        //MeowingVolumeSlider = root.Q("Meowing") as Slider;
        //MeowingVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, MeowingVolume));

        ButtonsVolumeSlider = root.Q("Buttons") as Slider;
        ButtonsVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, ButtonsVolume));

        Close = root.Q("Close") as Button;
        Close.clicked += () =>
        {
            ManageSettingsMenu(root);
        };

        SetupInitialValues();
    }
    private void ManageSettingsMenu(VisualElement root)
    {
        if (root.style.display == DisplayStyle.Flex)
            root.style.display = DisplayStyle.None;
        else
            root.style.display = DisplayStyle.Flex;
    }
    private void SetupInitialValues()
    {
        ButtonsVolumeSlider.value = PlayerPrefs.GetFloat(ButtonsVolume, 0.5f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolume, 0.5f);
        //MeowingVolumeSlider.value = PlayerPrefs.GetFloat(MeowingVolume, 0.5f);

        audioMixer.SetFloat(MusicVolume, PlayerPrefs.GetFloat(MusicVolume, 0.5f));
        //audioMixer.SetFloat(MeowingVolume, PlayerPrefs.GetFloat(MeowingVolume, 0.5f));
        audioMixer.SetFloat(ButtonsVolume, PlayerPrefs.GetFloat(ButtonsVolume, 0.5f));

        Debug.Log(PlayerPrefs.GetFloat(ButtonsVolume, 1f) + " " + PlayerPrefs.GetFloat(MusicVolume, 1f) + " " + PlayerPrefs.GetFloat(ButtonsVolume, 1f));
    }

    private void VolumeChange(float value,string type)
    {
        Debug.Log("new value"+ value + " " + type);
        //audioMixer.SetFloat(type, Mathf.Log10(value) * 20);
        //PlayerPrefs.SetFloat(type, Mathf.Log10(value) * 20);
        audioMixer.SetFloat(type, value);
        PlayerPrefs.SetFloat(type, value);
        switch (type)
        {
            default:
                if(audioSource!=null)
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = VolumeChangedSound;
                        audioSource.Play();
                    }
                break;
                //case MeowingVolume:
                //        if (!audioSource.isPlaying)
                //        {
                //            audioSource.clip = MeowSound;
                //            audioSource.Play();
                //        }
                //    break;
        }
    }
}

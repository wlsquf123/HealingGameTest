using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM; // 배경음
    public AudioSource[] SFX; // 효과음
    public Slider BgmSlider;
    public Slider SfxSlider;

    private void Update()
    {
        BGM.volume = BgmSlider.value;

        foreach (var fsx in SFX)
        {
            fsx.volume = SfxSlider.value;
        }
    }

}






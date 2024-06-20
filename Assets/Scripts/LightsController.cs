using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    public static bool _lightsOn = true;
    ParticleSystem[] _lights;
    Color _fogOriginalColor;
    [SerializeField] float _densityOff = 0.1f;
    [SerializeField] float _densityOn = 0.3f;
    float _densityOffRem = 0.1f;
    float _densityOnRem = 0.3f;
    [SerializeField] Color _offColor = Color.black;
    [SerializeField] Color _onColor;
    [SerializeField] float _thunderDensity = 0.01f;
    [SerializeField] Color _ambientColor = Color.black;
    public static bool _thunder = false;
    [SerializeField] float _speed = 0.5f;
    [SerializeField] float _delayThunder = 0.5f;
    [SerializeField] float _candleLightInten = 0.08f;
    [SerializeField] float _normalLightInten = 8f;
    // Start is called before the first frame update
    void Start()
    {
        _densityOffRem = _densityOff;
        _densityOnRem = _densityOn;
        _onColor = RenderSettings.fogColor;
        _lights = (ParticleSystem[])FindObjectsOfType(typeof(ParticleSystem));
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, _ambientColor, _speed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.O))
            _thunder = true;
        switch (_thunder)
        {
            case true:
                StartCoroutine(Thunder());
                break;
            case false:
                switch (_lightsOn)
                {
                    case true:
                        foreach (ParticleSystem light in _lights)
                        {
                            var lights = light.lights;
                            lights.enabled = true;
                            if(light.gameObject.transform.parent.CompareTag("candle"))
                                lights.intensity = _candleLightInten;
                            else if(!light.gameObject.transform.parent.CompareTag("chandelier"))
                                lights.intensity = _normalLightInten;
                        }
                        RenderSettings.fogColor = _onColor;
                        RenderSettings.fogDensity = RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, _densityOn, _speed * Time.deltaTime);
                        _lightsOn = true;
                        break;
                    case false:
                        foreach (ParticleSystem light in _lights)
                        {
                            var lights = light.lights;
                            lights.enabled = false;
                            if(light.gameObject.name == "Lamp_Lia" || light.gameObject.name == "FireLight")
                                lights.enabled = true;
                        }
                        RenderSettings.fogColor = _offColor;
                        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, _densityOff, _speed * Time.deltaTime);
                        _lightsOn = false;
                        break;
                    default:
                }
                break;
        }
    }

    IEnumerator Thunder(){
        _thunder = false;
        GetComponent<AudioSource>().Play();
        RenderSettings.fogDensity =_thunderDensity;
        _densityOff = _densityOn = _thunderDensity;
        RenderSettings.ambientSkyColor = Color.white;
        yield return new WaitForSeconds(_delayThunder);
        _densityOff = _densityOffRem;
        _densityOn = _densityOnRem;
        _ambientColor = Color.black;
    }
}

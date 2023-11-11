using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameMenu
{
    public class UISettingManager : MonoBehaviour
    {
        private static UISettingManager instance;

        public static UISettingManager NowInstance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<UISettingManager>();
                return instance;
            }
        }

        [Header("GAME SETTINGS")]
        public Slider musicSlider;
        public Slider sfxSlider;
        public GameObject showHudText;
        public GameObject toolTipsText;

        [Header("VIDEO SETTINGS")]
        public GameObject fullScreenText;
        public GameObject cameraEffectsText;
        public GameObject shadowofftextLINE;
        public GameObject shadowlowtextLINE;
        public GameObject shadowhightextLINE;
        public GameObject texturelowtextLINE;
        public GameObject texturemedtextLINE;
        public GameObject texturehightextLINE;
        public GameObject motionblurtext;

        //KeyBinding Settings
        private GameObject[] keyBindButtons;

        private void Awake()
        {
            keyBindButtons = GameObject.FindGameObjectsWithTag("KeyButton");
        }

        public void Start()
        {
            // check slider values
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 10);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 10);

            // check hud value
            if (PlayerPrefs.GetInt("ShowHUD") == 0)
                showHudText.GetComponent<TMP_Text>().text = "off";
            else
                showHudText.GetComponent<TMP_Text>().text = "on";


            // check tool tip value
            if (PlayerPrefs.GetInt("ToolTips") == 0)
                toolTipsText.GetComponent<TMP_Text>().text = "off";
            else
                toolTipsText.GetComponent<TMP_Text>().text = "on";


            // check full screen
            if (Screen.fullScreen == true)
                fullScreenText.GetComponent<TMP_Text>().text = "on";
            else if (Screen.fullScreen == false)
                fullScreenText.GetComponent<TMP_Text>().text = "off";

            // check motion blur
            if (PlayerPrefs.GetInt("CameraEffect") == 0)
            {
                cameraEffectsText.GetComponent<TMP_Text>().text = "off";
            }
            else if (PlayerPrefs.GetInt("CameraEffect") == 1)
            {
                cameraEffectsText.GetComponent<TMP_Text>().text = "on";
            }

            // check shadow distance/enabled
            if (PlayerPrefs.GetInt("Shadows") == 0)
            {
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadowDistance = 0;
                shadowofftextLINE.gameObject.SetActive(true);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 1)
            {
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadowDistance = 75;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(true);
                shadowhightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Shadows") == 2)
            {
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadowDistance = 500;
                shadowofftextLINE.gameObject.SetActive(false);
                shadowlowtextLINE.gameObject.SetActive(false);
                shadowhightextLINE.gameObject.SetActive(true);
            }

            // check texture quality
            if (PlayerPrefs.GetInt("Textures") == 0)
            {
                QualitySettings.masterTextureLimit = 2;
                texturelowtextLINE.gameObject.SetActive(true);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 1)
            {
                QualitySettings.masterTextureLimit = 1;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(true);
                texturehightextLINE.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Textures") == 2)
            {
                QualitySettings.masterTextureLimit = 0;
                texturelowtextLINE.gameObject.SetActive(false);
                texturemedtextLINE.gameObject.SetActive(false);
                texturehightextLINE.gameObject.SetActive(true);
            }

            // check motion blur
            if (PlayerPrefs.GetInt("MotionBlur") == 0)
            {
                motionblurtext.GetComponent<TMP_Text>().text = "off";
            }
            else if (PlayerPrefs.GetInt("MotionBlur") == 1)
            {
                motionblurtext.GetComponent<TMP_Text>().text = "on";
            }
        }

        #region GamePane
        public void MusicVolume()
        {
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            AudioManager.Instance.MusicVolume(musicSlider.value);
        }

        public void SfxVolume()
        {
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
            AudioManager.Instance.SfxVolume(sfxSlider.value);
        }

        // the playerprefs variable that is checked to enable hud while in game
        public void ShowHUD()
        {
            if (PlayerPrefs.GetInt("ShowHUD") == 0)
            {
                PlayerPrefs.SetInt("ShowHUD", 1);
                showHudText.GetComponent<TMP_Text>().text = "on";
            }
            else if (PlayerPrefs.GetInt("ShowHUD") == 1)
            {
                PlayerPrefs.SetInt("ShowHUD", 0);
                showHudText.GetComponent<TMP_Text>().text = "off";
            }
        }

        // show tool tips like: 'How to Play' control pop ups
        public void ToolTips()
        {
            if (PlayerPrefs.GetInt("ToolTips") == 0)
            {
                PlayerPrefs.SetInt("ToolTips", 1);
                toolTipsText.GetComponent<TMP_Text>().text = "on";
            }
            else if (PlayerPrefs.GetInt("ToolTips") == 1)
            {
                PlayerPrefs.SetInt("ToolTips", 0);
                toolTipsText.GetComponent<TMP_Text>().text = "off";
            }
        }

        #endregion

        #region KeyBindingPane
        public void UpdateKeyText(string key, KeyCode code)
        {
            TextMeshPro tmp = Array.Find(keyBindButtons, x => x.name == key).GetComponentInChildren<TextMeshPro>();
            tmp.text = code.ToString();
        }
        #endregion

        #region VideoPane
        public void FullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;

            if (Screen.fullScreen == true)
            {
                fullScreenText.GetComponent<TMP_Text>().text = "on";
            }
            else if (Screen.fullScreen == false)
            {
                fullScreenText.GetComponent<TMP_Text>().text = "off";
            }
        }

        public void CameraEffects()
        {
            if (PlayerPrefs.GetInt("CameraEffects") == 0)
            {
                PlayerPrefs.SetInt("CameraEffects", 1);
                cameraEffectsText.GetComponent<TMP_Text>().text = "on";
            }
            else if (PlayerPrefs.GetInt("CameraEffects") == 1)
            {
                PlayerPrefs.SetInt("CameraEffects", 0);
                cameraEffectsText.GetComponent<TMP_Text>().text = "off";
            }
        }

        public void ShadowsOff()
        {
            PlayerPrefs.SetInt("Shadows", 0);
            QualitySettings.shadowCascades = 0;
            QualitySettings.shadowDistance = 0;
            shadowofftextLINE.gameObject.SetActive(true);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsLow()
        {
            PlayerPrefs.SetInt("Shadows", 1);
            QualitySettings.shadowCascades = 2;
            QualitySettings.shadowDistance = 75;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(true);
            shadowhightextLINE.gameObject.SetActive(false);
        }

        public void ShadowsHigh()
        {
            PlayerPrefs.SetInt("Shadows", 2);
            QualitySettings.shadowCascades = 4;
            QualitySettings.shadowDistance = 500;
            shadowofftextLINE.gameObject.SetActive(false);
            shadowlowtextLINE.gameObject.SetActive(false);
            shadowhightextLINE.gameObject.SetActive(true);
        }

        public void TexturesLow()
        {
            PlayerPrefs.SetInt("Textures", 0);
            QualitySettings.masterTextureLimit = 2;
            texturelowtextLINE.gameObject.SetActive(true);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesMed()
        {
            PlayerPrefs.SetInt("Textures", 1);
            QualitySettings.masterTextureLimit = 1;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(true);
            texturehightextLINE.gameObject.SetActive(false);
        }

        public void TexturesHigh()
        {
            PlayerPrefs.SetInt("Textures", 2);
            QualitySettings.masterTextureLimit = 0;
            texturelowtextLINE.gameObject.SetActive(false);
            texturemedtextLINE.gameObject.SetActive(false);
            texturehightextLINE.gameObject.SetActive(true);
        }


        public void MotionBlur()
        {
            if (PlayerPrefs.GetInt("MotionBlur") == 0)
            {
                PlayerPrefs.SetInt("MotionBlur", 1);
                motionblurtext.GetComponent<TMP_Text>().text = "on";
            }
            else if (PlayerPrefs.GetInt("MotionBlur") == 1)
            {
                PlayerPrefs.SetInt("MotionBlur", 0);
                motionblurtext.GetComponent<TMP_Text>().text = "off";
            }
        }

        #endregion
    }
}
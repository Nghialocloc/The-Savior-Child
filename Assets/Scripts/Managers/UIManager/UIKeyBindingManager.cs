using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


namespace GameMenu
{
    public class UIKeyBindingManager : MonoBehaviour
    {
        private static UIKeyBindingManager instance;

        public static UIKeyBindingManager NowInstance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<UIKeyBindingManager>();
                return instance;
            }
        }

        public Dictionary<string, KeyCode> ListKeyBinds { get; private set; }
      
        private GameObject currentkey;
        [SerializeField]
        private GameObject confirmKeyPanel;

        // Use this for intialization
        void Start()
        {
            ListKeyBinds = new Dictionary<string, KeyCode>();

            BindKey("Move.up",(KeyCode) System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Move.up","W")));
            BindKey("Move.down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.down", "S")));
            BindKey("Move.left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.left", "A")));
            BindKey("Move.right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.right", "D")));
            BindKey("Move.jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.jump", "Space")));
            BindKey("Move.sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.sprint", "LeftShift")));
            BindKey("Move.interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Move.interact", "E")));
            BindKey("Game.Open", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Game.Open", "Q")));
            BindKey("Game.Book", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Game.Book", "V")));
            BindKey("Game.Equipment", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Game.Equipment", "F")));
            BindKey("GNR.Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GNR.Pause", "Escape")));
        }

        private void Update()
        {
            if (Input.GetKeyDown(ListKeyBinds["Move.up"]))
                Debug.Log("Up");
            if (Input.GetKeyDown(ListKeyBinds["Move.down"]))
                Debug.Log("Down");
            if (Input.GetKeyDown(ListKeyBinds["Move.left"]))
                Debug.Log("Left");
            if (Input.GetKeyDown(ListKeyBinds["Move.right"]))
                Debug.Log("Right");
        }

        // Can only use for the intial bind because it suck
        public void BindKey(string button, KeyCode keyBind)
        {
            Dictionary<string, KeyCode> currentDictionary = ListKeyBinds;

            if (!currentDictionary.ContainsKey(button))
            {
                currentDictionary.Add(button, keyBind);
                UISettingManager.NowInstance.UpdateKeyText(button, keyBind);
            }
            else if (currentDictionary.ContainsKey(button))
            {
                string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
                currentDictionary[myKey] = KeyCode.None;
                UISettingManager.NowInstance.UpdateKeyText(button, KeyCode.None);
            }

            currentDictionary[button] = keyBind;
            UISettingManager.NowInstance.UpdateKeyText(button, keyBind);

        }

        //Goi toi phim duoc gan vao
        public void KeyBindOnClick(GameObject buttonClicked)
        {
            currentkey = buttonClicked;
        }

        private void OnGUI()
        {
            if (currentkey != null)
            {
                Event changeE = Event.current;
                if (changeE.isKey)
                {
                    //Make sure key bind already can't be bind again
                    if (ListKeyBinds.ContainsValue(changeE.keyCode)) 
                    {
                        string myKey = ListKeyBinds.FirstOrDefault(x => x.Value == changeE.keyCode).Key;
                        ListKeyBinds[myKey] = KeyCode.None;
                        UISettingManager.NowInstance.UpdateKeyText(myKey, KeyCode.None);
                    }
                    ListKeyBinds[currentkey.name] = changeE.keyCode;
                    currentkey.transform.GetComponentInChildren<TextMeshPro>().text = changeE.keyCode.ToString();
                    currentkey = null;
                    confirmKeyPanel.SetActive(false);
                    Savekeys();
                }
            }
        }

        //Luu ket qua thay doi gan phim
        private void Savekeys()
        {
            foreach(var key in ListKeyBinds)
            {
                PlayerPrefs.SetString(key.Key, key.Value.ToString());
            }
            PlayerPrefs.Save();
        }
    }
}
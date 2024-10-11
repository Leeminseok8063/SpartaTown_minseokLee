using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Assets.Sources.Utils;
using UnityEngine.EventSystems;

namespace Assets.Sources.Manager
{
    internal class UIManager : MonoBehaviour
    {
        public static UIManager Instance()
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            UpdateTime();
            UpdatePos();
            UpdateNames();
        }

        public void UpdateTime()
        {
            DateTime localTime = DateTime.Now;
            Time.text = localTime.ToString();
        }
        public void UpdatePos()
        {
            if (GameManager.Instance().GetCurrentPlayer() == null) return;
            Pos pos = GameManager.Instance().GetCurrentPlayer().GetPlayerCell();
            string str = $"X:{pos.x} Y:{pos.y}";
            Pos.text = str;
        }
        public void UpdateNames()
        {
            Names.text = "접속자 목록" + "\n";
            foreach(string str in userNameList)
            {
                Names.text += str + "\n";
            }
        }
        public void SetComment(string str)
        {
            Comment.text = str;
        }
        public void ChangeChar()
        {
            Player player = GameManager.Instance().GetCurrentPlayer();
            CHARTYPE type = player.GetCharType() == CHARTYPE.BOY ? CHARTYPE.GIRL : CHARTYPE.BOY;
            player.SetCharType(type);
            EventSystem.current.SetSelectedGameObject(null);

        }
        public void ChangeName()
        {
            Player player = GameManager.Instance().GetCurrentPlayer();
            userNameList.Remove(player.Name.text);
            GameManager.Instance().GetCurrentPlayer().SetCharName(nameChangeField.text);
            AddNameInList(nameChangeField.text);
            TriggerNameChangeInterface();
        }
        public void CreateButtonClickedMale()
        {
            if (startField.text.Length > 0)
            {
               GameManager.Instance().CreatePlayer(startField.text, CHARTYPE.BOY);
            }
        }
        public void CreateButtonClickedFemale()
        {
            if (startField.text.Length > 0)
            {
                GameManager.Instance().CreatePlayer(startField.text, CHARTYPE.GIRL);
            }
        }
        public void ExitButton()
        {
            GameManager.Instance().ExitProgram();
        }
        
        public void TriggerNameChangeInterface()
        {
            nameChangeField.text = string.Empty;
            bool flag = NameChangeInterface.activeSelf;
            NameChangeInterface.SetActive(!flag);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void TriggerGameInterface()
        {
            bool flag = GameInterface.activeSelf;
            GameInterface.SetActive(!flag);
        }

        public void TriggerStartInterface()
        {
            bool flag = StarInterface.activeSelf;
            StarInterface.SetActive(!flag);
        }

        public void TriggerNamesInterface()
        {
            bool flag = NamesInterface.activeSelf;
            NamesInterface.SetActive(!flag);
            EventSystem.current.SetSelectedGameObject(null);

        }

        public void TriggerCommentInterface()
        {
            bool flag = Commentinterface.activeSelf;
            Commentinterface.SetActive(!flag);
        }

        public void AddNameInList(string str)
        {
            userNameList.Add(str);
        }
      
        public Text Time;
        public Text Pos;
        public Text Names;
        public Text Comment;
        public GameObject GameInterface;
        public GameObject StarInterface;
        public GameObject NameChangeInterface;
        public GameObject NamesInterface;
        public GameObject Commentinterface;
        public InputField startField;
        public InputField nameChangeField;

        private static UIManager instance;
        private List<string> userNameList = new List<string>();
    }
}

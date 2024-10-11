using Assets.Sources.Manager;
using Assets.Sources.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Sources.Object
{
    class Npc : MonoBehaviour
    {

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();  
        }

        public void CharSetting(Pos _pos, NPCTYPE _type, string _name)
        {
            name = _name;
            pos = _pos;
            type = _type;

            if (spriteRenderer == null) Debug.Log("렌더널");
            switch(type)
            {
                case NPCTYPE.NPC01:
                    spriteRenderer.sprite = Npc01;
                    break;
                case NPCTYPE.NPC02:
                    spriteRenderer.sprite = Npc02;
                    break;
                case NPCTYPE.NPC03:
                    spriteRenderer.sprite = Npc03;
                    break;
            }
        }
        public void CharCommentSetting()
        {
            string comple = $"[{name}]" + "\n" + comment + "\n\n" + "E를 누르면 대화창 종료.";
            UIManager.Instance().SetComment(comple);
        }
        public void CharCommentInit(string str)
        {
            comment = str;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) ActivePanel.SetActive(true);
        } 

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) ActivePanel.SetActive(false);
        }


        Pos pos;
        NPCTYPE type;
        string name;
        string comment;
        SpriteRenderer spriteRenderer;

        public GameObject ActivePanel; 
        public Sprite Npc01;
        public Sprite Npc02;
        public Sprite Npc03;
    }
   
}

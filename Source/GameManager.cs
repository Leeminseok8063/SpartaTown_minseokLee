using Assets.Sources.Utils;
using Assets.Sources.Object;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Assets.Sources.Manager
{
    internal class GameManager : MonoBehaviour
    {
        public static GameManager Instance()
        {
            if(instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        private void Awake()
        {
          instance = this;
        }

        private void Start()
        {
            {
                CreateNpc(new Pos { x = -4, y = 1 }, NPCTYPE.NPC01, "이민석"
                , "안녕하세요. 17조 이민석입니다, 컨텐츠를 추가해서 제출 하고 싶었지만 주말에 개인사정이 겹쳐 " +
                "일찍 제출하게 되었습니다, 필수기능과 도전기능에서 조금씩 변형하여 게임 스타일에 맞게 개발 해보았습니다.");

                CreateNpc(new Pos { x = -20, y = -13 }, NPCTYPE.NPC02, "조지"
                    , "1번을 눌러 괭이를 장착하고,울타리 안에 있는 부드러운 땅에서 SPACEBAR를 누르면 땅을 갈수있어!");

                CreateNpc(new Pos { x = -8, y = 1 }, NPCTYPE.NPC03, "할머니"
                    , "다리를 건너 남쪽으로 내려 가면 비어있는 집이 있단다..");
            }//SPAWN NPC
            
        }

        //================================================

        public void CreatePlayer(string name, CHARTYPE type)
        {
            UIManager.Instance().TriggerStartInterface();
            UIManager.Instance().TriggerGameInterface();
            UIManager.Instance().AddNameInList(name);
            Vector2Int StartCell = new Vector2Int(0, 0);
            Vector2 Cell = tileMap.GetCellCenterWorld((Vector3Int)StartCell);
            currentPlayer = Instantiate(playerPrefab, Cell, Quaternion.identity).GetComponent<Player>();
            currentPlayer.SetChar(name, type);       
        }
        public void CreateNpc(Pos pos, NPCTYPE type, string name, string comment)
        {
            Vector2 worldPos = tileMap.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
            Npc npc = Instantiate(npcPrefab, worldPos, Quaternion.identity).GetComponent<Npc>();
            npc.CharSetting(pos, type, name);
            npc.CharCommentInit(comment);
            UIManager.Instance().AddNameInList(name);
        }

        public Tilemap GetCurrentTileMap() { return tileMap; }
        public Player GetCurrentPlayer() { return currentPlayer; }
        public void ExitProgram()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        static GameManager instance = null;
        public GameObject playerPrefab = null;
        public GameObject npcPrefab = null;
        
        public Tilemap tileMap = null;
        private Player currentPlayer = null;
       
    }
}

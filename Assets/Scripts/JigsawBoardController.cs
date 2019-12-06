using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Managers;

namespace Nfynt.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class JigsawBoardController : Singleton<JigsawBoardController>
    {
        [Tooltip("In order of solution; starting from top left piece as index 0")]
        [SerializeField]
        private List<GameObject> targetPieces = new List<GameObject>();
        /// <summary>
        /// Slots having box collider
        /// </summary>
        private List<Collider> slots;
        /// <summary>
        /// Whether the slots are free
        /// </summary>
        public List<bool> slotsFree;
        /// <summary>
        /// Which slots have been solved
        /// </summary>
        public List<bool> solved;

        private bool boardSolvedOnce;
        MainSceneManager msMgr;

        private void Start()
        {
            slots = new List<Collider>();
            //slotsFree = new List<bool>();
            //solved = new List<bool>();
            foreach (Collider col in transform.GetComponentsInChildren<Collider>())
            {
                slots.Add(col);
                //slotsFree.Add(true);
                //solved.Add(false);
            }

            //int ind = 0;
            for (int i=0;i<slotsFree.Count;i++)
            {
                targetPieces[i].GetComponent<JigsawPieceBehaviour>().boardPos = i;
                targetPieces[i].GetComponent<JigsawPieceBehaviour>().SetTargetCollider(slots[i]);

                if (!slotsFree[i])
                    targetPieces[i].GetComponent<JigsawPieceBehaviour>().connectedCollider = slots[i];
            }
            boardSolvedOnce = false;
            if (MainSceneManager.Instance != null)
                msMgr = MainSceneManager.Instance;
            else
                msMgr = FindObjectOfType<MainSceneManager>();
        }
        
        /// <summary>
        /// Count of solved pieces.
        /// </summary>
        /// <returns>In range [0,9]</returns>
        public int SolvedPieces()
        {
            int res = 0;
            foreach (bool b in solved)
                if (b) res++;

            int cnt = 0;
            foreach (GameObject GO in targetPieces)
                if (GO.GetComponent<JigsawPieceBehaviour>().Solved())
                    cnt++;

            if (cnt != res)
                Debug.LogWarning("Solved mismatched! col count:" + cnt.ToString() + "- bool count:" + res.ToString());

            return cnt;
        }

        public bool IsSlotAvailable(Collider col)
        {
            int ind = slots.IndexOf(col);

            if (ind < 0)
            {
                Debug.Log("Invalid collider passed from: " + col.gameObject.name);
                return false;   //collider is not part of current board
            }

            if (slotsFree[ind])
                return true;

            return false;
        }

        public void SlotUsed(Collider col, int pieceInd)
        {
            int ind = slots.IndexOf(col);
            Debug.Log(ind.ToString() + " used");
            slotsFree[ind] = false;

            if (ind==pieceInd)
            {
                solved[ind] = true;
                Debug.Log("solved: " + ind);
            }
            else
            {
               // Debug.Log("P: "+pieceInd);
            }

            AudioManager.Instance.PlayClip(AudioManager.ClipType.JIGSAW_CLIP, GetComponent<AudioSource>());

            if(SolvedPieces()==9)
            {
                //doorController.OpenDoor();
                Debug.Log("Puzzle solved");
                msMgr.PuzzleSolved();
                boardSolvedOnce = true;
            }
            else
            {
                if (SolvedPieces() == 8 && !AnyslotFree())
                {
                    Debug.Log("Puzzle solved");
                    msMgr.PuzzleSolved();
                    boardSolvedOnce = true;
                }
                else
                    Debug.Log("Remaining: " + (9 - SolvedPieces()).ToString());
            }

            if (boardSolvedOnce && SolvedPieces()==7 && !AnyslotFree())
            {
                msMgr.BreakTheWall();
            }
        }

        bool AnyslotFree()
        {
            foreach (bool b in slotsFree)
                if (b) return true;
            return false;
        }

        public void ReleaseSlot(Collider col)
        {
            int ind = slots.IndexOf(col);
            Debug.Log(ind.ToString() + " released");
            slotsFree[ind] = true;
            solved[ind] = false;
        }
    }

}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

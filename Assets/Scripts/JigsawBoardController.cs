using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (!slotsFree[i])
                    targetPieces[i].GetComponent<JigsawPieceBehaviour>().connectedCollider = slots[i];
            }
            
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

            return res;
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

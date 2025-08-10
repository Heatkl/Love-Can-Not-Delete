using Codice.Client.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VNCreator
{
    public class DisplayBase : MonoBehaviour
    {
        public StoryObject story;

        [Header("Выбрать награду за задания")]
        public List<ChoicesScore> choices;
        public int sumScore = 0;

        protected NodeData currentNode;
        protected bool lastNode;

        protected List<string> loadList = new List<string>();

        private void OnValidate()
        {
            choices = new(); 

            choices.AddRange(
                story.nodes
                .Where(n => n.choices > 1)
                .Select(n => new ChoicesScore(n.choices, n.guid))
);
        }
        void Awake()
        {
            if (PlayerPrefs.GetString(GameSaveManager.currentLoadName) == string.Empty)
            {
                currentNode = story.GetFirstNode();
                loadList.Add(currentNode.guid);
            }
            else
            {
                loadList = GameSaveManager.Load();
                if(loadList == null || loadList.Count == 0)
                {
                    currentNode = story.GetFirstNode();
                    loadList = new List<string>();
                    loadList.Add(currentNode.guid);
                }
                else
                {
                    currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
                }
            }
            
        }

        protected virtual void NextNode(int _choiceId)
        {
            if (!lastNode) 
            {
                if(choices.Count(n => n.nodeId == currentNode.guid) > 0)
                {
                    int choiceScore = choices.First(n => n.nodeId == currentNode.guid).score[_choiceId];
                    sumScore += choiceScore;
                }
                currentNode = story.GetNextNode(currentNode.guid, _choiceId);
                lastNode = currentNode.endNode;
                loadList.Add(currentNode.guid);

                
            }
        }

        protected virtual void Previous()
        {
            loadList.RemoveAt(loadList.Count - 1);
            currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
            lastNode = currentNode.endNode;
        }

        protected void Save()
        {
            GameSaveManager.Save(loadList);
        }
    }

    [System.Serializable]
    public class ChoicesScore
    {
        [Header("Количество очков за каждый выбор")]
        public int[] score;
        public string nodeId;
        public ChoicesScore(int choicesQt, string _nodeId)
        {
            score = new int[choicesQt];
            nodeId = _nodeId;
        }
    }
}

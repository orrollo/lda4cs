using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Vocabulary
    {
        Dictionary<String, int> word2idMap;
        List<string> id2wordMap;

        public Vocabulary()
        {
            word2idMap = new Dictionary<string, int>();
            id2wordMap = new List<string>();
        }

        public int getId(String word)
        {
            return getId(word, false);
        }

        public String getWord(int id)
        {
            return id2wordMap[id];
        }

        public int getId(String word, bool create)
        {
            int id = word2idMap.ContainsKey(word) ? word2idMap[word] : -1;
            if (!create) return id;
            if (id == -1) id = id2wordMap.Count;
            word2idMap[word] = id;
            id2wordMap.Add(word);
            return id;
        }

        private void resize(int n)
        {
            //String[] nArray = new String[n];
            //System.arraycopy(id2wordMap, 0, nArray, 0, id2wordMap.length);
            //id2wordMap = nArray;
        }

        private void loseWeight()
        {
            //if (size() == id2wordMap.Count) return;
            //resize(word2idMap.size());
        }

        public int size()
        {
            return word2idMap.Count;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < id2wordMap.Count; i++)
            {
                sb.Append(i)
                    .Append("=")
                    .Append(id2wordMap[i])
                    .Append("\n");
            }
            return sb.ToString();
        }
    }

}

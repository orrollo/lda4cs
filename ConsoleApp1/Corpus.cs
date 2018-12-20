using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public class Corpus
    {
        List<int[]> documentList;
        Vocabulary vocabulary;

        public Corpus()
        {
            documentList = new List<int[]>();
            vocabulary = new Vocabulary();
        }

        public int[] addDocument(List<string> document)
        {
            int[] doc = new int[document.Count];
            int i = 0;
            foreach (var word in document) doc[i++] = vocabulary.getId(word, true);
            documentList.Add(doc);
            return doc;
        }

        //public int[][] ToArray()
        //{
        //    return documentList.ToArray(new int[0][]);
        //}

        public int getVocabularySize()
        {
            return vocabulary.size();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int[] doc in documentList) sb.Append(string.Join(",", doc)).Append("\n");
            sb.Append(vocabulary);
            return sb.ToString();
        }

        /**
         * Load documents from disk
         *
         * @param folderPath is a folder, which contains text documents.
         * @return a corpus
         * @throws IOException
         */
        public static Corpus load(string folderPath) //throws IOException
        {
            Corpus corpus = new Corpus();
            var folder = Directory.GetFiles(folderPath, "*.*");
            foreach (var file in folder)
            {
                using (var rdr = new StreamReader(file, Encoding.UTF8))
                {
                    string line;
                    List<string> wordList = new List<string>();
                    while (!rdr.EndOfStream)
                    {
                        line = rdr.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] words = line.Split(' ');
                        foreach (string word in words)
                        {
                            if (word.Trim().Length < 2) continue;
                            wordList.Add(word);
                        }
                    }
                    corpus.addDocument(wordList);
                }
            }
        if (corpus.getVocabularySize() == 0) return null;
        return corpus;
    }

    public Vocabulary getVocabulary()
{
    return vocabulary;
}

        //public int[][] getDocument()
        //{
        //    return toArray();
        //}

        public static int[] loadDocument(string path, Vocabulary vocabulary) //throws IOException
        {
            using (var rdr = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                List<int> wordList = new List<int>();
                while (!rdr.EndOfStream)
                {
                    line = rdr.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word.Trim().Length < 2) continue;
                        var wordId = vocabulary.getId(word);
                        wordList.Add(wordId);
                    }
                }
                return wordList.ToArray();
            }


            //        BufferedReader br = new BufferedReader(new FileReader(path));
            //        String line;
            //List<Integer> wordList = new LinkedList<Integer>();
            //        while ((line = br.readLine()) != null)
            //        {
            //            String[] words = line.split(" ");
            //            for (String word : words)
            //            {
            //                if (word.trim().length() < 2) continue;
            //                Integer id = vocabulary.getId(word);
            //                if (id != null)
            //                    wordList.add(id);
            //            }
            //        }
            //        br.close();
            //        int[] result = new int[wordList.size()];
            //int i = 0;
            //        for (Integer integer : wordList)
            //        {
            //            result[i++] = integer;
            //        }
            //        return result;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class LdaUtil
    {
        /**
         * To translate a LDA matrix to readable result
         * @param phi the LDA model
         * @param vocabulary
         * @param limit limit of max words in a topic
         * @return a map array
         */
        public static Dictionary<string, double>[] translate(double[][] phi, Vocabulary vocabulary, int limit)
        {
            limit = Math.Min(limit, phi[0].Length);
            var result = new Dictionary<string, double>[phi.Length];
            for (int k = 0; k < phi.Length; k++)
            {
                var rankMap = new Dictionary<string, double>();
                for (int i = 0; i < phi[k].Length; i++) rankMap.Add(vocabulary.getWord(i), phi[k][i]);
                var words = rankMap.Keys.ToList();
                words.Sort((a, b) => rankMap[b].CompareTo(rankMap[a]));
                result[k] = new Dictionary<string, double>();
                for (int i = 0; i < words.Count && i < limit; i++) result[k].Add(words[i], rankMap[words[i]]);
            }
            return result;
        }

        public static Dictionary<String, Double> translate(double[] tp, double[][] phi, Vocabulary vocabulary, int limit)
        {
            Dictionary<String, Double>[] topicMapArray = translate(phi, vocabulary, limit);
            double p = -1.0;
            int t = -1;
            for (int k = 0; k < tp.Length; k++)
            {
                if (tp[k] <= p) continue;
                p = tp[k];
                t = k;
            }
            return topicMapArray[t];
        }

        /**
         * To print the result in a well formatted form
         * @param result
         */
        public static void explain(Dictionary<String, Double>[] result)
        {
            int i = 0;
            foreach (Dictionary<String, Double> topicMap in result)
            {
                Console.WriteLine("topic {0}:", i++);
                explain(topicMap);
                Console.WriteLine();
            }
        }

        public static void explain(Dictionary<String, Double> topicMap)
        {
            foreach (var entry in topicMap)
            {
                Console.WriteLine("{0}={1}", entry.Key, entry.Value);
            }
        }
    }
}

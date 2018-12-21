using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Imports
    {

    }

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
                var rankMap = new Dictionary<Double, String>();
                for (int i = 0; i < phi[k].Length; i++)
                {
                    rankMap.Add(phi[k][i], vocabulary.getWord(i));
                }
                Iterator<Map.Entry<Double, String>> iterator = rankMap.entrySet().iterator();
                result[k] = new LinkedHashMap<String, Double>();
                for (int i = 0; i < limit; ++i)
                {
                    Map.Entry<Double, String> entry = iterator.next();
                    result[k].put(entry.getValue(), entry.getKey());
                }
            }
            return result;
        }

        public static Map<String, Double> translate(double[] tp, double[][] phi, Vocabulary vocabulary, int limit)
        {
            Map<String, Double>[] topicMapArray = translate(phi, vocabulary, limit);
            double p = -1.0;
            int t = -1;
            for (int k = 0; k < tp.length; k++)
            {
                if (tp[k] > p)
                {
                    p = tp[k];
                    t = k;
                }
            }
            return topicMapArray[t];
        }

        /**
         * To print the result in a well formatted form
         * @param result
         */
        public static void explain(Map<String, Double>[] result)
        {
            int i = 0;
            for (Map<String, Double> topicMap : result)
            {
                System.out.printf("topic %d :\n", i++);
                explain(topicMap);
                System.out.println();
            }
        }

        public static void explain(Map<String, Double> topicMap)
        {
            for (Map.Entry<String, Double> entry : topicMap.entrySet())
            {
                System.out.println(entry);
            }
        }
    }
}

using lab8;
using System;
using System.Globalization;
using System.Security.Principal;

namespace lab8
{
    abstract class Task
    {
        protected string _text = "empty";
        public override string ToString()
        {
            return _text;
        }
    }

    class Task8 : Task
    {
        protected List<String> _lines = new List<string>();

        public Task8(string text)
        {
            int textlength = text.Length;
            int currtextlength = 0;
            while(text != "")
            {
                int j = 0;
                string curr = "";
                int currlength = 0;
                if (text.Length >= 50)
                {
                    while (currlength + j < 50)
                    {
                        if (text[j] == ' ')
                        {
                            curr += text.Substring(0, j);
                            currlength += text.Substring(0, j).Length;
                            text = text.Substring(j);
                            j = 0;
                        }
                        j++;
                    }
                }
                else
                {
                    while (currlength + j < textlength - currtextlength)
                    {
                        if (text[j] == ' ' | currlength + j == textlength - currtextlength - 1)
                        {
                            curr += text.Substring(0, j + 1);
                            currlength += text.Substring(0, j + 1).Length;
                            text = text.Substring(j + 1);
                            j = 0;
                        }
                        j++;
                    }
                }

                currtextlength += curr.Length;

                if (curr[0] == ' ')
                    curr = curr.Substring(1);

                //filling spaces
                List<int> spacespos = new List<int>();
                for (int k = 0; k < curr.Length; k++)
                {
                    if (curr[k] == ' ')
                        spacespos.Add(k);
                }

                if (spacespos.Count > 0)
                {
                    int m = 0;
                    while (curr.Length < 50)
                    {
                        if (m >= spacespos.Count)
                            m = 0;
                        curr = curr.Insert(spacespos[m], " ");
                        for (int l = m; l < spacespos.Count; l++)
                            spacespos[l] = spacespos[l] + 1;
                        m++;
                    }
                }
                
                _lines.Add(curr);
            }
        }

        public void PrintArr()  //тут не переопределить ToString :(
        {
            for (int i = 0; i < _lines.Count; i++)
                Console.WriteLine(_lines[i]);
        }
    }

    class Task9 : Task
    {
        public List<string[]> code = new List<string[]>();
        public Task9(string text)
        {
            int num = 10000;
            int count = 0;
            char curr;
            for (int i = 0; i < text.Length - 1; i++)
            {
                if ((text[i] == text[i + 1]) && ((int)text[i] < 10000))
                {
                    curr = text[i];
                    for (int j = i; j < text.Length - 1; j++)
                    {
                        if ((text[j] == text[j + 1]) && (text[j] == curr))
                            text = text.Remove(j, 2).Insert(j, char.ConvertFromUtf32(num));
                    }
                    string[] temp = { char.ToString(curr), num.ToString() };
                    code.Add(temp);
                    count++;
                    num++;
                }
            }
            _text = text;
        }
    }

    class Task10 : Task
    {
        public Task10(string text, List<string[]> code)
        {
            Task9 task9 = new Task9(text);
            for (int i = 0; i < text.Length; i++)
            {
                if ((int)text[i] >= 10000)
                {
                    text = text.Remove(i, 1).Insert(i, $"{code[(int)text[i] - 10000][0]}{code[(int)text[i] - 10000][0]}");
                }
            }

            _text = text;
        }
    }

    class Task12 : Task
    {
        public Task12(string text, string[,] code)  //code - вида {{слово, код}, ...}
        {
            string symbols = "!?,'()=-+*/#:;. ";
            int i = 0;
            text += '.';
            while (i < text.Length)
            {
                int wordlength = 0;
                if (!symbols.Contains(text[i]))
                {
                    wordlength = 0;
                    string codeword = "";
                    int wordstarts = i;
                    while (!symbols.Contains(text[i]))
                    {
                        wordlength++;
                        codeword += text[i];
                        i++;
                    }
                    for (int k = 0; k < code.GetLength(0); k++)
                    {
                        if (code[k, 1] == codeword)
                            text = text.Remove(wordstarts, wordlength).Insert(wordstarts, $"{code[k, 0]}");
                    }
                }
                i = i - wordlength + 1;
            }
            _text = text.Remove(text.Length - 1, 1);
        }
    }

    class Task13 : Task
    {
        private List<char> _statLetters = new List<char>();
        private List<int> _statCount = new List<int>();
        private int _countWords = 0;
        public Task13(string text)
        {
            int textlength = text.Length;
            string symbols = "-–'?/.,;:!@#$%^&*()+_\"0123456789 ";
            List<string> words = new List<string>();
            while (text != "")
            {
                if (symbols.Contains(text[0]))
                    text = text.Substring(1);
                else
                {
                    string curr = "";
                    while (!symbols.Contains(text[0]))
                    {
                        curr += text[0];
                        text = text.Substring(1);
                    }
                    words.Add(curr);
                }
            }

            //getting statistics
            List<char> statLetters = new List<char>();
            List<int> statCount = new List<int>();
            for (int k = 0; k < words.Count; k++)
            {
                bool isHere = false;
                for (int m = 0; m < statLetters.Count; m++)
                {
                    if (statLetters[m] == words[k][0])
                    {
                        isHere = true;
                        statCount[m]++;
                        break;
                    }
                }

                if (!isHere)
                {
                    statLetters.Add(words[k][0]);
                    statCount.Add(1);
                }
            }

            _statCount = statCount;
            _statLetters = statLetters;
            _countWords = words.Count;
        }

        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < _statCount.Count; i++)
                text += $"{_statLetters[i]} - {_statCount[i] / (_countWords * 1.0) * 100}%\n";

            return text;
        }
    }

    class Task15 : Task
    {
        private double _sum = 0;
        public Task15(string text)
        {
            string symbols = "0123456789,-";
            int i = 0;
            while(i < text.Length)
            {
                string num = "";
                while (symbols.Contains(text[i]) & i != text.Length - 1)
                {
                    num += text[i];
                    i++;
                }
                if (num != "" & num != "-" & num != ",")
                    _sum += Convert.ToDouble(num);
                i++;
            }
        }

        public override string ToString()
        {
            return $"{_sum}";
        }
    }

    internal static class Program
    {
        static void Main()
        {
            string[] texts = new string[]
            {
                "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.",
                "Двигатель самолета – это сложная инженерная конструкция, обеспечивающая подъем, управление и движение в воздухе. Он состоит из множества компонентов, каждый из которых играет важную роль в общей работе механизма. Внутреннее устройство двигателя включает в себя компрессор, камеру сгорания, турбину и системы управления и охлаждения. Принцип работы основан на воздушно-топливной смеси, которая подвергается сжатию, воспламенению и расширению, обеспечивая движение воздушного судна.",
                "1 июля 2015 года Греция объявила о дефолте по государственному долгу, став первой развитой страной в истории, которая не смогла выплатить свои долговые обязательства в полном объеме. Сумма дефолта составила порядка 1,6 миллиарда евро. Этому предшествовали долгие переговоры с международными кредиторами, такими как Международный валютный фонд (МВФ), Европейский центральный банк (ЕЦБ) и Европейская комиссия (ЕК), о программах финансовой помощи и реструктуризации долга. Основными причинами дефолта стали недостаточная эффективность реформ, направленных на улучшение финансовой стабильности страны, а также политическая нестабильность, что вызвало потерю доверия со стороны международных инвесторов и кредиторов. Последствия дефолта оказались глубокими и долгосрочными: сокращение кредитного рейтинга страны, увеличение затрат на заемный капитал, рост стоимости заимствований и утрата доверия со стороны международных инвесторов.",
                "Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.",
                "William Shakespeare, widely regarded as one of the greatest writers in the English language, authored a total of 37 plays, along with numerous poems and sonnets. He was born in Stratford-upon-Avon, England, in 1564, and died in 1616. Shakespeare's most famous works, including \"Romeo and Juliet,\" \"Hamlet,\" \"Macbeth,\" and \"Othello,\" were written during the late 16th and early 17th centuries. \"Romeo and Juliet,\" a tragic tale of young love, was penned around 1595. \"Hamlet,\" one of his most celebrated tragedies, was written in the early 1600s, followed by \"Macbeth,\" a gripping drama exploring themes of ambition and power, around 1606. \"Othello,\" a tragedy revolving around jealousy and deceit, was also composed during this period, believed to be around 1603.",
                "Первое кругосветное путешествие было осуществлено флотом, возглавляемым португальским исследователем Фернаном Магелланом. Путешествие началось -20 сентября 15,19 года, когда флот, состоящий из пяти кораблей и примерно 270 человек, отправился из порту Сан-Лукас в Испании. Хотя Магеллан не закончил свое путешествие из-за гибели в битве на Филиппинах в 1521 году, его экспедиция стала первой, которая успешно обогнула Землю и доказала ее круглую форму. Это путешествие открыло новые морские пути и имело огромное значение для картографии и географических открытий."
            };

            //Task8 a8 = new Task8(texts[5]);
            //a8.PrintArr();

            //Task9 a9 = new Task9(texts[5]);
            //Console.WriteLine(a9.ToString());

            //Task10 a10 = new Task10(a9.ToString(), a9.code);
            //Console.WriteLine(a10.ToString());

            //string[,] code = { { "1", "ОДИН" }, { "2", "ДВА" } };
            //Task12 a12 = new Task12("ОДИН ДВА ТРИ ОДИН ОДИН ДВА ТРИ", code);
            //Console.WriteLine(a12.ToString());

            //Task13 a13 = new Task13(texts[5]);
            //Console.WriteLine(a13.ToString());

            Task15 a15 = new Task15(texts[5]);
            Console.WriteLine(a15.ToString());
        }
    }
}
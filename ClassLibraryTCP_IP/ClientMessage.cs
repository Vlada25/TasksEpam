using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibraryTCP_IP
{
    class ClientMessage
    {
        public static string TranscodingMessage(string initialMessage)
        {

            StringBuilder transcodedMessage = new StringBuilder();

            const int AlphabetLen = 34;

            string[] rusAlphabet = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й",
                            "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц",
                            "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я", " " };

            string[] engAlphabet = { "A", "B", "V", "G", "D", "E", "E", "ZH", "Z", "I", "Y",
                            "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "KH", "TS",
                            "CH", "SH", "S4", null, "Y", null, "E", "YU", "YA", " "};


            if (Regex.IsMatch(initialMessage, @"[а-яА-Я]"))
            {
                for (int j = 0; j < initialMessage.Length; j++)
                {
                    for (int i = 0; i < AlphabetLen; i++)
                    {
                        if (initialMessage.Substring(j, 1) == rusAlphabet[i])
                        {
                            transcodedMessage.Append(engAlphabet[i]);
                        }   
                    }
                }
                    
            }
            else
            {
                for (int j = 0; j < initialMessage.Length; j++)
                {
                    for (int i = 0; i < AlphabetLen; i++)
                    {
                        if (initialMessage.Substring(j, 1) == engAlphabet[i])
                        {
                            string test = null;

                            if (j != initialMessage.Length - 1)
                            {
                                test = initialMessage.Substring(j, 1) + initialMessage.Substring(j + 1, 1);
                            }

                            switch (test)
                            {
                                case "ZH":
                                    transcodedMessage.Append("Ж");
                                    break;
                                case "KH":
                                    transcodedMessage.Append("X");
                                    break;
                                case "TS":
                                    transcodedMessage.Append("Ц");
                                    break;
                                case "YA":
                                    transcodedMessage.Append("Я");
                                    break;
                                case "CH":
                                    transcodedMessage.Append("Ч");
                                    break;
                                case "SH":
                                    transcodedMessage.Append("Ш");
                                    break;
                                case "YU":
                                    transcodedMessage.Append("Ю");
                                    break;
                                case "S4":
                                    transcodedMessage.Append("Щ");
                                    break;
                                default:
                                    transcodedMessage.Append(rusAlphabet[i]);
                                    break;
                            }
                        }
                    }  
                }
            }

            return transcodedMessage.ToString();
        }
    }
}

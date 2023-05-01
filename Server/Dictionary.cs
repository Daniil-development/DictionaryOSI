using System;
using System.IO;

namespace Server
{
    // Класс для работы со словарём
    class Dictionary
    {
        // Получаемые данные выглядят так: "0\nw1/w2/w3\n0"
        // где первое число: 0 - поиск по словам, 1 - вернуть весь словарь
        // w1, w2 .. wn - слова для поиска
        // последнее число: 0 - искать в термине, 1 - искать в определении.

        public static string FindWordsInDictionary(string data, string filePath)
        {
            try
            {
                if (data.Length < 4)
                {
                    return null;
                }

                if (data[0] == '0')
                {
                    data = data.Remove(0, 2);

                    if (data.Length < 3 || (data[data.Length - 1] != '0' && data[data.Length - 1] != '1'))
                        return null;

                    int searchInDescription = data[data.Length - 1];

                    data = data.Remove(data.Length - 2, 2);

                    string[] words = data.Split('/');

                    string fileText = File.ReadAllText(filePath);

                    if (fileText.Length == 0)
                        return null;
                    
                    string[] terms = fileText.Split('~');

                    string searchedTerms = "";

                    for (int i = 0; i < terms.Length; i++)
                    {
                        string term = terms[i].Remove(0, 2);
                        term = term.Remove(term.Length - 1, 1);

                        if (searchInDescription == '0')
                        {
                            term = term.Substring(0, term.IndexOf('\n'));
                            term = term.ToLower();

                            foreach (string word in words)
                            {
                                                                
                                if (term.Contains(word.ToLower()))
                                {
                                    if (searchedTerms.Length != 0)
                                        searchedTerms += '~';
                                    searchedTerms += (terms[i]);

                                    break;
                                }                            
                            }
                        }
                        else if (searchInDescription == '1')
                        {
                            term = term.Substring(term.IndexOf('\n') + 1, term.Length - 1 - term.IndexOf('\n'));
                            term = term.ToLower();

                            foreach (string word in words)
                            {
                                if (term.Contains(word.ToLower()))
                                {
                                    if (searchedTerms.Length != 0)
                                        searchedTerms += '~';
                                    searchedTerms += (terms[i]);

                                    break;
                                }
                            }
                        }
                    }

                    return searchedTerms;
                }

                else if (data[0] == '1')
                {
                    return File.ReadAllText(filePath);
                }

                else
                    return null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

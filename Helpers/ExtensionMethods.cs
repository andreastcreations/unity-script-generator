using System;
using System.Linq;

namespace ATMedia.CustomTools.ScriptGeneration
{
    /// <summary>
    /// Some extension methods for string manipulation.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Used to transform a variable name to a property name. Also ignores all other symbols.
        /// </summary>
        /// <remarks>
        /// Usually variables start with a lowercase letter or an underscore and properties start with an uppercase.
        /// </remarks>
        /// <param name="str">The <see cref="string"/> we want to manipulate.</param>
        /// <returns>A <see cref="string"/> that its first letter is uppercase.</returns>
        public static string ChangeFirstLetterToCaps(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsLetter(str[i]))
                    {
                        char[] newCharArray = new char[str.Length - i];

                        for (int j = 0; j < str.Length - i; j++)
                        {
                            if (j == 0)
                            {
                                newCharArray[j] = char.ToUpper(str[i + j]);
                            }
                            else
                            {
                                newCharArray[j] = str[i + j];
                            }
                        }

                        return new string(newCharArray);
                    }
                }
                return str;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// Retrieves the text from a TextArea field in order to add line breaks.
        /// </summary>
        /// <param name="textArea">The <see cref="string"/> inside the TextArea field.</param>
        /// <returns>A <see cref="string"/> array. Each index holds a different line.</returns>
        public static string[] GetTextAreaFieldWithLineBreaks(string textArea)
        {
            string[] lines = textArea.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            return lines;
        }

        /// <summary>
        /// Removes the white space in a <see cref="string"/>.
        /// </summary>
        /// <param name="text">The <see cref="string"/> whose white space is removed.</param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(this string text)
        {
            return String.Concat(text.Where(c => !Char.IsWhiteSpace(c)));
        }
    }
}

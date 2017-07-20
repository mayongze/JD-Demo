using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JDDemo.Helper
{
    public class MUJson
    {
        protected static void eatWhitespace(char[] json, ref int index)
        {
            while (index < json.Length)
            {
                if (" \t\n\r".IndexOf(json[index]) == -1)
                {
                    return;
                }
                index++;
            }
        }

        public static int getLastErrorIndex()
        {
            return MUJson.lastErrorIndex;
        }

        public static string getLastErrorSnippet()
        {
            if (MUJson.lastErrorIndex == -1)
            {
                return "";
            }
            int num = MUJson.lastErrorIndex - 5;
            int num2 = MUJson.lastErrorIndex + 15;
            if (num < 0)
            {
                num = 0;
            }
            if (num2 >= MUJson.lastDecode.Length)
            {
                num2 = MUJson.lastDecode.Length - 1;
            }
            return MUJson.lastDecode.Substring(num, num2 - num + 1);
        }

        protected static int getLastIndexOfNumber(char[] json, int index)
        {
            int num = index;
            while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
            {
                num++;
            }
            return num - 1;
        }

        // Token: 0x06000160 RID: 352 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
        public static object jsonDecode(string json)
        {
            MUJson.lastDecode = json;
            if (json != null)
            {
                char[] json2 = json.ToCharArray();
                int num = 0;
                bool flag = true;
                object result = MUJson.parseValue(json2, ref num, ref flag);
                if (flag)
                {
                    MUJson.lastErrorIndex = -1;
                }
                else
                {
                    MUJson.lastErrorIndex = num;
                }
                return result;
            }
            return null;
        }

        // Token: 0x06000161 RID: 353 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
        public static string jsonEncode(object json)
        {
            StringBuilder stringBuilder = new StringBuilder(2000);
            if (!MUJson.serializeValue(json, stringBuilder))
            {
                return null;
            }
            return stringBuilder.ToString();
        }

        // Token: 0x06000162 RID: 354 RVA: 0x0000F41F File Offset: 0x0000D61F
        public static bool lastDecodeSuccessful()
        {
            return MUJson.lastErrorIndex == -1;
        }

        // Token: 0x0600016C RID: 364 RVA: 0x0000F854 File Offset: 0x0000DA54
        protected static int lookAhead(char[] json, int index)
        {
            int num = index;
            return MUJson.nextToken(json, ref num);
        }

        // Token: 0x0600016D RID: 365 RVA: 0x0000F86C File Offset: 0x0000DA6C
        protected static int nextToken(char[] json, ref int index)
        {
            MUJson.eatWhitespace(json, ref index);
            if (index == json.Length)
            {
                return 0;
            }
            char c = json[index];
            index++;
            char c2 = c;
            switch (c2)
            {
                case '"':
                    return 7;
                case '#':
                case '$':
                case '%':
                case '&':
                case '\'':
                case '(':
                case ')':
                case '*':
                case '+':
                case '.':
                case '/':
                    break;
                case ',':
                    return 6;
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return 8;
                case ':':
                    return 5;
                default:
                    switch (c2)
                    {
                        case '[':
                            return 3;
                        case '\\':
                            break;
                        case ']':
                            return 4;
                        default:
                            switch (c2)
                            {
                                case '{':
                                    return 1;
                                case '}':
                                    return 2;
                            }
                            break;
                    }
                    break;
            }
            index--;
            int num = json.Length - index;
            if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
            {
                index += 5;
                return 10;
            }
            if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
            {
                index += 4;
                return 9;
            }
            if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
            {
                index += 4;
                return 11;
            }
            return 0;
        }

        // Token: 0x06000166 RID: 358 RVA: 0x0000F514 File Offset: 0x0000D714
        protected static ArrayList parseArray(char[] json, ref int index)
        {
            ArrayList arrayList = new ArrayList();
            MUJson.nextToken(json, ref index);
            bool flag = false;
            while (!flag)
            {
                int num = MUJson.lookAhead(json, index);
                if (num == 0)
                {
                    return null;
                }
                if (num == 6)
                {
                    MUJson.nextToken(json, ref index);
                }
                else
                {
                    if (num == 4)
                    {
                        MUJson.nextToken(json, ref index);
                        break;
                    }
                    bool flag2 = true;
                    object value = MUJson.parseValue(json, ref index, ref flag2);
                    if (!flag2)
                    {
                        return null;
                    }
                    arrayList.Add(value);
                }
            }
            return arrayList;
        }

        protected static double parseNumber(char[] json, ref int index)
        {
            MUJson.eatWhitespace(json, ref index);
            int lastIndexOfNumber = MUJson.getLastIndexOfNumber(json, index);
            int num = lastIndexOfNumber - index + 1;
            char[] array = new char[num];
            Array.Copy(json, index, array, 0, num);
            index = lastIndexOfNumber + 1;
            return double.Parse(new string(array));
        }

        protected static Hashtable parseObject(char[] json, ref int index)
        {
            Hashtable hashtable = new Hashtable();
            MUJson.nextToken(json, ref index);
            bool flag = false;
            while (!flag)
            {
                int num = MUJson.lookAhead(json, index);
                if (num == 0)
                {
                    return null;
                }
                if (num == 6)
                {
                    MUJson.nextToken(json, ref index);
                }
                else
                {
                    if (num == 2)
                    {
                        MUJson.nextToken(json, ref index);
                        return hashtable;
                    }
                    string text = MUJson.parseString(json, ref index);
                    if (text == null)
                    {
                        return null;
                    }
                    num = MUJson.nextToken(json, ref index);
                    if (num != 5)
                    {
                        return null;
                    }
                    bool flag2 = true;
                    object value = MUJson.parseValue(json, ref index, ref flag2);
                    if (!flag2)
                    {
                        return null;
                    }
                    hashtable[text] = value;
                }
            }
            return hashtable;
        }

        protected static string parseString(char[] json, ref int index)
        {
            string text = "";
            MUJson.eatWhitespace(json, ref index);
            char c = json[index++];
            bool flag = false;
            while (!flag && index != json.Length)
            {
                c = json[index++];
                if (c == '"')
                {
                    flag = true;
                    break;
                }
                if (c == '\\')
                {
                    if (index == json.Length)
                    {
                        break;
                    }
                    c = json[index++];
                    if (c == '"')
                    {
                        text += '"';
                    }
                    else if (c == '\\')
                    {
                        text += '\\';
                    }
                    else if (c == '/')
                    {
                        text += '/';
                    }
                    else if (c == 'b')
                    {
                        text += '\b';
                    }
                    else if (c == 'f')
                    {
                        text += '\f';
                    }
                    else if (c == 'n')
                    {
                        text += '\n';
                    }
                    else if (c == 'r')
                    {
                        text += '\r';
                    }
                    else if (c == 't')
                    {
                        text += '\t';
                    }
                    else if (c == 'u')
                    {
                        int num = json.Length - index;
                        if (num < 4)
                        {
                            break;
                        }
                        char[] array = new char[4];
                        Array.Copy(json, index, array, 0, 4);
                        text = text + "&#x" + new string(array) + ";";
                        index += 4;
                    }
                }
                else
                {
                    text += c;
                }
            }
            if (!flag)
            {
                return null;
            }
            return text;
        }

        protected static object parseValue(char[] json, ref int index, ref bool success)
        {
            switch (MUJson.lookAhead(json, index))
            {
                case 1:
                    return MUJson.parseObject(json, ref index);
                case 3:
                    return MUJson.parseArray(json, ref index);
                case 7:
                    return MUJson.parseString(json, ref index);
                case 8:
                    return MUJson.parseNumber(json, ref index);
                case 9:
                    MUJson.nextToken(json, ref index);
                    return bool.Parse("TRUE");
                case 10:
                    MUJson.nextToken(json, ref index);
                    return bool.Parse("FALSE");
                case 11:
                    MUJson.nextToken(json, ref index);
                    return null;
            }
            success = false;
            return null;
        }

        protected static bool serializeArray(ArrayList anArray, StringBuilder builder)
        {
            builder.Append("[");
            bool flag = true;
            for (int i = 0; i < anArray.Count; i++)
            {
                object value = anArray[i];
                if (!flag)
                {
                    builder.Append(", ");
                }
                if (!MUJson.serializeValue(value, builder))
                {
                    return false;
                }
                flag = false;
            }
            builder.Append("]");
            return true;
        }

        protected static bool serializeDictionary(Dictionary<string, string> dict, StringBuilder builder)
        {
            builder.Append("{");
            bool flag = true;
            foreach (KeyValuePair<string, string> current in dict)
            {
                if (!flag)
                {
                    builder.Append(", ");
                }
                MUJson.serializeString(current.Key, builder);
                builder.Append(":");
                MUJson.serializeString(current.Value, builder);
                flag = false;
            }
            builder.Append("}");
            return true;
        }


        protected static void serializeNumber(double number, StringBuilder builder)
        {
            builder.Append(Convert.ToString(number));
        }


        protected static bool serializeObject(Hashtable anObject, StringBuilder builder)
        {
            builder.Append("{");
            IDictionaryEnumerator enumerator = anObject.GetEnumerator();
            bool flag = true;
            while (enumerator.MoveNext())
            {
                string aString = enumerator.Key.ToString();
                object value = enumerator.Value;
                if (!flag)
                {
                    builder.Append(", ");
                }
                MUJson.serializeString(aString, builder);
                builder.Append(":");
                if (!MUJson.serializeValue(value, builder))
                {
                    return false;
                }
                flag = false;
            }
            builder.Append("}");
            return true;
        }


        protected static bool serializeObjectOrArray(object objectOrArray, StringBuilder builder)
        {
            if (objectOrArray is Hashtable)
            {
                return MUJson.serializeObject((Hashtable)objectOrArray, builder);
            }
            return objectOrArray is ArrayList && MUJson.serializeArray((ArrayList)objectOrArray, builder);
        }

        protected static void serializeString(string aString, StringBuilder builder)
        {
            builder.Append("\"");
            char[] array = aString.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                char c = array[i];
                if (c == '"')
                {
                    builder.Append("\\\"");
                }
                else if (c == '\\')
                {
                    builder.Append("\\\\");
                }
                else if (c == '\b')
                {
                    builder.Append("\\b");
                }
                else if (c == '\f')
                {
                    builder.Append("\\f");
                }
                else if (c == '\n')
                {
                    builder.Append("\\n");
                }
                else if (c == '\r')
                {
                    builder.Append("\\r");
                }
                else if (c == '\t')
                {
                    builder.Append("\\t");
                }
                else
                {
                    int num = Convert.ToInt32(c);
                    if (num >= 32 && num <= 126)
                    {
                        builder.Append(c);
                    }
                    else
                    {
                        builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
                    }
                }
            }
            builder.Append("\"");
        }

        protected static bool serializeValue(object value, StringBuilder builder)
        {
            if (value == null)
            {
                builder.Append("null");
            }
            else if (value.GetType().IsArray)
            {
                MUJson.serializeArray(new ArrayList((ICollection)value), builder);
            }
            else if (value is string)
            {
                MUJson.serializeString((string)value, builder);
            }
            else if (value is char)
            {
                MUJson.serializeString(Convert.ToString((char)value), builder);
            }
            else if (value is Hashtable)
            {
                MUJson.serializeObject((Hashtable)value, builder);
            }
            else if (value is Dictionary<string, string>)
            {
                MUJson.serializeDictionary((Dictionary<string, string>)value, builder);
            }
            else if (value is ArrayList)
            {
                MUJson.serializeArray((ArrayList)value, builder);
            }
            else if (value is bool && (bool)value)
            {
                builder.Append("true");
            }
            else if (value is bool && !(bool)value)
            {
                builder.Append("false");
            }
            else
            {
                if (!value.GetType().IsPrimitive)
                {
                    return false;
                }
                MUJson.serializeNumber(Convert.ToDouble(value), builder);
            }
            return true;
        }
        private const int BUILDER_CAPACITY = 2000;

        protected static string lastDecode = "";

        protected static int lastErrorIndex = -1;

        private const int TOKEN_COLON = 5;

        private const int TOKEN_COMMA = 6;

        private const int TOKEN_CURLY_CLOSE = 2;

        private const int TOKEN_CURLY_OPEN = 1;

        private const int TOKEN_FALSE = 10;

        private const int TOKEN_NONE = 0;

        private const int TOKEN_NULL = 11;

        private const int TOKEN_NUMBER = 8;

        private const int TOKEN_SQUARED_CLOSE = 4;

        private const int TOKEN_SQUARED_OPEN = 3;

        private const int TOKEN_STRING = 7;

        private const int TOKEN_TRUE = 9;
    }
}
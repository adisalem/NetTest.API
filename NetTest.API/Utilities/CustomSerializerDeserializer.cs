using NetTest.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetTest.API.Utilities
{
    public class CustomSerializerDeserializer
    {
        public static string Serialize<T>(T entity, StringBuilder sb)
        {
            sb.Append("{");
            var getType = entity.GetType();
            IList<PropertyInfo> propInfos = new List<PropertyInfo>(getType.GetProperties());
            foreach (var prop in propInfos)
            {
                var value = prop.GetValue(entity, null);

                if (prop.Name.Equals("Address"))
                {
                    sb.Append(prop.Name + ":");
                    return Serialize(value, sb) + "}";

                }
                else
                {
                    if(value.GetType()==typeof(long))
                    {
                       sb.Append(prop.Name + ":" + value + ",");
                    }
                    else
                    {
                        sb.Append(prop.Name + ":" + "'" + value + "'" + ",");

                    }
                }
            }
            sb.Append("" +
                "}");
            string result = sb.ToString();
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (result[i].Equals(','))
                {
                    result = result.Substring(0, i);
                    result = result + "}";
                    break;
                }
            }

            return result;
        }
        public static List<string> Extract(string data, string startString = "{", string endString = "}")
        {
            if (data.Contains("\n"))
                data = data.Replace("\n", string.Empty);

            if (data.Contains("\t"))
                data = data.Replace("\t", string.Empty);

            var result = new List<string>();
            var stop = false;
            while (!stop)
            {
                var indexStart = data.IndexOf(startString, StringComparison.Ordinal);
                var indexEnd = data.LastIndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    result.Add(data.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));
                    data = data.Substring(indexEnd + endString.Length);
                }
                else
                {
                    stop = true;
                }
            }
            return result;
        }

        public static List<Data> GetDataValue(string data)
        {
            var listOfValues = new List<Data>();
            data = String.Concat(data.Where(c => !Char.IsWhiteSpace(c)));
            var index = data.IndexOf(":{");
            if (index != -1)
            {

                int range = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == ',')
                    {
                        int CommaIndex = index - i;
                        if (CommaIndex > 0)
                            range = i;
                        else
                            break;
                    }
                }
                string data1 = data.Substring(0, range);
                string data2 = data.Remove(0, range + 1);
                string[] arr = new string[2];
                arr[0] = data1;
                arr[1] = data2;
                var string0 = arr[0].Split(",");

                var string1 = Extract(arr[1], "{", "}");
                var string10 = string1[0].Split(',');

                foreach (var item in string0)
                {

                    var pName = item.Substring(0, item.IndexOf(":", StringComparison.Ordinal));
                    var pValue = item.Substring(item.IndexOf(":", StringComparison.Ordinal) + 1);
                    long num;
                    bool isPvalueDigit = Int64.TryParse(pValue,out num);
                    if(!isPvalueDigit)
                    {
                        if (pValue.StartsWith('\'') && pValue.EndsWith('\''))
                        {
                            pValue = pValue.Replace("\'", "");
                        }
                        else
                        {
                            listOfValues.Add(new Data());
                            return listOfValues;
                        }
                    }
                    listOfValues.Add(new Data { PropertyName = pName, Value = pValue });


                }
                foreach (var item in string10)
                {
                    if (item.Contains(":"))
                    {
                        var pName = item.Substring(0, item.IndexOf(":", StringComparison.Ordinal)).ToLower();
                        var pValue = item.Substring(item.IndexOf(":", StringComparison.Ordinal) + 1).ToLower();
                        long num;
                        bool isPvalueDigit = Int64.TryParse(pValue, out num);
                        if (!isPvalueDigit)
                        {
                            if (pValue.StartsWith('\'') && pValue.EndsWith('\''))
                            {
                                pValue = pValue.Replace("\'", "");
                            }
                            else
                            {
                                listOfValues.Add(new Data());
                                return listOfValues;
                            }
                        }
                        listOfValues.Add(new Data { PropertyName = pName, Value = pValue });

                    }
                }
            }

            return listOfValues;
        }
        public static T DeSerialize<T>(string jsonData, T target) where T : new()
        {
            var deserializedObjects = Extract(jsonData);
            try
            {
                foreach (var ent in deserializedObjects)
                {
                    var properties = GetDataValue(ent);
                    foreach (var prop in properties)
                    {

                        var propInfo = target.GetType().GetProperty(prop.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        propInfo?.SetValue(target,
                        Convert.ChangeType(prop.Value, propInfo.PropertyType), null);
                    }
                }
                return target;

            }
            catch (Exception e)
            {
                return (T)(object) new SavePerson();
            }
            
        }
      
        public class Data
        {
            public string PropertyName;
            public string Value;
        }
     
    }
}

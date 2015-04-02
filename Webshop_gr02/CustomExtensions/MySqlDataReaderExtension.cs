using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace CustomExtensions
{
    //Extension methods must be defined in a static class
    public static class MySqlDataReaderExtension
    {
        public static string SafeGetString(this MySqlDataReader reader, String colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                {
                    return reader.GetString(colIndex);
                }
                else
                {
                    return string.Empty;
                }

        }
        public static bool SafeGetBoolean(this MySqlDataReader reader, String colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
            {
                return reader.GetBoolean(colIndex);
            }
            else
            {
                return false;
            }
        }
        public static int SafeGetInt32(this MySqlDataReader reader, String colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
            {
                return reader.GetInt32(colIndex);
            }
            else
            {
                return 0;
            }
        }
        public static short SafeGetInt16(this MySqlDataReader reader, String colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
            {
                return reader.GetInt16(colIndex);
            }
            else
            {
                return 0;
            }
        }
        public static float SafeGetFloat(this MySqlDataReader reader, String colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
            {
                return reader.GetFloat(colIndex);
            }
            else
            {
                return 0;
            }
        }
    }
}
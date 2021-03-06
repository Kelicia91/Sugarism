﻿using System;
using System.Windows; // MessageBox


namespace CharacterEditor
{
    class Log
    {
        private enum Group
        {
            DEBUG,
            ERROR
        }

        private static string DEFAULT_TITLE = string.Empty;
        

        public static void Debug(string format, params object[] args)
        {
            Debug(DEFAULT_TITLE, format, args);
        }

        public static void Debug(string title, string format, params object[] args)
        {
            string content = String.Format(format, args);
            
            print(Group.DEBUG, title, content);
        }


        public static void Error(string format, params object[] args)
        {
            Error(DEFAULT_TITLE, format, args);
        }

        public static void Error(string title, string format, params object[] args)
        {
            string content = String.Format(format, args);

            print(Group.ERROR, title, content);
        }


        private static void print(Group g, string title, string content)
        {
            // @note : wpf not to support console...
            //Console.WriteLine("[{0}][{1}] {2}", g.ToString(), title, content);

            MessageBox.Show(content, title);
        }
    }
}

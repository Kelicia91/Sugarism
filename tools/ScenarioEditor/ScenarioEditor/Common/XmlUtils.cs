using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace ScenarioEditor
{
    class XmlUtils
    {
        public const string FILE_EXTENSION = ".xml";
        public const string FILE_FILTER = "XML TEXT(.xml)|*.xml";

        /// <summary>
        /// Parse Model.ArtsResources[] from xml file.
        /// only just set XElement named Model.ArtsResource.XML_COLUMN_NAME 
        /// into property Model.ArtsResource.Description.
        /// </summary>
        /// <param name="filePath">xml file path</param>
        /// <param name="result">Model.ArtsResources[] set Description</param>
        /// <param name="objectName">XElement name to find (refer to DataTable in Game)</param>
        /// <returns>If result isn't null return true, otherwise false.</returns>
        public static bool Parse(string filePath, out Model.ArtsResource[] result, string objectName)
        {
            result = null;

            XDocument doc = XDocument.Load(filePath);
            if (null == doc)
            {
                Log.Error("XDocument is null");
                return false;
            }

            XElement root = doc.Root;
            if (null == root)
            {
                Log.Error("XDocument.Root is null");
                return false;
            }

            IEnumerable<XElement> objEnumerable = root.Elements(objectName);
            if (null == objEnumerable)
            {
                Log.Error("not found XElement '{0}'", objectName);
                return false;
            }

            IEnumerable<Model.ArtsResource> resultEnumerable = null;
            try
            {
                resultEnumerable = objEnumerable.Select(
                x => new Model.ArtsResource
                {
                    Description = x.Element(Model.ArtsResource.XML_COLUMN_NAME).Value
                });
            }
            catch(Exception e)
            {
                Log.Error(Properties.Resources.ErrDeserialize, e.Message);
                return false;
            }

            result = resultEnumerable.ToArray();

            if (null == result)
                return false; 
            else
                return true;
        }

        /// <summary>
        /// Parse Model.Character[] from xml file.
        /// only just set XElement named Model.Character.XML_COLUMN_NAME 
        /// into property Model.Character.Name.
        /// </summary>
        /// <param name="filePath">xml file path</param>
        /// <param name="result">Model.Character[] set Name</param>
        /// <returns>If result isn't null return true, otherwise false.</returns>
        public static bool Parse(string filePath, out Model.Character[] result)
        {
            result = null;

            XDocument doc = XDocument.Load(filePath);
            if (null == doc)
            {
                Log.Error("XDocument is null");
                return false;
            }

            XElement root = doc.Root;
            if (null == root)
            {
                Log.Error("XDocument.Root is null");
                return false;
            }

            const string XML_OBJECT_NAME = Model.Character.XML_CHARACTER_NAME;
            IEnumerable<XElement> objEnumerable = root.Elements(XML_OBJECT_NAME);
            if (null == objEnumerable)
            {
                Log.Error("not found XElement '{0}'", XML_OBJECT_NAME);
                return false;
            }

            IEnumerable<Model.Character> resultEnumerable = null;
            try
            {
                resultEnumerable = objEnumerable.Select(
                x => new Model.Character
                {
                    Name = x.Element(Model.Character.XML_COLUMN_NAME).Value
                });
            }
            catch (Exception e)
            {
                Log.Error(Properties.Resources.ErrDeserialize, e.Message);
                return false;
            }

            result = resultEnumerable.ToArray();

            if (null == result)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Parse Model.Target[] from xml file.
        /// only just set XElement named Model.Target.XML_COLUMN_NAME 
        /// into property Model.Target.CharacterId.
        /// </summary>
        /// <param name="filePath">xml file path</param>
        /// <param name="result">Model.Target[] set CharacterId</param>
        /// <returns>If result isn't null return true, otherwise false.</returns>
        public static bool Parse(string filePath, out Model.Target[] result)
        {
            result = null;

            XDocument doc = XDocument.Load(filePath);
            if (null == doc)
            {
                Log.Error("XDocument is null");
                return false;
            }

            XElement root = doc.Root;
            if (null == root)
            {
                Log.Error("XDocument.Root is null");
                return false;
            }

            const string XML_OBJECT_NAME = Model.Target.XML_TARGET_NAME;
            IEnumerable<XElement> objEnumerable = root.Elements(XML_OBJECT_NAME);
            if (null == objEnumerable)
            {
                Log.Error("not found XElement '{0}'", XML_OBJECT_NAME);
                return false;
            }

            IEnumerable<Model.Target> resultEnumerable = null;
            try
            {
                resultEnumerable = objEnumerable.Select(
                x => new Model.Target
                {
                    CharacterId = Convert.ToInt32(x.Element(Model.Target.XML_COLUMN_NAME).Value)
                });
            }
            catch (Exception e)
            {
                Log.Error(Properties.Resources.ErrDeserialize, e.Message);
                return false;
            }

            result = resultEnumerable.ToArray();

            if (null == result)
                return false;
            else
                return true;
        }

    }   // class
}

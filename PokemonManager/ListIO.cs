using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace PokemonManager
{
    public class ListIO<Type>
    {
        private string name;
        private string dir;
        private XmlSerializer s;

        public ListIO(string dir, string name)
        {
            this.dir = dir;
            this.name = ".\\" + dir + "\\" + name + ".xml";
            this.s = new XmlSerializer(typeof(Type));

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(".\\" + dir);
            }
        }
        
        /// <summary>
        /// ItemList を XML に出力する。
        /// </summary>
        /// <param name="list">アイテムリスト</param>
        public void Write(Type list)
        {
            using (FileStream file = new FileStream(name, FileMode.Create))
            {
                s.Serialize(file, list);
            }
        }

        /// <summary>
        /// XML から ItemList を読み込む。
        /// </summary>
        public Type Read()
        {
            Type list = default(Type);
            using (FileStream file = new FileStream(name, FileMode.Open))
            {
                list = (Type)s.Deserialize(file);
            }
            return list;
        }

		public bool exist()
		{
			return File.Exists(name);
		}
    }
}

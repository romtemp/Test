using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    class MediaInfo
    {
        
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public override string ToString()
        {
            return String.Format("Name {0}", Name);
        }
        public void Add(string name)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365Launcher.TaskTrayApp.Model
{
    public class BookmarkInfo
    {
        public BookmarkInfo()
        {
        }
        public BookmarkInfo(string groupName)
        {
            this.Id = Guid.NewGuid();
            this.GroupName = groupName;
        }
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public List<BkmKeyValuePair<string, string>> Links { get; set; }
    }


    [Serializable]
    public struct BkmKeyValuePair<K, V>
    {
        public BkmKeyValuePair(K k, V v) : this() { Key = k; Value = v; }
        public K Key
        { get; set; }

        public V Value
        { get; set; }
    }
}

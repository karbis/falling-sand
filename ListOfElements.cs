using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falling_sand {
    public class ListOfElements {
        private static List<Element> list = [];
        public static List<Element> List {
            get {
                if (list.Count != 0) {
                    return list;
                }

                foreach (Type type in typeof(Element).Assembly.GetTypes()) {
                    if (!type.IsSubclassOf(typeof(Element))) continue;
                    object? element = Activator.CreateInstance(type);
                    if (element == null) continue;
                    list.Add((Element)element);
                }
                list = list.OrderBy((Element x) => x.SelectionBarOrder).ToList();

                return list;
            }
        }
    }
}

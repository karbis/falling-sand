using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using falling_sand.Elements;

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
        //public static Element[] List = [new Sand(), new Stone(), new Fire(), new Water(), new Plant(), new Virus()];
    }
}

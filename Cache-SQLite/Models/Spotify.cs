using System;
using System.Collections.Generic;

namespace CacheSQLite.Models
{
    public class ExternalUrls
    {
        public string spotify { get; set; }
    }

    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Item
    {
        public string album_type { get; set; }
        public IList<Artist> artists { get; set; }
        public IList<string> available_markets { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public IList<Image> images { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Albums
    {
        public string href { get; set; }
        public IList<Item> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }


    }

    public class SpotifyAlbums : Cacheable
    {
        public Albums albums { get; set; }
    }

    #region UI Models
    public class UIItemsList
    {
        public List<UIItems> items { get; set; }

        public UIItemsList(){
            items = new List<UIItems>();
        }

        public void Add(UIItems pairedItems){
            if(items != null){
                items.Add(pairedItems);
            }
        }
    }

    public class UIItems
    {
        public Item Item1 { get; set; }
        public Item Item2 { get; set; }
    }
    #endregion

    #region Utils
    public static class Utilities{
        public static UIItemsList GetUIItemsList(IList<Item> list){
            UIItemsList itemsList = new UIItemsList();
            UIItems pairedItems = new UIItems();
            for (int i = 0; i <= list.Count - 1; i++)
            {
                var item = list[i];
                if (pairedItems == null)
                {
                    pairedItems = new UIItems();
                }

                if (pairedItems.Item1 == null)
                {
                    pairedItems.Item1 = item;
                    if (i == list.Count - 1)
                    {
                        itemsList.Add(pairedItems);
                        pairedItems = null;
                    }
                }
                else if (pairedItems.Item2 == null)
                {
                    pairedItems.Item2 = item;
                    itemsList.Add(pairedItems);
                    pairedItems = null;
                }
                else
                {
                    pairedItems = new UIItems();
                    pairedItems.Item1 = item;
                }
            }
            return itemsList;
        }
    }
#endregion
}

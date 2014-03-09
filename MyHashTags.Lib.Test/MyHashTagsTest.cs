using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyHashTags.Lib.Test
{
    [TestClass]
    public class VisualStudioTest
    {
        private MyHashTags cut;
        [TestInitialize]
        public void Init()
        {
            cut = new MyHashTags("",
                "",
                "",
                "");
        }

        [TestMethod]
        public void GetHashTagsTest()
        {
            Dictionary<string,int> msdev = cut.GetHashtags("wbsimms");
            DumpData(msdev,"wbsimms");
            Console.WriteLine();
            Dictionary<string,int> visualstudio = cut.GetHashtags("visualstudio");
            DumpData(visualstudio, "visualstudio");
        }

        [TestMethod]
        public void GetUsers()
        {
            List<string> friends = cut.GetUsers("wbsimms",FriendshipType.FriendsList);
            List<string> followers = cut.GetUsers("wbsimms", FriendshipType.FollowersList);
        }

        [TestMethod]
        public void GetHastagsForUsersTest()
        {
            List<string> friends = cut.GetUsers("wbsimms", FriendshipType.FriendsList);
            Dictionary<string, int> friendsTags = cut.GetHashtags(friends);
            DumpData(friendsTags, "wbsimms Friends");

            Console.WriteLine("================================");
            List<string> followers = cut.GetUsers("wbsimms", FriendshipType.FollowersList);
            Dictionary<string, int> followersTags = cut.GetHashtags(followers);
            DumpData(followersTags, "wbsimms Followers");
        }

        public void DumpData(Dictionary<string,int> data, string username )
        {
            Console.WriteLine(username);
            foreach (string key in data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Keys)
            {
                if (data[key] <= 2) continue;
                Console.WriteLine(key+" : "+data[key]);
            }
        }
    }
}

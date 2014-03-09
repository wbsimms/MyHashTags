using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace MyHashTags.Lib
{

    public class MyHashTags
    {
        private SingleUserAuthorizer auth;
        private TwitterContext twitter;

        public MyHashTags(string accessToken, string accessTokenSecret, string consumerKey, string consumerSecret )
        {
            auth = new SingleUserAuthorizer()
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    AccessToken = "",
                    AccessTokenSecret = "",
                    ConsumerKey = "",
                    ConsumerSecret = ""
                }
            };
            twitter = new TwitterContext(auth);
        }

        public List<string> GetUsers(string username, FriendshipType friendshipType)
        {
            var friends3 =
                twitter.Friendship.FirstOrDefault(x => x.Type == friendshipType && x.ScreenName == username && x.Count == 100)
                    .Users.Select(x => x.ScreenNameResponse)
                    .ToList();

            return friends3;
        }

        public Dictionary<string, int> GetHashtags(string username)
        {
            List<Status> tweets = twitter.Status
                .Where(x => x.Type == StatusType.User && x.ScreenName == username && x.Count == 100)
                .ToList();

            Dictionary<string,int> retval = new Dictionary<string, int>();

            foreach (Status s in tweets)
            {
                if (s.Entities != null && s.Entities.HashTagEntities != null && s.Entities.HashTagEntities.Count > 0)
                {
                    foreach (HashTagEntity ht in s.Entities.HashTagEntities)
                    {
                        if (!retval.ContainsKey(ht.Tag.ToLower()))
                            retval.Add(ht.Tag.ToLower(),0);
                        ++retval[ht.Tag.ToLower()];
                    }
                }
            }
            return retval;
        }

        public Dictionary<string, int> GetHashtags(List<string> usernames)
        {
            Dictionary<string, int> retval = new Dictionary<string, int>();
            foreach (string username in usernames)
            {
                try
                {
                    List<Status> tweets = twitter.Status
                        .Where(x => x.Type == StatusType.User && x.ScreenName == username && x.Count == 100)
                        .ToList();

                    foreach (Status s in tweets)
                    {
                        if (s.Entities != null && s.Entities.HashTagEntities != null && s.Entities.HashTagEntities.Count > 0)
                        {
                            foreach (HashTagEntity ht in s.Entities.HashTagEntities)
                            {
                                if (!retval.ContainsKey(ht.Tag.ToLower()))
                                    retval.Add(ht.Tag.ToLower(), 0);
                                ++retval[ht.Tag.ToLower()];
                            }
                        }
                    }

                }
                catch (Exception)
                {
                }
            }
            return retval;
        }

    }
}

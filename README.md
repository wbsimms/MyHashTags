MyHashTags
==========

Find the twitter hashtags you should use.


Usage:

1. You need twitter API credentials from 
[https://dev.twitter.com/](https://dev.twitter.com/)
2. Create a program of your choice and link to the MyHashTags.Lib.dll
3. Create an instance of MyHashTags passing in your credentials
4. Get which set of users you want. (FriendsList or FollowersList)
`List<string> friends = myhashtags.GetUsers("wbsimms", FriendshipType.FriendsList);`
5. Get the hashtags with the number of counts for the group
`Dictionary<string, int> friendsTags = myhashtags.GetHashtags(friends);`

See the unit test *GetHastagsForUsersTest* for an example.

Output will look like this:

- fsharp : 46
- agile : 42
- sxsw : 32
- spsnh : 30
- agileindia2014 : 23
- icreatedthis : 23
- pmot : 22
- microsoftstudio : 21
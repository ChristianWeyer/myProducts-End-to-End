using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinktecture.Applications.Framework.SignalR
{
    public class HubUser
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }

    [Authorize]
    public class UsersHub : Hub
    {
        private static readonly ConcurrentDictionary<string, HubUser> Users
            = new ConcurrentDictionary<string, HubUser>(StringComparer.InvariantCultureIgnoreCase);

        public IEnumerable<string> GetConnectedUsers()
        {
            return Users.Where(x =>
            {
                lock (x.Value.ConnectionIds)
                {
                    return !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
                }

            }).Select(x => x.Key);
        }

        public override Task OnConnected()
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new HubUser
            {
                Name = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            HubUser user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                    if (!user.ConnectionIds.Any())
                    {
                        HubUser removedUser;
                        Users.TryRemove(userName, out removedUser);
                    }
                }
            }

            return base.OnDisconnected();
        }
    }
}

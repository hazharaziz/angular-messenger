using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        IFollowerRepository Followers { get; }
        IDirectRepository Directs { get; }
        IDirectMessageRepository DirectMessages { get; }
        IGroupRepository Groups { get; }
        IGroupMemeberRepository GroupMembers { get; }
        IGroupMessageRepository GroupMessages { get; }
        int Save();
    }
}

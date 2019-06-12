using SPTestUsersRankingAPI.Model;
using System.Collections.Concurrent;

namespace SPTestUsersRankingAPI.Database
{
    public interface IContext
    {
        ConcurrentDictionary<int, User> Users { get; }
        ConcurrentDictionary<int, UserGame> UserGames { get; }
        ConcurrentDictionary<int, Game> Games { get; }
    }
}

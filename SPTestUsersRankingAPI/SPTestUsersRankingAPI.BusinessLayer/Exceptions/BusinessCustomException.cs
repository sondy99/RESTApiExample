using System;

namespace SPTestUsersRankingAPI.BusinessLayer.Exceptions
{
    class BusinessCustomException : Exception
    {
        public BusinessCustomException(string message) : base(message)
        {
        }
    }
}

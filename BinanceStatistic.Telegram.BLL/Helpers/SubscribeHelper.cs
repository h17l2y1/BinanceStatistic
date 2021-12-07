using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Helpers.Interfaces;
using BinanceStatistic.Telegram.BLL.Models;
using Subscribe = BinanceStatistic.Telegram.BLL.Models.Subscribe;

namespace BinanceStatistic.Telegram.BLL.Helpers
{
    public class SubscribeHelper : ISubscribeHelper
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IUserSubscribeRepository _userSubscribeRepository;

        public SubscribeHelper(IUserRepository userRepository, ISubscribeRepository subscribeRepository,
            IUserSubscribeRepository userSubscribeRepository)
        {
            _userRepository = userRepository;
            _subscribeRepository = subscribeRepository;
            _userSubscribeRepository = userSubscribeRepository;
        }

        public async Task<Subscribe> CreateOrRemoveSubscribe(long telegramUserId, string subscribeType)
        {
            int subscribeMinutes = Int32.Parse(Regex.Replace(subscribeType, "[^0-9.]", ""));
            DAL.Entities.Subscribe currentSubscribe = await _subscribeRepository.FindByMinutes(subscribeMinutes);
            
            User user = await _userRepository.GetUserWithSubscriptions(telegramUserId);

            UserSubscribe userSubscribe = user.UserSubscribes.SingleOrDefault(s => s.SubscribeId == currentSubscribe.Id);

            bool isSubscribed = userSubscribe != null;
            
            var test = new Subscribe(user.UserSubscribes, isSubscribed);

            if (!isSubscribed)
            {
                var newUserSubscribe = new UserSubscribe(user.Id, currentSubscribe.Id);
                await _userSubscribeRepository.Create(newUserSubscribe);
                return test;
            }

            await _userSubscribeRepository.RemoveById(userSubscribe.Id);
            return test;
        }
    }
}
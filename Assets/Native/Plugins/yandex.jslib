mergeInto(LibraryManager.library, {
  
    SendGameReady : function() {
      YaGames.init()
      .then((ysdk) => {
        ysdk.features.LoadingAPI.ready()
      })
      .catch(console.error);
    },
  
    SendGameStart : function() {
      try {
        ysdk.features.GameplayAPI.start()
      } catch(err) {
        console.error;
      }
    },
  
    SendGameStop : function() {
      try {
        ysdk.features.GameplayAPI.stop()
      } catch(err) {
        console.error;
      }
    },

    GetLang : function () {
      try {
        var lang = ysdk.environment.i18n.lang;
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        console.log('YSDK LANG', buffer);
        return buffer;
      } catch(err) {
        // Получить язык с браузера
        var lang = navigator.language;
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        console.log('NAVIGATOR', buffer);
        return buffer;
      }
    },

    CallRateGame : function() {
        ysdk.feedback.canReview()
          .then(({ value, reason }) => {
              if (value) {
                  ysdk.feedback.requestReview()
                      .then(({ feedbackSent }) => {
                          gameInstance.SendMessage('YandexManager', 'GetRatedAward');
                      })
              } else {
                  console.log(reason)
              }
          })
    },
  
    ShowAd : function(){
      ysdk.adv.showFullscreenAdv({
      callbacks: {
          onClose: function(wasShown) {
            gameInstance.SendMessage('GameState', 'StartGame');
          },
          onError: function(error) {
          }
        }
      })
    },
    
    ShowReward : function(num){
      ysdk.adv.showRewardedVideo({
      callbacks: {
          onOpen: () => {
            console.log('Reward Video Opened', num);
          },
          onRewarded: () => {
            gameInstance.SendMessage('YandexManager', 'Rewarded', num);
            console.log('Reward Video Watched', num);
          },
          onClose: () => {
            gameInstance.SendMessage('GameState', 'StartGame');
            console.log('Reward Video Closed', num);
          },
          onError: (e) => {
            console.log('Error while open video ad:', e);
          }
        }
      })
    },

    SetScoreLeaderboard: function(record){
      ysdk.getLeaderboards()
        .then(lb => {
          lb.setLeaderboardScore("leaderboard", record);
          console.log('Leaderboard was update', record);
      })
    },

    GetScoreLeaderboard: function(){
      ysdk.getLeaderboards()
        .then(lb => {
          lb.getLeaderboardEntries('leaderboard', { quantityTop: 10, includeUser: true, quantityAround: 3 })
            .then(res => console.log(res));
      })
    },
  });
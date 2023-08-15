mergeInto(LibraryManager.library, {
  GetLanguage: function () {
    var returnStr = "ru";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  IsMobile : function() {
        return (`ontouchstart` in window || navigator.maxTouchPoints > 0 || navigator.msMaxTouchPoints > 0 || /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent));
  },

  ShareGame: function() {
    vkBridge.send('VKWebAppShare', {
      link: 'https://vk.com/app51724451_156033207'
    }).then((data) => { 
      if (data.result) {
        // Запись размещена
      }
    }).catch((error) => {
      // Ошибка
      console.log(error);
  });
  },

  ShowInterstitialAd: function() {
    vkBridge.send('VKWebAppShowNativeAds', { ad_format: 'interstitial' })
    .then((data) => {
      if (data.result)
        console.log('Реклама показана');
      else
        console.log('Ошибка при показе');
    })
    .catch((error) => { 
      console.log(error); /* Ошибка */
    })
  },

  CheckNativeAdsInerstitial: function() {
    vkBridge.send('VKWebAppCheckNativeAds', {
      ad_format: 'interstitial' /* Тип рекламы */ 
    }).then((data) => { 
      if (data.result) { 
        // Предзагруженные материалы есть
      } else {
        // Материалов нет
      }    
    }).catch((error) => { 
      console.log(error); 
    });
  },

  ShowBannerAd: function() {
    vkBridge.send('VKWebAppShowBannerAd', {
      banner_location: 'bottom',
      can_close: false,
      layout_type: 'resize'
    }).then((data) => { 
      if (data.result) {
        // Баннерная реклама отобразилась
      }
    }).catch((error) => {
      // Ошибка
      console.log(error);
    });
  },

  CheckBannerAd: function() {
    vkBridge.send('VKWebAppCheckBannerAd')
    .then((data) => { 
      if (data.result) {
        return true;
      }
    }).catch((error) => {
      // Ошибка
      console.log(error);
      return fa;
    });
  },
});
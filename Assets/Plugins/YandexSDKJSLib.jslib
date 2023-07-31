mergeInto(LibraryManager.library, {
    GetLanguage: function () {
        var returnStr = yandexSDK.environment.i18n.lang;
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    },
});
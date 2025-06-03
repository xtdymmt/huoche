var QgGameBridge = {

    $CONSTANT: {
        ACTION_CALL_BACK_CLASS_NAME_DEFAULT: "QGMiniGameManager",
        ACTION_CALL_BACK_METHORD_NAME_DEFAULT: "DefaultResponseCallback",
        ACTION_CALL_BACK_METHORD_NAME_AD_ERROR: "AdOnErrorCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_LOAD: "AdOnLoadCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_SHOW: "AdOnShowCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE: "AdOnCloseCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_HIDE: "AdOnHideCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE_REWARDED: "RewardedVideoAdOnCloseCallBack",
        ACTION_CALL_BACK_METHORD_NAME_AD_LOAD_NATIVE: "NativeAdOnLoadCallBack"
    },

    $mAdMap: {},

    $mFileData: {},

	QGCollectIndex: function (index) {
    },

    QGLogin: function (success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        qg.login({
            success: function (res) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: res.data
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "LoginResponseCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "LoginResponseCallback", json);
            }
        })
    },

    QGGetUserInfo: function (success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        qg.getUserInfo({
            success: function (res) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: res.data
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetUserInfoResponseCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetUserInfoResponseCallback", json);
            }
        })
    },

    QGHasShortcutInstalled: function (success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        qg.hasShortcutInstalled({
            success: function (res) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: {
                        hasShortcutInstalled: res
                    }
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "ShortcutResponseCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "ShortcutResponseCallback", json);
            }
        })
    },

    QGInstallShortcut: function (message, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        var messageStr = UTF8ToString(message);

        qg.installShortcut({
            message: messageStr,
            success: function (res) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: res
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },

    QGCreateBannerAd: function (adId, posId, style, adIntervals) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var styleStr = UTF8ToString(style);
        var adIdStr = UTF8ToString(adId);

        var bannerAd;
        if (styleStr) {
            bannerAd = qg.createBannerAd({
                posId: posIdStr,
                style: JSON.parse(styleStr),
                adIntervals: adIntervals
            });
        } else {
            bannerAd = qg.createBannerAd({
                posId: posIdStr,
                adIntervals: adIntervals
            });
        }
        if (bannerAd) {
            mAdMap.set(adIdStr, bannerAd)
            bannerAd.onLoad(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })
            bannerAd.onClose(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE, json);
            })
            bannerAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateInterstitialAd: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);

        var interstitialAd = qg.createInterstitialAd({
            posId: posIdStr
        });
        if (interstitialAd) {
            mAdMap.set(adIdStr, interstitialAd)
            interstitialAd.onLoad(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })
            interstitialAd.onClose(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE, json);
            })
            interstitialAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateBoxBannerAd: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);

        var boxBannerAd = qg.createBoxBannerAd({
            posId: posIdStr
        });
        if (boxBannerAd) {
            mAdMap.set(adIdStr, boxBannerAd)
            boxBannerAd.onLoad(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })
            boxBannerAd.onClose(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE, json);
            })
            boxBannerAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateBoxPortalAd: function (adId, posId, image, marginTop) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);
        var imageStr = UTF8ToString(image);

        var boxPortalAd = qg.createBoxPortalAd({
            posId: posIdStr,
            image: imageStr,
            marginTop: marginTop
        });
        if (boxPortalAd) {
            mAdMap.set(adIdStr, boxPortalAd)
            boxPortalAd.onShow(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_SHOW, json);
            })
            boxPortalAd.onLoad(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })
            boxPortalAd.onClose(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE, json);
            })
            boxPortalAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateRewardedVideoAd: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);

        var rewardedVideoAd = qg.createRewardedVideoAd({
            posId: posIdStr
        });
        if (rewardedVideoAd) {
            mAdMap.set(adIdStr, rewardedVideoAd)
            rewardedVideoAd.onLoad(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })

            rewardedVideoAd.onClose(function (rec) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    isEnded: rec.isEnded
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE_REWARDED, json);
            })
            rewardedVideoAd.onError(function (err) {
                console.error(" rewardedVideoAd.onError = " + JSON.stringify(err))
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateNativeAd: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);

        var nativeAd = qg.createNativeAd({
            posId: posIdStr
        });
        if (nativeAd) {
            mAdMap.set(adIdStr, nativeAd)
            nativeAd.onLoad(function (rec) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    adList: rec.adList
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD_NATIVE, json);
            })
            nativeAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGCreateCustomAd: function (adId, posId, style) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var posIdStr = UTF8ToString(posId);
        var adIdStr = UTF8ToString(adId);
        var styleStr = UTF8ToString(style);

        var customAd = qg.createCustomAd({
            posId: posIdStr,
            style: styleStr ? JSON.parse(styleStr) : {}
        });
        if (customAd) {
            mAdMap.set(adIdStr, customAd)
            customAd.onLoad(function (rec) {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_LOAD, json);
            })
            customAd.onClose(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_CLOSE, json);
            })
            customAd.onHide(function () {
                var json = JSON.stringify({
                    callbackId: adIdStr
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_HIDE, json);
            })
            customAd.onError(function (err) {
                var json = JSON.stringify({
                    callbackId: adIdStr,
                    errMsg: err.errMsg,
                    errCode: err.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_AD_ERROR, json);
            })
        }
    },

    QGShowAd: function (adId, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        var adIdStr = UTF8ToString(adId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.show().then(function () {
                var json = JSON.stringify({
                    callbackId: successID
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }).catch(function (err) {
                var errMsgStr = !err ? "" : err.data ? err.data.errMsg : err.errMsg
                var errCodeValue = !err ? "" : err.data ? err.data.errCode : err.errCode
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: errMsgStr,
                    errCode: errCodeValue
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            })
        } else {
            var json = JSON.stringify({
                callbackId: failID,
                errMsg: "ad is undefined",
                errCode: 404
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
        }
    },

    QGHideAd: function (adId, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        var adIdStr = UTF8ToString(adId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.hide().then(function () {
                var json = JSON.stringify({
                    callbackId: successID
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }).catch(function (err) {
                var errMsgStr = !err ? "" : err.data ? err.data.errMsg : err.errMsg
                var errCodeValue = !err ? "" : err.data ? err.data.errCode : err.errCode
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: errMsgStr,
                    errCode: errCodeValue
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            })
        } else {
            var json = JSON.stringify({
                callbackId: failID,
                errMsg: "ad is undefined",
                errCode: 404
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
        }
    },

    QGLoadAd: function (adId, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        var adIdStr = UTF8ToString(adId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.load().then(function () {
                var json = JSON.stringify({
                    callbackId: successID
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }).catch(function (err) {
                var errMsgStr = !err ? "" : err.data ? err.data.errMsg : err.errMsg
                var errCodeValue = !err ? "" : err.data ? err.data.errCode : err.errCode
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: errMsgStr,
                    errCode: errCodeValue
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            })
        } else {
            var json = JSON.stringify({
                callbackId: failID,
                errMsg: "ad is undefined",
                errCode: 404
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
        }
    },

    QGDestroyAd: function (adId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var adIdStr = UTF8ToString(adId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.destroy()
            mAdMap.delete(adIdStr);
        }
    },

    QGReportAdShow: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var adIdStr = UTF8ToString(adId);
        var posIdStr = UTF8ToString(posId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.reportAdShow({
                adId: posIdStr
            });
        }
    },

    QGReportAdClick: function (adId, posId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var adIdStr = UTF8ToString(adId);
        var posIdStr = UTF8ToString(posId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            ad.reportAdClick({
                adId: posIdStr
            });
        }
    },

    QGIsShow: function (adId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        if (!(mAdMap instanceof Map)) {
            mAdMap = new Map()
        }

        var adIdStr = UTF8ToString(adId);

        var ad = mAdMap.get(adIdStr)

        if (ad) {
            return ad.isShow();
        }
    },

    QGStorageSetIntSync: function (key, value) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var valueStr = value + "";

        qg.setStorageSync({
            key: keyStr,
            value: valueStr
        })
    },

    QGStorageGetIntSync: function (key, defaultValue) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var defaultValueStr = defaultValue + "";

        var result = qg.getStorageSync({
            key: keyStr,
            default: defaultValueStr
        })
        return parseInt(result);
    },

    QGStorageSetStringSync: function (key, value) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var valueStr = UTF8ToString(value);

        qg.setStorageSync({
            key: keyStr,
            value: valueStr
        })
    },

    QGStorageGetStringSync: function (key, defaultValue) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var defaultValueStr = UTF8ToString(defaultValue);

        var result = qg.getStorageSync({
            key: keyStr,
            default: defaultValueStr
        })

        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);

        return buffer;
    },

    QGStorageSetFloatSync: function (key, value) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var valueStr = value + "";

        qg.setStorageSync({
            key: keyStr,
            value: valueStr
        })
    },

    QGStorageGetFloatSync: function (key, defaultValue) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        var defaultValueStr = defaultValue + "";

        var result = qg.getStorageSync({
            key: keyStr,
            default: defaultValueStr
        })
        return parseFloat(result);
    },

    QGStorageDeleteAllSync: function () {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        qg.clearStorageSync()
    },

    QGStorageDeleteKeySync: function (key) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);
        qg.deleteStorageSync({
            key: keyStr
        })
    },

    QGStorageHasKeySync: function (key) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var keyStr = UTF8ToString(key);

        var result = qg.getStorageSync({
            key: keyStr
        })

        return (result === "" ? false : true)

    },

    QGPay: function (param, success, fail, cancel, complete) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var paramStr = UTF8ToString(param);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        var cancelID = UTF8ToString(cancel);
        var completeID = UTF8ToString(complete);

        qg.pay({
            orderInfo: paramStr,
            success: function (ret) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: ret.data
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "PayResponseCallback", json);
            },
            fail: function (err) {
                var json = JSON.stringify({
                    callbackId: failID,
                    data: ret.data
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "PayResponseCallback", json);
            },
            cancel: function (ret) {
                var json = JSON.stringify({
                    callbackId: cancelID,
                    data: ret.data
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "PayResponseCallback", json);
            },
            complete: function () {
                var json = JSON.stringify({
                    callbackId: completeID
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "PayResponseCallback", json);
            }
        })
    },

    QGAccessFile: function (uri) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var uriStr = UTF8ToString(uri);
        var result = qg.accessFile({
            uri: uriStr
        })

        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);

        return buffer;
    },

    QGReadFile: function (uri, encoding, position, length, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var uriStr = UTF8ToString(uri);
        var encodingStr = UTF8ToString(encoding);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);


        qg.readFile({
            uri: uriStr,
            encoding: encodingStr,
            position: position,
            length: length,
            success: function (data) {
                if (encodingStr == "binary") {
                    mFileData[successID] = data.text;
                }
                var json = JSON.stringify({
                    callbackId: successID,
                    textStr: data.text,
                    encoding: encodingStr,
                    byteLength: data.text.byteLength
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "ReadFileResponseCallback", json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "ReadFileResponseCallback", json);
            }
        })
    },

    QGReadFileSync: function (uri, encoding, position, length) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var uriStr = UTF8ToString(uri);
        var encodingStr = UTF8ToString(encoding);
        var successID = 'successID' + Math.random().toString();
        const data = qg.readFileSync({
            uri: uriStr,
            encoding: encodingStr,
            position: position,
            length: length
        })
        var result;
        if (encodingStr == "utf8") {
            result = JSON.stringify({
                callbackId: successID,
                textStr: data.text,
                encoding: encodingStr,
                byteLength: data.text.byteLength,
            });
        } else {
            mFileData[successID] = data.text;
            result = JSON.stringify({
                callbackId: successID,
                encoding: encodingStr,
                byteLength: data.text.byteLength,
            });
        }
        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },

    QGGetFileBuffer: function (buffer, callBackId) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var callBackIdStr = UTF8ToString(callBackId);
        HEAPU8.set(new Uint8Array(mFileData[callBackIdStr]), buffer);
        delete mFileData[callBackIdStr];
    },

    QGWriteFile: function (uri, encoding, position, textStr, textData, length, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var uriStr = UTF8ToString(uri);
        var encodingStr = UTF8ToString(encoding);
        var textStrFinal = UTF8ToString(textStr);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        var textFinal = (encodingStr == "utf8") ? textStrFinal : HEAPU8.slice(textData, length + textData).buffer;

        qg.writeFile({
            uri: uriStr,
            encoding: encodingStr,
            position: position,
            text: textFinal,
            success: function (uri) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "WriteFileResponseCallback", json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "WriteFileResponseCallback", json);
            }
        })
    },

    QGWriteFileSync: function (uri, encoding, position, textStr, textData, length) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var uriStr = UTF8ToString(uri);
        var encodingStr = UTF8ToString(encoding);
        var textStrFinal = UTF8ToString(textStr);

        var textFinal = (encodingStr == "utf8") ? textStrFinal : HEAPU8.slice(textData, length + textData).buffer;

        var result = qg.writeFileSync({
            uri: uriStr,
            encoding: encodingStr,
            position: position,
            text: textFinal
        })

        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },

    QGShowKeyboard: function (param, success, cancel, complete) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var paramStr = UTF8ToString(param);
        var paramData = JSON.parse(paramStr);
        var successID = UTF8ToString(success);
        var cancelID = UTF8ToString(cancel);
        var completeID = UTF8ToString(complete);

        qg.showKeyboard({
            defaultValue: paramData.defaultValue,
            maxLength: paramData.maxLength,
            multiple: paramData.multiple,
            confirmHold: paramData.confirmHold,
            confirmType: paramData.confirmType,
            success: function (ret) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            cancel: function (ret) {
                var json = JSON.stringify({
                    callbackId: cancelID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            complete: function () {
                var json = JSON.stringify({
                    callbackId: completeID
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },


    QGOnKeyboardInput: function (callback) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var callbackID = UTF8ToString(callback);

        var func = function (data) {
            var json = JSON.stringify({
                callbackId: callbackID,
                value: data.value
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "OnKeyboardInputResponseCallback", json);
        };
        qg.onKeyboardInput(func);
    },

    QGOnKeyboardConfirm: function (callback) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var callbackID = UTF8ToString(callback);

        var func = function (data) {
            var json = JSON.stringify({
                callbackId: callbackID,
                value: data.value
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "OnKeyboardInputResponseCallback", json);
        };
        qg.onKeyboardConfirm(func);
    },

    QGOnKeyboardComplete: function (callback) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var callbackID = UTF8ToString(callback);

        var func = function (data) {
            var json = JSON.stringify({
                callbackId: callbackID,
                value: data.value
            })
            unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "OnKeyboardInputResponseCallback", json);
        };
        qg.onKeyboardComplete(func);
    },

    QGHideKeyboard: function () {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        qg.hideKeyboard();
    },

    QGExitApplication: function () {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        qg.exitApplication();
    },

    QGSubscribe: function (templateIds, clientId, userId, scene, type, subDesc, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var templateIdsStr = UTF8ToString(templateIds);
        var clientIdStr = UTF8ToString(clientId);
        var userIdStr = UTF8ToString(userId);
        var sceneStr = UTF8ToString(scene);
        var subDescStr = UTF8ToString(subDesc);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        var templateIdsArray = templateIdsStr.split(",");

        qg.subscribe({
            params: {
                templateIds: templateIdsArray,
                clientId: clientIdStr,
                userId: userIdStr,
                scene: sceneStr,
                subDesc: subDescStr,
                type: type
            },
            success: function (data) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },

    QGUnSubscribe: function (templateIds, clientId, userId, scene, type, subDesc, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var templateIdsStr = UTF8ToString(templateIds);
        var clientIdStr = UTF8ToString(clientId);
        var userIdStr = UTF8ToString(userId);
        var sceneStr = UTF8ToString(scene);
        var subDescStr = UTF8ToString(subDesc);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        var templateIdsArray = templateIdsStr.split(",");

        qg.unsubscribe({
            params: {
                templateIds: templateIdsArray,
                clientId: clientIdStr,
                userId: userIdStr,
                scene: sceneStr,
                subDesc: subDescStr,
                type: type
            },
            success: function (data) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },

    QGIsRelationExist: function (templateIds, clientId, userId, scene, type, subDesc, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var templateIdsStr = UTF8ToString(templateIds);
        var clientIdStr = UTF8ToString(clientId);
        var userIdStr = UTF8ToString(userId);
        var sceneStr = UTF8ToString(scene);
        var subDescStr = UTF8ToString(subDesc);
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        var templateIdsArray = templateIdsStr.split(",");

        qg.isRelationExist({
            params: {
                templateIds: templateIdsArray,
                clientId: clientIdStr,
                userId: userIdStr,
                scene: sceneStr,
                subDesc: subDescStr,
                type: type
            },
            success: function (data) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },

    QGGetStatus: function (success, fail) {
        console.error("QGGetStatus: function (success, fail)");
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        console.error("successID = " + successID + " failID = " + failID);

        qg.getstate({
            success: function (data) {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            },
            fail: function (data, code) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errCode: code
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, CONSTANT.ACTION_CALL_BACK_METHORD_NAME_DEFAULT, json);
            }
        })
    },
    QGIsVivoRuntime: function () {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return false;
        }

        if (window.qg && window.qg.getProvider() === 'vivo') {
            console.log("current is vivo runtime")
            return true;
        }
        return false;
    },
    QGGetSystemInfo: function (success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }

        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);

        qg.getSystemInfo({
            success: function (data) {
                var res = JSON.stringify(data);
                var json = JSON.stringify({
                    callbackId: successID,
                    data: res
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetSystemInfoCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetSystemInfoCallback", json);
            }
        })
    },
    QGGetSystemInfoSync: function () {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var data = qg.getSystemInfoSync();
        var result = JSON.stringify(data);

        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);

        return buffer;
    },
    QGSetClipboardData: function (p, success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        qg.setClipboardData({
            text: UTF8ToString(p),
            success: function () {
                var json = JSON.stringify({
                    callbackId: successID,
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "SetClipboardDataCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "SetClipboardDataCallback", json);
            }
        })
    },
    QGGetClipboardData: function (success, fail) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        var successID = UTF8ToString(success);
        var failID = UTF8ToString(fail);
        qg.getClipboardData({
            success: function (data) {
                var json = JSON.stringify({
                    callbackId: successID,
                    data: data.text
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetClipboardDataCallback", json);
            },
            fail: function (res) {
                var json = JSON.stringify({
                    callbackId: failID,
                    errMsg: res.errMsg,
                    errCode: res.errCode
                })
                unityInstance.SendMessage(CONSTANT.ACTION_CALL_BACK_CLASS_NAME_DEFAULT, "GetClipboardDataCallback", json);
            }
        })
    },
    QGSetPreferredFramesPerSecond: function (fps) {
        if (typeof (qg) == 'undefined') {
            console.log("qg.minigame.jslib  qg is undefined");
            return;
        }
        qg.setPreferredFramesPerSecond(fps);
    }
};

autoAddDeps(QgGameBridge, '$mAdMap');
autoAddDeps(QgGameBridge, '$CONSTANT');
autoAddDeps(QgGameBridge, '$mFileData');

mergeInto(LibraryManager.library, QgGameBridge);
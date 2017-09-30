//********************************************
//* 弹窗的使用方法开始
//********************************************
var _index_loading; //加载中层

/**
 * 显示加载中
 */
var ShowLoading = function () {
    if (window !== top) {
        _index_loading = top.layer.load();
    } else {
        _index_loading = layer.load();
    }
};

/**
 * 隐藏加载中
 */
var HidenLoading = function () {
    if (window !== top) {
        top.layer.close(_index_loading);
    } else {
        layer.close(_index_loading);
    }
};

/**
 * 关闭当前最顶层弹窗
 */
var CloseTopWindow = function () {
    layer.close(layer.index);
};

/**
 * 关闭当前页面
 */
var CloseCurrentWindow = function () {
    var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    parent.layer.close(index);
};

/**
 * 最顶层关闭当前页面
 */
var TopCloseCurrentWindow = function () {
    var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
    top.layer.close(index);
};

/**
 * 展示提示内容
 * @param {String} msg 提示内容
 * @param {Int32} icon 0警告 1成功 2错误 3疑问 4锁定 5不满意 6满意
 * @param {Function} callBack 回调方法
 */
var ShowMsg = function (msg, icon, callBack) {
    if (window !== top) {
        top.layer.msg(msg, {
            time: 3000,
            icon: icon,
            btn: ["确定"],
            btnAlign: "c"
        }, function (index) {
            callBack && callBack();
            layer.close(index);
        });
    } else {
        layer.msg(msg, {
            time: 3000,
            icon: icon,
            btn: ["确定"],
            btnAlign: "c"
        }, function (index) {
            callBack && callBack();
            layer.close(index);
        });
    }
};

/**
 * 提示成功信息
 * @param {String} msg 提示内容
 * @param {Function} callBack 回调方法
 */
var ShowSuccess = function (msg, callBack) {
    ShowMsg(msg, 1, callBack);
};

/**
 * 提示警告信息
 * @param {String} msg 提示内容
 * @param {Function} callBack 回调方法
 */
var ShowWarning = function (msg, callBack) {
    ShowMsg(msg, 0, callBack);
};

/**
 * 提示错误信息
 * @param {String} msg 提示内容
 * @param {Function} callBack 回调方法
 */
var ShowError = function (msg, callBack) {
    ShowMsg(msg, 2, callBack);
};

/**
 * 弹出需要确认后才关闭的信息
 * @param {String} msg 提示内容
 * @param {Int32} icon -1信息框 0警告 1成功 2错误 3疑问 4锁定 5不满意 6满意
 * @param {Function} confirmCallBack 确认后回调方法
 * @param {Function} cancelCallBack 取消回调方法
 */
var ShowNeedConfirmMsg = function (msg, icon, confirmCallBack, cancelCallBack) {
    if (window !== top) {
        top.layer.msg(msg, {
            time: 0,
            icon: icon,
            btn: ["确定", "取消"],
            btnAlign: "c",
            btn1: function (index, layero) {
                confirmCallBack && confirmCallBack();
                layer.close(index);
            },
            btn2: function (index, layero) {
                cancelCallBack && cancelCallBack();
                layer.close(index);
            }
        });
    } else {
        layer.msg(msg, {
            time: 0,
            icon: icon,
            btn: ["确定", "取消"],
            btnAlign: "c",
            btn1: function (index, layero) {
                confirmCallBack && confirmCallBack();
                layer.close(index);
            },
            btn2: function (index, layero) {
                cancelCallBack && cancelCallBack();
                layer.close(index);
            }
        });
    }
};

/**
 * 弹出提示框，需要用户确认,也可以取消
 * @param {String} msg 提示内容
 * @param {Function} confirmCallBack 确认后回调方法
 * @param {Function} cancelCallBack 取消回调方法
 */
var ShowAlert = function (msg, confirmCallBack, cancelCallBack) {
    ShowNeedConfirmMsg(msg, -1, confirmCallBack, cancelCallBack);
};

/**
 * 弹出确认框
 * @param {String} msg 提示内容
 * @param {Function} confirmCallBack 确认后回调方法
 * @param {Function} cancelCallBack 取消回调方法
 */
var ShowConfirm = function (msg, confirmCallBack, cancelCallBack) {
    ShowNeedConfirmMsg(msg, 3, confirmCallBack, cancelCallBack);
};

/**
 * 获取请求参数值
 */
var Request = {
    //获取请求参数指
    QueryString: function (val, url) {
        var re = new RegExp("" + val + "=([^&?]*)", "ig");
        return url.match(re) ? decodeURI(url.match(re)[0].substr(val.length + 1)) : '';
    }
};

/**
 * 降低至的请求参数字典化
 * @param {String} url 请求地址
 *@returns {object} 参数字典化
 */
var ParseQuery = function (url) {
    var query = url.replace(/^[^\?]+\??/, '');
    var Params = {};
    if (!query) { return Params; }// return empty object
    var Pairs = query.split(/[;&]/);
    for (var i = 0; i < Pairs.length; i++) {
        var KeyVal = Pairs[i].split('=');
        if (!KeyVal || KeyVal.length !== 2) { continue; }
        var key = unescape(KeyVal[0]);
        var val = unescape(KeyVal[1]);
        val = val.replace(/\+/g, ' ');
        Params[key] = val;
    }
    return Params;
};

/**
 * 打开一个Frame弹窗
 * @param {String} title 标题
 * @param {String} url 地址
 */
var OpenNewFrame = function (title, url) {
    var param = ParseQuery(url);
    if (title === null || title === '') {
        title = Request.QueryString('t', url); //param['t'];
    }
    if (title === "")
        title = "请取一个页面标题";
    if (url === null || url === '') {
        url = "404.html";
    }
    var w = Request.QueryString('w', url);
    var h = Request.QueryString('h', url);
    var id = Request.QueryString('frameid', url);// param['frameid'];
    var isMaxMin = Request.QueryString('maxmin', url);//param['maxmin'];

    if (w === null || w === '') {
        w = 800;
    }
    if (h === null || h === '') {
        h = $(window).height() - 50;
    }
    if (id === null) {
        id = '';
    }
    if (isMaxMin === null) {
        isMaxMin = false;
    }
    layer.open({
        type: 2,
        id: id,
        area: [w + 'px', h + 'px'],
        fix: true, //不固定
        maxmin: isMaxMin,
        shade: 0.4,
        title: title,
        anim: -1,
        content: [url, 'yes'],
        scrollbar: true,
        shadeClose: false//是否点击遮罩关闭
    });
};

//********************************************
//* 弹窗的使用方法结束
//********************************************

/**
 * 对Ajax的封装
 * @param {object} config 请求参数信息
 * @param {Function} successCallBack 成功回调方法
 * @param {Function} failedCallBack 失败回调方法
 */
var AjaxRequest = function (config, successCallBack, failedCallBack) {
    ShowLoading();
    $.ajax(config).done(function (result) {
        if (result.code === 1) {
            successCallBack && successCallBack(result);
        } else if (result.code === 1000) {
            ShowWarning("请先登录", function () {
                if (window !== top) {
                    top.OpenWin("登录", result.redirectUrl);
                } else {
                    OpenWin("登录", result.redirectUrl);
                }
            });
        } else {
            if (result.message && result.message.trim() !== "") {
                ShowWarning(result.message);
            }
            failedCallBack && failedCallBack(result);
        }
        HidenLoading();
    }).fail(function (err) {
        HidenLoading();
        ShowError("请求服务失败");
    });
};

/**
 * 对dom元素绑定双击打开Frame
 * @param {any} document dom元素
 */
var InitDailog = function (document) {
    $(document).click(function (event) {
        event && event.stopPropagation();
        var t = this.title || this.name || null;
        var a = this.href || this.alt;
        if (window !== top) {
            top.OpenNewFrame(t, a);
        } else {
            OpenNewFrame(t, a);
        }
        this.blur();
        return false;
    });
};

$(function () {
    /*弹窗绑定
    *Url: *******&h=400&w=800&t=新增
    *参数h=高度 w=宽度  t=标题
    */
    InitDailog(".OpenFrame");
});
/**
 * 绑定项目列表
 * @param {object} obj 绑定对象
 * @param {String} currentID 当前项目ID
 */
var BindProjectList = function (obj, currentID) {
    AjaxRequest({
        type: "post",
        url: "/Project/ProjectSelectList"
    }, function (successData) {
        $("<option></option>").val("").text("-请选择-").appendTo($(obj));
        $.each(successData.Data, function (i, item) {
            if (currentID !== null && currentID !== undefined && currentID !== "" && item["FID"].toString() === currentID) {
                $("<option></option>").val(item["FID"]).text(item["FName"]).attr("selected", true).appendTo($(obj));
            } else {
                $("<option></option>").val(item["FID"]).text(item["FName"]).appendTo($(obj));
            }
        });
    });
};

/**
 * 绑定服务器列表
 * @param {object} obj 绑定对象
 * @param {String} currentID 当前ID
 */
var BindServicerList = function (obj, currentID) {
    AjaxRequest({
        type: "post",
        url: "/Servicer/ServicerSelectList"
    }, function (successData) {
        $("<option></option>").val("").text("-请选择-").appendTo($(obj));
        $.each(successData.Data, function (i, item) {
            if (currentID !== null && currentID !== undefined && currentID !== "" && item["FID"].toString() === currentID){
                $("<option></option>").val(item["FID"]).text(item["FName"]).attr("selected", true).appendTo($(obj));
            } else {
                $("<option></option>").val(item["FID"]).text(item["FName"]).appendTo($(obj));
            }
        });
    });
};
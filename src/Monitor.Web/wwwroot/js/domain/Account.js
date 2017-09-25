/**
* 登录
*/
$('#LoginForm').on('submit', function () {
    if ($(this).valid()) {
        AjaxRequest({
            type: this.method,
            url: this.action,
            data: $(this).serialize()
        }, function (data) {
            ShowSuccess(data.message, function () {
                location.href = "/Home/Index";
            });
        }, function () {
            $("#validatorImg").click();
        });
    }
    return false;
});

/**
* 切换验证码
*/
$("#validatorImg").on('click', function () {
    var src = $(this).data("src");
    $(this).attr("src", src + "?v=" + Math.random());
});
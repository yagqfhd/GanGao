define(['jquery'], function ($) {
    // ajax函数的默认参数
    var ajaxOptions = {        
        method: 'GET',
        async: true,
        timeout: 0,
        data: null,
        dataType: 'json',
        headers: "Content-Type:application/x-www-form-urlencoded",
        
    }
    
    var http = {
        post: function (url, param) {
            ///定义可以使用的promise的对象
            var def = $.Deferred();
            /// 定义Ajax配置信息，并合并默认值
            var options = {};
            for (var k in ajaxOptions) {
                options[k] = param[k] || ajaxOptions[k];
            }
            options.method = "POST";
            options.async = options.async === false ? false : true;
            options.url = url;
            ///调用AJAX
            $.ajax(options).then(function (data, textStatus, jqXHR) {
                def.resolve(data);
            }, function (jqXHR, textStatus, errorThrown) {
                def.rejectWith(jqXHR);
            });
            return def.promise(); //就在这里调用
        },
        get: function (url, param) {
            ///定义可以使用的promise的对象
            var def = $.Deferred();
            /// 定义Ajax配置信息，并合并默认值
            var options = {};
            for (var k in ajaxOptions) {
                options[k] = param[k] || ajaxOptions[k];
            }
            options.method = "GET";
            options.async = options.async === false ? false : true;
            options.url = url;
            ///调用AJAX
            $.ajax(options).then(function (data, textStatus, jqXHR) {
                def.resolve(data);
            }, function (jqXHR, textStatus, errorThrown) {
                def.rejectWith(jqXHR);
            });
            return def.promise(); //就在这里调用
        }
    };
    return http;
});
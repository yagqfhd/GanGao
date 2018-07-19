define(["bootstrap", "bootstrapTable"], function () {

    var bootstrapTableDefaultParam = {
        toolbar: '#toolbar',                //工具按钮用哪个容器
        queryParams: function (param) {
            return { limit: param.limit, offset: param.offset };
        },//传递参数（*）
        pagination: true,                   //是否显示分页（*）
        sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
        pageNumber: 1,                      //初始化加载第一页，默认第一页
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
        method: 'get',
        search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
        strictSearch: true,
        showColumns: true,                  //是否显示所有的列
        cache: false,
        showRefresh: true,                  //是否显示刷新按钮
        minimumCountColumns: 2,             //最少允许的列数
        clickToSelect: true,                //是否启用点击选中行
        showToggle: true,
    };

    var ganGaoTable = {
        //tableParams: {}, // 表格参数
        element:null,
        init: function (element, options) {
            var tableParams = $.extend({}, bootstrapTableDefaultParam, options || {});
            ///保存表格控件
            var $table = $("#userTable");
            ganGaoTable.element = $table;
            $table.bootstrapTable(tableParams);
            ganGaoTable.refresh();
        },
        //得到选中的记录
        getSelections : function () {
            var arrRes = ganGaoTable.element.bootstrapTable("getSelections")
            return arrRes;
        },
        refresh : function () {
            ganGaoTable.element.bootstrapTable("refresh");
        }
    };
    
    function ganGaoTableParams (options){
        var tableParams = $.extend({}, bootstrapTableDefaultParam, options || {});
        return tableParams;
    }

    return ganGaoTable;
});

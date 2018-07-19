define(["knockout", "jquery", "bootstrap"], function (ko) {
    //var dialog = $("<sysDialog id='navTop' data-bind='component: { name: 'bootstrapModel'}'></sysDialog>");
    //$("body").append(dialog);
    var defaultParams = {
        sysDialogId: "sysDialogId",
        componentName: "bootstrapModel",
        parentName: "body",
        viewModelName: "",
        templateName: "/templates/component/bootstrapModel.html",
    };

    var initialRun = true;

    function bootstrapViewModel() {
        var that = this;
        
        this.page = ko.observable({
            name: '',
            data: {
                init: function () { }
            }
        });
        
        this.initJS = function (pageName) {
            require([pageName + '-js'], function (page) {
                that.init(pageName, page);
            });
        };

        this.init = function (pageName, pageData) {
            
            pageData.init();

            that.page({
                name: pageName,
                data: pageData
            });
            ko.applyBindings(that, document.getElementById("modalmain"));
            //if (initialRun) {
            //    ko.applyBindings(that, document.getElementById("modalmain"));//.getElementsByTagName('modalmain')[0]);
            //    initialRun = false;
            //}
        };

        this.afterRender = function () {
            if (that.page().data.afterRender) {
                that.page().data.afterRender();
            }
        }

        this.show = function (pageName) {
            var dlg = $("<div class='modal fade' id='bootstrapModal' tabindex='-1' role='dialog' aria-labelledby='bootstrapModalLabel' aria-hidden='false'>");
            dlg.load("/templates/component/bootstrapModel.html", null, function () { });
            
            that.initJS(pageName);
            $("body").append(dlg);
            dlg.modal().on('hidden.bs.modal', function () {
                //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
                ko.cleanNode(document.getElementById("modalmain"));
                dlg.remove();
                //self.bootstrapTable.refresh();
            });
        };
    };

    return new bootstrapViewModel();
});
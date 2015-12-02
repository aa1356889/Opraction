var commObj = {
    //渲染表格的方法
    LoadTable: function (domId, Columns,columnDefs, url,setDataCallBack) {
        //插件语言为中文
        var language = {
            "sProcessing": "处理中...",
            "sLengthMenu": "显示 _MENU_ 项结果",
            "sZeroRecords": "没有匹配结果",
            "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
            "sInfoPostFix": "",
            "sSearch": "搜索:",
            "sUrl": "",
            "sEmptyTable": "表中数据为空",
            "sLoadingRecords": "载入中...",
            "sInfoThousands": ",",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上页",
                "sNext": "下页",
                "sLast": "末页"
            },
            "oAria": {
                "sSortAscending": ": 以升序排列此列",
                "sSortDescending": ": 以降序排列此列"
            }
        }
       
        var aa = $(domId).DataTable({
            processing: true,//载入数据的时候是否显示“载入中”
            pageLength: 10,//首次加载的数据条数
            ordering: false,//排序操作在服务端进行，所以可以关了。
            language: language,
            columns: Columns,
            columnDefs: columnDefs,
            bServerSide:true,
            ajax: {
                type: "post",
                url: url,
                data: setDataCallBack
            }
            ,
            dom: "<bottom iprl>"

        })
        return aa;
    },index:0,
    InitModal: function (domId, title, width, height, top, left) {
        if (!top) {
            top = 10;
        }
        if (!left) {
            left = 0;
        }
        var html = "<div class=\"modal-dialog  modal fade \" role=\"document\"  tabindex=\"-1\" role=\"dialog\"  id=\"myModal" + commObj.index + "\"  style=\"top:" + top + "%; left:" + left + "%; max-width:" + width + "px; position:absolute;max-height:" + height + "px;\">" +
    "<div class=\"modal-content\" style=\"height:"+height+"px\">" +
      "<div class=\"modal-header\">"+
      "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" +
      "<h4 class=\"modal-title\" id=\"myModalLabel" + commObj.index + "\">" + title + "</h4>" +
    "</div>" +
    "<div class=\"modal-body\">" +
    "</div>" +
"</div>" +
    "</div>";
        var model = $(html)
        if ($("#" + domId)) {
            model.children().children(".modal-body").append($("#" + domId));
        }
      $("body").append(model);
      commObj.index++;

      return model;
    }, ShowCurrentEdit: function picture_edit(title, url, id) {
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }, CommondAjaxPost: function (url, parameters, ProcessOkCall, ProcessNodeCallBack) {
        $.ajax({
            type: "POST",
            url:url,
            dataType: "json",
            data: parameters,
        success: function (ajaxObj) {
            var dataArr = [];
            if (ajaxObj.State == 0) {
                if (ProcessOkCall) {
                    ProcessOkCall(ajaxObj);
                }
            } else {
                if (ProcessNodeCallBack) {
                    ProcessNodeCallBack(ajaxObj);
                }
            }
        }
    })
    }

};
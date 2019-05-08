$(function () {
    $('#toolbar').find('select').change(function () {
        $('#table').bootstrapTable('destroy').bootstrapTable({

            url: 'http://localhost:52763/api/BooksInfo',
            queryParams: "queryParams",
            toolbar: "#toolbar",
            sidePagination: "client",
            striped: true, // 是否显示行间隔色
            //search : "true",
            clickToSelect: true,
            showExport: true,  //是否显示导出按钮
            exportDataType: $(this).val(), //导出表格方式（默认basic：只导出当前页的表格数据；all：导出所有数据；selected：导出选中的数据）
            //导出文件类型
            exportTypes: ['excel'],
            uniqueId: "ID",
            pageSize: "5",
            pageList: [10, 25, 50, 100, 'All'],        //可供选择的每页的行数（*）
            pagination: true, // 是否分页
            sortable: true, // 是否启用排序
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            columns: [{
                checkbox: true
            }, {
                field: 'ID',
                title: '编号',
                width: '100px',
                align: 'center',
                valign: 'middle'
            },
                   {
                       field: 'BookName',
                       title: '图书名',
                       width: '150px',
                       align: 'center',
                       valign: 'middle',
                   },
                   {
                       field: 'Author',
                       title: '作者',
                       width: '120px',
                       align: 'center',
                       valign: 'middle',
                   },
                   {
                       field: 'Publication_date',
                       title: '出版时间',
                       width: '200px',
                       align: 'center',
                       valign: 'middle',
                       formatter: dateTimeFormatter,

                   },
                    {
                        field: 'Remark',
                        title: '简介',

                    },

            ]
        });

    }).trigger('change')
    //时间解析    
    function dateTimeFormatter(value, row, index) {
        var dateVal = value + "";

        var date = new Date(dateVal);
        var year = date.getFullYear();
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var day = date.getDay() < 10 ? "0" + date.getDate() : date.getDate();
        return year + "-" + month + "-" + day;
    }

    $(".form_date").datetimepicker({
        language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        //年月日
        format: 'yyyy-mm-dd',
    });
    //重置按钮操作
    $("#btn_add_reset").click(function () {
        $("#add_book_name").val("");
        $("#add_author").val("");
        $("#add_date").val("");
        $("#add_remark").val("");
    })
    //新增操作
    $("#btn_add_submit").click(function () {
       
        
        //  var param = $("#add_form_modal").serialize();
        var BookName = $("#add_book_name").val();
        var Author = $("#add_author").val();
        var Publication_date = $("#add_date").val();
        var Remark = $("#add_remark").val();

        // console.log(param)
        $.ajax({
            url: 'http://localhost:52763/api/BooksInfo/addBooks',
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify({
                BookName: BookName,
                Author: Author,
                Publication_date: Publication_date,
                Remark: Remark
            }),
            success: function (data) {
                if (data == true) {
                    alert("添加成功！")
                    $("#table").bootstrapTable('refresh', data);
                     
                }
            }

        })

    })

    //模态框打开
    $("#btn_add").click(function () {
       
        $(":input").val("");
    })
    //条件查询
    $("#btn_query").click(function () {
        //自定义条件
        BookName = $("#txt_search_booktname").val();
        Author = $("#txt_search_author").val();

        $.ajax({
            url: 'http://localhost:52763/api/BooksInfo/GetInfoBybookNameAndAuthor',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ BookName: BookName, Author: Author, limit: "0", offset: "5" }),
            success: function (data) {
                console.log(data.rows)
                $("#table").bootstrapTable('load', data.rows);

            }
        })
    });
    //删除当前行数据
    $table = $("#table");
    $("#btn_delete").click(function () {


        var ids = $.map($table.bootstrapTable('getSelections'), function (rows) {

            return rows.ID

        })
        if (ids.length == 0) {
            alert("请选择要删除的选项！")
        } else {

            $table.bootstrapTable('remove', {
                field: 'ID',
                value: ids
            });
            $.ajax({
                url: 'http://localhost:52763/api/BooksInfo/delete',
                type: 'post',
                data: JSON.stringify(ids),
                contentType: 'application/json',
                success: function (data) {
                    $('#table').bootstrapTable('refresh', data);
                }
            })
        }
         
    })

})
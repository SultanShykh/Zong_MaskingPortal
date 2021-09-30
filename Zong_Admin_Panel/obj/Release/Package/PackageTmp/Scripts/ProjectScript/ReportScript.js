var ReportScript = {

    Columns:
        [
            { "data": "Id", "name": "Id" },
            //{
            //    "class": "details-control",
            //    "orderable": false,
            //    "data": null,
            //    "defaultContent": ""
            //},
            { "data": "AccountNumber", "name": "AccountNumber" },
            { "data": "CreatedDate", "name": "CreatedDate" },
            { "data": "Masking", "name": "Masking" },
            { "data": "NTN_FTN", "name": "NTN_FTN" },
            { "data": "Username", "name": "Username" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                   
                    return '<textarea disabled>' + data.Comments + '</textarea>'
                }
            },
            { "data": "CurrentStatus", "name": "CurrentStatus" },
             { "data": "j", "name": "Jazz" },
            { "data": "t", "name": "Telenor" },
            { "data": "u", "name": "Ufone" },
             { "data": "TatStatus", "name": "TatStatus" },
            { "data": "DaysElapsed", "name": "DaysElapsed" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                   
                    return '<button title="History" type="button" class="btn btn-danger btn-sm history btn-sm" id="' + data.Id + '" value="history" ><i class="fa fa-link"></i></button>'
                }
            }

        ],

    RenderDataTable: function (url) {

        CrudScript.makeAjaxRequest('GET', url).then(function (data) {
            debugger
            var dt = $('.tblmanage').DataTable();
             dt.destroy();
             debugger
            CrudScript.JqueryDataTableForReport('.tblmanage', data, ReportScript.Columns);



        })

     
},
   LoadHistory: function (id) {
        CrudScript.makeAjaxRequest('GET', '/History/GetById/', { Id: id }).then(function (data) {
            localStorage.setItem("Permissions", JSON.stringify(data));
            var url = "/History/Index";
            // do something with the url client side variable, for example redirect
            window.location.href = url;
        });

    },
    LoadHistory: function (id) {
        CrudScript.makeAjaxRequest('GET', '/History/GetById/', { Id: id }).then(function (data) {
            localStorage.setItem("Permissions", JSON.stringify(data));
            var url = "/History/Index";
            // do something with the url client side variable, for example redirect
            window.location.href = url;
        });

    },

    FetchData: function (formdata) {

        debugger
        $.ajax({
            type: "POST",
            url: '/Sender/ReportGetAll/',
            dataType: "json",
            data: formdata,
            //data: JSON.stringify({ data }),
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (data)
            {
                debugger
          
                var dt = $('.tblmanage').DataTable();
                dt.destroy();
                debugger
                CrudScript.JqueryDataTableForReport('.tblmanage', data, ReportScript.Columns);
               
            },
            error: function myfunction()
            {
                alert("Error");
            }
        })
  
    },


}
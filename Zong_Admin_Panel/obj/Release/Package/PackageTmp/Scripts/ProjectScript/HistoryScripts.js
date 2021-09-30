var HistoryScript =
{
    Columns:
        [

            { "data": "Id", "name": "Id" },
            { "data": "UserName", "name": "UserName" },
            { "data": "MaskingId", "name": "MaskingId" },
            { "data": "Activitytype", "name": "Activitytype" },
            { "data": "Activity", "name": "Activity" },
            { "data": "Date", "name": "Datetime" },

        ],

    RenderDataTable: function (historyData) {
        debugger
        if (historyData != '' && historyData != null) {
            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', historyData, HistoryScript.Columns);
        }
        else {
            CrudScript.makeAjaxRequest('GET', '/History/GetAll').then(function (data) {
                var dt = $('.tblmanage').DataTable();
                dt.destroy();
                CrudScript.JqueryDataTableForReport('.tblmanage', data, HistoryScript.Columns);
            });

        }


    },

}
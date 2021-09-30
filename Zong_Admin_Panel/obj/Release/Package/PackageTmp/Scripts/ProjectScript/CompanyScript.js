var CompanyScript = {

    Columns:
        [
            { "data": "Id", "name": "Id" },
            { "data": "Name", "name": "Name" },
            { "data": "Address", "name": "Address" },
            { "data": "WebsiteUrl", "name": "WebsiteUrl" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                    if (data.Status == true)
                    {
                        data.Status = 1;
                        return '<label class="switch"><input type="checkbox"  name="Status" id="' + data.Status + '" value="' + data.Status + '" checked><span class="slider round"></span></label>'
                    }
                    else
                    {
                        data.Status = 0;
                        return '<label class="switch"><input type="checkbox"  name="Status" id="' + data.Status + '" value="' + data.Status + '"><span class="slider round"></span></label>'
                    }
                    

                }
            },
            
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (data, type, full) {
                    return '<button type="button" class="btn btn-primary edit mr-2" id="' + data.Id + '" value="Edit"><i class="fa fa-edit"></i></button>'

                }
            }
        ],


    RenderDataTable: function () {
        CrudScript.makeAjaxRequest('GET', '/Company/GetCompanies').then(function (data) {

            var dt = $('.tblmanage').DataTable();
            dt.destroy();
            CrudScript.JqueryDataTableForReport('.tblmanage', data.data, CompanyScript.Columns);
        })


    },

    SaveCompany: function () {

        CrudScript.makeAjaxRequest('Post', '/Company/CreateCompany', $("form").serialize()).then(function (data) {
            $('#add_modal').modal("hide");
            if (data.message == "Successfully Created!!!") {
                var mainTable = $('#mainTable').DataTable();
                mainTable.destroy();
                CompanyScript.RenderDataTable();
                ShowDivSuccess(data.message);
            }

            else {
                ShowDivError(data.message);
            }

        })
    },

    FillModal: function (data) {
        $('#add_modal').modal("show");
        $('.modal-title').text('Update Company');
        $('#btnUpdate').css('display', 'block');
        $('#btnAdd').css('display', 'none');
        $('#Id').val(data.Id);
        $('#Name').val(data.Name);
        $('#Address').val(data.Address);
        $('#url').val(data.WebsiteUrl);
    },

    UpdateCompany: function () {
        CrudScript.makeAjaxRequest('Post', '/Company/UpdateCompany', $("form").serialize()).then(function (data) {
            $('#add_modal').modal("hide");
            var mainTable = $('#mainTable').DataTable();
            mainTable.destroy();
            CompanyScript.RenderDataTable();
            ShowDivSuccess("Updated Successfully");
        }).catch(function (err) {
            alert(err);                           // will show "foo"
        });
    },

    DeleteCompany: function (data) {
        var url = '/Company/DeleteCompany';
        DeleteConfirm(url, data.Id, CompanyScript);
    },

    ToggleCompany: function (data) {
        const Id = data.Id;
        CrudScript.makeAjaxRequest('Post', '/Company/ToggleCompany', { Id: data.Id, Status: data.Status }).then(function (data) {
            if (data.message == "Company Disabled" || data.message == "Company Enabled") {
                ShowDivSuccess(data.message);
            }

            else {
                ShowDivError(data.message);
            }
            var mainTable = $('#mainTable').DataTable();
            mainTable.destroy();
            CompanyScript.RenderDataTable();


        })
    }


}
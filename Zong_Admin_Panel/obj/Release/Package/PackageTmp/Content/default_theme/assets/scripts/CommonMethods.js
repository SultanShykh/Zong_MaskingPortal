

var objPageRoleAssign =
{
    AllowCreate: false,
    AllowEdit: false,
    AllowView: false,
    AllowDelete: false
};

function CallAsyncService(url, Param, CallbackFunction) {
    try {
        $.ajax({
            type: "POST",
            url: url,
            data: Param,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            timeout: (1000 * 60 * 4),
            success: function (data) {
                CallbackFunction(data);
            },
            error: function (xhr) {
                if (xhr.responseText) {
                    var err = xhr.responseText;
                    if (err) {
                        console.log(err);
                        ShowDivError(err);
                    }
                    else {
                        ShowDivError('Error');
                    }
                }
            }
        });
    }
    catch (Err) {
        ShowDivError(Err);
    }
    return false;
}

function DeleteConfirm(url, Id,Script) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        buttons: {
            confirm: {
                text: 'Yes, delete it!',
                className: 'btn btn-success'
            },
            cancel: {
                visible: true,
                className: 'btn btn-danger'
            }
        }
    }).then((Delete) => {
        var scrpt = Script;
        if (Delete) {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify({ 'Id': Id }),
                contentType: 'application/json; charset = utf-8',
                success: function (result) {
                    debugger
                    if (result == true) {
                        ShowDivSuccess('Deleted Successfully')
                        var mainTable = $('.tblmanage').DataTable();
                        mainTable.destroy();
                        var ss = "/Sender/GetAll";
                        scrpt.RenderDataTable(ss);
                    }
                    else {
                        ShowDivError(result.message);
                    }
                },
                error: function (e) {
                    ShowDivError('Error while Deleting');
                }

            })

        } else {
            swal.close();
        }
    });

}

function ShowDivError(msg) {


    swal("Error!!",msg, {
        icon: "error",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: 'btn btn-danger',
                closeModal: true
            }
        },
    });
    
}
function HideDivError() {
    $('#lblError').html('');
    $('#divError').hide();
}

function ShowDivSuccess(msg) {

    
    swal({
        text: msg,
        icon: "success",
        buttons: {
            confirm: {
                text: "OK",
                value: true,
                visible: true,
                className: "btn btn-success",
                closeModal: true
            }
        }
    });
    
}
function HideDivSuccess() {
    $('#lblSuccess').html('');
    $('#divSuccess').hide();
}

function PageRoleAssign(AllowCreate, AllowEdit, AllowView, AllowDelete) {
    objPageRoleAssign.AllowCreate = AllowCreate;
    objPageRoleAssign.AllowEdit = AllowEdit;
    objPageRoleAssign.AllowView = AllowView;
    objPageRoleAssign.AllowDelete = AllowDelete;
}

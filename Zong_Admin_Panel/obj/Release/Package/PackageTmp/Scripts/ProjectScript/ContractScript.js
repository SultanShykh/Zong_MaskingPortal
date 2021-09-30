var ContractScript = {


    SaveContract: function (formData) {
        debugger
        $.ajax({
            url: '/Contract/Create',
            type: 'POST',
            data: formData,
            success: function (data) {
                if (data.message.includes("Contract")) {
                    var mainTable = $('#mainTable').DataTable();
                    ShowDivSuccess(data.message);
                    setTimeout(function () { window.location.href = '/Contract' }, 2000);

                }

                else {
                    ShowDivError(data.message);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
    },



    AppendSubServiceMileStone: function (count) {
        var Milestone = '<div class="row"> <div class="col-md-6"> <div class="form-group form-group-default"> <label>Milestone' + count + '</label> <input id="milestone2" name="Milestone' + count + '" type="text" class="form-control"> </div> </div> <div class="col-md-6"> <div class="form-group form-group-default"> <label>Billing Date</label> <input id="billingDate' + count + '" type="text" name="billingDate' + count + '" data-provide="datepicker" data-date-format="dd/MM/yyyy" data-date-autoclose="true" class="form-control date"> <a href="#" class="remove_this  col-md-1"><span class="glyphicon glyphicon-remove "/></a></div></div></div>';
        $(".dynamicView1").append(Milestone);
        return false;
    },

    Details: function (data) {

        CrudScript.makeAjaxRequest('Post', '/Contract/Details', { Id: data.Id }).then(function (data) {
        });
    },

    ApproveORDecline: function (value)
    {
        var ContractId = $('#Id').val();
        CrudScript.makeAjaxRequest('Post', '/Contract/ApproveDecline', { Id: ContractId, data: value }).then(function (data) {
        });

    }
}
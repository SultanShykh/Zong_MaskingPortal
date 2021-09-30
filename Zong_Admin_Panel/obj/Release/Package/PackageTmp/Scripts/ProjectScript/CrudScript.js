var CrudScript = {

    makeAjaxRequest: function (methodType,url,params,FormData) {
        return new Promise(function (resolve, reject) {
            if (methodType == "GET") {
                if (params == null) {

                    $.get(url).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    })
                } else {
                    $.get(url, params).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    })
                }
            } else {
                if (params == null) {
                    $.post(url).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    });
                }
                else {
                    $.post(url, params).done(function (response) {
                        resolve(response)
                    }).fail(function (response) {
                        reject(response);
                    });
                }
            }
            })
            },
    JqueryDataTableForReport: function (elem, data, columns) {
        
        $(elem).DataTable({

            data: data,
            //"aaData": data,
            "columns": columns,
            "order": [[1, "asec"]],
            "bPaginate": true, //
            "aaSorting": [],
            "bFilter": true, //
            "bInfo": false, //
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false,
                    className: "hide_column"
                }
            ],
            //"lengthMenu": [5, 10, 50, 100],
            "pageLength": 10,
            //"scrollX": true,
            dom: 'Bfrtip',
            buttons: [{
                extend: 'excelHtml5',
                text: 'Export All',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                },
                title: 'Maskings'
            },
            {
                extend: 'excelHtml5',
                text: 'Export selected',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,12],
                    modifier: {
                        selected: true,
                    }
                },
                title: 'Maskings',
             }
            ],
            select: {
                style: "multi"
            },
        });

        
    },
    JqueryDataTableWithExport: function (elem, data, columns) {
        debugger
        var ss = data;
        var dd = columns;
       
        $(elem).DataTable({

                data: data,
            //"aaData": data,
            "columns": columns,
            "order": [[1, "asec"]],
            "bPaginate": true, //
            "aaSorting": [],
            "bFilter": true, //
            "bInfo": false, //
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false,
                    className: "hide_column"
                }
            ],

            //"lengthMenu": [5, 10, 50, 100],
            "pageLength": 10,
            "scrollX": true,
            dom: 'Bfrtip',
            buttons: [{
                extend: 'excelHtml5',
                text: 'Export All',
                exportOptions: {

                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,16,17,18,19,20],
                },
                title: 'Maskings'
            },
            {
                extend: 'excelHtml5',
                text: 'Export selected',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,16,17,18,19,20],

                    modifier: {
                        selected: true
                    }

                },
                title: 'Maskings'
            }
            ],
            select:
            {
                style: "multi"
            },

            "fnDrawCallback": function () {

                $('.tblmanage tbody tr').each(function () {
              
            
                    var txt1 = $("td:nth-last-child(8)", $(this)).text();
                    

                    if (txt1 != '--') 
                    {
                        $(".edit", $(this)).remove();

                    }


                });
            },
            
             "responsive": {
                 "details": {
                    
                     "renderer": function (api, rowIdx, columns)
                    {
                      
                             // Show hidden columns in row details
                        var data = $.map(columns, function (col, i) {
                            debugger
                                 if (i == 13) { return; }
                                 if (i == 14) { return; }
                                 if (i == 11) { return; }
                                 if (i == 12) { return; }
                            if (ss[0].Usertypess == 3)
                            {
                                if (i == 2) { return; }
                            }
                                 
                           
                                 return col.hidden
                                     ? '<tr><td>' + col.title + ':</td> ' +
                                     '<td>' + col.data + '</td></tr>'
                                     : '';
                        }).join('');
                      
                      
                             // Extra: Show "Name" in row details
                             //data += '<tr><td>Name:</td><td>' + api.cell(rowIdx, 0).data() + '</td></tr>';
                             // Generate a table
                             data = $('<table width="100%"/>').append(data).prop('outerHTML');
                        
                             // Extra: Show custom content
                             //data += '<div class="content-custom">Custom content</div>';

                             return data;
                         
                     }
                }
            },
         
            columnDefs: [{
               
                className: 'none',
                
                 targets: [0,2, 8, 9, 10,4, 5, 6,11,12, 13, 14, 15,18,7,19,20]
              
            }],   
            //"buttons": function () {
            //    debugger
            //    $('.tblmanage').DataTable({
            //        dom: 'Bfrtip',
            //        buttons: [
            //            'colvis'
            //        ]
            //    });
            //}
        });
      

    },


    JqueryDataTableWithExportApproved: function (elem, data, columns) {

        var ss = data;
        $(elem).DataTable({

            data: data,
            //"aaData": data,
            "columns": columns,
            "order": [[1, "asec"]],
            "bPaginate": true, //
            "aaSorting": [],
            "bFilter": true, //
            "bInfo": false, //
            "columnDefs": [
                {
                    "targets": [0],
                    "visible": false,
                    "searchable": false,
                    className: "hide_column"
                }
            ],

            //"lengthMenu": [5, 10, 50, 100],
            "pageLength": 10,
            "scrollX": true,
            dom: 'Bfrtip',
            buttons: [{
                extend: 'excelHtml5',
                text: 'Export All',
                exportOptions: {

                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,18,19,20],
                },
                title: 'Maskings'
            },
            {
                extend: 'excelHtml5',
                text: 'Export selected',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,18,19,20],

                    modifier: {
                        selected: true
                    }

                },
                title: 'Maskings'
            }
            ],
            select:
            {
                style: "multi"
            },

            "fnDrawCallback": function () {

                $('.tblmanage tbody tr').each(function () {


                    var txt1 = $("td:nth-last-child(6)", $(this)).text();

                    if (txt1 != '--') {

                        $(".edit", $(this)).remove();

                    }


                });
            },
            "responsive": {
                "details": {
                    "renderer": function (api, rowIdx, columns) {

                        // Show hidden columns in row details
                        var data = $.map(columns, function (col, i) {

                            if (i == 13) { return; }
                            if (i == 16) { return; }
                            if (i == 12) { return; }
                            if (i == 11) { return; }
                            if (ss[0].Usertypess == 3)
                            {
                                if (i == 2) { return; }
                            }

                            return col.hidden
                                ? '<tr><td>' + col.title + ':</td> ' +
                                '<td>' + col.data + '</td></tr>'
                                : '';
                        }).join('');

                        // Extra: Show "Name" in row details
                        //data += '<tr><td>Name:</td><td>' + api.cell(rowIdx, 0).data() + '</td></tr>';
                        // Generate a table
                        data = $('<table width="100%"/>').append(data).prop('outerHTML');

                        // Extra: Show custom content
                        //data += '<div class="content-custom">Custom content</div>';

                        return data;

                    }
                }
            },

            columnDefs: [{

                className: 'none',
                //targets: [0,2,7, 4, 8, 9, 10, 12, 11, 13, 14, 15, 18, 19, 5]
                targets: [0, 2, 8, 9, 10, 4, 5, 6, 11, 12,16,13, 15,18 ,7,19,20]

            }]
        });


    },
    JqueryDataTableForMultiRowSpan: function (elem, data, columns) {

        $(elem).DataTable({
            data: data,
            //"aaData": data,
            "columns": columns,
            "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false,
                className: "hide_column"
            },
            ],
            "order": [[1, "asc"]],
            "bPaginate": false, //
            "aaSorting": [],
            "bFilter": true, //
            "bInfo": false, //
            "fnDrawCallback": function () {

                var x = '';
                var y = '';
                var z = '';
                $(elem+' tbody tr').each(function () {
                    var txt = $("td:nth-last-child(5)", $(this)).text();
                    var txt1 = $("td:nth-last-child(4)", $(this)).text();
                    var txt2 = $("td:nth-last-child(3)", $(this)).text();
                    if (txt == x) {
                        $("td:first-child", $(this)).css('visibility', 'hidden');
                        $("td:last-child", $(this)).css('visibility', 'hidden');
                        $("td:nth-last-child(2)", $(this)).css('visibility', 'hidden')
                    }
                    else {
                        $("td:first-child", $(this)).css('visibility', 'visible');
                        x = txt;
                     
                    }
                    if (txt1 == y)
                    {
                        $("td:nth-last-child(4)", $(this)).css('visibility', 'hidden')
                    }
                    else {
                        $("td:nth-last-child(4)", $(this)).css('visibility', 'visible');
                     
                        y = txt1;
                     
                    }

                    if (txt2 == z)
                    {
                        $("td:nth-last-child(3)", $(this)).css('visibility', 'hidden')
                    }
                    else {
                        $("td:nth-last-child(3)", $(this)).css('visibility', 'visible');
                        z = txt2;
                    }
                   
                });
            } // end fnDrawC

        });


    },
    JqueryPFDataTable: function (elem, data, columns) {
        
        $(elem).DataTable({
            data: data,
            //"aaData": data,
            "columns": columns,
            //"columnDefs": [
            //{
            //    "targets": [0],
            //    "visible": false,
            //    "searchable": false,
            //    className: "hide_column"
            //},
            //],
            "bPaginate": false,
            "aaSorting": [],
            "bFilter": false, //
            "bInfo": false, //
        });
    },

    JqueryDataTable: function (elem, data, columns) {
        
        $(elem).DataTable({
            data: data,
            //"aaData": data,
            "columns": columns,
            "createdRow": function (row, data, index) {

                $('td', row).eq(0).prepend('<i class="fa fa-fw fa-link fa-lg Find"></i>');
              
            },
            "order": [[1, "des"]],
            "sPaginationType": "simple_numbers", //
            "aaSorting": [],
            "bFilter": true, //
            "bInfo": false, //
        });
    },


    JqueryDataTableServerSideProcessing: function (elem, data, columns) {
        
        $(elem).DataTable({
            data: data,
           //"aaData": data,
            "columns": columns,
            ////"columnDefs": [
            ////{
            ////    "targets": [0],
            ////    "visible": false,
            ////    "searchable": false,
            ////    className: "hide_column"
            ////},
            //],
            "processing":true,
            "serverSide":true,
            "sPaginationType": "simple_numbers", //
           
            "bFilter": true, //
            "bInfo": false, //
        });
    },
    Response:function (response) {
        var x = document.getElementById("snackbar");
        x.textContent = '' + response.Msg;

        if (response.MsgType == "1") {
            
            $('#snackbar').css("background-color", "mediumseagreen");
        }
        else {
            $('#snackbar').css("background-color", "salmon");
        }

        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
}
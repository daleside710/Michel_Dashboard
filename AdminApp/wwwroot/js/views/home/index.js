var popup, dataTable;

$(document).ready(function () {
    /* Morris.js Charts */
    // RegStat chart
    $.ajax({
        type: 'GET',
        //url: 'dashboard/api/DashboardRegStat',
        url: home_url + '/api/DashboardRegStat',
        contentType: 'application/json',
        success: function (data) {
            Morris.Area({
                element: 'register-chart',
                resize: true,
                data: data,
                xkey: 'date',
                ykeys: ['esCount', 'ptCount'],
                labels: ['ES', 'PT'],
                lineColors: ['#a0d0e0', '#3c8dbc'],
                hideHover: 'auto',
                parseTime: false
            });
        }
    });

    $.ajax({
        type: 'GET',
        //url: 'dashboard/api/DashboardRegStat',
        url: home_url + '/api/DashboardRegStat',
        contentType: 'application/json',
        success: function (data) {
            Morris.Area({
                element: 'register-es-chart',
                resize: true,
                data: data,
                xkey: 'date',
                ykeys: ['esCount'],
                labels: ['ES'],
                lineColors: ['#a0d0e0'],
                hideHover: 'auto',
                parseTime: false
            });
        }
    });

    $.ajax({
        type: 'GET',
        //url: 'dashboard/api/DashboardRegStat',
        url: home_url + '/api/DashboardRegStat',
        contentType: 'application/json',
        success: function (data) {
            Morris.Area({
                element: 'register-pt-chart',
                resize: true,
                data: data,
                xkey: 'date',
                ykeys: ['ptCount'],
                labels: ['PT'],
                lineColors: ['#3c8dbc'],
                hideHover: 'auto',
                parseTime: false
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
    

    // alert DataTable

    
    dataTable = $('#alertTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            //"url": 'dashboard/api/GetAlertDataTable',
            "url": home_url + '/api/GetAlertDataTable',
            "type": 'POST',
            "datatype": 'json'
        },
        "responsive": true,
        "columns": [
            { "data": "product" },
            {
                "data": "stock",
                "render": function (data) {
                    return "<p style='text-align: center;'>" + data + "</p>";
                }
            },
            {
                "data": "pendiente",
                "render": function (data) {
                    return "<p style='text-align: center;'>" + data + "</p>";
                }
            },
            {
                "data": "validadas",
                "render": function (data) {
                    return "<p style='text-align: center;'>" + data + "</p>";
                }
            },
            {
                "data": "total",
                "render": function (data) {
                    return "<p style='text-align: center;'>"+data+"</p>";
                }
            },
            {
                "data": "status",
                "render": function (data) {
                    var activeStatus = "";
                    if (data === 0) {
                        activeStatus = "<span style='color: green; font-size: 16px;'><i class='fa fa-battery-full'></i></span>";
                    } else if (data === 1) {
                        activeStatus = "<span style='color: orange; font-size: 16px;'><i class='fa fa-battery-quarter'></i></span>";
                    } else if (data === 2) {
                        activeStatus = "<span style='color: red; font-size: 16px;'><i class='fa fa-battery-empty'></i></span>";
                    }
                    return activeStatus;
                }
            }
        ],
        "dom": 'Blfrtip',
        "language": {
            "emptyTable": "no data found.",
            "processing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw'>"
        },
        "buttons": [],
        "lengthChange": true,
        "bDestroy": true,
        "paging": false,
        "searching": false
    });
    
});
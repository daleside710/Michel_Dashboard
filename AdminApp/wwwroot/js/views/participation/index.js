var popup, dataTable;
var entity = 'Participaciones';
var apiurl = 'api/' + entity;

$(document).ready(function () {

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    });
    var id_par = $("#id_par").is(":checked");
    var registrofecha_par = $("#registrofecha_par").is(":checked");
    var registrohora_par = $("#registrohora_par").is(":checked");
    var nombre_par = $("#nombre_par").is(":checked");
    var apellidos_par = $("#apellidos_par").is(":checked");
    var telefono_par = $("#telefono_par").is(":checked");
    var telefono_par = $("#telefono_par").is(":checked");
    var email_par = $("#email_par").is(":checked");
    var dni_par = $("#dni_par").is(":checked");
    var regalo_par = $("#regalo_par").is(":checked");
    var estado_par = $("#estado_par").is(":checked");
    $("#todos").on('click', function () {
        if($("#todos").is(":checked")){
            $("#id_par").prop("checked", true);
            $("#registrofecha_par").prop("checked", true);
            $("#registrohora_par").prop("checked", true);
            $("#nombre_par").prop("checked", true);
            $("#apellidos_par").prop("checked", true);
            $("#telefono_par").prop("checked", true);
            $("#email_par").prop("checked", true);
            $("#dni_par").prop("checked", true);
            $("#regalo_par").prop("checked", true);
            $("#pais_par").prop("checked", true);
            $("#estado_par").prop("checked", true);
        }else{
            $("#id_par").prop("checked", false);
            $("#registrofecha_par").prop("checked", false);
            $("#registrohora_par").prop("checked", false);
            $("#nombre_par").prop("checked", false);
            $("#apellidos_par").prop("checked", false);
            $("#telefono_par").prop("checked", false);
            $("#email_par").prop("checked", false);
            $("#dni_par").prop("checked", false);
            $("#regalo_par").prop("checked", false);
            $("#pais_par").prop("checked", false);
            $("#estado_par").prop("checked", false);
        }
    });
    $("#id_par").on('click', function () {
        if (!$("#id_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#registrofecha_par").on('click', function () {
        if (!$("#registrofecha_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#registrohora_par").on('click', function () {
        if (!$("#registrohora_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#nombre_par").on('click', function () {
        if (!$("#nombre_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#apellidos_par").on('click', function () {
        if (!$("#apellidos_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#telefono_par").on('click', function () {
        if (!$("#telefono_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#email_par").on('click', function () {
        if (!$("#email_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#dni_par").on('click', function () {
        if (!$("#dni_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#regalo_par").on('click', function () {
        if (!$("#regalo_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    $("#pais_par").on('click', function () {
        if (!$("#pais_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
     $("#estado_par").on('click', function () {
        if (!$("#estado_par").is(":checked")) {
            $("#todos").prop("checked", false);
        } else {
            if(id_par && registrofecha_par && registrohora_par && nombre_par && apellidos_par && 
                telefono_par && email_par && dni_par && regalo_par && pais_par && estado_par){
                $("#todos").prop("checked", true);
            }
        }
    });
    dataTable = $('#grid').DataTable({
        "initComplete": function () {
            var input = $('.dataTables_filter input').unbind(),
                self = this.api(),
                $searchButton = $('<button class="btn btn-default">')
                    .text('Buscar')
                    .click(function () {
                        self.search(input.val()).draw();
                    })
                $('.dataTables_filter').append($searchButton);
        },
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": apiurl,
            "type": 'POST',
            "datatype": 'json',
            "data": function (request) {
                request.id_par = $("#id_par").is(":checked");
                request.registrofecha_par = $("#registrofecha_par").is(":checked");
                request.registrohora_par = $("#registrohora_par").is(":checked");
                request.nombre_par = $("#nombre_par").is(":checked");
                request.apellidos_par = $("#apellidos_par").is(":checked");
                request.telefono_par = $("#telefono_par").is(":checked");
                request.email_par = $("#email_par").is(":checked");
                request.dni_par = $("#dni_par").is(":checked");
                request.regalo_par = $("#regalo_par").is(":checked");
                request.pais_par = $("#pais_par").is(":checked");
                request.estado_par = $("#estado_par").is(":checked");
            }
        },
        "responsive": true,
        "order": [[0, "desc"]],
        "columns": [
            { "data": "id_par" }, 
            {
                "data": "registrofecha_par",
                "render": function (data) {
                    var dateSplit = data.split('T');
                    var dateSplit1 = dateSplit[0].split('-');
                    return dateSplit1[2] + "/" + dateSplit1[1] + "/" + dateSplit1[0];
                }
            },
            { "data": "registrohora_par" }, 
            { "data": "nombre_par" },
            { "data": "apellidos_par" },
            { "data": "telefono_par" }, 
            {
                "data": "email_par",
                "render": function (data) {
                    return data.toLowerCase();
                }
            }, 
            { "data": "dni_par" },
            {
                "data": "premioSelFrnt_par",
                "render": function (data, type, row, meta) {
                    var valorpremio_par = row["valorpremio_par"];
                    
                    if (valorpremio_par == null) {
                        if (row['regalo']) {
                            return row['regalo']['producto'];
                        }
                    } else {
                        return valorpremio_par;
                    }
                    
                    return "";
                }
            },
            { "data": "pais_par" },
            {
                "data": "id_est",
                "render": function (data, type, row, meta) {
                    var id_est = row['id_est'];
                    var solicitar_adjunto = row['solicitar_adjunto'];
                    var adjunto_adjunto = row['adjunto_adjunto'];
                    if (id_est == "3") {
                        return '<p style="color: #C40F12">Rechazado</p>';
                    } else if (id_est == "2") {
                        return '<p style="color: #079328">Validado</p>';
                    } else if (id_est == "1") {
                        if (solicitar_adjunto == "1" && adjunto_adjunto == "1") {
                            return '<p style="color: #EC721F">Actualizando</p>';
                        } else if (solicitar_adjunto == "1" && adjunto_adjunto == "0") {
                            return '<p style="color: #EC721F">Modificando</p>';
                        } else {
                            return '<p style="color: #EC721F">Pendiente</p>';
                        }
                    } else {
                        return "-";
                    }
                }
            },
            {
                "data": "id_par",
                "render": function (data) {
                    var btnEdit = "<div><a onclick=ShowPopup('" + entity + "/Edit/" + data + "') class='btn btn-block btn-default btn-sm'><i class='fa fa-edit'></i> Detalles</a></div>";
                    return btnEdit;
                }
            },
        ],
        "columnDefs": [
            {
                "orderable": false, "targets": [11],
            },
            
        ],
        "fixedColumns": true,
        "dom": 'Blfrtip',
        "language": {
            "emptyTable": "no data found.",
            "processing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw'>"
        },
        "buttons": [],
        "lengthChange": true,
        "bDestroy": true,
    });
    
});

/**
 * 
 * Main Edit Action
 */

function SubmitEdit(form) {
    $('.alert').remove();
    $.validator.unobtrusive.parse(form);
    
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        var id = data.id_par;
        
		var nombre_par = data.nombre_par;
		var apellidos_par = data.apellidos_par;
        var factura1_par = data.factura1_par;
        var factura2_par = data.factura2_par;
        var fechaCompra1_par = data.fechaCompra1_par;
		var fechaCompra2_par = data.fechaCompra2_par;
		var errorMsgContainer;
		var errorMsg = "Se deben completar los datos siguientes para poder Actualizar: Nombre y Apellidos, Nº Factura, Fecha, Modelo Neumático y Dimensión.";

		if (nombre_par === "" || apellidos_par === "" ||
			factura1_par === "" && factura2_par === "") {

            errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">'+
                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >'+
                errorMsg +
                '</div>';
            $('#par_edit').prepend(errorMsgContainer);
            return false;
        }

        if (fechaCompra1_par === "" && fechaCompra2_par === "") {
            errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">' +
                '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >' +
                errorMsg +
                '</div>';
            $('#par_edit').prepend(errorMsgContainer);
            return false;
        }

        data = JSON.stringify(data);
        
        $.ajax({
            type: 'POST',
            url: apiurl + '/' + id,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    //popup.modal('hide');
                    ShowMessage(data.message);
					dataTable.ajax.reload(null, false);
					$('#fullname').text(nombre_par + ' ' + apellidos_par);
                } else {
                    if (data.message == "duplicate") {
                        errorMsg = "Factura validada para el mismo taller en la participación #" + data.validated_id_par;
                        console.log(errorMsg);
                        var errorMsgContainer = '<div class="alert alert-danger alert-dismissible" role="alert">' +
                            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button >' +
                            errorMsg +
                            '</div>';
                        $('#par_edit').prepend(errorMsgContainer);
                        return false;
                    }
                    ShowMessageError(data.message);
                }
            }
        });
    }
    return false;
}

function ShowPopup(url) {
    var modalId = 'modalDefault';
    var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#' + modalId + '').modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
}
function downloadFile(id, filename) {
    window.open(entity + "/DownloadFile?id=" + id + "&filename=" + filename, '_blank');
}


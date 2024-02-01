
//Metodo para validar que datos fueron ingresados correctamente mediante ASP, para luego
//enviar una confirmacion al usuario mediante SweetAlert
function Validate(ctl, event, titulo, tipo) {

    var $form = $('#formID');
    event.preventDefault();
    if ($form.valid()) {
        if (tipo == 1) {
            Swal.fire({
                title: "Confirmación",
                text: "¿Desea agregar el registro de " + titulo + "?",
                icon: "question",
                showDenyButton: true,
                confirmButtonText: 'Sí',
                denyButtonText: 'No'
            })
                .then((willDelete) => {
                    if (willDelete.isConfirmed) {
                        $form.submit();
                    } else {
                        Swal.fire('No fue agregado.', '', 'info')
                    }
                });
        } else {
            Swal.fire({
                title: "Confirmación",
                text: "¿Desea editar el registro de " + titulo + "?",
                icon: "question",
                showDenyButton: true,
                confirmButtonText: 'Sí',
                denyButtonText: 'No'
            })
                .then((willDelete) => {
                    if (willDelete.isConfirmed) {
                        $form.submit();
                    } else {
                        Swal.fire('No fue editado.', '', 'info')
                    }
                });
        }
    } else {
        $form.submit();
    }
}

//Metodo para confirmar el regreso a la pagina anterior, para evitar que se pierdan datos a la hora de editar/agregar
function ValidateReturn() {
    event.preventDefault();
    var $link = document.getElementById('regresar');
    Swal.fire({
        title: "¿Desea regresar a la página anterior?",
        text: "Perderá todos los cambios.",
        icon: "question",
        showDenyButton: true,
        confirmButtonText: 'Sí',
        denyButtonText: 'No'
    })
        .then((willReturn) => {
            if (willReturn.isConfirmed) {
                $link.click();
            }
        });

}
//Funcion para poder llamar Metodo Agregar del Controlador a través del botón presente en DataTable
function agregar() {
    var button = document.getElementById("agregar");
    button.click();
};

//Funcion iniciadora de DataTable. Trae botones de exportar
$(document).ready(function () {
    var table = $('#tablaSmart').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        buttons: [
            {
                text: 'Agregar',
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                    agregar();
                }
            },
            {
                extend: 'excelHtml5',                
                split: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    
                ],
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'
            
        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ - _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ en total)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvis: 'Elegir columnas',
                copy: 'Copiar'
            }
        },
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"

    });
    var table = $('#tablaSmartAdmin').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        buttons: [
            {
                text: 'Agregar',
                className: 'btn btn-success',
                action: function agregar() {
                    document.getElementById("agregarAdmin").click();
                }
            },
            {
                extend: 'excelHtml5',
                split: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                ],
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'

        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ - _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ en total)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvis: 'Elegir columnas',
                copy: 'Copiar'
            }
        },
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"

    });
    var table = $('#tablaSmartLeg').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        buttons: [
            {
                text: 'Agregar',
                className: 'btn btn-success',
                action: function agregar() {
                    document.getElementById("agregarLeg").click();
                }
            },
            {
                extend: 'excelHtml5',
                split: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                ],
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'

        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvis: 'Elegir columnas',
                copy: 'Copiar'
            }
        },
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"

    });
    var table = $('#tablaSmartTem').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        buttons: [
            {
                text: 'Agregar',
                className: 'btn btn-success',
                action: function agregar() {
                    document.getElementById("agregarTem").click();
                }
            },
            {
                extend: 'excelHtml5',
                split: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                ],
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'

        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvis: 'Elegir columnas',
                copy: 'Copiar'
            }
        },
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"

    });
    var table = $('#tablaSmartFrac').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        buttons: [
            {
                text: 'Agregar',
                className: 'btn btn-success',
                action: function agregar() {
                    document.getElementById("agregarFrac").click();
                }
            },
            {
                extend: 'excelHtml5',
                split: [
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: ':visible'
                        }
                    },

                ],
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'

        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvis: 'Elegir columnas',
                copy: 'Copiar'
            }
        },
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"

    });
});

//INICIO funciones comisión

function RegistrarComision() {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var nombreComision = $("#NombreComision").val();
    var detalle = $("#Detalle").val();
    var tipo = $("#Tipo").val();
    var temaId = $("#TemaID").val();


        Swal.fire({
            title: "Confirmación",
            text: "¿Desea agregar la comisión " + nombreComision + "?",
            icon: "question",
            showDenyButton: true,
            confirmButtonText: 'Sí',
            denyButtonText: 'No'
        })
            .then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        url: '/Comisions/Create',
                        data: {
                            __RequestVerificationToken: token,
                            NombreComision: nombreComision,
                            Detalle: detalle,
                            Tipo: tipo,
                            TemaID: temaId
                        },
                        dataType: 'json',
                        success: function (data) {
                            if (data.mensaje == 'agregado') {
                                var idComision = data.idComision;
                                Swal.fire({
                                    title: 'Comisión creada',
                                    text: "¿Desea agregar un registro legislativo a la comisión?",
                                    icon: 'success',
                                    showDenyButton: true,
                                    confirmButtonText: 'Sí',
                                    denyButtonText: 'No'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        window.location.href = "/Comisions/AddLegislativeYear?ComisionID=" + idComision;
                                    } else {
                                        event.preventDefault();
                                        Swal.fire('Comisión agregada sin registros.', '', 'success');
                                        window.onload = setInterval(redirigir("/Comisions/Index"), 10000);
                                    }
                                })

                            } else if (data.mensaje == 'nombreExistente') {
                                Swal.fire({
                                    icon: 'warning',
                                    title: 'Comisión no agregada',
                                    text: 'Ya existe una comisión con ese nombre.'
                                })
                            } else {
                                Swal.fire(
                                    '¡Ups!',
                                    'Ocurrió un error al crear la comisión.',
                                    'error'
                                );
                            }
                        },
                        error: function (data) {
                            Swal.fire(
                                '¡Ups!',
                                'Ocurrió un error al crear la comisión.',
                                'error'
                            );
                        }
                    });
                } else {
                    Swal.fire('No fue agregada.', '', 'info')
                }
            });

    

}

function redirigir(direccion) {
    document.location.href = direccion;
}


function MostrarDiputadosPorAnoLegislativo(diputados) {
    var idAnoLegislativo = $("#selectAnoLegislativo").val();
    var listaDiputados = $("ul#listaDiputados li");
    var ul = document.getElementById("listaDiputados");
    const arrDiputados = [];
    const direccion = "GetDiputadosFromPlenario";
    if (diputados != null) {
        arrDiputados = [...diputados];
        direccion = "GetDiputadosFromComision";
    }
    
    $('#loadingSpinner').show();

    $.ajax({
        type: 'GET',
        url: '/Comisions/GetDiputadosFromPlenario',
        data: {
            IdLegislativo: idAnoLegislativo
        },
        dataType: 'json',
        success: function (data) {
            if (data.mensaje == '1') {
                for (i = 0; i < listaDiputados.length; i++) {
                    listaDiputados.eq(i).remove();
                }
                for (i = 0; i < data.datos.length; i++) {
                    var li = document.createElement("li");
                    var input = document.createElement("input");
                    input.setAttribute("type", "checkbox");
                    if (arrDiputados.length > 0) {
                        for (x = 0; x < arrDiputados.length; x++) {
                            if (arrDiputados[x] == data.datos[i].diputadoID) {
                                input.setAttribute("checked", "true");
                            }
                        }
                    }
                    input.setAttribute("name", "DiputadosID");
                    input.setAttribute("value", data.datos[i].diputadoID);
                    li.appendChild(input);
                    li.appendChild(document.createTextNode(" " + data.datos[i].nombreCompleto));
                    ul.appendChild(li);
                }
                $('#loadingSpinner').hide();
            } else if (data.mensaje == '2') {
                for (i = 0; i < listaDiputados.length; i++) {
                    listaDiputados.eq(i).remove();
                }
                $('#loadingSpinner').hide();
                Swal.fire(
                    '¡Sin diputados!',
                    'No existen diputados registrados en el año legislativo seleccionado.',
                    'warning'
                );
            } else {
                $('#loadingSpinner').hide();
                for (i = 0; i < listaDiputados.length; i++) {
                    listaDiputados.eq(i).remove();
                }
                Swal.fire(
                    '¡Ups!',
                    'Ocurrió un error al cargar los diputados del periodo legislativo.',
                    'error'
                );
            }
        },
        error: function (data) {
            $('#loadingSpinner').hide();
            Swal.fire(
                '¡Ups!',
                'Ocurrió un error al cargar los diputados del periodo legislativo.',
                'error'
            );
        }
    });
}

function RegistrarAnoLegislativoAComision(idComision, tipo) {
    var anoLegislativo = $("#selectAnoLegislativo").val();
    var comisionId = idComision;

    if (anoLegislativo == null) {
        Swal.fire({
            title: 'Año legislativo',
            text: 'Primero debe seleccionar un año legislativo.',
            icon: 'warning',
            timer: 3000
        });
    } else if (($("ul#listaDiputados").has("li").length === 0)) {
        Swal.fire(
            '¡Sin diputados!',
            'No existen diputados registrados en el año legislativo seleccionado.',
            'warning'
        );
    } else {
        if (tipo == 1) {
            AgregarDiputados(comisionId,1);
        } else {
            $.ajax({
                type: 'POST',
                url: '/Comisions/AddLegislativeYear',
                data: {
                    ComisionID: comisionId,
                    LegislativoID: anoLegislativo
                },
                dataType: 'json',
                success: function (data) {
                    if (data.mensaje == '200') {

                        AgregarDiputados(comisionId,0);

                    } else {
                        Swal.fire(
                            '¡Ups!',
                            'Ocurrió un error al registrar el año legislativo 1.',
                            'error'
                        );
                    }
                },
                error: function (data) {
                    Swal.fire(
                        '¡Ups!',
                        'Ocurrió un error al registrar el año legislativo 2.',
                        'error'
                    );
                }
            });
        }
    }
}

function AgregarDiputados(id,tipo) {

    var arr = [];
    $('.list-group-item input[type="checkbox"]:checked').each(function () {
        arr.push($(this).val());
    });
    if (arr.length > 0) {
        $.ajax({
            type: 'POST',
            url: '/Comisions/AgregarDiputados',
            data: {
                DiputadosID: arr
            },
            dataType: 'json',
            success: function (data) {
                if (data.mensaje == '200') {
                    Swal.fire({
                        title: '¡Éxito!',
                        text: 'El año legislativo y los diputados fueron registrados correctamente a la comisión.',
                        icon: 'success'
                    });
                    var direccion = `/Comisions/Details/` + id;
                    if (tipo == 1) {
                        direccion = `/Comisions/Edit/` + id
                    }
                    setTimeout(() => {
                        window.location.replace(direccion);
                    }, 1000)
                } else {
                    Swal.fire(
                        '¡Ups!',
                        'Ocurrió un error al registrar los diputados en la comisión a.',
                        'error'
                    );
                }
            },
            error: function (data) {
                Swal.fire(
                    '¡Ups!',
                    'Ocurrió un error al registrar los diputados en la comisión b.',
                    'error'
                );
            }
        });
    } else {
        Swal.fire(
            '¡Ups!',
            'Debe elegir diputados de la lista.',
            'error'
        );
    }
}

function redirectToMain() {
    setTimeout(() => {
        window.location.replace(`/Comisions/Index`);
    }, 3000)
}

//Funciones para Sesion de comision
function MostrarAñosLegislativosDeComision() {
    var idComision = $("#selectComision").val();
    var listaLegislativos = $("#selectLegislativos");
    $("#selectLegislativos").empty();
    $('#loadingSpinner').show();

    $.ajax({
        type: 'GET',
        url: '/SesionComisiones/GetLegislativosFromComision',
        data: {
            IDComision: idComision
        },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                $.each(data, function (i, comisionLegislativos) {
                    $("#selectLegislativos").append('<option value="' + comisionLegislativos.value + '">' +
                        comisionLegislativos.text + '</option>');
                });
                $('#loadingSpinner').hide();

            } else {

                swal.fire(
                    '¡Sin años legislativos!',
                    'No existen periodos legislativos registrados en la comisión seleccionada.',
                    'warning');
                $('#loadingspinner').hide();
            }
        },
        error: function (data) {

            listaLegislativos.empty();
            Swal.fire(
                '¡Ups!',
                'Ocurrió un error al cargar los diputados del periodo legislativo.',
                'error'
            );
            $('#loadingSpinner').hide();
        }

    });
}



//FIN funciones Sesion Comision

//Funciones para Administracion

function RegistrarAdministracion() {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var adminPeriodo = $("#AdministracionPeriodo").val();
    var presiRepublica = $("#PresidenteRepublica").val();
    var formFull = $('#formID');

    $.ajax({
        type: 'POST',
        url: '/Administraciones/Create',
        data: {
            __RequestVerificationToken: token,
            AdministracionPeriodo: adminPeriodo,
            PresidenteRepublica: presiRepublica,
        },
        dataType: 'json',
        success: function (data) {
            if (data.mensaje == 'agregado') {
                Swal.fire({
                    icon: 'success',
                    title: '¡Administración agregada!',
                    timer: 3000
                })
                window.location.href = "/Utilidades/Index";
            } else if (data.mensaje == 'adminExistente') {
                Swal.fire({
                    icon: 'warning',
                    title: 'Administración no agregada',
                    text: 'Ya existe una administración con ese período.'
                })
            } else if (data.mensaje == 'vacio') {
                Swal.fire(
                    '¡Ups!',
                    'Debe llenar todas las casillas.',
                    'error'
                );
                ValidatorValidate(forFull);
            } else {
                Swal.fire(
                    '¡Ups!',
                    'Ocurrió un error al agregar la administración..',
                    'error'
                );
            }
        },
        error: function (data) {
            
            Swal.fire(
                '¡Ups!',
                'Ocurrió un error al agregar la administración.',
                'error'
            );
            $('#formID').validate()
        }
    });
}

//FIN funciones Administración

//Funciones para Legislativo

function RegistrarLegislativo() {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var legisPeriodo = $("#AnnoLegislativo").val();
    //var blah = $("#AdministracionID").val();

    $.ajax({
        type: 'POST',
        url: '/Legislativos/Create',
        data: {
            __RequestVerificationToken: token,
            AnnoLegislativo: legisPeriodo,
            //AdministracionID: blah
        },
        dataType: 'json',
        success: function (data) {
            if (data.mensaje == 'agregado') {
                Swal.fire({
                    icon: 'success',
                    title: '¡Legislación agregada!',
                    timer: 3000
                })
                window.location.href = "/Utilidades/Index";
            } else if (data.mensaje == 'legisExistente') {
                Swal.fire({
                    icon: 'warning',
                    title: 'Legislación no agregada',
                    text: 'Ya existe una legislación con ese período.'
                })
            } else if (data.mensaje == 'vacio') {
                Swal.fire(
                    '¡Ups!',
                    'Debe llenar todas las casillas.',
                    'error'
                );
            } else if (data.mensaje = 'noExisteAdministracion') {
                Swal.fire(
                    '¡Ups!',
                    'No existe administración para esa legislación, debe agregarla primero.',
                    'error'
                );
            } else {
                Swal.fire(
                    '¡Ups!',
                    'Ocurrió un error al agregar la legislación...',
                    'error'
                );
            }
        },
        error: function (data) {

            Swal.fire(
                '¡Ups!',
                'Ocurrió un error al agregar la legislación.',
                'error'
            );
        }
    });
}

//FIN funciones Legislativo

//SWEET ALERT MENSAJES DE CONFIRMACIÓN

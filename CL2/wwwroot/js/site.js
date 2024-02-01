// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function exportData() {
    /* Get the HTML data using Element by Id */
    var table = document.getElementById("ProyxDip");

    /* Declaring array variable */
    var rows = [];

    //iterate through rows of table
    for (var i = 0, row; row = table.rows[i]; i++) {
        //rows would be accessed using the "row" variable assigned in the for loop
        //Get each cell value/column from the row
        column1 = row.cells[0].innerText;
        column2 = row.cells[1].innerText;
        column3 = row.cells[2].innerText;
        column4 = row.cells[3].innerText;
        column5 = row.cells[4].innerText;

        /* add a new records in the array */
        rows.push(
            [
                column1,
                column2,
                column3,
                column4,
                column5
            ]
        );

    }
    csvContent = "data:text/csv;charset=utf-8,";
    /* add the column delimiter as comma(,) and each row splitted by new line character (\n) */
    rows.forEach(function (rowArray) {
        row = rowArray.join(",");
        csvContent += row + "\r\n";
    });

    /* create a hidden <a> DOM node and set its download attribute */
    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "Proyecto_por_Diputado.csv");
    document.body.appendChild(link);
    /* download the data file  */
    link.click();


   
}


function ExportToExcel(type, fn, dl) {
    var elt = document.getElementById('ProyxDip');
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || ('ProyectoDeLeyPorDiputado.' + (type || 'xlsx')));
}



function AddIntegrantesRecord() {
    var IntegrantesRecord = "<tr><td>" + $("#DiputadoID").val() + "</td><td>" + $("#FraccionID").val() + "</td></tr>";
    $("#tablaIntegrantesRecords").append(IntegrantesRecord);
    $("#DiputadoID").val("");
    $("#FraccionID").val("");
}



function SaveIntegrantesRecord() {
    var integrantesPlenariosLista = new Array();   
    $("#tablaIntegrantesRecords").find("tr:gt(0)").each(function () {
        var DipID = $(this).find("td:eq(0)").text();
        var diputado = Number.parseInt(DipID);
        var FracID = $(this).find("td:eq(1)").text();
        var Fraccion = Number.parseInt(FracID);
        var plenario = document.getElementById("PlenarioID").value;
        var plenID = Number.parseInt(plenario);

        var IntegrantesPlenario = {};
        IntegrantesPlenario.PlenarioID = plenID;
        IntegrantesPlenario.DiputadoID = diputado;
        IntegrantesPlenario.FraccionID = Fraccion;
        integrantesPlenariosLista.push(IntegrantesPlenario);
    });

    
    integrantesPlenariosLista2 = JSON.stringify(integrantesPlenariosLista);

    $.ajax({
        url: '/IntegrantesPlenario/AddFraccion',
        type: "POST",
        data: {
            listofusers: JSON.stringify(integrantesPlenariosLista),
        },
        success: function (data) {
        },
        error: function (error) {
        }
    })

    //$.ajax({
    //    traditional: true,
    //    type: 'POST',
    //    dataType: 'json',   
    //    url: '/IntegrantesPlenario/AddFraccion',
    //    data: { "IntegrantesPlenario": integrantesPlenariosLista},
    //    success: function (data) {
    //        if (data.mensaje == '200') {
    //            Swal.fire({
    //                title: '¡Plenario ha sido creado!',
    //                text: 'Integrantes agregados correctamente',
    //                icon: 'success',
    //                timer: 3000
    //            })
    //            window.location.href = "/Plenarios/Index";
    //        } else {
    //            Swal.fire(
    //                '¡Ups!',
    //                'Ocurrió un error',
    //                'error'
    //            );
    //        }
    //    },
    //    error: function (data) {
    //        Swal.fire(
    //            '¡Ups!',
    //            'Ocurrió un error',
    //            'error'
    //        );
    //    }
    //});
}
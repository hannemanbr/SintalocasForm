// $('#Cpf').blur(function ()
// {
//     var url = "/Afiliacao/ValidarCPF/";
//     var valor = $(this).val();
//     $.get(url, { Cpf: valor}, function (data)
//     {
//         $("#rCpf").html(data);
//     });
// })

// $('#Cnpj').blur(function ()
// {
//     var url = "/Afiliacao/ValidarCNPJ/";
//     var valor = $(this).val();
//     $.get(url, { Cnpj: valor}, function (data)
//     {
//         $("#rCnpj").html(data);
//     });
// })

// $('#Pis').blur(function ()
// {
//     var url = "/Afiliacao/ValidarPIS/";
//     var valor = $(this).val();
//     $.get(url, { Pis: valor}, function (data)
//     {
//         $("#rPis").html(data);
//     });
// })

// $('#CEP').blur(function ()
// {
//     var url = "/Afiliacao/ValidarCEP/";
//     var valor = $(this).val();
//     $.getJSON(url, { Cep: valor}, function (data)
//     {
//         $("#Bairro").val(data.Bairro);
//         $("#Rua").val(data.Rua);
//         $("#Cidade").val(data.Cidade);
//         $("#UF").val(data.UF);
//     });
// })

// $('#UF').blur(function ()
// {
//     var url = "/Afiliacao/ValidarUF/";
//     var valor = $(this).val();
//     $.get(url, { Uf: valor}, function (data)
//     {
//         $("#rUF").html(data);
//     });
// })

function validarFormAfilia() {

    // var form = $("#formAfilia").serialize();
    
    // $.ajax({
    //     type: 'POST',
    //     url: "/Afiliacao/ValidarFormJSON",
    //     data: form,
    //     dataType: 'json',
    //     success: function (data) {
    //         if (data.result == "Error") {
    //             alert(data.message);
    //         }
    //     }
    // });

    var formData = $("#formAfilia").serialize();

    $.ajax({
        type: "POST",
        data: formData,
        url: "/Afiliacao/ValidarFormJSON",
        dataType: 'json',
        // contentType: false,
        // processData: false,               
        success: function (response) {
            
            //alert(response.msg + ' - ' + response.success);
            $("#rForm").html(response.msg);
            //return response.success;

            if (response.success) {
                $("#formAfilia").submit();
            } else {
                alert(response.msg);
            }

        },
        error: function (response) {
            // alert("Ocorreu um erro durante o processo, tente novamente!");   
            alert("Ocorreu um erro durante o processo, tente novamente!\n retorno: " + response.status); 
        }
    });
}

function validarCPF(valor) 
{
    var url = "/Afiliacao/ValidarCPF/";
    var valor = $(valor).val();
    $.get(url, { Cpf: valor}, function (data)
    {
        $("#rCpf").html(data);
    });
}

function validarCNPJ(valor)
{
    var url = "/Afiliacao/ValidarCNPJ/";
    var valor = $(valor).val();
    $.get(url, { Cnpj: valor}, function (data)
    {
        $("#rCnpj").html(data);
    });
}

function validarPIS(valor) {

    var url = "/Afiliacao/ValidarPIS/";
    var valor = $(valor).val();
    $.get(url, { Pis: valor}, function (data)
    {
        $("#rPis").html(data);
    });

}

function validarCEP(valor) {

    var url = "/Afiliacao/ValidarCEP/";
    var valor = $(valor).val();
    $.getJSON(url, { Cep: valor}, function (data)
    {
        $("#Bairro").val(data.Bairro);
        $("#Rua").val(data.Logradouro);
        $("#Cidade").val(data.Cidade);
        $("#UF").val(data.UF);
    });

}

function validarUF(valor) 
{
    var url = "/Afiliacao/ValidarUF/";
    var valor = $(valor).val();
    $.get(url, { Uf: valor}, function (data)
    {
        $("#rUF").html(data);
    });
}

// MASCARA NOS CAMPOS
$(document).ready(function(){
    // $("#DtNasc").mask("(99) 9999-9999");
    $("#TelCelNum").mask("99999-9999");
    $("#TelResNum").mask("9999-9999");
    $("#DtNasc").mask("99/99/9999");
    $("#Rg").mask("9999999999");
    $("#Cpf").mask("999999999999");
    $("#Cnpj").mask("999999999999");
    $("#CEP").mask("99999999");
});